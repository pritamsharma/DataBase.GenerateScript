#region [ Version Info ]
/*=============================================  
-- Author:      Pritam Sharma  
-- Create date: 14th Dec 2009
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
using System.IO;
using System.Xml;
using System.Collections;

namespace DataBase.GenerateScript
{
    public partial class BaseForm : Form
    {

        #region Global Variables

        /// <summary>
        /// Default path for logging all the error occoured
        /// </summary>
        protected string strErrorLogPath = string.Empty;

        /// <summary>
        /// Default path for saving generated file
        /// </summary>
        protected string strFileSavePath = string.Empty;

        /// <summary>
        /// Default path for saving the and retriving comma seperated script list form which Database Script is to be generated
        /// </summary>
        protected string strSavedScriptPath = string.Empty;

        /// <summary>
        /// Stores the value of the Connection String
        /// </summary>
        protected string strConnectionString = string.Empty;

        #endregion Global Variables

        #region Constructor

        public BaseForm()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region User Defined Methods

        /// <summary>
        /// Logs the error in the mentioned Path
        /// </summary>
        /// <param name="ex"></param>
        protected void LogError(Exception ex)
        {
            MessageBox.Show("Error Occoured please send the log file to the Administrator (" + ex.Message + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.Append(Environment.NewLine);
            sbMessage.Append("**************************************************");
            sbMessage.Append(Environment.NewLine);
            sbMessage.Append("Message: " + ex.Message);
            sbMessage.Append(Environment.NewLine);
            sbMessage.Append("Trace: " + ex.StackTrace);
            sbMessage.Append(Environment.NewLine);
            sbMessage.Append("MachineName: " + Environment.MachineName);
            sbMessage.Append(Environment.NewLine);
            sbMessage.Append("TimeStamp: " + DateTime.Now);
            sbMessage.Append(Environment.NewLine);
            sbMessage.Append("WindowsIdentity: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            sbMessage.Append(Environment.NewLine);

            if (!Directory.Exists(strErrorLogPath))
                Directory.CreateDirectory(strErrorLogPath);
            string strFileName = strErrorLogPath + @"\" + DateTime.Today.ToString("MM-dd-yyyy") + ".txt";
            File.AppendAllText(strFileName, sbMessage.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPath"></param>
        protected void CreateDirecotry(string strPath)
        {
            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        protected string GetDirecotryPath(string strFilePath)
        {
            string strDirecotryPath = strFilePath.Contains("\\") ? strFilePath.Remove(strFilePath.LastIndexOf("\\")) : strFilePath;
            return strDirecotryPath;
        }

        /// <summary>
        /// Removes duplicate from the string array and returns arraylist
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        protected ArrayList RemoveDuplicateFromArray(string[] strParam)
        {
            ArrayList arryRet = new ArrayList();
            if (strParam != null && strParam.Length > 0)
            {
                Array.Sort(strParam);
                for (int i = 0; i < strParam.Length; i++)
                {
                    if (i == 0)
                        arryRet.Add(strParam[0].Replace("\r", string.Empty).Replace("\n", string.Empty));
                    else if (arryRet[arryRet.Count-1].ToString().Trim() != strParam[i].Trim())
                        arryRet.Add(strParam[i].Replace("\r",string.Empty).Replace("\n",string.Empty));
                }
            }
            return arryRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strDisplayText"></param>
        /// <param name="strSaveText"></param>
        protected void SaveFile(string strDisplayText, string strSaveText)
        {
            if (!string.IsNullOrEmpty(strDisplayText))
            {
                if (!Directory.Exists(strFileSavePath))
                {
                    strFileSavePath = ConfigurationManager.AppSettings["DefaultSavePath"];
                    CreateDirecotry(strFileSavePath);
                }

                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.Filter = "SQL File (*.sql)|*.sql";
                svDialog.DefaultExt = ".sql";
                svDialog.FileName = strFileSavePath + @"\save_" + DateTime.Today.ToString("MM-dd-yyyy");
                if (svDialog.ShowDialog() == DialogResult.OK)
                {
                    string strFile = svDialog.FileName;
                    if (File.Exists(strFile))
                        File.Delete(strFile);
                    File.WriteAllText(strFile, strSaveText);
                    if (File.Exists(strFile))
                        MessageBox.Show("File saved successfully.");
                    strFileSavePath = GetDirecotryPath(svDialog.FileName);
                }
            }
        }

        /// <summary>
        /// Opens Folder Browser to select a folder location and return the location
        /// </summary>
        /// <returns></returns>
        protected string DefaultFileSaveFolder()
        {
            string strFilePathReturn = string.Empty;
            if (string.IsNullOrEmpty(strFileSavePath) && !Directory.Exists(strFileSavePath))
            {
                strFileSavePath = ConfigurationManager.AppSettings["DefaultSavePath"];
                CreateDirecotry(strFileSavePath);
            }
            strFilePathReturn = strFileSavePath;
            FolderBrowserDialog objFolderBrowserDialog = new FolderBrowserDialog();
            objFolderBrowserDialog.ShowNewFolderButton = true;
            if (!string.IsNullOrEmpty(strFileSavePath))
                objFolderBrowserDialog.SelectedPath = strFilePathReturn;
            objFolderBrowserDialog.ShowDialog();
            if (!string.IsNullOrEmpty(objFolderBrowserDialog.SelectedPath))
            {
                strFilePathReturn = objFolderBrowserDialog.SelectedPath;
                strFileSavePath = strFilePathReturn;
            }
            return strFilePathReturn;
        }

        /// <summary>
        /// Modifies the config file
        /// </summary>
        /// <param name="strKeyToUpdate"></param>
        /// <param name="strNewValue"></param>
        protected void SaveConfigFile(string strKeyToUpdate, string strNewValue)
        {
            string strFilePath = AppDomain.CurrentDomain.BaseDirectory + @"DataBase.GenerateScript.exe.config";

            if (!File.Exists(strFilePath))
            {
                FileStream flStream = File.Create(strFilePath);
                flStream.Close();
                flStream.Dispose();

                StringBuilder strBuilder = new StringBuilder(string.Empty);
                strBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                strBuilder.AppendLine("<configuration>");
                strBuilder.AppendLine("  <appSettings>");
                strBuilder.AppendLine("    <add key=\"ConnectionString\" value=\"Persist Security Info=False;User ID=InsidEdge;password=Telsource12345;Initial Catalog=InsidEdge;Data Source=DSCP35182\\SQL2005\" />");
                strBuilder.AppendLine("    <add key=\"ErrorPath\" value=\"C:\\DataBase.GenerateScript\\Error\" />");
                strBuilder.AppendLine("    <add key=\"DefaultSavePath\" value=\"C:\\DataBase.GenerateScript\\SavedFile\" />");
                strBuilder.AppendLine("    <add key=\"SavedScriptPath\" value=\"C:\\DataBase.GenerateScript\\List\" />");
                strBuilder.AppendLine("  </appSettings>");
                strBuilder.AppendLine("</configuration>");

                File.WriteAllText(strFilePath, strBuilder.ToString(), Encoding.Default);
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFilePath);
            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes[0].Value.Equals(strKeyToUpdate))
                        {
                            node.Attributes[1].Value = strNewValue;
                            break;
                        }
                    }
                }
            }
            xmlDoc.Save(strFilePath);
            ConfigurationManager.OpenExeConfiguration(strFilePath);
            ConfigurationManager.RefreshSection("appSettings");
            ConfigurationManager.AppSettings[strKeyToUpdate] = strNewValue;
        }

        /// <summary>
        /// Sets the Width of DataGrid View Column to the Maximum Length of Data Contained in the cell
        /// or the column header. Also allows the user to change the Width of the Column
        /// </summary>
        /// <param name="dgvGridView">DataGridView Whose Column Width is to be set</param>
        protected void SetGridColumnWidth(DataGridView dgvGridView, string ColumnName)
        {
            if (ColumnName != null && ColumnName.Length > 0)
            {
                if (dgvGridView.Columns.Contains(ColumnName))
                {
                    dgvGridView.Columns[ColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    int iColWidth = dgvGridView.Columns[ColumnName].Width;
                    dgvGridView.Columns[ColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgvGridView.Columns[ColumnName].Width = iColWidth;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgvGridView"></param>
        protected void SetGridColumnWidth(DataGridView dgvGridView)
        {
            foreach (DataGridViewColumn dgvcColumn in dgvGridView.Columns)
                SetGridColumnWidth(dgvGridView, dgvcColumn.Name);
        }

        /// <summary>
        /// Setting Tab of the controls in the Form
        /// </summary>
        /// <param name="objForm"></param>
        private static void SetTabOrder(Form objForm)
        {
            if (objForm.Controls != null && objForm.Controls.Count > 0)
                SetTabOrder(objForm.Controls);
        }

        /// <summary>
        /// Setting Tab in the controls contained in objControl passed as parameter
        /// </summary>
        /// <param name="objControl"></param>
        private static void SetTabOrder(Control objControl)
        {
            if (objControl.Controls != null && objControl.Controls.Count > 0)
                SetTabOrder((Control.ControlCollection)objControl.Controls);
        }

        /// <summary>
        /// Setting Tab in the controls contained in objControl passed as parameter
        /// </summary>
        /// <param name="objControl"></param>
        private static void SetTabOrder(Control.ControlCollection objControl)
        {
            if (objControl != null && objControl.Count > 0)
            {
                int iCorrectionRange = 2;//Change the value to add an error of +-iCorrectionRange in location Y axis during sorting of control according to location 

                int iControlCount = objControl.Count;
                string[,] strControlName = new string[iControlCount, 4];

                int iTabCount = 0;
                foreach (Control objControlLoc in objControl)
                {
                    strControlName[iTabCount, 0] = objControlLoc.Name;                   //ControlName
                    strControlName[iTabCount, 1] = objControlLoc.Location.X.ToString();  //Location X - Axis
                    strControlName[iTabCount, 2] = objControlLoc.Location.Y.ToString();  //Location Y - Axis
                    strControlName[iTabCount, 3] = iTabCount.ToString();                 //DefaultTabOrder
                    iTabCount++;

                    if (objControlLoc.Controls != null && objControlLoc.Controls.Count > 0)
                        SetTabOrder((Control.ControlCollection)objControlLoc.Controls);
                }

                int iSortedTill = iControlCount - 1;
                for (int j = 0; j < iControlCount - 1; j++)
                {
                    for (int i = 0; i < iSortedTill; i++)
                    {
                        bool bExchange = false;
                        int iFirstControlLoc = Convert.ToInt32(strControlName[i, 2]);
                        int iSecondControlLoc = Convert.ToInt32(strControlName[i + 1, 2]);
                        int iFirstControlRngeStrt = Convert.ToInt32(strControlName[i, 2]) - iCorrectionRange;
                        int iFirstControlRngeEnd = Convert.ToInt32(strControlName[i, 2]) + iCorrectionRange;
                        int iSecondControlRngeStrt = Convert.ToInt32(strControlName[i + 1, 2]) - iCorrectionRange;
                        int iSecondControlRngeEnd = Convert.ToInt32(strControlName[i + 1, 2]) + iCorrectionRange;

                        if (iFirstControlLoc > iSecondControlLoc)
                        {
                            if (iFirstControlRngeStrt > iSecondControlRngeEnd)
                                bExchange = true;
                        }
                        else if (iFirstControlLoc < iSecondControlLoc)
                        {
                            if (iFirstControlRngeEnd > iSecondControlRngeStrt)
                                bExchange = true;
                        }

                        if (bExchange)
                        {
                            string strCntrName = strControlName[i, 0];
                            string strLocationX = strControlName[i, 1];
                            string strLocationY = strControlName[i, 2];

                            strControlName[i, 0] = strControlName[i + 1, 0];
                            strControlName[i, 1] = strControlName[i + 1, 1];
                            strControlName[i, 2] = strControlName[i + 1, 2];

                            strControlName[i + 1, 0] = strCntrName;
                            strControlName[i + 1, 1] = strLocationX;
                            strControlName[i + 1, 2] = strLocationY;
                        }
                    }
                    iSortedTill--;
                }

                iSortedTill = iControlCount - 1;
                for (int j = 0; j < iControlCount - 1; j++)
                {
                    for (int i = 0; i < iSortedTill; i++)
                    {
                        bool bExchange = false;
                        int iFirstControlLoc = Convert.ToInt32(strControlName[i, 2]);
                        int iSecondControlLoc = Convert.ToInt32(strControlName[i + 1, 2]);
                        int iFirstControlRngeStrt = Convert.ToInt32(strControlName[i, 2]) - iCorrectionRange;
                        int iFirstControlRngeEnd = Convert.ToInt32(strControlName[i, 2]) + iCorrectionRange;
                        int iSecondControlRngeStrt = Convert.ToInt32(strControlName[i + 1, 2]) - iCorrectionRange;
                        int iSecondControlRngeEnd = Convert.ToInt32(strControlName[i + 1, 2]) + iCorrectionRange;

                        if (iFirstControlLoc == iSecondControlLoc)
                        {
                            bExchange = true;
                        }
                        else if (iFirstControlLoc > iSecondControlLoc)
                        {
                            if (iFirstControlRngeStrt >= iSecondControlRngeStrt && iFirstControlRngeStrt <= iSecondControlRngeEnd)
                                bExchange = true;
                        }
                        else if (iFirstControlLoc < iSecondControlLoc)
                        {
                            if (iSecondControlRngeStrt >= iFirstControlRngeStrt && iSecondControlRngeStrt <= iFirstControlRngeEnd)
                                bExchange = true;
                        }

                        if (bExchange && Convert.ToInt32(strControlName[i, 1]) > Convert.ToInt32(strControlName[i + 1, 1]))
                        {
                            string strCntrName = strControlName[i, 0];
                            string strLocationX = strControlName[i, 1];
                            string strLocationY = strControlName[i, 2];

                            strControlName[i, 0] = strControlName[i + 1, 0];
                            strControlName[i, 1] = strControlName[i + 1, 1];
                            strControlName[i, 2] = strControlName[i + 1, 2];

                            strControlName[i + 1, 0] = strCntrName;
                            strControlName[i + 1, 1] = strLocationX;
                            strControlName[i + 1, 2] = strLocationY;
                        }
                    }
                    iSortedTill--;
                }

                for (int i = 0; i < iControlCount; i++)
                    if (!string.IsNullOrEmpty(strControlName[i, 0]))
                        objControl[strControlName[i, 0]].TabIndex = Convert.ToInt32(strControlName[i, 3]);
            }
        }

        #endregion User Defined Methods

        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Load(object sender, EventArgs e)
        {
            try
            {
                strErrorLogPath = ConfigurationManager.AppSettings["ErrorPath"];
                strFileSavePath = ConfigurationManager.AppSettings["DefaultSavePath"];
                strSavedScriptPath = ConfigurationManager.AppSettings["SavedScriptPath"];
                strConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                CreateDirecotry(strErrorLogPath);
                CreateDirecotry(strFileSavePath);
                CreateDirecotry(strSavedScriptPath);
            }
            catch (Exception ex)
            {
                strErrorLogPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\DataBase.GenerateScript\Error";
                LogError(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Shown(object sender, EventArgs e)
        {
            try
            {
                SetTabOrder(this);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        #endregion Events

    }
}