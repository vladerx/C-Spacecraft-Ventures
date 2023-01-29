
namespace spacebattle
{
    partial class Game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.mainbox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainbox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainbox
            // 
            this.mainbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(6)))));
            this.mainbox.Location = new System.Drawing.Point(0, 2);
            this.mainbox.Name = "mainbox";
            this.mainbox.Size = new System.Drawing.Size(1448, 873);
            this.mainbox.TabIndex = 12;
            this.mainbox.TabStop = false;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(1636, 876);
            this.Controls.Add(this.mainbox);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MaximizeBox = false;
            this.Name = "Game";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Spacecraft Ventures";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameClose);
            this.Load += new System.EventHandler(this.Game_Start);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IsKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.IsKeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.mainbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mainbox;
    }
}

