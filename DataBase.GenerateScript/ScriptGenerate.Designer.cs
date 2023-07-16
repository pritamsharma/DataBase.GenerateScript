namespace DataBase.GenerateScript
{
    partial class ScriptGenerate
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptGenerate));
            this.grpBoxOptions = new System.Windows.Forms.GroupBox();
            this.rdBtnSpCreate = new System.Windows.Forms.RadioButton();
            this.rdBtnInsert = new System.Windows.Forms.RadioButton();
            this.rdBtnScSpViFn = new System.Windows.Forms.RadioButton();
            this.txtDisplayScript = new System.Windows.Forms.RichTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.grpBoxCriteriaScript = new System.Windows.Forms.GroupBox();
            this.grpScriptInsertDeleteorEnter = new System.Windows.Forms.GroupBox();
            this.chkInsertDeleteOrEnter = new System.Windows.Forms.CheckBox();
            this.grpScriptType = new System.Windows.Forms.GroupBox();
            this.rdBtnSp = new System.Windows.Forms.CheckBox();
            this.rdFunc = new System.Windows.Forms.CheckBox();
            this.rdView = new System.Windows.Forms.CheckBox();
            this.cmbSearchScript = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdBtnModifiedByDate = new System.Windows.Forms.RadioButton();
            this.rdBtnGetListFrmDatabase = new System.Windows.Forms.RadioButton();
            this.rdBtnEnterOwnValue = new System.Windows.Forms.RadioButton();
            this.lblGetScriptName = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.txtScriptNameList = new System.Windows.Forms.TextBox();
            this.cmsGetScriptList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiGetScrptNameFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveScriptNameInFile = new System.Windows.Forms.ToolStripMenuItem();
            this.lblScript = new System.Windows.Forms.Label();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.grpBoxCriteriaInsertTable = new System.Windows.Forms.GroupBox();
            this.btnDeleteRow = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.cmbSearchTable = new System.Windows.Forms.ComboBox();
            this.dgvColumnName = new System.Windows.Forms.DataGridView();
            this.chkIncludeIdentityColumn = new System.Windows.Forms.CheckBox();
            this.chkEntireTable = new System.Windows.Forms.CheckBox();
            this.chkSearchByPrimaryKey = new System.Windows.Forms.CheckBox();
            this.chkIncludeDelete = new System.Windows.Forms.CheckBox();
            this.lblEnterFilterValue = new System.Windows.Forms.Label();
            this.dgvSearchValue = new System.Windows.Forms.DataGridView();
            this.lblGetColumnName = new System.Windows.Forms.Label();
            this.lblGetTableName = new System.Windows.Forms.Label();
            this.grpBoxCriteriaSp = new System.Windows.Forms.GroupBox();
            this.cmbTableName = new System.Windows.Forms.ComboBox();
            this.chkEntireDataBase = new System.Windows.Forms.CheckBox();
            this.lblTableList = new System.Windows.Forms.Label();
            this.lblTableName = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.txtTableNameList = new System.Windows.Forms.TextBox();
            this.ttGenerateScript = new System.Windows.Forms.ToolTip(this.components);
            this.btnConfigure = new System.Windows.Forms.Button();
            this.picBoxHelp = new System.Windows.Forms.PictureBox();
            this.btnInsidEdge = new System.Windows.Forms.Button();
            this.btnSaveOnRetrive = new System.Windows.Forms.Button();
            this.grpBoxOptions.SuspendLayout();
            this.grpBoxCriteriaScript.SuspendLayout();
            this.grpScriptInsertDeleteorEnter.SuspendLayout();
            this.grpScriptType.SuspendLayout();
            this.panel1.SuspendLayout();
            this.cmsGetScriptList.SuspendLayout();
            this.grpBoxCriteriaInsertTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumnName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchValue)).BeginInit();
            this.grpBoxCriteriaSp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBoxOptions
            // 
            this.grpBoxOptions.Controls.Add(this.rdBtnSpCreate);
            this.grpBoxOptions.Controls.Add(this.rdBtnInsert);
            this.grpBoxOptions.Controls.Add(this.rdBtnScSpViFn);
            this.grpBoxOptions.Location = new System.Drawing.Point(5, 2);
            this.grpBoxOptions.Name = "grpBoxOptions";
            this.grpBoxOptions.Size = new System.Drawing.Size(463, 44);
            this.grpBoxOptions.TabIndex = 1;
            this.grpBoxOptions.TabStop = false;
            this.grpBoxOptions.Text = "Options (Script):";
            // 
            // rdBtnSpCreate
            // 
            this.rdBtnSpCreate.AutoSize = true;
            this.rdBtnSpCreate.Location = new System.Drawing.Point(315, 19);
            this.rdBtnSpCreate.Name = "rdBtnSpCreate";
            this.rdBtnSpCreate.Size = new System.Drawing.Size(142, 17);
            this.rdBtnSpCreate.TabIndex = 2;
            this.rdBtnSpCreate.Text = "Create Stored Procedure";
            this.rdBtnSpCreate.UseVisualStyleBackColor = true;
            this.rdBtnSpCreate.CheckedChanged += new System.EventHandler(this.rdBtnSpCreate_CheckedChanged);
            // 
            // rdBtnInsert
            // 
            this.rdBtnInsert.AutoSize = true;
            this.rdBtnInsert.Location = new System.Drawing.Point(217, 19);
            this.rdBtnInsert.Name = "rdBtnInsert";
            this.rdBtnInsert.Size = new System.Drawing.Size(96, 17);
            this.rdBtnInsert.TabIndex = 1;
            this.rdBtnInsert.Text = "Insert for Table";
            this.rdBtnInsert.UseVisualStyleBackColor = true;
            this.rdBtnInsert.CheckedChanged += new System.EventHandler(this.rdBtnInsert_CheckedChanged);
            // 
            // rdBtnScSpViFn
            // 
            this.rdBtnScSpViFn.AutoSize = true;
            this.rdBtnScSpViFn.Location = new System.Drawing.Point(6, 19);
            this.rdBtnScSpViFn.Name = "rdBtnScSpViFn";
            this.rdBtnScSpViFn.Size = new System.Drawing.Size(207, 17);
            this.rdBtnScSpViFn.TabIndex = 0;
            this.rdBtnScSpViFn.Text = "Stored Procedure, Function and Views";
            this.rdBtnScSpViFn.UseVisualStyleBackColor = true;
            this.rdBtnScSpViFn.CheckedChanged += new System.EventHandler(this.rdBtnScSpViFn_CheckedChanged);
            // 
            // txtDisplayScript
            // 
            this.txtDisplayScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDisplayScript.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtDisplayScript.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplayScript.Location = new System.Drawing.Point(4, 512);
            this.txtDisplayScript.Name = "txtDisplayScript";
            this.txtDisplayScript.ReadOnly = true;
            this.txtDisplayScript.Size = new System.Drawing.Size(721, 55);
            this.txtDisplayScript.TabIndex = 12;
            this.txtDisplayScript.Text = "";
            this.ttGenerateScript.SetToolTip(this.txtDisplayScript, "Use the Save button to save the text displayed here.");
            this.txtDisplayScript.WordWrap = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(540, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.ttGenerateScript.SetToolTip(this.btnSave, "Click to save Script.");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(603, 2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(60, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.ttGenerateScript.SetToolTip(this.btnReset, "Reset the screen to initial value.");
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // grpBoxCriteriaScript
            // 
            this.grpBoxCriteriaScript.Controls.Add(this.grpScriptInsertDeleteorEnter);
            this.grpBoxCriteriaScript.Controls.Add(this.grpScriptType);
            this.grpBoxCriteriaScript.Controls.Add(this.cmbSearchScript);
            this.grpBoxCriteriaScript.Controls.Add(this.panel1);
            this.grpBoxCriteriaScript.Controls.Add(this.lblGetScriptName);
            this.grpBoxCriteriaScript.Controls.Add(this.lblTo);
            this.grpBoxCriteriaScript.Controls.Add(this.lblFrom);
            this.grpBoxCriteriaScript.Controls.Add(this.dtTo);
            this.grpBoxCriteriaScript.Controls.Add(this.dtFrom);
            this.grpBoxCriteriaScript.Controls.Add(this.txtScriptNameList);
            this.grpBoxCriteriaScript.Controls.Add(this.lblScript);
            this.grpBoxCriteriaScript.Location = new System.Drawing.Point(4, 47);
            this.grpBoxCriteriaScript.Name = "grpBoxCriteriaScript";
            this.grpBoxCriteriaScript.Size = new System.Drawing.Size(721, 131);
            this.grpBoxCriteriaScript.TabIndex = 9;
            this.grpBoxCriteriaScript.TabStop = false;
            this.grpBoxCriteriaScript.Text = "Details:";
            // 
            // grpScriptInsertDeleteorEnter
            // 
            this.grpScriptInsertDeleteorEnter.Controls.Add(this.chkInsertDeleteOrEnter);
            this.grpScriptInsertDeleteorEnter.Location = new System.Drawing.Point(522, 75);
            this.grpScriptInsertDeleteorEnter.Name = "grpScriptInsertDeleteorEnter";
            this.grpScriptInsertDeleteorEnter.Size = new System.Drawing.Size(193, 30);
            this.grpScriptInsertDeleteorEnter.TabIndex = 6;
            this.grpScriptInsertDeleteorEnter.TabStop = false;
            // 
            // chkInsertDeleteOrEnter
            // 
            this.chkInsertDeleteOrEnter.AutoSize = true;
            this.chkInsertDeleteOrEnter.Location = new System.Drawing.Point(5, 8);
            this.chkInsertDeleteOrEnter.Name = "chkInsertDeleteOrEnter";
            this.chkInsertDeleteOrEnter.Size = new System.Drawing.Size(143, 17);
            this.chkInsertDeleteOrEnter.TabIndex = 0;
            this.chkInsertDeleteOrEnter.Text = "Insert-Delete Type Script";
            this.chkInsertDeleteOrEnter.UseVisualStyleBackColor = true;
            // 
            // grpScriptType
            // 
            this.grpScriptType.Controls.Add(this.rdBtnSp);
            this.grpScriptType.Controls.Add(this.rdFunc);
            this.grpScriptType.Controls.Add(this.rdView);
            this.grpScriptType.Location = new System.Drawing.Point(522, 8);
            this.grpScriptType.Name = "grpScriptType";
            this.grpScriptType.Size = new System.Drawing.Size(193, 67);
            this.grpScriptType.TabIndex = 1;
            this.grpScriptType.TabStop = false;
            this.grpScriptType.Text = "Script Type";
            // 
            // rdBtnSp
            // 
            this.rdBtnSp.AutoSize = true;
            this.rdBtnSp.Location = new System.Drawing.Point(6, 15);
            this.rdBtnSp.Name = "rdBtnSp";
            this.rdBtnSp.Size = new System.Drawing.Size(109, 17);
            this.rdBtnSp.TabIndex = 0;
            this.rdBtnSp.Text = "Stored Procedure";
            this.rdBtnSp.UseVisualStyleBackColor = true;
            // 
            // rdFunc
            // 
            this.rdFunc.AutoSize = true;
            this.rdFunc.Location = new System.Drawing.Point(6, 48);
            this.rdFunc.Name = "rdFunc";
            this.rdFunc.Size = new System.Drawing.Size(72, 17);
            this.rdFunc.TabIndex = 2;
            this.rdFunc.Text = "Functions";
            this.rdFunc.UseVisualStyleBackColor = true;
            // 
            // rdView
            // 
            this.rdView.AutoSize = true;
            this.rdView.Location = new System.Drawing.Point(6, 32);
            this.rdView.Name = "rdView";
            this.rdView.Size = new System.Drawing.Size(49, 17);
            this.rdView.TabIndex = 1;
            this.rdView.Text = "View";
            this.rdView.UseVisualStyleBackColor = true;
            // 
            // cmbSearchScript
            // 
            this.cmbSearchScript.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSearchScript.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSearchScript.FormattingEnabled = true;
            this.cmbSearchScript.Location = new System.Drawing.Point(82, 80);
            this.cmbSearchScript.Name = "cmbSearchScript";
            this.cmbSearchScript.Size = new System.Drawing.Size(433, 21);
            this.cmbSearchScript.TabIndex = 8;
            this.ttGenerateScript.SetToolTip(this.cmbSearchScript, "Select from the Dropdown Option or Type the Script Name to search.");
            this.cmbSearchScript.MouseHover += new System.EventHandler(this.cmbSearchScript_MouseHover);
            this.cmbSearchScript.SelectedIndexChanged += new System.EventHandler(this.cmbSearchScript_SelectedIndexChanged);
            this.cmbSearchScript.Leave += new System.EventHandler(this.cmbSearchScript_Leave);
            this.cmbSearchScript.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSearchScript_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdBtnModifiedByDate);
            this.panel1.Controls.Add(this.rdBtnGetListFrmDatabase);
            this.panel1.Controls.Add(this.rdBtnEnterOwnValue);
            this.panel1.Location = new System.Drawing.Point(84, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 25);
            this.panel1.TabIndex = 0;
            // 
            // rdBtnModifiedByDate
            // 
            this.rdBtnModifiedByDate.AutoSize = true;
            this.rdBtnModifiedByDate.Location = new System.Drawing.Point(258, 4);
            this.rdBtnModifiedByDate.Name = "rdBtnModifiedByDate";
            this.rdBtnModifiedByDate.Size = new System.Drawing.Size(105, 17);
            this.rdBtnModifiedByDate.TabIndex = 2;
            this.rdBtnModifiedByDate.Text = "Modified by Date";
            this.rdBtnModifiedByDate.UseVisualStyleBackColor = true;
            this.rdBtnModifiedByDate.CheckedChanged += new System.EventHandler(this.rdBtnModifiedByDate_CheckedChanged);
            // 
            // rdBtnGetListFrmDatabase
            // 
            this.rdBtnGetListFrmDatabase.AutoSize = true;
            this.rdBtnGetListFrmDatabase.Location = new System.Drawing.Point(119, 4);
            this.rdBtnGetListFrmDatabase.Name = "rdBtnGetListFrmDatabase";
            this.rdBtnGetListFrmDatabase.Size = new System.Drawing.Size(133, 17);
            this.rdBtnGetListFrmDatabase.TabIndex = 1;
            this.rdBtnGetListFrmDatabase.Text = "Get List from Database";
            this.rdBtnGetListFrmDatabase.UseVisualStyleBackColor = true;
            this.rdBtnGetListFrmDatabase.CheckedChanged += new System.EventHandler(this.rdBtnGetListFrmDatabase_CheckedChanged);
            // 
            // rdBtnEnterOwnValue
            // 
            this.rdBtnEnterOwnValue.AutoSize = true;
            this.rdBtnEnterOwnValue.Location = new System.Drawing.Point(8, 4);
            this.rdBtnEnterOwnValue.Name = "rdBtnEnterOwnValue";
            this.rdBtnEnterOwnValue.Size = new System.Drawing.Size(105, 17);
            this.rdBtnEnterOwnValue.TabIndex = 0;
            this.rdBtnEnterOwnValue.Text = "Enter Own Value";
            this.rdBtnEnterOwnValue.UseVisualStyleBackColor = true;
            this.rdBtnEnterOwnValue.CheckedChanged += new System.EventHandler(this.rdBtnEnterOwnValue_CheckedChanged);
            // 
            // lblGetScriptName
            // 
            this.lblGetScriptName.AutoSize = true;
            this.lblGetScriptName.Location = new System.Drawing.Point(5, 84);
            this.lblGetScriptName.Name = "lblGetScriptName";
            this.lblGetScriptName.Size = new System.Drawing.Size(74, 13);
            this.lblGetScriptName.TabIndex = 7;
            this.lblGetScriptName.Text = "Search Script:";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(289, 60);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(23, 13);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "To:";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(46, 59);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(33, 13);
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Text = "From:";
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(315, 56);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(200, 20);
            this.dtTo.TabIndex = 5;
            // 
            // dtFrom
            // 
            this.dtFrom.Location = new System.Drawing.Point(82, 56);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(200, 20);
            this.dtFrom.TabIndex = 3;
            // 
            // txtScriptNameList
            // 
            this.txtScriptNameList.ContextMenuStrip = this.cmsGetScriptList;
            this.txtScriptNameList.Location = new System.Drawing.Point(82, 105);
            this.txtScriptNameList.Multiline = true;
            this.txtScriptNameList.Name = "txtScriptNameList";
            this.txtScriptNameList.Size = new System.Drawing.Size(633, 20);
            this.txtScriptNameList.TabIndex = 10;
            this.txtScriptNameList.MouseHover += new System.EventHandler(this.txtScriptNameList_MouseHover);
            // 
            // cmsGetScriptList
            // 
            this.cmsGetScriptList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGetScrptNameFromFile,
            this.tsmiSaveScriptNameInFile});
            this.cmsGetScriptList.Name = "cmsGetScriptList";
            this.cmsGetScriptList.Size = new System.Drawing.Size(207, 48);
            this.cmsGetScriptList.Opening += new System.ComponentModel.CancelEventHandler(this.cmsGetScriptList_Opening);
            // 
            // tsmiGetScrptNameFromFile
            // 
            this.tsmiGetScrptNameFromFile.Name = "tsmiGetScrptNameFromFile";
            this.tsmiGetScrptNameFromFile.Size = new System.Drawing.Size(206, 22);
            this.tsmiGetScrptNameFromFile.Text = "Get Scrpt Name From File";
            this.tsmiGetScrptNameFromFile.Click += new System.EventHandler(this.tsmiGetScrptNameFromFile_Click);
            // 
            // tsmiSaveScriptNameInFile
            // 
            this.tsmiSaveScriptNameInFile.Name = "tsmiSaveScriptNameInFile";
            this.tsmiSaveScriptNameInFile.Size = new System.Drawing.Size(206, 22);
            this.tsmiSaveScriptNameInFile.Text = "Save Script Name In File";
            this.tsmiSaveScriptNameInFile.Click += new System.EventHandler(this.tsmiSaveScriptNameInFile_Click);
            // 
            // lblScript
            // 
            this.lblScript.AutoSize = true;
            this.lblScript.Location = new System.Drawing.Point(23, 109);
            this.lblScript.Name = "lblScript";
            this.lblScript.Size = new System.Drawing.Size(56, 13);
            this.lblScript.TabIndex = 9;
            this.lblScript.Text = "Script List:";
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(477, 2);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(60, 23);
            this.btnRetrieve.TabIndex = 2;
            this.btnRetrieve.Text = "Retrieve";
            this.ttGenerateScript.SetToolTip(this.btnRetrieve, "Click to Generate Script.");
            this.btnRetrieve.UseVisualStyleBackColor = true;
            this.btnRetrieve.Click += new System.EventHandler(this.btnRetrieve_Click);
            // 
            // grpBoxCriteriaInsertTable
            // 
            this.grpBoxCriteriaInsertTable.Controls.Add(this.btnDeleteRow);
            this.grpBoxCriteriaInsertTable.Controls.Add(this.btnAddRow);
            this.grpBoxCriteriaInsertTable.Controls.Add(this.cmbSearchTable);
            this.grpBoxCriteriaInsertTable.Controls.Add(this.dgvColumnName);
            this.grpBoxCriteriaInsertTable.Controls.Add(this.chkIncludeIdentityColumn);
            this.grpBoxCriteriaInsertTable.Controls.Add(this.chkEntireTable);
            this.grpBoxCriteriaInsertTable.Controls.Add(this.chkSearchByPrimaryKey);
            this.grpBoxCriteriaInsertTable.Controls.Add(this.chkIncludeDelete);
            this.grpBoxCriteriaInsertTable.Controls.Add(this.lblEnterFilterValue);
            this.grpBoxCriteriaInsertTable.Controls.Add(this.dgvSearchValue);
            this.grpBoxCriteriaInsertTable.Controls.Add(this.lblGetColumnName);
            this.grpBoxCriteriaInsertTable.Controls.Add(this.lblGetTableName);
            this.grpBoxCriteriaInsertTable.Location = new System.Drawing.Point(4, 179);
            this.grpBoxCriteriaInsertTable.Name = "grpBoxCriteriaInsertTable";
            this.grpBoxCriteriaInsertTable.Size = new System.Drawing.Size(721, 250);
            this.grpBoxCriteriaInsertTable.TabIndex = 10;
            this.grpBoxCriteriaInsertTable.TabStop = false;
            this.grpBoxCriteriaInsertTable.Text = "Details:";
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Location = new System.Drawing.Point(514, 144);
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteRow.TabIndex = 11;
            this.btnDeleteRow.Text = "Delete Row";
            this.ttGenerateScript.SetToolTip(this.btnDeleteRow, "Click to delete selected Row form Filter String Table.");
            this.btnDeleteRow.UseVisualStyleBackColor = true;
            this.btnDeleteRow.Click += new System.EventHandler(this.btnDeleteRow_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(514, 115);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(75, 23);
            this.btnAddRow.TabIndex = 10;
            this.btnAddRow.Text = "Add Row";
            this.ttGenerateScript.SetToolTip(this.btnAddRow, "Click to add Row in Filter String Table.");
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // cmbSearchTable
            // 
            this.cmbSearchTable.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSearchTable.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSearchTable.FormattingEnabled = true;
            this.cmbSearchTable.Location = new System.Drawing.Point(82, 14);
            this.cmbSearchTable.Name = "cmbSearchTable";
            this.cmbSearchTable.Size = new System.Drawing.Size(426, 21);
            this.cmbSearchTable.TabIndex = 1;
            this.ttGenerateScript.SetToolTip(this.cmbSearchTable, "Select from the Dropdown Option or Type the Script Name to search.");
            this.cmbSearchTable.MouseHover += new System.EventHandler(this.cmbSearchTable_MouseHover);
            this.cmbSearchTable.SelectedIndexChanged += new System.EventHandler(this.cmbSearchTable_SelectedIndexChanged);
            this.cmbSearchTable.Leave += new System.EventHandler(this.cmbSearchTable_Leave);
            this.cmbSearchTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSearchTable_KeyDown);
            // 
            // dgvColumnName
            // 
            this.dgvColumnName.AllowUserToAddRows = false;
            this.dgvColumnName.AllowUserToDeleteRows = false;
            this.dgvColumnName.AllowUserToResizeRows = false;
            this.dgvColumnName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumnName.ContextMenuStrip = this.cmsGetScriptList;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvColumnName.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvColumnName.Location = new System.Drawing.Point(82, 41);
            this.dgvColumnName.Name = "dgvColumnName";
            this.dgvColumnName.RowHeadersVisible = false;
            this.dgvColumnName.RowHeadersWidth = 21;
            this.dgvColumnName.Size = new System.Drawing.Size(426, 66);
            this.dgvColumnName.TabIndex = 4;
            this.ttGenerateScript.SetToolTip(this.dgvColumnName, "Please Check the column by which you want to Filter the data.");
            this.dgvColumnName.MouseHover += new System.EventHandler(this.dgvColumnName_MouseHover);
            this.dgvColumnName.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvColumnName_CurrentCellDirtyStateChanged);
            // 
            // chkIncludeIdentityColumn
            // 
            this.chkIncludeIdentityColumn.AutoSize = true;
            this.chkIncludeIdentityColumn.Location = new System.Drawing.Point(514, 93);
            this.chkIncludeIdentityColumn.Name = "chkIncludeIdentityColumn";
            this.chkIncludeIdentityColumn.Size = new System.Drawing.Size(136, 17);
            this.chkIncludeIdentityColumn.TabIndex = 7;
            this.chkIncludeIdentityColumn.Text = "Include Identity Column";
            this.chkIncludeIdentityColumn.UseVisualStyleBackColor = true;
            // 
            // chkEntireTable
            // 
            this.chkEntireTable.AutoSize = true;
            this.chkEntireTable.Location = new System.Drawing.Point(514, 68);
            this.chkEntireTable.Name = "chkEntireTable";
            this.chkEntireTable.Size = new System.Drawing.Size(202, 17);
            this.chkEntireTable.TabIndex = 6;
            this.chkEntireTable.Text = "Create Script for Entire Table Content";
            this.chkEntireTable.UseVisualStyleBackColor = true;
            this.chkEntireTable.CheckedChanged += new System.EventHandler(this.chkEntireTable_CheckedChanged);
            // 
            // chkSearchByPrimaryKey
            // 
            this.chkSearchByPrimaryKey.AutoSize = true;
            this.chkSearchByPrimaryKey.Location = new System.Drawing.Point(514, 17);
            this.chkSearchByPrimaryKey.Name = "chkSearchByPrimaryKey";
            this.chkSearchByPrimaryKey.Size = new System.Drawing.Size(133, 17);
            this.chkSearchByPrimaryKey.TabIndex = 2;
            this.chkSearchByPrimaryKey.Text = "Search By Primary Key";
            this.chkSearchByPrimaryKey.UseVisualStyleBackColor = true;
            this.chkSearchByPrimaryKey.CheckedChanged += new System.EventHandler(this.chkSearchByPrimaryKey_CheckedChanged);
            // 
            // chkIncludeDelete
            // 
            this.chkIncludeDelete.AutoSize = true;
            this.chkIncludeDelete.Location = new System.Drawing.Point(514, 43);
            this.chkIncludeDelete.Name = "chkIncludeDelete";
            this.chkIncludeDelete.Size = new System.Drawing.Size(95, 17);
            this.chkIncludeDelete.TabIndex = 5;
            this.chkIncludeDelete.Text = "Include Delete";
            this.chkIncludeDelete.UseVisualStyleBackColor = true;
            // 
            // lblEnterFilterValue
            // 
            this.lblEnterFilterValue.AutoSize = true;
            this.lblEnterFilterValue.Location = new System.Drawing.Point(20, 116);
            this.lblEnterFilterValue.Name = "lblEnterFilterValue";
            this.lblEnterFilterValue.Size = new System.Drawing.Size(59, 13);
            this.lblEnterFilterValue.TabIndex = 8;
            this.lblEnterFilterValue.Text = "Filter String";
            // 
            // dgvSearchValue
            // 
            this.dgvSearchValue.AllowUserToAddRows = false;
            this.dgvSearchValue.AllowUserToDeleteRows = false;
            this.dgvSearchValue.AllowUserToResizeRows = false;
            this.dgvSearchValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchValue.Location = new System.Drawing.Point(82, 113);
            this.dgvSearchValue.Name = "dgvSearchValue";
            this.dgvSearchValue.RowHeadersVisible = false;
            this.dgvSearchValue.RowHeadersWidth = 21;
            this.dgvSearchValue.Size = new System.Drawing.Size(426, 130);
            this.dgvSearchValue.TabIndex = 9;
            this.ttGenerateScript.SetToolTip(this.dgvSearchValue, "To add your data filter please click Add Row button.");
            this.dgvSearchValue.MouseHover += new System.EventHandler(this.dgvSearchValue_MouseHover);
            this.dgvSearchValue.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchValue_CellEnter);
            // 
            // lblGetColumnName
            // 
            this.lblGetColumnName.AutoSize = true;
            this.lblGetColumnName.Location = new System.Drawing.Point(3, 44);
            this.lblGetColumnName.Name = "lblGetColumnName";
            this.lblGetColumnName.Size = new System.Drawing.Size(76, 13);
            this.lblGetColumnName.TabIndex = 3;
            this.lblGetColumnName.Text = "Column Name:";
            // 
            // lblGetTableName
            // 
            this.lblGetTableName.AutoSize = true;
            this.lblGetTableName.Location = new System.Drawing.Point(11, 18);
            this.lblGetTableName.Name = "lblGetTableName";
            this.lblGetTableName.Size = new System.Drawing.Size(68, 13);
            this.lblGetTableName.TabIndex = 0;
            this.lblGetTableName.Text = "Table Name:";
            // 
            // grpBoxCriteriaSp
            // 
            this.grpBoxCriteriaSp.Controls.Add(this.cmbTableName);
            this.grpBoxCriteriaSp.Controls.Add(this.chkEntireDataBase);
            this.grpBoxCriteriaSp.Controls.Add(this.lblTableList);
            this.grpBoxCriteriaSp.Controls.Add(this.lblTableName);
            this.grpBoxCriteriaSp.Controls.Add(this.txtTableName);
            this.grpBoxCriteriaSp.Controls.Add(this.txtTableNameList);
            this.grpBoxCriteriaSp.Location = new System.Drawing.Point(5, 431);
            this.grpBoxCriteriaSp.Name = "grpBoxCriteriaSp";
            this.grpBoxCriteriaSp.Size = new System.Drawing.Size(720, 68);
            this.grpBoxCriteriaSp.TabIndex = 11;
            this.grpBoxCriteriaSp.TabStop = false;
            this.grpBoxCriteriaSp.Text = "Details:";
            // 
            // cmbTableName
            // 
            this.cmbTableName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTableName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTableName.FormattingEnabled = true;
            this.cmbTableName.Location = new System.Drawing.Point(81, 16);
            this.cmbTableName.Name = "cmbTableName";
            this.cmbTableName.Size = new System.Drawing.Size(390, 21);
            this.cmbTableName.TabIndex = 1;
            this.ttGenerateScript.SetToolTip(this.cmbTableName, "Select from the Dropdown Option or Type the Script Name to search.");
            this.cmbTableName.MouseHover += new System.EventHandler(this.cmbTableName_MouseHover);
            this.cmbTableName.SelectedIndexChanged += new System.EventHandler(this.cmbTableName_SelectedIndexChanged);
            this.cmbTableName.Leave += new System.EventHandler(this.cmbTableName_Leave);
            this.cmbTableName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTableName_KeyDown);
            // 
            // chkEntireDataBase
            // 
            this.chkEntireDataBase.AutoSize = true;
            this.chkEntireDataBase.Location = new System.Drawing.Point(484, 19);
            this.chkEntireDataBase.Name = "chkEntireDataBase";
            this.chkEntireDataBase.Size = new System.Drawing.Size(233, 17);
            this.chkEntireDataBase.TabIndex = 3;
            this.chkEntireDataBase.Text = "Create SP for all the Tables in the Database";
            this.chkEntireDataBase.UseVisualStyleBackColor = true;
            this.chkEntireDataBase.CheckedChanged += new System.EventHandler(this.chkEntireDataBase_CheckedChanged);
            // 
            // lblTableList
            // 
            this.lblTableList.AutoSize = true;
            this.lblTableList.Location = new System.Drawing.Point(10, 44);
            this.lblTableList.Name = "lblTableList";
            this.lblTableList.Size = new System.Drawing.Size(56, 13);
            this.lblTableList.TabIndex = 4;
            this.lblTableList.Text = "Table List:";
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(10, 19);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(68, 13);
            this.lblTableName.TabIndex = 0;
            this.lblTableName.Text = "Table Name:";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(81, 16);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(390, 20);
            this.txtTableName.TabIndex = 2;
            // 
            // txtTableNameList
            // 
            this.txtTableNameList.ContextMenuStrip = this.cmsGetScriptList;
            this.txtTableNameList.Location = new System.Drawing.Point(81, 40);
            this.txtTableNameList.Multiline = true;
            this.txtTableNameList.Name = "txtTableNameList";
            this.txtTableNameList.Size = new System.Drawing.Size(636, 20);
            this.txtTableNameList.TabIndex = 5;
            this.ttGenerateScript.SetToolTip(this.txtTableNameList, "Right click to get the list of Table from file.");
            this.txtTableNameList.MouseHover += new System.EventHandler(this.txtTableNameList_MouseHover);
            // 
            // btnConfigure
            // 
            this.btnConfigure.Location = new System.Drawing.Point(666, 2);
            this.btnConfigure.Name = "btnConfigure";
            this.btnConfigure.Size = new System.Drawing.Size(60, 23);
            this.btnConfigure.TabIndex = 5;
            this.btnConfigure.Text = "Configure";
            this.ttGenerateScript.SetToolTip(this.btnConfigure, "Click to Configure Database setting.");
            this.btnConfigure.UseVisualStyleBackColor = true;
            this.btnConfigure.Click += new System.EventHandler(this.btnConfigure_Click);
            // 
            // picBoxHelp
            // 
            this.picBoxHelp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxHelp.ErrorImage = ((System.Drawing.Image)(resources.GetObject("picBoxHelp.ErrorImage")));
            this.picBoxHelp.Image = ((System.Drawing.Image)(resources.GetObject("picBoxHelp.Image")));
            this.picBoxHelp.InitialImage = ((System.Drawing.Image)(resources.GetObject("picBoxHelp.InitialImage")));
            this.picBoxHelp.Location = new System.Drawing.Point(699, 27);
            this.picBoxHelp.Name = "picBoxHelp";
            this.picBoxHelp.Size = new System.Drawing.Size(23, 23);
            this.picBoxHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxHelp.TabIndex = 8;
            this.picBoxHelp.TabStop = false;
            this.ttGenerateScript.SetToolTip(this.picBoxHelp, "Help");
            this.picBoxHelp.Click += new System.EventHandler(this.picBoxHelp_Click);
            // 
            // btnInsidEdge
            // 
            this.btnInsidEdge.Location = new System.Drawing.Point(477, 27);
            this.btnInsidEdge.Name = "btnInsidEdge";
            this.btnInsidEdge.Size = new System.Drawing.Size(90, 23);
            this.btnInsidEdge.TabIndex = 6;
            this.btnInsidEdge.Text = "More Options";
            this.ttGenerateScript.SetToolTip(this.btnInsidEdge, "Open Screen for Generating Operation Insert Script.");
            this.btnInsidEdge.UseVisualStyleBackColor = true;
            this.btnInsidEdge.Visible = false;
            this.btnInsidEdge.Click += new System.EventHandler(this.btnInsidEdge_Click);
            // 
            // btnSaveOnRetrive
            // 
            this.btnSaveOnRetrive.Location = new System.Drawing.Point(573, 27);
            this.btnSaveOnRetrive.Name = "btnSaveOnRetrive";
            this.btnSaveOnRetrive.Size = new System.Drawing.Size(120, 23);
            this.btnSaveOnRetrive.TabIndex = 7;
            this.btnSaveOnRetrive.Text = "Save on Retieve";
            this.ttGenerateScript.SetToolTip(this.btnSaveOnRetrive, "Saves script in the default location with the Object Name as File Name.\r\nMultiple" +
                    " object name is given in comma separated format multiple Files with the Object n" +
                    "ame will be saved.\r\n\r\n");
            this.btnSaveOnRetrive.UseVisualStyleBackColor = true;
            this.btnSaveOnRetrive.Click += new System.EventHandler(this.btnSaveOnRetrive_Click);
            // 
            // ScriptGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 580);
            this.Controls.Add(this.btnSaveOnRetrive);
            this.Controls.Add(this.picBoxHelp);
            this.Controls.Add(this.grpBoxCriteriaSp);
            this.Controls.Add(this.btnInsidEdge);
            this.Controls.Add(this.grpBoxCriteriaInsertTable);
            this.Controls.Add(this.grpBoxCriteriaScript);
            this.Controls.Add(this.btnConfigure);
            this.Controls.Add(this.txtDisplayScript);
            this.Controls.Add(this.grpBoxOptions);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRetrieve);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(739, 607);
            this.Name = "ScriptGenerate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Script Generator";
            this.Load += new System.EventHandler(this.ScriptGenerate_Load);
            this.Shown += new System.EventHandler(this.ScriptGenerate_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScriptGenerate_KeyDown);
            this.grpBoxOptions.ResumeLayout(false);
            this.grpBoxOptions.PerformLayout();
            this.grpBoxCriteriaScript.ResumeLayout(false);
            this.grpBoxCriteriaScript.PerformLayout();
            this.grpScriptInsertDeleteorEnter.ResumeLayout(false);
            this.grpScriptInsertDeleteorEnter.PerformLayout();
            this.grpScriptType.ResumeLayout(false);
            this.grpScriptType.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cmsGetScriptList.ResumeLayout(false);
            this.grpBoxCriteriaInsertTable.ResumeLayout(false);
            this.grpBoxCriteriaInsertTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumnName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchValue)).EndInit();
            this.grpBoxCriteriaSp.ResumeLayout(false);
            this.grpBoxCriteriaSp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxHelp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxOptions;
        private System.Windows.Forms.RadioButton rdBtnSpCreate;
        private System.Windows.Forms.RadioButton rdBtnInsert;
        private System.Windows.Forms.RadioButton rdBtnScSpViFn;
        private System.Windows.Forms.RichTextBox txtDisplayScript;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox grpBoxCriteriaScript;
        private System.Windows.Forms.Label lblGetScriptName;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.CheckBox rdFunc;
        private System.Windows.Forms.CheckBox rdView;
        private System.Windows.Forms.CheckBox rdBtnSp;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.TextBox txtScriptNameList;
        private System.Windows.Forms.Button btnRetrieve;
        private System.Windows.Forms.Label lblScript;
        private System.Windows.Forms.GroupBox grpBoxCriteriaInsertTable;
        private System.Windows.Forms.Label lblGetTableName;
        private System.Windows.Forms.Label lblGetColumnName;
        private System.Windows.Forms.Label lblEnterFilterValue;
        private System.Windows.Forms.DataGridView dgvSearchValue;
        private System.Windows.Forms.CheckBox chkIncludeDelete;
        private System.Windows.Forms.CheckBox chkSearchByPrimaryKey;
        private System.Windows.Forms.CheckBox chkEntireTable;
        private System.Windows.Forms.GroupBox grpBoxCriteriaSp;
        private System.Windows.Forms.Label lblTableList;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.TextBox txtTableNameList;
        private System.Windows.Forms.CheckBox chkEntireDataBase;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdBtnModifiedByDate;
        private System.Windows.Forms.RadioButton rdBtnGetListFrmDatabase;
        private System.Windows.Forms.RadioButton rdBtnEnterOwnValue;
        private System.Windows.Forms.ToolTip ttGenerateScript;
        private System.Windows.Forms.ContextMenuStrip cmsGetScriptList;
        private System.Windows.Forms.ToolStripMenuItem tsmiGetScrptNameFromFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveScriptNameInFile;
        private System.Windows.Forms.Button btnConfigure;
        private System.Windows.Forms.CheckBox chkIncludeIdentityColumn;
        private System.Windows.Forms.DataGridView dgvColumnName;
        private System.Windows.Forms.ComboBox cmbSearchTable;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnDeleteRow;
        private System.Windows.Forms.ComboBox cmbTableName;
        private System.Windows.Forms.ComboBox cmbSearchScript;
        //private System.Windows.Forms.HelpProvider helpProvider;
        private System.Windows.Forms.Button btnInsidEdge;
       private System.Windows.Forms.GroupBox grpScriptType;
       private System.Windows.Forms.GroupBox grpScriptInsertDeleteorEnter;
       private System.Windows.Forms.CheckBox chkInsertDeleteOrEnter;
       private System.Windows.Forms.PictureBox picBoxHelp;
        private System.Windows.Forms.Button btnSaveOnRetrive;
    }
}