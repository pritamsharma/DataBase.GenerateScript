/*=============================================  
-- Author:      Pritam Sharma  
-- Create date: 12th Dec 2009
-- Description: Generating Script
-- ============================================= */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataBase.GenerateScript
{
   public partial class Help : BaseForm
   {
      /// <summary>
      /// Indicates the type for which to display the helptext
      /// </summary>
      public enum HelpType
      {
         /// <summary>
         /// Helptext for Create new SP templete
         /// </summary>
         CreateSpTemplete,
         /// <summary>
         /// Helptext for generating script for existing SP, View and Function
         /// </summary>
         GenerateScript,
         /// <summary>
         /// Helptext for creating Insert script for data contained in table
         /// </summary>
         CreateInsertScriptForTable
      }

      /// <summary>
      /// Helptext type selected
      /// </summary>
      private HelpType HelpName;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="HelpResourceName">Indicates the type for which to display the helptext</param>
      public Help(HelpType HelpResourceName)
      {
         InitializeComponent();
         HelpName = HelpResourceName;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void Help_Load(object sender, EventArgs e)
      {
         try
         {
            txtHelp.ReadOnly = true;
            string strDisplayText = string.Empty;
            switch (HelpName)
            {
               case HelpType.CreateInsertScriptForTable:
                  this.Text = this.Text + " (Insert for Table)";
                  strDisplayText = DataBase.GenerateScript.Properties.Resources.CreateInsertScriptForTable;
                  break;
               case HelpType.CreateSpTemplete:
                  this.Text = this.Text + " (Create Stored Procedure)";
                  strDisplayText = DataBase.GenerateScript.Properties.Resources.CreateSpTemplete;
                  break;
               case HelpType.GenerateScript:
                  this.Text = this.Text + " (Stored Procedure, Function and Views)";
                  strDisplayText = DataBase.GenerateScript.Properties.Resources.GenerateScript;
                  break;
            }
            txtHelp.Text = strDisplayText;
         }
         catch (Exception ex)
         {
            LogError(ex);
         }
      }

   }
}