namespace CG_Laba_6
{
    partial class Main_Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.draw_pictureBox = new System.Windows.Forms.PictureBox();
            this.nextStep_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.draw_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // draw_pictureBox
            // 
            this.draw_pictureBox.Location = new System.Drawing.Point(-1, 0);
            this.draw_pictureBox.Name = "draw_pictureBox";
            this.draw_pictureBox.Size = new System.Drawing.Size(630, 630);
            this.draw_pictureBox.TabIndex = 0;
            this.draw_pictureBox.TabStop = false;
            // 
            // nextStep_button
            // 
            this.nextStep_button.Location = new System.Drawing.Point(763, 488);
            this.nextStep_button.Name = "nextStep_button";
            this.nextStep_button.Size = new System.Drawing.Size(136, 62);
            this.nextStep_button.TabIndex = 1;
            this.nextStep_button.Text = "Следующий шаг";
            this.nextStep_button.UseVisualStyleBackColor = true;
            this.nextStep_button.Click += new System.EventHandler(this.nextStep_button_Click);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 635);
            this.Controls.Add(this.nextStep_button);
            this.Controls.Add(this.draw_pictureBox);
            this.Name = "Main_Form";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.draw_pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox draw_pictureBox;
        private Button nextStep_button;
    }
}