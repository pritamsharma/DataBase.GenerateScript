using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataBase.GenerateScript
{
    public partial class Find : Form
    {
        /// <summary>
        /// This Event is fired if the Text being searched is found
        /// </summary>
        public TextFoundEventHandler TextFound;

        /// <summary>
        /// Control in which text is to be searched
        /// </summary>
        private Control ControlToBeSearched;      

        public Find(Control ControlToBeSearched)
        {
            InitializeComponent();
            this.ControlToBeSearched = ControlToBeSearched;           
        }

        private void Find_Load(object sender, EventArgs e)
        {

        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            RichTextBox txtTextToSearch = (RichTextBox)ControlToBeSearched;
            //txtTextToSearch.Select(10000, 5);
            TextFound(ControlToBeSearched, new TextFoundEventArgs(0,0));            
            txtTextToSearch.Find("item", RichTextBoxFinds.MatchCase);
        }
    }
}