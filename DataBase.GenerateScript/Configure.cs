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
using System.Configuration;

namespace DataBase.GenerateScript
{

    /// <summary>
    /// For configuring the connection string of the database
    /// </summary>
    public partial class Configure : BaseForm
    {

        #region Global Variables

        /// <summary>
        /// This event is fired when Ok button of Configure is pressed
        /// </summary>
        public EventHandler OkPress;

        /// <summary>
        /// 
        /// </summary>
        public bool ValidationSuccess;// = false;

        /// <summary>
        /// 
        /// </summary>
        public bool DontAskResetQuestion;// = false;

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public Configure()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region User Defined Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strConnection"></param>
        /// <returns></returns>
        private bool CheckConnection(string strConnection)
        {
            bool bReturn = false;
            if ((new FetchData(strConnection)).ValidateConnectionString(strConnection))
            {
                lblConnectionCheck.ForeColor = Color.Black;
                lblConnectionCheck.Text = "Valid Connection";
                bReturn = true;
            }
            else
            {
                lblConnectionCheck.ForeColor = Color.Red;
                lblConnectionCheck.Text = "Invalid Connection";
                bReturn = false;
            }
            return bReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ScreenFormat()
        {
            try
            {
                if (rdBtnDetailed.Checked)
                {
                    grpBoxAuthenticationType.Enabled = true;
                    pnlDetails.Visible = true;
                    pnlEntireString.Visible = false;
                    DisplayConnectionStringToScreen(false);
                }
                else
                {
                    grpBoxAuthenticationType.Enabled = false;
                    pnlDetails.Visible = false;
                    pnlEntireString.Visible = true;
                    DisplayConnectionStringToScreen(false);
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
        /// <param name="LoadFromAppSetting"></param>
        private void DisplayConnectionStringToScreen(bool LoadFromAppSetting)
        {
            string strConfigString = string.Empty;

            if (LoadFromAppSetting)
                strConfigString = ConfigurationManager.AppSettings["ConnectionString"];
            else
                strConfigString = GetUserEnteredConnectionString(rdBtnConnectionString.Checked);

            if (rdBtnDetailed.Checked)
            {
                if (strConfigString != null && !String.IsNullOrEmpty(strConfigString))
                {
                    string[] strSplitLvl1 = strConfigString.Split(new char[] { ';' });
                    if (strSplitLvl1 != null && strSplitLvl1.Length > 0)
                    {
                        foreach (string strIndivi in strSplitLvl1)
                        {
                            string[] strSplitLvl2 = strIndivi.Split(new char[] { '=' });
                            if (strSplitLvl2 != null && strSplitLvl2.Length > 0)
                            {
                                switch (strSplitLvl2[0].ToUpper().Trim())
                                {
                                    case "PERSIST SECURITY INFO":
                                        if (strSplitLvl2.Length > 1)
                                        {
                                            if (strSplitLvl2[1].ToUpper().Trim() == "TRUE")
                                                rdBtnTrue.Checked = true;
                                            else
                                                rdBtnFalse.Checked = true;
                                        }
                                        else
                                            rdBtnFalse.Checked = false;
                                        break;
                                    case "USER ID":
                                        if (rdBtnSqlServer.Checked)
                                        {
                                            if (strSplitLvl2.Length > 1)
                                                txtUserId.Text = strSplitLvl2[1];
                                            else
                                                txtUserId.Text = string.Empty;
                                        }
                                        else
                                        {
                                            txtUserId.Clear();
                                            txtPassword.Clear();
                                        }
                                        break;
                                    case "PASSWORD":
                                        if (rdBtnSqlServer.Checked)
                                        {
                                            if (strSplitLvl2.Length > 1)
                                                txtPassword.Text = strSplitLvl2[1];
                                            else
                                                txtPassword.Text = string.Empty;
                                        }
                                        else
                                        {
                                            txtUserId.Clear();
                                            txtPassword.Clear();
                                        }
                                        break;
                                    case "INITIAL CATALOG":
                                        if (strSplitLvl2.Length > 1)
                                            txtInitialCatalog.Text = strSplitLvl2[1];
                                        else
                                            txtInitialCatalog.Text = string.Empty;
                                        break;
                                    case "DATA SOURCE":
                                        if (strSplitLvl2.Length > 1)
                                            txtDataSource.Text = strSplitLvl2[1];
                                        else
                                            txtDataSource.Text = string.Empty;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                txtConnectionString.Text = strConfigString;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetUserEnteredConnectionString(bool FetchFromDetail)
        {
            string strConnectionString = string.Empty;
            if (FetchFromDetail)
            {
                if (rdBtnSqlServer.Checked)
                {
                    strConnectionString = @"Persist Security Info=" + rdBtnTrue.Checked.ToString() +
                                     @";User ID=" + txtUserId.Text +
                                     @";password=" + txtPassword.Text +
                                     @";Initial Catalog=" + txtInitialCatalog.Text +
                                     @";Data Source=" + txtDataSource.Text;
                }
                else
                {
                    strConnectionString = @"Persist Security Info=" + rdBtnTrue.Checked.ToString() +
                                     //@";User ID=" + txtUserId.Text +
                                     //@";password=" + txtPassword.Text +
                                     @";Initial Catalog=" + txtInitialCatalog.Text +
                                     @";Data Source=" + txtDataSource.Text;
                }
            }
            else
            {
                strConnectionString = txtConnectionString.Text;
            }
            return strConnectionString;
        }

        #endregion User Defined Methods

        #region Events

        /// <summary>
        /// Button check connection click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckConnection_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string strConnectionString = GetUserEnteredConnectionString(rdBtnDetailed.Checked);
                CheckConnection(strConnectionString);
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
        /// Configure form shown Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Configure_Shown(object sender, EventArgs e)
        {
            try
            {
                rdBtnSqlServer.Focus();
                rdBtnDetailed.Focus();
                DisplayConnectionStringToScreen(true);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        /// <summary>
        /// Click event of Ok button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string strConnectionString = GetUserEnteredConnectionString(rdBtnDetailed.Checked);
                if (CheckConnection(strConnectionString))
                {
                    if (ConfigurationManager.AppSettings["ConnectionString"].Trim().Replace(" ", string.Empty).ToUpper() !=
                        strConnectionString.Trim().Replace(" ", string.Empty).ToUpper())
                    {
                        if (DontAskResetQuestion || MessageBox.Show("Form will be Reset. Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (OkPress != null)
                                OkPress(strConnectionString, new EventArgs());
                            this.ValidationSuccess = true;
                            this.Close();
                        }
                    }
                    else
                    {
                        this.ValidationSuccess = true;
                        this.Close();
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
        private void rdBtnDetailed_CheckedChanged(object sender, EventArgs e)
        {
            ScreenFormat();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdBtnConnectionString_CheckedChanged(object sender, EventArgs e)
        {
            ScreenFormat();
        }

        private void AuthenticationType()
        {
            try
            {
                if (rdBtnSqlServer.Checked)
                {
                    txtUserId.Enabled = true;
                    txtPassword.Enabled = true;
                    lblUserID.Enabled = true;
                    lblPassword.Enabled = true;
                }
                else
                {
                    txtUserId.Enabled = false;
                    txtPassword.Enabled = false;
                    lblUserID.Enabled = false;
                    lblPassword.Enabled = false;
                    txtUserId.Clear();
                    txtPassword.Clear();
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void rdBtnWindow_CheckedChanged(object sender, EventArgs e)
        {
            AuthenticationType();
        }

        private void rdBtnSqlServer_CheckedChanged(object sender, EventArgs e)
        {
            AuthenticationType();
        }

        #endregion Events

    }
}