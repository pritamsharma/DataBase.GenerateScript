using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.GenerateScript
{
    public class Utility
    {
    }

    /// <summary>
    /// Delegate for Text found Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void TextFoundEventHandler(object sender, TextFoundEventArgs e);

    /// <summary>
    /// Event Argument for passing into Text found event
    /// </summary>
    public class TextFoundEventArgs : EventArgs
    {
        /// <summary>
        /// Starting Index of the Found Text
        /// </summary>
        public int SelectionStartIndex;

        /// <summary>
        /// The selection length of the text found
        /// </summary>
        public int SelectionLength;

        /// <summary>
        /// Event Argument for passing into Text found event
        /// </summary>
        /// <param name="SelectionStartIndex">Starting Index of the Found Text</param>
        /// <param name="SelectionLength">The selection length of the text found</param>
        public TextFoundEventArgs(int SelectionStartIndex, int SelectionLength)
        {
            this.SelectionStartIndex = SelectionStartIndex;
            this.SelectionLength = SelectionLength;
        }
    }
}
