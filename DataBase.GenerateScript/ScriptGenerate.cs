#region [ Version Info ]
/*=============================================  
-- Author:      Pritam Sharma  
-- Create date: 29th May 2009
-- Description: Generating Script
-- ============================================= */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataBase.GenerateScript.Properties;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Security.Permissions;

[assembly: SecurityPermission(SecurityAction.RequestMinimum)]
[assembly: CLSCompliantAttribute(true)]
namespace DataBase.GenerateScript
{
    [CLSCompliantAttribute(true)]
    /// <summary>
    /// Utility Form for generating database script
    /// </summary>
    public partial class ScriptGenerate : BaseForm
    {

        #region Global Variables

        /// <summary>
        /// The final query which is saved as .sql file
        /// </summary>
        private string strQerySave = string.Empty;

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public ScriptGenerate()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region User Defined Methods

        /// <summary>
        /// Specifies the type of Script to Generate and prepares the Display accordingly.
        /// </summary>
        private void FormatDisplayOptions()
        {
            if (rdBtnScSpViFn.Checked)//Generates a Script for already present Stored Procedure,View and Function from a database.
            {
                rdBtnEnterOwnValue.Checked = true;
                FormatControlForSearchType();
                grpBoxCriteriaScript.Visible = true;
                grpBoxCriteriaInsertTable.Visible = false;
                grpBoxCriteriaSp.Visible = false;
                grpBoxCriteriaScript.Location = new Point(grpBoxOptions.Location.X, grpBoxOptions.Location.Y + grpBoxOptions.Height + 5);
                txtDisplayScript.Location = new Point(grpBoxCriteriaScript.Location.X, grpBoxCriteriaScript.Location.Y + grpBoxCriteriaScript.Height + 5);
                txtDisplayScript.Size = new Size(txtDisplayScript.Size.Width, this.Height - txtDisplayScript.Location.Y - 40);
            }
            else if (rdBtnInsert.Checked)//Creates an Insert Statement for an already present value in a table
            {
                cmbSearchTable.Text = string.Empty;
                ClearCreateInsertScript();
                grpBoxCriteriaScript.Visible = false;
                grpBoxCriteriaInsertTable.Visible = true;
                grpBoxCriteriaSp.Visible = false;
                grpBoxCriteriaInsertTable.Location = new Point(grpBoxOptions.Location.X, grpBoxOptions.Location.Y + grpBoxOptions.Height + 5);
                txtDisplayScript.Location = new Point(grpBoxCriteriaInsertTable.Location.X, grpBoxCriteriaInsertTable.Location.Y + grpBoxCriteriaInsertTable.Height + 5);
                txtDisplayScript.Size = new Size(txtDisplayScript.Size.Width, this.Height - txtDisplayScript.Location.Y - 40);
            }
            else if (rdBtnSpCreate.Checked)//Creates stored procedure templete for an Table containing Insert, Update and Delete.
            {
                chkEntireDataBase.Checked = false;
                FormatNewSpGenerate();
                grpBoxCriteriaScript.Visible = false;
                grpBoxCriteriaInsertTable.Visible = false;
                grpBoxCriteriaSp.Visible = true;
                grpBoxCriteriaSp.Location = new Point(grpBoxOptions.Location.X, grpBoxOptions.Location.Y + grpBoxOptions.Height + 5);
                txtDisplayScript.Location = new Point(grpBoxCriteriaSp.Location.X, grpBoxCriteriaSp.Location.Y + grpBoxCriteriaSp.Height + 5);
                txtDisplayScript.Size = new Size(txtDisplayScript.Size.Width, this.Height - txtDisplayScript.Location.Y - 40);
            }
        }

        /// <summary>
        /// Format the Form for different search type is selected on selection of Drop and Create Script
        /// </summary>
        private void FormatControlForSearchType()
        {
            if (rdBtnEnterOwnValue.Checked)
            {
                dtFrom.Value = DateTime.Today.Date;
                dtTo.Value = DateTime.Today.Date;
                rdBtnSp.Checked = false;
                rdView.Checked = false;
                rdFunc.Checked = false;
                dtFrom.Enabled = false;
                dtTo.Enabled = false;
                rdBtnSp.Enabled = false;
                rdView.Enabled = false;
                rdFunc.Enabled = false;
                txtScriptNameList.ReadOnly = false;
                cmbSearchScript.Enabled = false;
                tsmiGetScrptNameFromFile.Enabled = true;
            }
            else if (rdBtnGetListFrmDatabase.Checked)
            {
                dtFrom.Value = DateTime.Today.Date;
                dtTo.Value = DateTime.Today.Date;
                rdBtnSp.Checked = false;
                rdView.Checked = false;
                rdFunc.Checked = false;
                dtFrom.Enabled = false;
                dtTo.Enabled = false;
                rdBtnSp.Enabled = false;
                rdView.Enabled = false;
                rdFunc.Enabled = false;
                txtScriptNameList.ReadOnly = true;
                cmbSearchScript.Enabled = true;
                tsmiGetScrptNameFromFile.Enabled = false;
            }
            else if (rdBtnModifiedByDate.Checked)
            {
                dtFrom.Value = DateTime.Today.Date;
                dtTo.Value = DateTime.Today.Date;
                rdBtnSp.Checked = true;
                rdView.Checked = false;
                rdFunc.Checked = false;
                dtFrom.Enabled = true;
                dtTo.Enabled = true;
                rdBtnSp.Enabled = true;
                rdView.Enabled = true;
                rdFunc.Enabled = true;
                txtScriptNameList.ReadOnly = true;
                cmbSearchScript.Enabled = false;
                tsmiGetScrptNameFromFile.Enabled = false;
            }
            txtScriptNameList.Clear();
            cmbSearchScript.Text = string.Empty;
            txtDisplayScript.Clear();
        }

        /// <summary>
        /// Format the Form for different search type is selected on selection new SP template creation
        /// </summary>
        private void FormatNewSpGenerate()
        {
            if (chkEntireDataBase.Checked)
                cmbTableName.Enabled = false;
            else
                cmbTableName.Enabled = true;
            cmbTableName.Text = string.Empty;
            txtTableNameList.Clear();
            txtDisplayScript.Clear();
        }

        /// <summary>
        /// Clears the form when Insert Script for table option is selected
        /// </summary>
        private void ClearCreateInsertScript()
        {
            chkEntireTable.Checked = false;
            cmbSearchTable.Enabled = true;
            dgvColumnName.ReadOnly = false;
            dgvSearchValue.Enabled = true;
            chkSearchByPrimaryKey.Enabled = true;
            chkIncludeDelete.Enabled = true;
            dgvColumnName.Rows.Clear();
            dgvColumnName.Columns.Clear();
            dgvSearchValue.Rows.Clear();
            dgvSearchValue.Columns.Clear();
            txtDisplayScript.Clear();
            dgvSearchValue.DataSource = null;
            chkSearchByPrimaryKey.Checked = false;
            chkIncludeDelete.Checked = false;
        }

        /// <summary>
        /// Format the form when Insert Script for table option is selected
        /// </summary>
        private void FormatCreateInsertScript()
        {
            if (chkEntireTable.Checked)
            {
                cmbSearchTable.Enabled = false;
                dgvColumnName.ReadOnly = true;
                dgvSearchValue.Enabled = false;
                chkSearchByPrimaryKey.Enabled = false;
            }
            else
            {
                cmbSearchTable.Enabled = true;
                dgvColumnName.ReadOnly = false;
                dgvSearchValue.Enabled = true;
                chkSearchByPrimaryKey.Enabled = true;
            }
            dgvColumnName.Rows.Clear();
            dgvColumnName.Columns.Clear();
            txtDisplayScript.Clear();
            dgvSearchValue.DataSource = null;
            chkSearchByPrimaryKey.Checked = false;
        }

        /// <summary>
        /// Polulates the AutoCompleteString collection to the Textbox passed
        /// </summary>
        /// <param name="strScriptType"></param>
        /// <param name="txtBox"></param>
        private void PopulateAutoCompleteStringCollection(string strScriptType, TextBox txtBox)
        {
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            List<Entity> lstScrptName = (new FetchData()).GetScriptName(strScriptType);
            foreach (Entity srpLst in lstScrptName)
                autoComplete.Add(srpLst.Text);
            txtBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBox.AutoCompleteCustomSource = autoComplete;
        }

        /// <summary>
        /// Populates autocomplete for selected textbox
        /// </summary>
        private void PopupateAutoComplete()
        {
            List<Entity> lstScrptName = new List<Entity>();
            lstScrptName = (new FetchData()).GetScriptName("'P','V','FN'");
            foreach (Entity srpLst in lstScrptName)
                cmbSearchScript.Items.Add(srpLst.Text);
            lstScrptName = new List<Entity>();
            lstScrptName = (new FetchData()).GetScriptName("'U'");
            foreach (Entity srpLst in lstScrptName)
            {
                cmbSearchTable.Items.Add(srpLst.Text);
                cmbTableName.Items.Add(srpLst.Text);
            }
        }

        /// <summary>
        /// Opens the Configuration screen
        /// </summary>
        /// <returns></returns>
        private bool ConfigureSetting(bool DontAskResetQuestion)
        {
            Configure objConfigure = new Configure();
            objConfigure.OkPress += new EventHandler(OkPressSuccess);
            objConfigure.DontAskResetQuestion = DontAskResetQuestion;
            objConfigure.ShowDialog();
            return objConfigure.ValidationSuccess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkPressSuccess(object sender, EventArgs e)
        {
            try
            {
                string strConnection = (string)sender;
                ResetForm();
                SaveConfigFile("ConnectionString", strConnection);
                strConnectionString = strConnection;
                SetFormName();
                PopupateAutoComplete();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        /// <summary>
        /// Resets the Form
        /// </summary>
        private void ResetForm()
        {
            rdBtnEnterOwnValue.Checked = true;
            chkEntireDataBase.Checked = false;
            chkEntireTable.Checked = false;
            FormatControlForSearchType();
            ClearCreateInsertScript();
            FormatNewSpGenerate();
            cmbSearchTable.Text = string.Empty;
            chkIncludeDelete.Checked = false;
            chkEntireTable.Enabled = false;
        }

        /// <summary>
        /// Gets the column name for table
        /// </summary>
        private void GetColumnForTable()
        {
            List<Entity> lstColumnName = new List<Entity>();
            if (!string.IsNullOrEmpty(cmbSearchTable.Text))
            {
                if (cmbSearchTable.Items.Contains(cmbSearchTable.Text))
                {
                    string strColumnType = chkSearchByPrimaryKey.Checked ? "Primary Key Column" : "All Column";
                    lstColumnName = (new FetchData()).GetColumnName(cmbSearchTable.Text, strColumnType);
                    chkEntireTable.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Table name entered is not present in the database.", "Table Not Present", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbSearchTable.Text = string.Empty;
                    chkEntireTable.Enabled = false;
                }
            }
            else
            {
                chkEntireTable.Enabled = false;
            }
            bool bColumnsNotPresent = false;
            if (dgvColumnName.ColumnCount == lstColumnName.Count)
            {
                for (int i = 0; i < dgvColumnName.ColumnCount; i++)
                {
                    bool bMatch = false;
                    for (int j = 0; j < lstColumnName.Count; j++)
                    {
                        if (dgvColumnName.Columns[i].Name.Trim() == lstColumnName[j].Text.Trim())
                        {
                            bMatch = true;
                            break;
                        }
                    }
                    if (!bMatch)
                    {
                        bColumnsNotPresent = true;
                        break;
                    }
                }
            }
            else
            {
                bColumnsNotPresent = true;
            }
            if (bColumnsNotPresent)
            {
                if (chkSearchByPrimaryKey.Checked)
                {
                    dgvColumnName.Rows.Clear();
                    dgvColumnName.Columns.Clear();
                    dgvColumnName.ReadOnly = true;
                    dgvSearchValue.Rows.Clear();
                    dgvSearchValue.Columns.Clear();
                    if (lstColumnName.Count > 0)
                    {
                        foreach (Entity objEntity in lstColumnName)
                        {
                            dgvSearchValue.Columns.Add(objEntity.Text, objEntity.Text);
                            DataGridViewCheckBoxColumn dgvChkBoxColumn = new DataGridViewCheckBoxColumn();
                            dgvChkBoxColumn.Name = objEntity.Text;
                            dgvChkBoxColumn.HeaderText = objEntity.Text;
                            dgvColumnName.Columns.Add(dgvChkBoxColumn);
                        }
                        dgvColumnName.Rows.Add();
                        dgvSearchValue.Rows.Add();
                        for (int i = 0; i < dgvColumnName.ColumnCount; i++)
                            dgvColumnName.Rows[0].Cells[i].Value = true;
                    }
                }
                else
                {
                    dgvColumnName.Rows.Clear();
                    dgvColumnName.Columns.Clear();
                    dgvColumnName.ReadOnly = false;
                    dgvSearchValue.Rows.Clear();
                    dgvSearchValue.Columns.Clear();
                    if (lstColumnName.Count > 0)
                    {
                        foreach (Entity objEntity in lstColumnName)
                        {
                            DataGridViewCheckBoxColumn dgvChkBoxColumn = new DataGridViewCheckBoxColumn();
                            dgvChkBoxColumn.Name = objEntity.Text;
                            dgvChkBoxColumn.HeaderText = objEntity.Text;
                            dgvColumnName.Columns.Add(dgvChkBoxColumn);
                        }
                        dgvColumnName.Rows.Add();
                    }
                }
            }
            SetGridColumnWidth(dgvColumnName);
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddTableNameToGenerateSpTempleteList()
        {
            try
            {
                bool bNotDuplicate = true;
                if (txtTableNameList.Text.Length > 0)
                {
                    if (cmbTableName.Items.Contains(cmbTableName.Text))
                    {
                        string[] strSplitChekDup = txtTableNameList.Text.Split(new char[] { ',' });
                        if (strSplitChekDup != null && strSplitChekDup.Length > 0)
                        {
                            for (int i = 0; i < strSplitChekDup.Length; i++)
                            {
                                if (cmbTableName.Text.Trim() == strSplitChekDup[i].Trim())
                                {
                                    bNotDuplicate = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (cmbTableName.Text.Trim() == txtTableNameList.Text.Trim())
                                bNotDuplicate = false;
                        }
                        if (bNotDuplicate && !txtTableNameList.Text.EndsWith(","))
                            txtTableNameList.Text = txtTableNameList.Text + ",";
                    }
                    else
                    {
                        cmbTableName.Text = string.Empty;
                        bNotDuplicate = false;
                    }
                }
                if (cmbTableName.Text.Trim().Length > 0 && !cmbTableName.Items.Contains(cmbTableName.Text))
                {
                    MessageBox.Show("Table name entered is not present in the database.", "Table Not Present", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbTableName.Text = string.Empty;
                }
                if (bNotDuplicate)
                    txtTableNameList.Text = txtTableNameList.Text + cmbTableName.Text;
                cmbTableName.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddTableNameToGenerateScriptFromDatabase()
        {
            try
            {
                bool bNotDuplicate = true;
                if (txtScriptNameList.Text.Length > 0)
                {
                    if (cmbSearchScript.Items.Contains(cmbSearchScript.Text))
                    {
                        string[] strSplitChekDup = txtScriptNameList.Text.Split(new char[] { ',' });
                        if (strSplitChekDup != null && strSplitChekDup.Length > 0)
                        {
                            for (int i = 0; i < strSplitChekDup.Length; i++)
                            {
                                if (cmbSearchScript.Text.Trim() == strSplitChekDup[i].Trim())
                                {
                                    bNotDuplicate = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (cmbSearchScript.Text.Trim() == txtScriptNameList.Text.Trim())
                                bNotDuplicate = false;
                        }
                        if (bNotDuplicate && !txtScriptNameList.Text.EndsWith(","))
                            txtScriptNameList.Text = txtScriptNameList.Text + ",";
                    }
                    else
                    {
                        cmbSearchScript.Text = string.Empty;
                        bNotDuplicate = false;
                    }
                }
                if (cmbSearchScript.Text.Trim().Length > 0 && !cmbSearchScript.Items.Contains(cmbSearchScript.Text))
                {
                    MessageBox.Show("Table name entered is not present in the database.", "Table Not Present", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbSearchScript.Text = string.Empty;
                }
                if (bNotDuplicate)
                    txtScriptNameList.Text = txtScriptNameList.Text + cmbSearchScript.Text;
                cmbSearchScript.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LoadFromAppSetting"></param>
        private void SetFormName()
        {
            if (string.IsNullOrEmpty(strConnectionString))
            {
                this.Text = "Script Generator";
            }
            else
            {
                string strFormDisplayText = "Script Generator";
                string[] strSplitLvl1 = strConnectionString.Split(new char[] { ';' });
                if (strSplitLvl1 != null && strSplitLvl1.Length > 0)
                {
                    string strDatabaseEngine = string.Empty;
                    string strDatabase = string.Empty;
                    foreach (string strIndivi in strSplitLvl1)
                    {
                        string[] strSplitLvl2 = strIndivi.Split(new char[] { '=' });
                        if (strSplitLvl2 != null && strSplitLvl2.Length > 0)
                        {
                            switch (strSplitLvl2[0].ToUpper().Trim())
                            {
                                case "INITIAL CATALOG":
                                    if (strSplitLvl2.Length > 1)
                                        strDatabase = "Database: " + strSplitLvl2[1];
                                    break;
                                case "DATA SOURCE":
                                    if (strSplitLvl2.Length > 1)
                                        strDatabaseEngine = "Database Engine: " + strSplitLvl2[1];
                                    break;
                            }
                        }
                    }
                    if (strDatabaseEngine.Length > 0)
                        strFormDisplayText = strFormDisplayText + " (" + strDatabaseEngine;

                    if (strDatabase.Length > 0)
                    {
                        if (strDatabaseEngine.Length == 0)
                            strFormDisplayText = strFormDisplayText + " (" + strDatabase + ")";
                        else
                            strFormDisplayText = strFormDisplayText + ", " + strDatabase + ")";
                    }
                    else
                        strFormDisplayText = strFormDisplayText + ")";
                }
                this.Text = strFormDisplayText;
            }
        }

        /// <summary>
        /// Opens the HelpText Window
        /// </summary>
        /// <param name="HelpType"></param>
        private void OpenHelpText(Help.HelpType HelpType)
        {
            Help objHelp = new Help(HelpType);
            objHelp.Show();
        }

        #endregion User Defined Methods

        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScriptGenerate_Load(object sender, EventArgs e)
        {
            try
            {
                SetFormName();
                rdBtnScSpViFn.Checked = true;
                rdBtnEnterOwnValue.Checked = true;
                txtDisplayScript.ReadOnly = false;
                txtTableNameList.ReadOnly = true;
                cmbSearchTable.Text = string.Empty;
                chkIncludeDelete.Checked = false;
                chkEntireTable.Enabled = false;
                btnInsidEdge.Visible = false;
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
        private void ScriptGenerate_Shown(object sender, EventArgs e)
        {
            try
            {
                rdBtnScSpViFn.Focus();
                if (!(new FetchData()).ValidateConnectionString(strConnectionString))
                {
                    if (!ConfigureSetting(true))
                        Application.Exit();
                }
                (new FetchData()).CreateFnSplitFunction();
                PopupateAutoComplete();
                txtDisplayScript.ReadOnly = true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                Application.Exit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdBtnScSpViFn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdBtnScSpViFn.Checked)
                    FormatDisplayOptions();
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
        private void rdBtnInsert_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdBtnInsert.Checked)
                    FormatDisplayOptions();
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
        private void rdBtnSpCreate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdBtnSpCreate.Checked)
                    FormatDisplayOptions();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        /// <summary>
        /// Method for Retrieving the Script
        /// </summary>
        /// <param name="bSaveRetrieve">If true file will be saved on retrieve by default.</param>
        private void RetrieveRecord(bool bSaveRetrieve)
        {
            try
            {
                if (bSaveRetrieve)
                {
                    string strQeryLoc = string.Empty;
                    FetchData objFetch = new FetchData();
                    List<Entity> objEntityList = new List<Entity>();
                    if (rdBtnScSpViFn.Checked)//Creating Insert and Drop script
                    {
                        if ((rdBtnEnterOwnValue.Checked || rdBtnGetListFrmDatabase.Checked) &&
                            !string.IsNullOrEmpty(txtScriptNameList.Text) && txtScriptNameList.Text.Trim().Length > 0)
                        {
                            string strDefaultFileSavePath = DefaultFileSaveFolder();
                            this.Cursor = Cursors.WaitCursor;
                            ArrayList arrayScriptlist = RemoveDuplicateFromArray(txtScriptNameList.Text.Split(new char[] { ',' }));
                            if (arrayScriptlist.Count>0)
                            {
                                for (int i = 0; i < arrayScriptlist.Count; i++)
                                {
                                    ArrayList alParam = new ArrayList();
                                    alParam.Add(arrayScriptlist[i].ToString());
                                    alParam.Add(chkInsertDeleteOrEnter.Checked ? "Y" : "N");
                                    List<Entity> objEntityListLoc = objFetch.CreateDropInsertScript(alParam, "Enter Own Value");
                                    objEntityList.AddRange(objEntityListLoc);
                                    string strQery = string.Empty;
                                    foreach (Entity objEntity in objEntityListLoc)
                                    {
                                        strQery = strQery + objEntity.Text;
                                        strQeryLoc = strQeryLoc + objEntity.Text;
                                    }
                                    if (strQery.Length > 0)
                                    {
                                        string strFileLocationForSave = strDefaultFileSavePath + @"\" + arrayScriptlist[i].ToString() + ".sql";
                                        if (File.Exists(strFileLocationForSave))
                                            File.Delete(strFileLocationForSave);
                                        File.WriteAllText(strFileLocationForSave, strQery);
                                    }
                                }

                                if (strQeryLoc.Length > 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show("Files saved successfully.");
                                }
                            }
                        }
                        else if (rdBtnModifiedByDate.Checked && dtFrom.Text != string.Empty && dtTo.Text != string.Empty)
                        {
                            if (dtFrom.Value > dtTo.Value)
                            {
                                MessageBox.Show("'From' date has to be less than 'To' date");
                            }
                            else if (!rdBtnSp.Checked && !rdFunc.Checked && !rdView.Checked)
                            {
                                MessageBox.Show("Please select atleast one Script Option.");
                            }
                            else
                            {
                                string strScriptType = string.Empty;
                                if (rdBtnSp.Checked)
                                    strScriptType = "'P'";
                                if (rdFunc.Checked)
                                {
                                    if (strScriptType == string.Empty)
                                        strScriptType = "'FN'";
                                    else
                                        strScriptType = strScriptType + ",'FN'";
                                }
                                if (rdView.Checked)
                                {
                                    if (strScriptType == string.Empty)
                                        strScriptType = "'V'";
                                    else
                                        strScriptType = strScriptType + ",'V'";
                                }
                                List<Entity> listSctiptName = objFetch.GetScriptsListModifiedByDate(strScriptType, dtFrom.Value.Date, dtTo.Value.Date.AddHours(24));
                                if (listSctiptName.Count > 0)
                                {
                                    string strDefaultFileSavePath = DefaultFileSaveFolder();
                                    this.Cursor = Cursors.WaitCursor;
                                    for (int i = 0; i < listSctiptName.Count; i++)
                                    {
                                        ArrayList alParam = new ArrayList();
                                        alParam.Add(listSctiptName[i].Text);
                                        alParam.Add(chkInsertDeleteOrEnter.Checked ? "Y" : "N");
                                        List<Entity> objEntityListLoc = objFetch.CreateDropInsertScript(alParam, "Enter Own Value");
                                        objEntityList.AddRange(objEntityListLoc);
                                        string strQery = string.Empty;
                                        foreach (Entity objEntity in objEntityListLoc)
                                        {
                                            strQery = strQery + objEntity.Text;
                                            strQeryLoc = strQeryLoc + objEntity.Text;
                                        }
                                        if (strQery.Length > 0)
                                        {
                                            string strFileLocationForSave = strDefaultFileSavePath + @"\" + listSctiptName[i].Text + ".sql";
                                            if (File.Exists(strFileLocationForSave))
                                                File.Delete(strFileLocationForSave);
                                            File.WriteAllText(strFileLocationForSave, strQery);
                                        }
                                    }

                                    if (strQeryLoc.Length > 0)
                                    {
                                        this.Cursor = Cursors.Default;
                                        MessageBox.Show("Files saved successfully.");
                                    }
                                }
                            }
                        }
                    }
                    else if (rdBtnInsert.Checked)//Creating insert script for a table
                    {
                        this.Cursor = Cursors.WaitCursor;
                        string strTableName = cmbSearchTable.Text;
                        string strSearchByColumns = string.Empty;
                        string strSearchByColumnValues = string.Empty;
                        string strDeleteRequired = "N";
                        string strSearchByPrimary = "N";
                        string strScriptEntireTable = "N";
                        string strIncludeIdentityColumn = "N";
                        if (chkIncludeIdentityColumn.Checked)
                            strIncludeIdentityColumn = "Y";
                        if (chkIncludeDelete.Checked)
                            strDeleteRequired = "Y";
                        if (chkEntireTable.Checked)
                        {
                            strScriptEntireTable = "Y";
                        }
                        else
                        {
                            if (chkSearchByPrimaryKey.Checked)
                                strSearchByPrimary = "Y";
                            else
                            {
                                for (int i = 0; i < dgvSearchValue.ColumnCount; i++)
                                {
                                    if (strSearchByColumns.Length == 0)
                                        strSearchByColumns = dgvSearchValue.Columns[i].Name;
                                    else
                                        strSearchByColumns = strSearchByColumns + "," + dgvSearchValue.Columns[i].Name;
                                }
                            }
                            //Enter Search By Column Value
                            if (dgvSearchValue.RowCount > 0)
                            {
                                for (int i = 0; i < dgvSearchValue.RowCount; i++)
                                {
                                    string strSearchByColumnValueComma = string.Empty;
                                    for (int j = 0; j < dgvSearchValue.ColumnCount; j++)
                                    {
                                        if (strSearchByColumnValueComma.Length == 0)
                                            strSearchByColumnValueComma = Convert.ToString(dgvSearchValue.Rows[i].Cells[j].Value == DBNull.Value ? "" : dgvSearchValue.Rows[i].Cells[j].Value).Trim();
                                        else
                                            strSearchByColumnValueComma = strSearchByColumnValueComma + "," + Convert.ToString(dgvSearchValue.Rows[i].Cells[j].Value == DBNull.Value ? "" : dgvSearchValue.Rows[i].Cells[j].Value).Trim();
                                    }

                                    if (strSearchByColumnValues.Length == 0)
                                        strSearchByColumnValues = strSearchByColumnValueComma;//strSearchByColumnValues + "|";
                                    else
                                        strSearchByColumnValues = strSearchByColumnValues + "|" + strSearchByColumnValueComma;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter filter by values.", "Missing Filter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        string strDefaultFileSavePath = DefaultFileSaveFolder();
                        objEntityList = objFetch.CreateInsertScriptForTable(strTableName, strSearchByColumns, strSearchByColumnValues, strDeleteRequired, strSearchByPrimary, strScriptEntireTable, strIncludeIdentityColumn);
                        foreach (Entity objEntity in objEntityList)
                            strQeryLoc = strQeryLoc + objEntity.Text;
                        if (strQeryLoc.Length > 0)
                        {
                            string strFileLocationForSave = strDefaultFileSavePath + @"\" + strTableName + "_Insert_Script.sql";
                            if (File.Exists(strFileLocationForSave))
                                File.Delete(strFileLocationForSave);
                            File.WriteAllText(strFileLocationForSave, strQeryLoc);
                        }
                        if (strQeryLoc.Length > 0)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Files saved successfully.");
                        }
                    }
                    else if (rdBtnSpCreate.Checked)//Creating stored procedure template for a table
                    {
                        ArrayList objTableName = new ArrayList();
                        if (chkEntireDataBase.Checked)
                        {
                            objTableName = objFetch.GetTables();
                        }
                        else if (!string.IsNullOrEmpty(txtTableNameList.Text) && txtTableNameList.Text.Trim().Length > 0)
                        {
                            objTableName = RemoveDuplicateFromArray(txtTableNameList.Text.Split(new char[] { ',' }));
                        }

                        if (objTableName.Count > 0)
                        {
                            string strDefaultFileSavePath = DefaultFileSaveFolder();
                            this.Cursor = Cursors.WaitCursor;

                            for (int i = 0; i < objTableName.Count; i++)
                            {
                                List<Entity> objEntityListLoc = objFetch.CreateStoredProcTempleteForTable(objTableName[i].ToString(), "N");
                                objEntityList.AddRange(objEntityListLoc);
                                string strQery = string.Empty;
                                foreach (Entity objEntity in objEntityListLoc)
                                {
                                    strQery = strQery + objEntity.Text;
                                    strQeryLoc = strQeryLoc + objEntity.Text;
                                }
                                if (strQery.Length > 0)
                                {
                                    string strFileLocationForSave = strDefaultFileSavePath + @"\usp_" + objTableName[i].ToString() + ".sql";
                                    if (File.Exists(strFileLocationForSave))
                                        File.Delete(strFileLocationForSave);
                                    File.WriteAllText(strFileLocationForSave, strQery);
                                }
                            }

                            if (strQeryLoc.Length > 0)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Files saved successfully.");
                            }
                        }
                    }
                    strQerySave = strQeryLoc;
                    txtDisplayScript.Text = strQeryLoc;
                }
                else
                {
                    this.Cursor = Cursors.WaitCursor;
                    FetchData objFetch = new FetchData();
                    List<Entity> objEntityList = new List<Entity>();
                    if (rdBtnScSpViFn.Checked)//Creating Insert and Drop script
                    {
                        ArrayList alParam = new ArrayList();
                        if ((rdBtnEnterOwnValue.Checked || rdBtnGetListFrmDatabase.Checked) &&
                            txtScriptNameList.Text.Trim().Length > 0)
                        {
                            alParam.Add(txtScriptNameList.Text);
                            alParam.Add(chkInsertDeleteOrEnter.Checked ? "Y" : "N");
                            objEntityList = objFetch.CreateDropInsertScript(alParam, "Enter Own Value");
                        }
                        else if (rdBtnModifiedByDate.Checked && dtFrom.Text != string.Empty && dtTo.Text != string.Empty)
                        {
                            if (dtFrom.Value > dtTo.Value)
                            {
                                MessageBox.Show("'From' date has to be less than 'To' date");
                            }
                            else if (!rdBtnSp.Checked && !rdFunc.Checked && !rdView.Checked)
                            {
                                MessageBox.Show("Please select atleast one Script Option.");
                            }
                            else
                            {
                                string strScriptType = string.Empty;
                                if (rdBtnSp.Checked)
                                    strScriptType = "'P'";
                                if (rdFunc.Checked)
                                {
                                    if (strScriptType == string.Empty)
                                        strScriptType = "'FN'";
                                    else
                                        strScriptType = strScriptType + ",'FN'";
                                }
                                if (rdView.Checked)
                                {
                                    if (strScriptType == string.Empty)
                                        strScriptType = "'V'";
                                    else
                                        strScriptType = strScriptType + ",'V'";
                                }
                                alParam.Add(strScriptType);
                                alParam.Add(chkInsertDeleteOrEnter.Checked ? "Y" : "N");
                                alParam.Add(dtFrom.Value.Date);
                                alParam.Add(dtTo.Value.Date.AddHours(12));
                                objEntityList = objFetch.CreateDropInsertScript(alParam, "Modified By Date");
                            }
                        }
                    }
                    else if (rdBtnInsert.Checked)//Creating insert script for a table
                    {
                        string strTableName = cmbSearchTable.Text;
                        string strSearchByColumns = string.Empty;
                        string strSearchByColumnValues = string.Empty;
                        string strDeleteRequired = "N";
                        string strSearchByPrimary = "N";
                        string strScriptEntireTable = "N";
                        string strIncludeIdentityColumn = "N";
                        if (chkIncludeIdentityColumn.Checked)
                            strIncludeIdentityColumn = "Y";
                        if (chkIncludeDelete.Checked)
                            strDeleteRequired = "Y";
                        if (chkEntireTable.Checked)
                        {
                            strScriptEntireTable = "Y";
                        }
                        else
                        {
                            if (chkSearchByPrimaryKey.Checked)
                                strSearchByPrimary = "Y";
                            else
                            {
                                for (int i = 0; i < dgvSearchValue.ColumnCount; i++)
                                {
                                    if (strSearchByColumns.Length == 0)
                                        strSearchByColumns = dgvSearchValue.Columns[i].Name;
                                    else
                                        strSearchByColumns = strSearchByColumns + "," + dgvSearchValue.Columns[i].Name;
                                }
                            }
                            //Enter Search By Column Value
                            if (dgvSearchValue.RowCount > 0)
                            {
                                for (int i = 0; i < dgvSearchValue.RowCount; i++)
                                {
                                    string strSearchByColumnValueComma = string.Empty;
                                    for (int j = 0; j < dgvSearchValue.ColumnCount; j++)
                                    {
                                        if (strSearchByColumnValueComma.Length == 0)
                                            strSearchByColumnValueComma = Convert.ToString(dgvSearchValue.Rows[i].Cells[j].Value == DBNull.Value ? "" : dgvSearchValue.Rows[i].Cells[j].Value).Trim();
                                        else
                                            strSearchByColumnValueComma = strSearchByColumnValueComma + "," + Convert.ToString(dgvSearchValue.Rows[i].Cells[j].Value == DBNull.Value ? "" : dgvSearchValue.Rows[i].Cells[j].Value).Trim();
                                    }

                                    if (strSearchByColumnValues.Length == 0)
                                        strSearchByColumnValues = strSearchByColumnValueComma;//strSearchByColumnValues + "|";
                                    else
                                        strSearchByColumnValues = strSearchByColumnValues + "|" + strSearchByColumnValueComma;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter filter by values.", "Missing Filter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        objEntityList = objFetch.CreateInsertScriptForTable(strTableName, strSearchByColumns, strSearchByColumnValues, strDeleteRequired, strSearchByPrimary, strScriptEntireTable, strIncludeIdentityColumn);
                    }
                    else if (rdBtnSpCreate.Checked)//Creating stored procedure tamplate for a table
                    {
                        string strTableName = string.Empty;
                        string strForAllDatabaseTable = "N";
                        if (chkEntireDataBase.Checked)
                            strForAllDatabaseTable = "Y";
                        else
                            strTableName = txtTableNameList.Text;
                        objEntityList = objFetch.CreateStoredProcTempleteForTable(strTableName, strForAllDatabaseTable);
                    }
                    string strQery = string.Empty;
                    foreach (Entity objEntity in objEntityList)
                        strQery = strQery + objEntity.Text;
                    strQerySave = strQery;
                    txtDisplayScript.Text = strQery;
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
        private void btnSaveOnRetrive_Click(object sender, EventArgs e)
        {
            RetrieveRecord(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            RetrieveRecord(false);
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    FetchData objFetch = new FetchData();
            //    List<Entity> objEntityList = new List<Entity>();
            //    if (rdBtnScSpViFn.Checked)//Creating Insert and Drop script
            //    {
            //        ArrayList alParam = new ArrayList();
            //        if ((rdBtnEnterOwnValue.Checked || rdBtnGetListFrmDatabase.Checked) &&
            //            txtScriptNameList.Text.Trim().Length > 0)
            //        {
            //            alParam.Add(txtScriptNameList.Text);
            //            alParam.Add(chkInsertDeleteOrEnter.Checked ? "Y" : "N");
            //            objEntityList = objFetch.CreateDropInsertScript(alParam, "Enter Own Value");
            //        }
            //        else if (rdBtnModifiedByDate.Checked && dtFrom.Text != string.Empty && dtTo.Text != string.Empty)
            //        {
            //            if (dtFrom.Value > dtTo.Value)
            //            {
            //                MessageBox.Show("'From' date has to be less than 'To' date");
            //            }
            //            else if (!rdBtnSp.Checked && !rdFunc.Checked && !rdView.Checked)
            //            {
            //                MessageBox.Show("Please select atleast one Script Option.");
            //            }
            //            else
            //            {
            //                string strScriptType = string.Empty;
            //                if (rdBtnSp.Checked)
            //                    strScriptType = "'P'";
            //                if (rdFunc.Checked)
            //                {
            //                    if (strScriptType == string.Empty)
            //                        strScriptType = "'FN'";
            //                    else
            //                        strScriptType = strScriptType + ",'FN'";
            //                }
            //                if (rdView.Checked)
            //                {
            //                    if (strScriptType == string.Empty)
            //                        strScriptType = "'V'";
            //                    else
            //                        strScriptType = strScriptType + ",'V'";
            //                }
            //                alParam.Add(strScriptType);
            //                alParam.Add(chkInsertDeleteOrEnter.Checked ? "Y" : "N");
            //                alParam.Add(dtFrom.Value.Date);
            //                alParam.Add(dtTo.Value.Date.AddHours(12));
            //                objEntityList = objFetch.CreateDropInsertScript(alParam, "Modified By Date");
            //            }
            //        }
            //    }
            //    else if (rdBtnInsert.Checked)//Creating insert script for a table
            //    {
            //        string strTableName = cmbSearchTable.Text;
            //        string strSearchByColumns = string.Empty;
            //        string strSearchByColumnValues = string.Empty;
            //        string strDeleteRequired = "N";
            //        string strSearchByPrimary = "N";
            //        string strScriptEntireTable = "N";
            //        string strIncludeIdentityColumn = "N";
            //        if (chkIncludeIdentityColumn.Checked)
            //            strIncludeIdentityColumn = "Y";
            //        if (chkIncludeDelete.Checked)
            //            strDeleteRequired = "Y";
            //        if (chkEntireTable.Checked)
            //        {
            //            strScriptEntireTable = "Y";
            //        }
            //        else
            //        {
            //            if (chkSearchByPrimaryKey.Checked)
            //                strSearchByPrimary = "Y";
            //            else
            //            {
            //                for (int i = 0; i < dgvSearchValue.ColumnCount; i++)
            //                {
            //                    if (strSearchByColumns.Length == 0)
            //                        strSearchByColumns = dgvSearchValue.Columns[i].Name;
            //                    else
            //                        strSearchByColumns = strSearchByColumns + "," + dgvSearchValue.Columns[i].Name;
            //                }
            //            }
            //            //Enter Search By Column Value
            //            if (dgvSearchValue.RowCount > 0)
            //            {
            //                for (int i = 0; i < dgvSearchValue.RowCount; i++)
            //                {
            //                    string strSearchByColumnValueComma = string.Empty;
            //                    for (int j = 0; j < dgvSearchValue.ColumnCount; j++)
            //                    {
            //                        if (strSearchByColumnValueComma.Length == 0)
            //                            strSearchByColumnValueComma = Convert.ToString(dgvSearchValue.Rows[i].Cells[j].Value == DBNull.Value ? "" : dgvSearchValue.Rows[i].Cells[j].Value).Trim();
            //                        else
            //                            strSearchByColumnValueComma = strSearchByColumnValueComma + "," + Convert.ToString(dgvSearchValue.Rows[i].Cells[j].Value == DBNull.Value ? "" : dgvSearchValue.Rows[i].Cells[j].Value).Trim();
            //                    }

            //                    if (strSearchByColumnValues.Length == 0)
            //                        strSearchByColumnValues = strSearchByColumnValueComma;//strSearchByColumnValues + "|";
            //                    else
            //                        strSearchByColumnValues = strSearchByColumnValues + "|" + strSearchByColumnValueComma;
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show("Please enter filter by values.", "Missing Filter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                return;
            //            }
            //        }
            //        objEntityList = objFetch.CreateInsertScriptForTable(strTableName, strSearchByColumns, strSearchByColumnValues, strDeleteRequired, strSearchByPrimary, strScriptEntireTable, strIncludeIdentityColumn);
            //    }
            //    else if (rdBtnSpCreate.Checked)//Creating stored procedure tamplate for a table
            //    {
            //        string strTableName = string.Empty;
            //        string strForAllDatabaseTable = "N";
            //        if (chkEntireDataBase.Checked)
            //            strForAllDatabaseTable = "Y";
            //        else
            //            strTableName = txtTableNameList.Text;
            //        objEntityList = objFetch.CreateStoredProcTempleteForTable(strTableName, strForAllDatabaseTable);
            //    }
            //    string strQery = string.Empty;
            //    foreach (Entity objEntity in objEntityList)
            //        strQery = strQery + objEntity.Text;
            //    strQerySave = strQery;
            //    txtDisplayScript.Text = strQery;
            //}
            //catch (Exception ex)
            //{
            //    LogError(ex);
            //}
            //finally
            //{
            //    this.Cursor = Cursors.Default;
            //}
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
                this.Cursor = Cursors.WaitCursor;
                SaveFile(txtDisplayScript.Text, strQerySave);
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
        private void rdBtnEnterOwnValue_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                FormatControlForSearchType();
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
        private void rdBtnGetListFrmDatabase_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                FormatControlForSearchType();
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
        private void rdBtnModifiedByDate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                FormatControlForSearchType();
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
        private void chkEntireTable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                FormatCreateInsertScript();
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
        private void chkEntireDataBase_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                FormatNewSpGenerate();
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
        private void txtScriptNameList_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ttGenerateScript.SetToolTip(txtScriptNameList, "Please enter script name in comma seperated format for multiple fetch.  \r\nRight click to get the list of script from file.");
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
        private void cmsGetScriptList_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                if (grpBoxCriteriaSp.Visible)
                {
                    tsmiGetScrptNameFromFile.Enabled = true;
                    if (txtTableNameList.Text.Trim().Length > 0)
                        tsmiSaveScriptNameInFile.Enabled = true;
                    else
                        tsmiSaveScriptNameInFile.Enabled = false;
                }
                else
                {
                    if (txtScriptNameList.Text.Trim().Length > 0)
                        tsmiSaveScriptNameInFile.Enabled = true;
                    else
                        tsmiSaveScriptNameInFile.Enabled = false;
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
        private void tsmiGetScrptNameFromFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdGetFile = new OpenFileDialog();
                fdGetFile.Multiselect = false;
                fdGetFile.Filter = "Text File (*.txt)|*.txt";
                fdGetFile.FileName = strSavedScriptPath + (grpBoxCriteriaSp.Visible ? @"\table_" : @"\script_") + DateTime.Today.ToString("MM-dd-yyyy");
                if (fdGetFile.ShowDialog() == DialogResult.OK)
                {
                    StreamReader srFile = File.OpenText(fdGetFile.FileName);
                    if (grpBoxCriteriaSp.Visible)
                        txtTableNameList.Text = srFile.ReadToEnd();
                    else
                        txtScriptNameList.Text = srFile.ReadToEnd();
                    srFile.Dispose();
                    srFile.Close();
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
        private void tsmiSaveScriptNameInFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtScriptNameList.Text))
                {
                    if (!Directory.Exists(strSavedScriptPath))
                    {
                        strSavedScriptPath = ConfigurationManager.AppSettings["SavedScriptPath"];
                        CreateDirecotry(strSavedScriptPath);
                    }

                    string strTextToSave = grpBoxCriteriaSp.Visible ? txtTableNameList.Text.Trim() : txtScriptNameList.Text.Trim();
                    SaveFileDialog svDialog = new SaveFileDialog();
                    svDialog.Filter = "Text File (*.txt)|*.txt";
                    svDialog.FileName = strSavedScriptPath + (grpBoxCriteriaSp.Visible ? @"\table_" : @"\script_") + DateTime.Today.ToString("MM-dd-yyyy");
                    svDialog.DefaultExt = ".txt";
                    if (svDialog.ShowDialog() == DialogResult.OK)
                    {
                        string strFile = svDialog.FileName;
                        if (File.Exists(strFile))
                            File.Delete(strFile);
                        File.WriteAllText(strFile, strTextToSave);
                        if (File.Exists(strFile))
                            MessageBox.Show("File saved successfully.");
                        strSavedScriptPath = GetDirecotryPath(svDialog.FileName);
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
        private void cmbSearchScript_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddTableNameToGenerateScriptFromDatabase();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSearchScript_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                AddTableNameToGenerateScriptFromDatabase();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSearchScript_Leave(object sender, EventArgs e)
        {
            AddTableNameToGenerateScriptFromDatabase();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddTableNameToGenerateSpTempleteList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTableName_Leave(object sender, EventArgs e)
        {
            AddTableNameToGenerateSpTempleteList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTableName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                AddTableNameToGenerateSpTempleteList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSearchByPrimaryKey_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSearchByPrimaryKey.Checked || chkEntireTable.Checked)
                    dgvColumnName.ReadOnly = true;
                else
                    dgvColumnName.ReadOnly = false;
                dgvColumnName.Rows.Clear();
                dgvColumnName.Columns.Clear();
                GetColumnForTable();
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
        private void btnConfigure_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigureSetting(false);
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
        private void cmbSearchTable_Leave(object sender, EventArgs e)
        {
            try
            {
                GetColumnForTable();
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
        private void cmbSearchTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSearchTable_Leave(sender, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSearchTable_KeyDown(object sender, KeyEventArgs e)
        {
            cmbSearchTable_Leave(sender, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbSearchTable.Text) && dgvSearchValue.ColumnCount > 0)
                    dgvSearchValue.Rows.Add();
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
        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbSearchTable.Text) && dgvSearchValue.ColumnCount > 0 && dgvSearchValue.RowCount > 0 && dgvSearchValue.CurrentRow != null)
                    dgvSearchValue.Rows.Remove(dgvSearchValue.CurrentRow);
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
        private void dgvColumnName_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                dgvColumnName.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (Convert.ToBoolean(dgvColumnName.CurrentCell.Value) == true)
                {
                    if (!dgvSearchValue.Columns.Contains(dgvColumnName.CurrentCell.OwningColumn.Name))
                    {
                        dgvSearchValue.Columns.Add(dgvColumnName.CurrentCell.OwningColumn.Name, dgvColumnName.CurrentCell.OwningColumn.Name);
                        SetGridColumnWidth(dgvSearchValue, dgvColumnName.CurrentCell.OwningColumn.Name);
                    }
                }
                else
                {
                    if (dgvSearchValue.Columns.Contains(dgvColumnName.CurrentCell.OwningColumn.Name))
                        dgvSearchValue.Columns.Remove(dgvColumnName.CurrentCell.OwningColumn.Name);
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
        private void txtTableNameList_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ttGenerateScript.SetToolTip(txtTableNameList, "Right click to get the list of Table from file.");
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
        private void cmbSearchScript_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ttGenerateScript.SetToolTip(txtTableNameList, "Select from the Dropdown Option or Type the Script Name to search.");
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
        private void cmbSearchTable_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ttGenerateScript.SetToolTip(txtTableNameList, "Select from the Dropdown Option or Type the Script Name to search.");
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
        private void cmbTableName_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ttGenerateScript.SetToolTip(txtTableNameList, "Select from the Dropdown Option or Type the Script Name to search.");
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
        private void dgvColumnName_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ttGenerateScript.SetToolTip(txtTableNameList, "Please Check the column by which you want to Filter the data.");
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
        private void dgvSearchValue_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ttGenerateScript.SetToolTip(txtTableNameList, "To add your data filter please click Add Row button.");
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
        private void dgvSearchValue_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvSearchValue.BeginEdit(false);
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
        private void btnInsidEdge_Click(object sender, EventArgs e)
        {
            try
            {
                MoreOptions objMoreOption = new MoreOptions();
                objMoreOption.ShowDialog();
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
        private void picBoxHelp_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdBtnScSpViFn.Checked)//Creating Script
                {
                    OpenHelpText(Help.HelpType.GenerateScript);
                }
                else if (rdBtnInsert.Checked)//Creating Insert Script
                {
                    OpenHelpText(Help.HelpType.CreateInsertScriptForTable);
                }
                else if (rdBtnSpCreate.Checked)//Creates Sp Templete
                {
                    OpenHelpText(Help.HelpType.CreateSpTemplete);
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
        private void ScriptGenerate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.F )
                {
                    //this.ContainsFocus
                    Find objFind = new Find(txtDisplayScript);
                    objFind.TextFound += new TextFoundEventHandler(Search_TextFound);
                    objFind.ShowDialog();
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
        private void Search_TextFound(object sender, TextFoundEventArgs e)
        {
            if (sender != null)
            {
                ////this.Focus();
                //RichTextBox txtSearchText = (RichTextBox)sender;
                //txtSearchText.Select(e.SelectionStartIndex, e.SelectionLength);
                ////txtSearchText.Find(
            }
        }

        #endregion Events

    }
}