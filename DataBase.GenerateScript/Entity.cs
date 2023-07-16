#region [ Version Info ]
/*=============================================  
-- Author:      Pritam Sharma  
-- Create date: 29th May 2009
-- Description: Generating Script
-- ============================================= */
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.GenerateScript
{

   [Serializable]
   public class Entity
   {

      private string _Text;
      public string Text
      {
         get { return (_Text == null ? string.Empty : _Text); }
         set { _Text = value; }
      }

   }
}
