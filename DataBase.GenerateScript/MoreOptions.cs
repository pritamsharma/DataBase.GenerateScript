/*=============================================  
-- Author:      Pritam Sharma  
-- Create date: 25th Oct 2009
-- Description: Generating Script
-- ============================================= */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DataBase.GenerateScript
{
   public partial class MoreOptions : BaseForm
   {

      #region Global Variable

      /// <summary>
      /// 
      /// </summary>
      private string strQerySave = string.Empty;

      /// <summary>
      /// 
      /// </summary>
      private string strFormClass = string.Empty;

      /// <summary>
      /// 
      /// </summary>
      private string strFormCalssDesigner = string.Empty;

      #endregion Global Variable

      #region Constructor

      /// <summary>
      /// 
      /// </summary>
      public MoreOptions()
      {
         InitializeComponent();
      }

      #endregion Constructor

      #region Methods

      /// <summary>
      /// 
      /// </summary>
      private void MenuOperationOnCheckOptionChanged()
      {
         try
         {
            if (rdBtnMenuGenerate.Checked)
            {
               cmbMenuName.Visible = false;
               lblOperation.Visible = false;
               lstMenuName.Location = new Point(lstMenuName.Location.X, 37);
               lstMenuName.Size = new Size(lstMenuName.Size.Width, 144);
               cmbMenuName.Items.Clear();
               cmbMenuName.Text = string.Empty;
               lstMenuName.Items.Clear();
               AttachMenu("lstMenuName");
            }
            else
            {
               cmbMenuName.Visible = true;
               lblOperation.Visible = true;
               lstMenuName.Location = new Point(lstMenuName.Location.X, 65);
               lstMenuName.Size = new Size(lstMenuName.Size.Width, 116);
               lstMenuName.Text = string.Empty;
               lstMenuName.Items.Clear();
               cmbMenuName.Items.Clear();
               cmbMenuName.Text = string.Empty;
               AttachMenu("cmbMenuName");
            }
         }
         catch (Exception ex)
         {
            LogError(ex);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private void ResetForm()
      {
         tabControlScript.SelectedTab = tabPageOperation;
         rdBtnMenuGenerate.Checked = true;
         txtDisplayScript.Clear();
         strQerySave = string.Empty;
         lstMenuName.ClearSelected();
      }

      /// <summary>
      /// pass either "cmbMenuName" or "lstMenuName"
      /// </summary>
      /// <param name="strControlName">pass either "cmbMenuName" or "lstMenuName"</param>
      private void AttachMenu(string strControlName)
      {
         if (strControlName == cmbMenuName.Name)
            cmbMenuName.Items.Clear();
         else if (strControlName == lstMenuName.Name)
            lstMenuName.Items.Clear();

         List<Entity> lstScrptName = (new FetchData()).GetMenuOperName("MENU", string.Empty);
         foreach (Entity srpLst in lstScrptName)
         {
            if (strControlName == cmbMenuName.Name)
               cmbMenuName.Items.Add(srpLst.Text);
            else if (strControlName == lstMenuName.Name)
               lstMenuName.Items.Add(srpLst.Text);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="strMenuName"></param>
      private void AttachOperation(string strMenuName)
      {
         lstMenuName.Items.Clear();
         List<Entity> lstScrptName = (new FetchData()).GetMenuOperName("OPERATION", strMenuName);
         foreach (Entity srpLst in lstScrptName)
            lstMenuName.Items.Add(srpLst.Text);
      }

      /// <summary>
      /// 
      /// </summary>
      private void RetrieveData()
      {
         if (tabControlScript.SelectedTab == tabPageOperation)
         {
            strQerySave = string.Empty;
            txtDisplayScript.Clear();
            if (lstMenuName.SelectedItems == null || lstMenuName.SelectedItems.Count == 0)
            {
               MessageBox.Show("Please select at least one option form the List Box", "Select Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               lstMenuName.Focus();
            }
            else
            {
               if (rdBtnMenuGenerate.Checked)
               {
                  StringBuilder strBuilder = new StringBuilder("-- Script Date --> " + DateTime.Today.ToString());
                  strBuilder.Append(@"
DECLARE 
@menu_id   SMALLINT,
@role_id   INT,
@oper_id   INT,
@menu_name VARCHAR(50)
");
                  foreach (Object objColl in lstMenuName.SelectedItems)
                  {
                     string strMenuName = objColl.ToString();
                     strBuilder.AppendLine(@"
------------------------------------------------------------------------------
--For Menu --> '" + strMenuName + @"'
------------------------------------------------------------------------------

-- Setting value for the Operation name and the Menu name --
SELECT
@menu_name = '" + strMenuName + @"',
@menu_id   = 0,
@oper_id   = 0

-- Getting menu_id FOR the entered menu_name --
SELECT @menu_id = menu_id 
FROM menu 
WHERE menu_name = @menu_name

--------------------------------------------------------------------

--Deleting Menu If already Present--
EXEC usp_security_i_u_menu_operation 
@menu_id,
@menu_name,
@menu_name,
@menu_name,
0,
'Y',
'Y',
0,
'',
10428,
NULL,
'D',
'M'

--Inserting Operation in the Table--
EXEC usp_security_i_u_menu_operation 
@menu_id,
@menu_name,
@menu_name,
@menu_name,
0,
'Y',
'Y',
0,
'',
10428,
NULL,
'I',
'M'
--------------------------------------------------------------------

--------------------------------------------------------------------
--Assigning Update Permission to all the Security Menu---

DECLARE cursor_all_roles CURSOR FOR
SELECT role_id FROM [role]

OPEN cursor_all_roles
FETCH next FROM cursor_all_roles INTO @role_id
WHILE @@FETCH_STATUS = 0
BEGIN

	-- Getting operation_id value --
	SELECT @oper_id = oper_id 
	FROM operation 
	WHERE oper_name = @menu_name
	AND menu_id     = @menu_id

	-- Giving update permission to all the roles --
	EXEC usp_u_assign_permission 
	@role_id,
	@oper_id,
	'U',
	10428,
	''

	FETCH next FROM cursor_all_roles INTO @role_id
END
CLOSE cursor_all_roles
DEALLOCATE cursor_all_roles

--------------------------------------------------------------------");
                  }

                  strQerySave = strBuilder.ToString();
                  txtDisplayScript.Text = strQerySave;
               }
               else
               {
                  if (cmbMenuName.Text == string.Empty)
                  {
                     MessageBox.Show("Please select menu name form the Combo Box", "Select Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                     lstMenuName.Focus();
                  }
                  else
                  {
                     string strMenuName = cmbMenuName.Text;
                     StringBuilder strBuilder = new StringBuilder("-- Script Date --> " + DateTime.Today.ToString());
                     strBuilder.Append(@"
DECLARE 
@menu_id   SMALLINT,
@role_id   INT,
@oper_id   INT,
@menu_name VARCHAR(50),
@oper_name VARCHAR(50)");

                     foreach (Object objColl in lstMenuName.SelectedItems)
                     {
                        string strOperName = objColl.ToString();
                        strBuilder.AppendLine(@"
------------------------------------------------------------------------------
--For Menu --> '" + strMenuName + @"' Operation --> '" + strOperName + @"' --
------------------------------------------------------------------------------

-- Setting value for the Operation name and the Menu name --
SELECT
@menu_name = '" + strMenuName + @"',
@oper_name = '" + strOperName + @"',
@menu_id   = 0,
@oper_id   = 0

-- Getting menu_id FOR the entered menu_name --
SELECT @menu_id = menu_id 
FROM menu 
WHERE menu_name = @menu_name

--------------------------------------------------------------------
-- Getting operation_id value --
SELECT @oper_id = oper_id 
FROM operation 
WHERE oper_name = @oper_name
AND menu_id     = @menu_id

--Deleting Operation If already Present--
EXEC usp_security_i_u_menu_operation 
@menu_id,
@oper_name,
@oper_name,
@oper_name,
0,
'Y',
'Y',
@oper_id,
@oper_name,
10428,
NULL,
'D',
'O'

--Inserting Operation in the Table--
EXEC usp_security_i_u_menu_operation 
@menu_id,
@oper_name,
@oper_name,
@oper_name,
0,
'Y',
'Y',
@oper_id,
@oper_name,
10428,
NULL,
'I',
'O'
--------------------------------------------------------------------

--------------------------------------------------------------------
--Assigning Update Permission to all the Security Menu---

DECLARE cursor_all_roles CURSOR FOR
SELECT role_id FROM [role]

OPEN cursor_all_roles
FETCH next FROM cursor_all_roles INTO @role_id
WHILE @@FETCH_STATUS = 0
BEGIN

	-- Giving update permission to all the roles --
	EXEC usp_u_assign_permission 
	@role_id,
	@oper_id,
	'U',
	10428,
	''

	FETCH next FROM cursor_all_roles INTO @role_id
END
CLOSE cursor_all_roles
DEALLOCATE cursor_all_roles

--------------------------------------------------------------------");
                     }
                     strQerySave = strBuilder.ToString();
                     txtDisplayScript.Text = strQerySave;
                  }
               }
            }
         }
         else if (tabControlScript.SelectedTab == tabNewFormAdd)
         {
            strFormClass = string.Empty;
            strFormCalssDesigner = string.Empty;
            txtDisplayScript.Clear();
            if (string.IsNullOrEmpty(cmbTableName.Text))
            {
               MessageBox.Show("Please select Table Name to create form Template.", "Enter Value", MessageBoxButtons.OK);
               cmbTableName.Focus();
            }
            if (string.IsNullOrEmpty(cmbResolution.Text))
            {
               MessageBox.Show("Please select Resolution to create form Template.", "Enter Value", MessageBoxButtons.OK);
               cmbResolution.Focus();
            }
            if (string.IsNullOrEmpty(cmbDisplayOption.Text))
            {
               MessageBox.Show("Please select Display Option to create form Template.", "Enter Value", MessageBoxButtons.OK);
               cmbDisplayOption.Focus();
            }
            else if (dgvColumnDetails.RowCount <= 0)
            {
               MessageBox.Show("There are no Columns added in the Display Control Grid.", "Enter Value", MessageBoxButtons.OK);
               dgvColumnDetails.Focus();
            }
            else if (string.IsNullOrEmpty(txtNameSpaceName.Text))
            {
               MessageBox.Show("Please enter name for Namespace.", "Enter Value", MessageBoxButtons.OK);
               txtNameSpaceName.Focus();
            }
            else if (string.IsNullOrEmpty(txtFormName.Text))
            {
               MessageBox.Show("Please enter name for the Form.", "Enter Value", MessageBoxButtons.OK);
               txtFormName.Focus();
            }
            else
            {
               List<ControlToAdd> lstControls = new List<ControlToAdd>();
               int iNoOfControlInALine = string.IsNullOrEmpty(txtControlPerLine.Text) ? 3 : Convert.ToInt32(txtControlPerLine.Text);
               int VerticalSpacingBetweenControls = string.IsNullOrEmpty(txtX.Text) ? 10 : Convert.ToInt32(txtX.Text);
               int HorizontalSpacingBetweenControls = string.IsNullOrEmpty(txtY.Text) ? 10 : Convert.ToInt32(txtY.Text);
               int iTabIndex = 0;
               int iLineno = 0;
               int iColNo = 0;
               for (int i = 0; i < dgvColumnDetails.RowCount; i++)
               {
                  if (dgvColumnDetails.Rows[i].Cells["Display"].Value.ToString() == "True")
                  {
                     iColNo = iTabIndex % iNoOfControlInALine;
                     if (iColNo == 0 && iTabIndex > 0)
                        iLineno++;
                     string strControlType = dgvColumnDetails.Rows[i].Cells["Control Type"].Value.ToString();
                     Point pLocation = new Point();
                     pLocation.Y = VerticalSpacingBetweenControls + (VerticalSpacingBetweenControls + 20) * iLineno;
                     pLocation.X = HorizontalSpacingBetweenControls + (HorizontalSpacingBetweenControls + 200) * iColNo;
                     ControlToAdd objControl = new ControlToAdd();
                     objControl.Name = dgvColumnDetails.Rows[i].Cells["Column Name"].Value.ToString();
                     objControl.Text = dgvColumnDetails.Rows[i].Cells["Column Display Name"].Value.ToString();
                     objControl.ControlType = string.IsNullOrEmpty(strControlType) ? "TextBox" : strControlType;
                     objControl.Size = new Size(100, 20);
                     objControl.TabIndex = iTabIndex;
                     objControl.Location = pLocation;
                     lstControls.Add(objControl);
                     iTabIndex++;
                  }
               }

               if (iTabIndex <= 0)
               {
                  MessageBox.Show("Please select atleast one Column from Display Control Grid.", "Enter Value", MessageBoxButtons.OK);
                  dgvColumnDetails.Focus();
               }
               else
               {
                  GenerateFormCode(lstControls);
               }
            }
         }
      }

      private struct ControlToAdd
      {
         public string ControlType;
         public string Name;
         public Point Location;
         public Size Size;
         public int TabIndex;
         public string Text;
      }

      private enum DesignerCreateOption
      {
         Instantiate,
         Declare
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="objControls"></param>
      /// <returns></returns>
      private string AddTextBox(ControlToAdd objControls, string strControlName, DesignerCreateOption objCreateOption, int iTabIndex)
      {
         string strDesigRet = string.Empty;
         switch (objCreateOption)
         {
            case DesignerCreateOption.Instantiate:
               strDesigRet = @"
        // 
        // lbl" + strControlName + @"
        // 
        this.lbl" + strControlName + @" = new System.Windows.Forms.Label();
        this.lbl" + strControlName + @".Location = new System.Drawing.Point(" + objControls.Location.X + ", " + objControls.Location.Y + @");
        this.lbl" + strControlName + @".Name = " + "\"lbl" + strControlName + "\";" + @"
        this.lbl" + strControlName + @".Size = new System.Drawing.Size(" + objControls.Size.Width + ", " + objControls.Size.Height + @");
        this.lbl" + strControlName + @".TabIndex = " + iTabIndex + @";
        this.lbl" + strControlName + @".Text = " + "\"" + objControls.Text + ":\";" + @"
        this.lbl" + strControlName + @".TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.Controls.Add(this.lbl" + strControlName + @");
        // 
        // txtBoxName
        // 
        this.txt" + strControlName + @" = new System.Windows.Forms.TextBox();
        this.txt" + strControlName + @".Location = new System.Drawing.Point(" + (objControls.Location.X + 100) + ", " + objControls.Location.Y + @");
        this.txt" + strControlName + @".Name = " + "\"txt" + strControlName + "\";" + @"
        this.txt" + strControlName + @".Size = new System.Drawing.Size(" + objControls.Size.Width + ", " + objControls.Size.Height + @");
        this.txt" + strControlName + @".TabIndex = " + iTabIndex + 1 + @";
        this.Controls.Add(this.txt" + strControlName + @");";
               break;
            case DesignerCreateOption.Declare:
               strDesigRet = @"
        private System.Windows.Forms.Label lbl" + strControlName + @";
        private System.Windows.Forms.TextBox txt" + strControlName + ";";
               break;
         }
         return strDesigRet;
      }

      private void GenerateFormCode(List<ControlToAdd> objControlsToAdd)
      {
         strFormClass = @"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace " + txtNameSpaceName.Text + @"
{
    public partial class " + txtFormName.Text + @" : Form
    {
        public " + txtFormName.Text + @"()
        {
            InitializeComponent();
        }
    }
}";
         strFormCalssDesigner = @"
namespace " + txtNameSpaceName.Text + @"
{
    partial class " + txtFormName.Text + @"
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name=" + "\"disposing\">true if managed resources should be disposed; otherwise, false.</param>" + @"
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        
        #region Controls

        ";

         foreach (ControlToAdd objControls in objControlsToAdd)
         {
            string strControlName = GetDisplayName(objControls.Name).Replace(" ", string.Empty);
            switch (objControls.ControlType)
            {
               case "TextBox":
                  strFormCalssDesigner = strFormCalssDesigner + AddTextBox(objControls, strControlName, DesignerCreateOption.Declare, 0);
                  break;
            }
         }

         strFormCalssDesigner = strFormCalssDesigner + @"

        #endregion Controls

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();";

         int iTabIndex = 0;
         foreach (ControlToAdd objControls in objControlsToAdd)
         {
            string strControlName = GetDisplayName(objControls.Name).Replace(" ", string.Empty);
            switch (objControls.ControlType)
            {
               case "TextBox":
                  strFormCalssDesigner = strFormCalssDesigner + AddTextBox(objControls, strControlName, DesignerCreateOption.Instantiate, iTabIndex);
                  break;
            }

            iTabIndex = iTabIndex + 2;
         }

         strFormCalssDesigner = strFormCalssDesigner + @"            
        // 
        // " + txtFormName.Text + @"
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1024, 768);
        this.Name = " + "\"" + txtFormName.Text + "\";" + @"
        this.Text = " + "\"" + txtFormName.Text + "\";" + @"
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    }
}";

         txtDisplayScript.Text = strFormClass + @"


" + strFormCalssDesigner;

      }

      /// <summary>
      /// 
      /// </summary>
      private void PopulateDataGridColumnData()
      {
         if (dgvColumnDetails.Rows.Count > 0)
            dgvColumnDetails.Rows.Clear();
         if (dgvColumnDetails.Columns.Count > 0)
            dgvColumnDetails.Columns.Clear();

         DataGridViewTextBoxColumn dgvtbcColName = new DataGridViewTextBoxColumn();
         dgvtbcColName.HeaderText = "Column Name";
         dgvtbcColName.Name = "Column Name";
         dgvtbcColName.ReadOnly = true;
         dgvtbcColName.Width = 100;

         DataGridViewTextBoxColumn dgvtbcColDispName = new DataGridViewTextBoxColumn();
         dgvtbcColDispName.HeaderText = "Column Display Name";
         dgvtbcColDispName.Name = "Column Display Name";
         dgvtbcColDispName.Width = 170;

         DataGridViewCheckBoxColumn dgvcbcToDisplay = new DataGridViewCheckBoxColumn();
         dgvcbcToDisplay.HeaderText = "Display";
         dgvcbcToDisplay.Name = "Display";
         dgvcbcToDisplay.Width = 50;

         DataGridViewComboBoxColumn dgvcbcControlType = new DataGridViewComboBoxColumn();
         dgvcbcControlType.HeaderText = "Control Type";
         dgvcbcControlType.Name = "Control Type";
         dgvcbcControlType.Width = 150;
         string[] strControlName = new string[] {"Button","CheckBox","ComboBox","DateTimePicker",
                    "MaskedTextBox","NumericDropdown","RadioButton","RichTextBox","TextBox","LinkLabel"};
         dgvcbcControlType.Items.AddRange(strControlName);

         dgvColumnDetails.Columns.Add(dgvtbcColName);
         dgvColumnDetails.Columns.Add(dgvtbcColDispName);
         dgvColumnDetails.Columns.Add(dgvcbcToDisplay);
         dgvColumnDetails.Columns.Add(dgvcbcControlType);
      }

      /// <summary>
      /// 
      /// </summary>
      private void PopulateNewFormAddControlOnFocus()
      {
         strFormClass = string.Empty;
         strFormCalssDesigner = string.Empty;

         cmbTableName.Items.Clear();
         List<Entity> lstTableName = (new FetchData()).GetScriptName("'U'");
         foreach (Entity srpLst in lstTableName)
            cmbTableName.Items.Add(srpLst.Text);
         cmbTableName.Text = string.Empty;

         cmbResolution.Items.Clear();
         string[] strResolution = new string[] { "800  X 600", "1024 X 768", "1152 X 864", "1280 X 720", "1280 X 768", "1280 X 960", "1280 X 1024" };
         cmbResolution.Items.AddRange(strResolution);
         cmbResolution.SelectedIndex = 1;

         cmbDisplayOption.Items.Clear();
         string[] strDisplayOption = new string[] { "Detailed Controls", "Data Grid View" };
         cmbDisplayOption.Items.AddRange(strDisplayOption);
         cmbDisplayOption.SelectedIndex = 0;

         PopulateDataGridColumnData();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="strControlName"></param>
      /// <returns></returns>
      private string GetDisplayName(string strControlName)
      {
         string strDisplayName = string.Empty;
         string[] strName = strControlName.Split(new string[] { "_" }, StringSplitOptions.None);
         if (strName != null && strName.Length > 0)
         {
            foreach (string strSubStr in strName)
            {
               if (strSubStr.Length > 0)
               {
                  strDisplayName = strDisplayName + strSubStr.Remove(1).ToUpper();
                  if (strSubStr.Length > 1)
                     strDisplayName = strDisplayName + strSubStr.Substring(1) + " ";
                  else
                     strDisplayName = strDisplayName + " ";
               }
            }
         }
         return strDisplayName;
      }

      /// <summary>
      /// 
      /// </summary>
      private void PopulateGridWithColumns()
      {
         if (!string.IsNullOrEmpty(cmbTableName.Text) && dgvColumnDetails.ColumnCount > 0)
         {
            List<Entity> lstColumnName = (new FetchData()).GetColumnName(cmbTableName.Text, "All Column");
            foreach (Entity objColName in lstColumnName)
            {
               dgvColumnDetails.Rows.Add(new string[] { objColName.Text, GetDisplayName(objColName.Text), "True", "" });
            }
            string strFormName = GetDisplayName(cmbTableName.Text).Replace(" ", string.Empty);
            txtNameSpaceName.Text = strFormName;
            txtFormName.Text = strFormName;
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private void CreateFormClass()
      {

      }

      #endregion Methods

      #region Events

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void MoreOptions_Load(object sender, EventArgs e)
      {
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void MoreOptions_Shown(object sender, EventArgs e)
      {
         try
         {
            ResetForm();
            cmbMenuName.Focus();
         }
         catch (Exception ex)
         {
            LogError(ex);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btnRetrieve_Click(object sender, EventArgs e)
      {
         try
         {
            RetrieveData();
         }
         catch (Exception ex)
         {
            LogError(ex);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btnSave_Click(object sender, EventArgs e)
      {
         try
         {
            if (tabControlScript.SelectedTab == tabPageOperation)
            {
               SaveFile(txtDisplayScript.Text, strQerySave);
            }
            else if (tabControlScript.SelectedTab == tabNewFormAdd)
            {
               SaveFileDialog svDialog = new SaveFileDialog();
               svDialog.Filter = "C# File (*.cs)|*.cs";
               svDialog.DefaultExt = ".cs";
               svDialog.FileName = strFileSavePath + "\\" + txtFormName.Text;
               if (svDialog.ShowDialog() == DialogResult.OK)
               {
                  string strFile = svDialog.FileName;
                  if (File.Exists(strFile))
                     File.Delete(strFile);
                  File.WriteAllText(strFile, strFormClass);

                  strFile = strFile.Replace(".cs", string.Empty);
                  strFile = strFile + ".Designer.cs";
                  if (File.Exists(strFile))
                     File.Delete(strFile);
                  File.WriteAllText(strFile, strFormCalssDesigner);

                  if (File.Exists(strFile))
                     MessageBox.Show("File saved successfully.");
                  strFileSavePath = GetDirecotryPath(svDialog.FileName);
               }
            }
         }
         catch (Exception ex)
         {
            LogError(ex);
         }
         finally
         {
            this.Cursor = Cursors.Default;
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btnReset_Click(object sender, EventArgs e)
      {
         try
         {
            ResetForm();
         }
         catch (Exception ex)
         {
            LogError(ex);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void rdBtnMenuGenerate_CheckedChanged(object sender, EventArgs e)
      {
         MenuOperationOnCheckOptionChanged();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void rdBtnOperationScript_CheckedChanged(object sender, EventArgs e)
      {
         MenuOperationOnCheckOptionChanged();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btnClose_Click(object sender, EventArgs e)
      {
         this.Close();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void cmbMenuName_SelectedIndexChanged(object sender, EventArgs e)
      {
         try
         {
            if (cmbMenuName.SelectedItem != null)
               AttachOperation(cmbMenuName.SelectedItem.ToString());
         }
         catch (Exception ex)
         {
            LogError(ex);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btnAddColumn_Click(object sender, EventArgs e)
      {
         try
         {
            if (cmbTableName.SelectedItem != null)
               dgvColumnDetails.Rows.Add();
         }
         catch (Exception ex)
         {
            LogError(ex);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void tabControlScript_SelectedIndexChanged(object sender, EventArgs e)
      {
         try
         {
            if (tabControlScript.SelectedTab != null)
            {
               if (tabControlScript.SelectedTab == tabPageOperation)
               {
                  btnRetrieve.Text = "Retrieve";
               }
               else if (tabControlScript.SelectedTab == tabNewFormAdd)
               {
                  btnRetrieve.Text = "Generate";
                  PopulateNewFormAddControlOnFocus();
               }
            }
         }
         catch (Exception ex)
         {
            LogError(ex);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void cmbTableName_SelectedIndexChanged(object sender, EventArgs e)
      {
         try
         {
            PopulateGridWithColumns();
         }
         catch (Exception ex)
         {
            LogError(ex);
         }
      }

      #endregion Events

   }
}