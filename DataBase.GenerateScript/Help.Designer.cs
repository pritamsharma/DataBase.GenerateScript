namespace DataBase.GenerateScript
{
   partial class Help
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
         this.txtHelp = new System.Windows.Forms.RichTextBox();
         this.SuspendLayout();
         // 
         // txtHelp
         // 
         this.txtHelp.Dock = System.Windows.Forms.DockStyle.Fill;
         this.txtHelp.Location = new System.Drawing.Point(0, 0);
         this.txtHelp.Name = "txtHelp";
         this.txtHelp.Size = new System.Drawing.Size(490, 401);
         this.txtHelp.TabIndex = 0;
         this.txtHelp.Text = "";
         // 
         // Help
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(490, 401);
         this.Controls.Add(this.txtHelp);
         this.MaximizeBox = false;
         this.Name = "Help";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Help";
         this.Load += new System.EventHandler(this.Help_Load);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.RichTextBox txtHelp;
   }
}