namespace DataBase.GenerateScript
{
    partial class Configure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configure));
            this.lblPersistSecurityInfo = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblInitialCatalog = new System.Windows.Forms.Label();
            this.lblDataSource = new System.Windows.Forms.Label();
            this.pnlPersistantSecurityInfo = new System.Windows.Forms.Panel();
            this.rdBtnFalse = new System.Windows.Forms.RadioButton();
            this.rdBtnTrue = new System.Windows.Forms.RadioButton();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtInitialCatalog = new System.Windows.Forms.TextBox();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.btnCheckConnection = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblConnectionCheck = new System.Windows.Forms.Label();
            this.pnlDetails = new System.Windows.Forms.GroupBox();
            this.pnlEntireString = new System.Windows.Forms.GroupBox();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.grpBoxConnectionType = new System.Windows.Forms.GroupBox();
            this.rdBtnConnectionString = new System.Windows.Forms.RadioButton();
            this.rdBtnDetailed = new System.Windows.Forms.RadioButton();
            this.grpBoxAuthenticationType = new System.Windows.Forms.GroupBox();
            this.rdBtnSqlServer = new System.Windows.Forms.RadioButton();
            this.rdBtnWindow = new System.Windows.Forms.RadioButton();
            this.pnlPersistantSecurityInfo.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.pnlEntireString.SuspendLayout();
            this.grpBoxConnectionType.SuspendLayout();
            this.grpBoxAuthenticationType.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPersistSecurityInfo
            // 
            this.lblPersistSecurityInfo.AutoSize = true;
            this.lblPersistSecurityInfo.Location = new System.Drawing.Point(4, 17);
            this.lblPersistSecurityInfo.Name = "lblPersistSecurityInfo";
            this.lblPersistSecurityInfo.Size = new System.Drawing.Size(103, 13);
            this.lblPersistSecurityInfo.TabIndex = 0;
            this.lblPersistSecurityInfo.Text = "Persist Security Info:";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Location = new System.Drawing.Point(61, 42);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(46, 13);
            this.lblUserID.TabIndex = 2;
            this.lblUserID.Text = "User ID:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(51, 69);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            // 
            // lblInitialCatalog
            // 
            this.lblInitialCatalog.AutoSize = true;
            this.lblInitialCatalog.Location = new System.Drawing.Point(34, 95);
            this.lblInitialCatalog.Name = "lblInitialCatalog";
            this.lblInitialCatalog.Size = new System.Drawing.Size(73, 13);
            this.lblInitialCatalog.TabIndex = 6;
            this.lblInitialCatalog.Text = "Initial Catalog:";
            // 
            // lblDataSource
            // 
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.Location = new System.Drawing.Point(37, 121);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(70, 13);
            this.lblDataSource.TabIndex = 8;
            this.lblDataSource.Text = "Data Source:";
            // 
            // pnlPersistantSecurityInfo
            // 
            this.pnlPersistantSecurityInfo.Controls.Add(this.rdBtnFalse);
            this.pnlPersistantSecurityInfo.Controls.Add(this.rdBtnTrue);
            this.pnlPersistantSecurityInfo.Location = new System.Drawing.Point(109, 14);
            this.pnlPersistantSecurityInfo.Name = "pnlPersistantSecurityInfo";
            this.pnlPersistantSecurityInfo.Size = new System.Drawing.Size(112, 20);
            this.pnlPersistantSecurityInfo.TabIndex = 1;
            // 
            // rdBtnFalse
            // 
            this.rdBtnFalse.AutoSize = true;
            this.rdBtnFalse.Location = new System.Drawing.Point(56, 1);
            this.rdBtnFalse.Name = "rdBtnFalse";
            this.rdBtnFalse.Size = new System.Drawing.Size(50, 17);
            this.rdBtnFalse.TabIndex = 1;
            this.rdBtnFalse.TabStop = true;
            this.rdBtnFalse.Text = "False";
            this.rdBtnFalse.UseVisualStyleBackColor = true;
            // 
            // rdBtnTrue
            // 
            this.rdBtnTrue.AutoSize = true;
            this.rdBtnTrue.Location = new System.Drawing.Point(3, 1);
            this.rdBtnTrue.Name = "rdBtnTrue";
            this.rdBtnTrue.Size = new System.Drawing.Size(47, 17);
            this.rdBtnTrue.TabIndex = 0;
            this.rdBtnTrue.TabStop = true;
            this.rdBtnTrue.Text = "True";
            this.rdBtnTrue.UseVisualStyleBackColor = true;
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(109, 39);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(297, 20);
            this.txtUserId.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(109, 66);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(297, 20);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtInitialCatalog
            // 
            this.txtInitialCatalog.Location = new System.Drawing.Point(109, 92);
            this.txtInitialCatalog.Name = "txtInitialCatalog";
            this.txtInitialCatalog.Size = new System.Drawing.Size(297, 20);
            this.txtInitialCatalog.TabIndex = 7;
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(109, 118);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(297, 20);
            this.txtDataSource.TabIndex = 9;
            // 
            // btnCheckConnection
            // 
            this.btnCheckConnection.Location = new System.Drawing.Point(113, 246);
            this.btnCheckConnection.Name = "btnCheckConnection";
            this.btnCheckConnection.Size = new System.Drawing.Size(111, 23);
            this.btnCheckConnection.TabIndex = 4;
            this.btnCheckConnection.Text = "Check Connection";
            this.btnCheckConnection.UseVisualStyleBackColor = true;
            this.btnCheckConnection.Click += new System.EventHandler(this.btnCheckConnection_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(230, 246);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblConnectionCheck
            // 
            this.lblConnectionCheck.AutoSize = true;
            this.lblConnectionCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionCheck.Location = new System.Drawing.Point(203, 12);
            this.lblConnectionCheck.Name = "lblConnectionCheck";
            this.lblConnectionCheck.Size = new System.Drawing.Size(12, 16);
            this.lblConnectionCheck.TabIndex = 2;
            this.lblConnectionCheck.Text = " ";
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.lblPersistSecurityInfo);
            this.pnlDetails.Controls.Add(this.lblUserID);
            this.pnlDetails.Controls.Add(this.lblPassword);
            this.pnlDetails.Controls.Add(this.lblInitialCatalog);
            this.pnlDetails.Controls.Add(this.txtDataSource);
            this.pnlDetails.Controls.Add(this.lblDataSource);
            this.pnlDetails.Controls.Add(this.txtInitialCatalog);
            this.pnlDetails.Controls.Add(this.pnlPersistantSecurityInfo);
            this.pnlDetails.Controls.Add(this.txtPassword);
            this.pnlDetails.Controls.Add(this.txtUserId);
            this.pnlDetails.Location = new System.Drawing.Point(3, 96);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(412, 145);
            this.pnlDetails.TabIndex = 3;
            this.pnlDetails.TabStop = false;
            this.pnlDetails.Text = "Connection Detail";
            // 
            // pnlEntireString
            // 
            this.pnlEntireString.Controls.Add(this.txtConnectionString);
            this.pnlEntireString.Location = new System.Drawing.Point(3, 96);
            this.pnlEntireString.Name = "pnlEntireString";
            this.pnlEntireString.Size = new System.Drawing.Size(412, 145);
            this.pnlEntireString.TabIndex = 2;
            this.pnlEntireString.TabStop = false;
            this.pnlEntireString.Text = "Connection String";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(8, 19);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(398, 115);
            this.txtConnectionString.TabIndex = 0;
            // 
            // grpBoxConnectionType
            // 
            this.grpBoxConnectionType.Controls.Add(this.rdBtnConnectionString);
            this.grpBoxConnectionType.Controls.Add(this.lblConnectionCheck);
            this.grpBoxConnectionType.Controls.Add(this.rdBtnDetailed);
            this.grpBoxConnectionType.Location = new System.Drawing.Point(3, 45);
            this.grpBoxConnectionType.Name = "grpBoxConnectionType";
            this.grpBoxConnectionType.Size = new System.Drawing.Size(412, 38);
            this.grpBoxConnectionType.TabIndex = 1;
            this.grpBoxConnectionType.TabStop = false;
            this.grpBoxConnectionType.Text = "View Type:";
            // 
            // rdBtnConnectionString
            // 
            this.rdBtnConnectionString.AutoSize = true;
            this.rdBtnConnectionString.Location = new System.Drawing.Point(80, 15);
            this.rdBtnConnectionString.Name = "rdBtnConnectionString";
            this.rdBtnConnectionString.Size = new System.Drawing.Size(109, 17);
            this.rdBtnConnectionString.TabIndex = 1;
            this.rdBtnConnectionString.TabStop = true;
            this.rdBtnConnectionString.Text = "Connection String";
            this.rdBtnConnectionString.UseVisualStyleBackColor = true;
            this.rdBtnConnectionString.CheckedChanged += new System.EventHandler(this.rdBtnConnectionString_CheckedChanged);
            // 
            // rdBtnDetailed
            // 
            this.rdBtnDetailed.AutoSize = true;
            this.rdBtnDetailed.Location = new System.Drawing.Point(8, 16);
            this.rdBtnDetailed.Name = "rdBtnDetailed";
            this.rdBtnDetailed.Size = new System.Drawing.Size(64, 17);
            this.rdBtnDetailed.TabIndex = 0;
            this.rdBtnDetailed.TabStop = true;
            this.rdBtnDetailed.Text = "Detailed";
            this.rdBtnDetailed.UseVisualStyleBackColor = true;
            this.rdBtnDetailed.CheckedChanged += new System.EventHandler(this.rdBtnDetailed_CheckedChanged);
            // 
            // grpBoxAuthenticationType
            // 
            this.grpBoxAuthenticationType.Controls.Add(this.rdBtnSqlServer);
            this.grpBoxAuthenticationType.Controls.Add(this.rdBtnWindow);
            this.grpBoxAuthenticationType.Location = new System.Drawing.Point(3, 0);
            this.grpBoxAuthenticationType.Name = "grpBoxAuthenticationType";
            this.grpBoxAuthenticationType.Size = new System.Drawing.Size(412, 39);
            this.grpBoxAuthenticationType.TabIndex = 0;
            this.grpBoxAuthenticationType.TabStop = false;
            this.grpBoxAuthenticationType.Text = "Authentication Type";
            // 
            // rdBtnSqlServer
            // 
            this.rdBtnSqlServer.AutoSize = true;
            this.rdBtnSqlServer.Location = new System.Drawing.Point(6, 16);
            this.rdBtnSqlServer.Name = "rdBtnSqlServer";
            this.rdBtnSqlServer.Size = new System.Drawing.Size(74, 17);
            this.rdBtnSqlServer.TabIndex = 0;
            this.rdBtnSqlServer.TabStop = true;
            this.rdBtnSqlServer.Text = "Sql Server";
            this.rdBtnSqlServer.UseVisualStyleBackColor = true;
            this.rdBtnSqlServer.CheckedChanged += new System.EventHandler(this.rdBtnSqlServer_CheckedChanged);
            // 
            // rdBtnWindow
            // 
            this.rdBtnWindow.AutoSize = true;
            this.rdBtnWindow.Location = new System.Drawing.Point(80, 16);
            this.rdBtnWindow.Name = "rdBtnWindow";
            this.rdBtnWindow.Size = new System.Drawing.Size(69, 17);
            this.rdBtnWindow.TabIndex = 1;
            this.rdBtnWindow.TabStop = true;
            this.rdBtnWindow.Text = "Windows";
            this.rdBtnWindow.UseVisualStyleBackColor = true;
            this.rdBtnWindow.CheckedChanged += new System.EventHandler(this.rdBtnWindow_CheckedChanged);
            // 
            // Configure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 274);
            this.Controls.Add(this.grpBoxAuthenticationType);
            this.Controls.Add(this.grpBoxConnectionType);
            this.Controls.Add(this.pnlEntireString);
            this.Controls.Add(this.pnlDetails);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCheckConnection);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Configure";
            this.Text = "Configure";
            this.Shown += new System.EventHandler(this.Configure_Shown);
            this.pnlPersistantSecurityInfo.ResumeLayout(false);
            this.pnlPersistantSecurityInfo.PerformLayout();
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.pnlEntireString.ResumeLayout(false);
            this.pnlEntireString.PerformLayout();
            this.grpBoxConnectionType.ResumeLayout(false);
            this.grpBoxConnectionType.PerformLayout();
            this.grpBoxAuthenticationType.ResumeLayout(false);
            this.grpBoxAuthenticationType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPersistSecurityInfo;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblInitialCatalog;
        private System.Windows.Forms.Label lblDataSource;
        private System.Windows.Forms.Panel pnlPersistantSecurityInfo;
        private System.Windows.Forms.RadioButton rdBtnFalse;
        private System.Windows.Forms.RadioButton rdBtnTrue;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtInitialCatalog;
        private System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.Button btnCheckConnection;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblConnectionCheck;
        private System.Windows.Forms.GroupBox pnlDetails;
        private System.Windows.Forms.GroupBox pnlEntireString;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.GroupBox grpBoxConnectionType;
        private System.Windows.Forms.RadioButton rdBtnConnectionString;
        private System.Windows.Forms.RadioButton rdBtnDetailed;
        private System.Windows.Forms.GroupBox grpBoxAuthenticationType;
        private System.Windows.Forms.RadioButton rdBtnSqlServer;
        private System.Windows.Forms.RadioButton rdBtnWindow;
    }
}