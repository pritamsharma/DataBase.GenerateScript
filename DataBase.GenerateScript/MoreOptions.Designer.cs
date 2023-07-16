namespace DataBase.GenerateScript
{
    partial class MoreOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoreOptions));
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.txtDisplayScript = new System.Windows.Forms.RichTextBox();
            this.tabControlScript = new System.Windows.Forms.TabControl();
            this.tabPageOperation = new System.Windows.Forms.TabPage();
            this.lstMenuName = new System.Windows.Forms.ListBox();
            this.lblOperation = new System.Windows.Forms.Label();
            this.lblMenuName = new System.Windows.Forms.Label();
            this.cmbMenuName = new System.Windows.Forms.ComboBox();
            this.grpBoxMenuOption = new System.Windows.Forms.GroupBox();
            this.rdBtnOperationScript = new System.Windows.Forms.RadioButton();
            this.rdBtnMenuGenerate = new System.Windows.Forms.RadioButton();
            this.tabNewFormAdd = new System.Windows.Forms.TabPage();
            this.lblControlPerLine = new System.Windows.Forms.Label();
            this.txtControlPerLine = new System.Windows.Forms.MaskedTextBox();
            this.lblX = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.MaskedTextBox();
            this.lblMargin = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.MaskedTextBox();
            this.lblFormName = new System.Windows.Forms.Label();
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.lblNameSpace = new System.Windows.Forms.Label();
            this.txtNameSpaceName = new System.Windows.Forms.TextBox();
            this.dgvColumnDetails = new System.Windows.Forms.DataGridView();
            this.btnAddColumn = new System.Windows.Forms.Button();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            this.lblResolution = new System.Windows.Forms.Label();
            this.cmbDisplayOption = new System.Windows.Forms.ComboBox();
            this.lblDisplayOption = new System.Windows.Forms.Label();
            this.cmbTableName = new System.Windows.Forms.ComboBox();
            this.lblTableName = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.toolTipScriptGenerate = new System.Windows.Forms.ToolTip(this.components);
            //this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControlScript.SuspendLayout();
            this.tabPageOperation.SuspendLayout();
            this.grpBoxMenuOption.SuspendLayout();
            this.tabNewFormAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumnDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnReset.Location = new System.Drawing.Point(269, 454);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(60, 23);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(206, 454);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRetrieve.Location = new System.Drawing.Point(143, 454);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(60, 23);
            this.btnRetrieve.TabIndex = 8;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.UseVisualStyleBackColor = true;
            this.btnRetrieve.Click += new System.EventHandler(this.btnRetrieve_Click);
            // 
            // txtDisplayScript
            // 
            this.txtDisplayScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDisplayScript.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtDisplayScript.Location = new System.Drawing.Point(2, 236);
            this.txtDisplayScript.Name = "txtDisplayScript";
            this.txtDisplayScript.ReadOnly = true;
            this.txtDisplayScript.Size = new System.Drawing.Size(534, 212);
            this.txtDisplayScript.TabIndex = 11;
            this.txtDisplayScript.Text = "";
            this.txtDisplayScript.WordWrap = false;
            // 
            // tabControlScript
            // 
            this.tabControlScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlScript.Controls.Add(this.tabPageOperation);
            this.tabControlScript.Controls.Add(this.tabNewFormAdd);
            this.tabControlScript.Location = new System.Drawing.Point(2, 1);
            this.tabControlScript.Name = "tabControlScript";
            this.tabControlScript.SelectedIndex = 0;
            this.tabControlScript.Size = new System.Drawing.Size(536, 233);
            this.tabControlScript.TabIndex = 12;
            this.tabControlScript.SelectedIndexChanged += new System.EventHandler(this.tabControlScript_SelectedIndexChanged);
            // 
            // tabPageOperation
            // 
            this.tabPageOperation.BackColor = System.Drawing.Color.Transparent;
            this.tabPageOperation.Controls.Add(this.lstMenuName);
            this.tabPageOperation.Controls.Add(this.lblOperation);
            this.tabPageOperation.Controls.Add(this.lblMenuName);
            this.tabPageOperation.Controls.Add(this.cmbMenuName);
            this.tabPageOperation.Controls.Add(this.grpBoxMenuOption);
            this.tabPageOperation.Location = new System.Drawing.Point(4, 22);
            this.tabPageOperation.Name = "tabPageOperation";
            this.tabPageOperation.Size = new System.Drawing.Size(528, 207);
            this.tabPageOperation.TabIndex = 0;
            this.tabPageOperation.Text = "Menu & Operation";
            this.tabPageOperation.UseVisualStyleBackColor = true;
            // 
            // lstMenuName
            // 
            this.lstMenuName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMenuName.Location = new System.Drawing.Point(89, 65);
            this.lstMenuName.Name = "lstMenuName";
            this.lstMenuName.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstMenuName.Size = new System.Drawing.Size(425, 108);
            this.lstMenuName.TabIndex = 5;
            this.toolTipScriptGenerate.SetToolTip(this.lstMenuName, "Select multiple  name for generating script for more then one name.");
            // 
            // lblOperation
            // 
            this.lblOperation.AutoSize = true;
            this.lblOperation.Location = new System.Drawing.Point(1, 67);
            this.lblOperation.Name = "lblOperation";
            this.lblOperation.Size = new System.Drawing.Size(87, 13);
            this.lblOperation.TabIndex = 4;
            this.lblOperation.Text = "Operation Name:";
            // 
            // lblMenuName
            // 
            this.lblMenuName.AutoSize = true;
            this.lblMenuName.Location = new System.Drawing.Point(18, 41);
            this.lblMenuName.Name = "lblMenuName";
            this.lblMenuName.Size = new System.Drawing.Size(68, 13);
            this.lblMenuName.TabIndex = 2;
            this.lblMenuName.Text = "Menu Name:";
            // 
            // cmbMenuName
            // 
            this.cmbMenuName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMenuName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbMenuName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMenuName.FormattingEnabled = true;
            this.cmbMenuName.Location = new System.Drawing.Point(89, 37);
            this.cmbMenuName.Name = "cmbMenuName";
            this.cmbMenuName.Size = new System.Drawing.Size(425, 21);
            this.cmbMenuName.TabIndex = 1;
            this.cmbMenuName.SelectedIndexChanged += new System.EventHandler(this.cmbMenuName_SelectedIndexChanged);
            // 
            // grpBoxMenuOption
            // 
            this.grpBoxMenuOption.Controls.Add(this.rdBtnOperationScript);
            this.grpBoxMenuOption.Controls.Add(this.rdBtnMenuGenerate);
            this.grpBoxMenuOption.Location = new System.Drawing.Point(7, 3);
            this.grpBoxMenuOption.Name = "grpBoxMenuOption";
            this.grpBoxMenuOption.Size = new System.Drawing.Size(415, 31);
            this.grpBoxMenuOption.TabIndex = 0;
            this.grpBoxMenuOption.TabStop = false;
            this.grpBoxMenuOption.Text = "Options:";
            // 
            // rdBtnOperationScript
            // 
            this.rdBtnOperationScript.AutoSize = true;
            this.rdBtnOperationScript.Location = new System.Drawing.Point(141, 13);
            this.rdBtnOperationScript.Name = "rdBtnOperationScript";
            this.rdBtnOperationScript.Size = new System.Drawing.Size(148, 17);
            this.rdBtnOperationScript.TabIndex = 1;
            this.rdBtnOperationScript.TabStop = true;
            this.rdBtnOperationScript.Text = "Generate Operation Script";
            this.rdBtnOperationScript.UseVisualStyleBackColor = true;
            this.rdBtnOperationScript.CheckedChanged += new System.EventHandler(this.rdBtnOperationScript_CheckedChanged);
            // 
            // rdBtnMenuGenerate
            // 
            this.rdBtnMenuGenerate.AutoSize = true;
            this.rdBtnMenuGenerate.Location = new System.Drawing.Point(6, 13);
            this.rdBtnMenuGenerate.Name = "rdBtnMenuGenerate";
            this.rdBtnMenuGenerate.Size = new System.Drawing.Size(129, 17);
            this.rdBtnMenuGenerate.TabIndex = 0;
            this.rdBtnMenuGenerate.TabStop = true;
            this.rdBtnMenuGenerate.Text = "Generate Menu Script";
            this.rdBtnMenuGenerate.UseVisualStyleBackColor = true;
            this.rdBtnMenuGenerate.CheckedChanged += new System.EventHandler(this.rdBtnMenuGenerate_CheckedChanged);
            // 
            // tabNewFormAdd
            // 
            this.tabNewFormAdd.Controls.Add(this.lblControlPerLine);
            this.tabNewFormAdd.Controls.Add(this.txtControlPerLine);
            this.tabNewFormAdd.Controls.Add(this.lblX);
            this.tabNewFormAdd.Controls.Add(this.txtY);
            this.tabNewFormAdd.Controls.Add(this.lblMargin);
            this.tabNewFormAdd.Controls.Add(this.txtX);
            this.tabNewFormAdd.Controls.Add(this.lblFormName);
            this.tabNewFormAdd.Controls.Add(this.txtFormName);
            this.tabNewFormAdd.Controls.Add(this.lblNameSpace);
            this.tabNewFormAdd.Controls.Add(this.txtNameSpaceName);
            this.tabNewFormAdd.Controls.Add(this.dgvColumnDetails);
            this.tabNewFormAdd.Controls.Add(this.btnAddColumn);
            this.tabNewFormAdd.Controls.Add(this.cmbResolution);
            this.tabNewFormAdd.Controls.Add(this.lblResolution);
            this.tabNewFormAdd.Controls.Add(this.cmbDisplayOption);
            this.tabNewFormAdd.Controls.Add(this.lblDisplayOption);
            this.tabNewFormAdd.Controls.Add(this.cmbTableName);
            this.tabNewFormAdd.Controls.Add(this.lblTableName);
            this.tabNewFormAdd.Location = new System.Drawing.Point(4, 22);
            this.tabNewFormAdd.Name = "tabNewFormAdd";
            this.tabNewFormAdd.Size = new System.Drawing.Size(528, 207);
            this.tabNewFormAdd.TabIndex = 1;
            this.tabNewFormAdd.Text = "New Form";
            this.tabNewFormAdd.UseVisualStyleBackColor = true;
            // 
            // lblControlPerLine
            // 
            this.lblControlPerLine.AutoSize = true;
            this.lblControlPerLine.Location = new System.Drawing.Point(200, 188);
            this.lblControlPerLine.Name = "lblControlPerLine";
            this.lblControlPerLine.Size = new System.Drawing.Size(85, 13);
            this.lblControlPerLine.TabIndex = 17;
            this.lblControlPerLine.Text = "Control Per Line:";
            // 
            // txtControlPerLine
            // 
            this.txtControlPerLine.Location = new System.Drawing.Point(288, 184);
            this.txtControlPerLine.Mask = "0";
            this.txtControlPerLine.Name = "txtControlPerLine";
            this.txtControlPerLine.Size = new System.Drawing.Size(33, 20);
            this.txtControlPerLine.TabIndex = 16;
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(142, 188);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(11, 13);
            this.lblX.TabIndex = 15;
            this.lblX.Text = "*";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(156, 184);
            this.txtY.Mask = "00";
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(33, 20);
            this.txtY.TabIndex = 14;
            // 
            // lblMargin
            // 
            this.lblMargin.AutoSize = true;
            this.lblMargin.Location = new System.Drawing.Point(30, 188);
            this.lblMargin.Name = "lblMargin";
            this.lblMargin.Size = new System.Drawing.Size(75, 13);
            this.lblMargin.TabIndex = 13;
            this.lblMargin.Text = "Margin (X * Y):";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(106, 184);
            this.txtX.Mask = "00";
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(33, 20);
            this.txtX.TabIndex = 12;
            // 
            // lblFormName
            // 
            this.lblFormName.AutoSize = true;
            this.lblFormName.Location = new System.Drawing.Point(41, 164);
            this.lblFormName.Name = "lblFormName";
            this.lblFormName.Size = new System.Drawing.Size(64, 13);
            this.lblFormName.TabIndex = 11;
            this.lblFormName.Text = "Form Name:";
            // 
            // txtFormName
            // 
            this.txtFormName.Location = new System.Drawing.Point(107, 161);
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Size = new System.Drawing.Size(415, 20);
            this.txtFormName.TabIndex = 10;
            // 
            // lblNameSpace
            // 
            this.lblNameSpace.AutoSize = true;
            this.lblNameSpace.Location = new System.Drawing.Point(5, 141);
            this.lblNameSpace.Name = "lblNameSpace";
            this.lblNameSpace.Size = new System.Drawing.Size(100, 13);
            this.lblNameSpace.TabIndex = 9;
            this.lblNameSpace.Text = "NameSpace Name:";
            // 
            // txtNameSpaceName
            // 
            this.txtNameSpaceName.Location = new System.Drawing.Point(107, 138);
            this.txtNameSpaceName.Name = "txtNameSpaceName";
            this.txtNameSpaceName.Size = new System.Drawing.Size(415, 20);
            this.txtNameSpaceName.TabIndex = 8;
            // 
            // dgvColumnDetails
            // 
            this.dgvColumnDetails.AllowUserToAddRows = false;
            this.dgvColumnDetails.AllowUserToDeleteRows = false;
            this.dgvColumnDetails.AllowUserToResizeRows = false;
            this.dgvColumnDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumnDetails.Location = new System.Drawing.Point(7, 57);
            this.dgvColumnDetails.Name = "dgvColumnDetails";
            this.dgvColumnDetails.RowHeadersWidth = 21;
            this.dgvColumnDetails.Size = new System.Drawing.Size(513, 76);
            this.dgvColumnDetails.TabIndex = 7;
            // 
            // btnAddColumn
            // 
            this.btnAddColumn.Location = new System.Drawing.Point(364, 30);
            this.btnAddColumn.Name = "btnAddColumn";
            this.btnAddColumn.Size = new System.Drawing.Size(98, 23);
            this.btnAddColumn.TabIndex = 6;
            this.btnAddColumn.Text = "Add Column";
            this.btnAddColumn.UseVisualStyleBackColor = true;
            this.btnAddColumn.Click += new System.EventHandler(this.btnAddColumn_Click);
            // 
            // cmbResolution
            // 
            this.cmbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResolution.FormattingEnabled = true;
            this.cmbResolution.Location = new System.Drawing.Point(364, 4);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(158, 21);
            this.cmbResolution.TabIndex = 5;
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Location = new System.Drawing.Point(302, 8);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(60, 13);
            this.lblResolution.TabIndex = 4;
            this.lblResolution.Text = "Resolution:";
            // 
            // cmbDisplayOption
            // 
            this.cmbDisplayOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisplayOption.FormattingEnabled = true;
            this.cmbDisplayOption.Location = new System.Drawing.Point(82, 32);
            this.cmbDisplayOption.Name = "cmbDisplayOption";
            this.cmbDisplayOption.Size = new System.Drawing.Size(210, 21);
            this.cmbDisplayOption.TabIndex = 3;
            // 
            // lblDisplayOption
            // 
            this.lblDisplayOption.AutoSize = true;
            this.lblDisplayOption.Location = new System.Drawing.Point(2, 36);
            this.lblDisplayOption.Name = "lblDisplayOption";
            this.lblDisplayOption.Size = new System.Drawing.Size(78, 13);
            this.lblDisplayOption.TabIndex = 2;
            this.lblDisplayOption.Text = "Display Option:";
            // 
            // cmbTableName
            // 
            this.cmbTableName.FormattingEnabled = true;
            this.cmbTableName.Location = new System.Drawing.Point(82, 4);
            this.cmbTableName.Name = "cmbTableName";
            this.cmbTableName.Size = new System.Drawing.Size(210, 21);
            this.cmbTableName.TabIndex = 1;
            this.cmbTableName.SelectedIndexChanged += new System.EventHandler(this.cmbTableName_SelectedIndexChanged);
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(43, 8);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(37, 13);
            this.lblTableName.TabIndex = 0;
            this.lblTableName.Text = "Table:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Location = new System.Drawing.Point(333, 454);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MoreOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 480);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControlScript);
            this.Controls.Add(this.txtDisplayScript);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRetrieve);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MoreOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Script Generator (Telsource Specific)";
            this.Shown += new System.EventHandler(this.MoreOptions_Shown);
            this.Load += new System.EventHandler(this.MoreOptions_Load);
            this.tabControlScript.ResumeLayout(false);
            this.tabPageOperation.ResumeLayout(false);
            this.tabPageOperation.PerformLayout();
            this.grpBoxMenuOption.ResumeLayout(false);
            this.grpBoxMenuOption.PerformLayout();
            this.tabNewFormAdd.ResumeLayout(false);
            this.tabNewFormAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumnDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRetrieve;
        private System.Windows.Forms.RichTextBox txtDisplayScript;
        private System.Windows.Forms.TabControl tabControlScript;
        private System.Windows.Forms.TabPage tabPageOperation;
        private System.Windows.Forms.GroupBox grpBoxMenuOption;
        private System.Windows.Forms.RadioButton rdBtnMenuGenerate;
        private System.Windows.Forms.RadioButton rdBtnOperationScript;
        private System.Windows.Forms.ComboBox cmbMenuName;
        private System.Windows.Forms.Label lblOperation;
        private System.Windows.Forms.Label lblMenuName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox lstMenuName;
        private System.Windows.Forms.ToolTip toolTipScriptGenerate;
        private System.Windows.Forms.TabPage tabNewFormAdd;
        private System.Windows.Forms.Button btnAddColumn;
        private System.Windows.Forms.ComboBox cmbResolution;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.ComboBox cmbDisplayOption;
        private System.Windows.Forms.Label lblDisplayOption;
        private System.Windows.Forms.ComboBox cmbTableName;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.DataGridView dgvColumnDetails;
        private System.Windows.Forms.Label lblNameSpace;
        private System.Windows.Forms.TextBox txtNameSpaceName;
        private System.Windows.Forms.Label lblFormName;
        private System.Windows.Forms.TextBox txtFormName;
        private System.Windows.Forms.MaskedTextBox txtY;
        private System.Windows.Forms.Label lblMargin;
        private System.Windows.Forms.MaskedTextBox txtX;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblControlPerLine;
        private System.Windows.Forms.MaskedTextBox txtControlPerLine;
        //private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}