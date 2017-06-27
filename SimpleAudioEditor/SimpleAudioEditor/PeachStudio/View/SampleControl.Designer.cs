﻿namespace SimpleAudioEditor.PeachStudio {
    partial class SampleControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.maskedTextBoxSplitEndTime = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBoxSplitStartTime = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBoxCurrentTime = new System.Windows.Forms.MaskedTextBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.maskedTextBox5 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBoxResultTime = new System.Windows.Forms.MaskedTextBox();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // maskedTextBoxSplitEndTime
            // 
            this.maskedTextBoxSplitEndTime.BackColor = System.Drawing.Color.Black;
            this.maskedTextBoxSplitEndTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxSplitEndTime.ForeColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxSplitEndTime.Location = new System.Drawing.Point(70, 80);
            this.maskedTextBoxSplitEndTime.Mask = "00:00:00.00";
            this.maskedTextBoxSplitEndTime.Name = "maskedTextBoxSplitEndTime";
            this.maskedTextBoxSplitEndTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxSplitEndTime.TabIndex = 16;
            // 
            // maskedTextBoxSplitStartTime
            // 
            this.maskedTextBoxSplitStartTime.BackColor = System.Drawing.Color.Black;
            this.maskedTextBoxSplitStartTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxSplitStartTime.ForeColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxSplitStartTime.Location = new System.Drawing.Point(145, 80);
            this.maskedTextBoxSplitStartTime.Mask = "00:00:00.00";
            this.maskedTextBoxSplitStartTime.Name = "maskedTextBoxSplitStartTime";
            this.maskedTextBoxSplitStartTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxSplitStartTime.TabIndex = 15;
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.BackColor = System.Drawing.Color.Black;
            this.maskedTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBox2.ForeColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBox2.Location = new System.Drawing.Point(215, 80);
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(6, 13);
            this.maskedTextBox2.TabIndex = 14;
            this.maskedTextBox2.Text = "=";
            this.maskedTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // maskedTextBoxCurrentTime
            // 
            this.maskedTextBoxCurrentTime.BackColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxCurrentTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxCurrentTime.Location = new System.Drawing.Point(0, 80);
            this.maskedTextBoxCurrentTime.Mask = "00:00:00.00";
            this.maskedTextBoxCurrentTime.Name = "maskedTextBoxCurrentTime";
            this.maskedTextBoxCurrentTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxCurrentTime.TabIndex = 13;
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.Color.DarkOrange;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.Location = new System.Drawing.Point(0, 40);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(40, 40);
            this.buttonStop.TabIndex = 11;
            this.buttonStop.Text = "■";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackColor = System.Drawing.Color.DarkOrange;
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.buttonPlay.Location = new System.Drawing.Point(0, 0);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(40, 40);
            this.buttonPlay.TabIndex = 10;
            this.buttonPlay.Text = ">";
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // maskedTextBox5
            // 
            this.maskedTextBox5.BackColor = System.Drawing.Color.Black;
            this.maskedTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBox5.ForeColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBox5.Location = new System.Drawing.Point(140, 80);
            this.maskedTextBox5.Name = "maskedTextBox5";
            this.maskedTextBox5.Size = new System.Drawing.Size(5, 13);
            this.maskedTextBox5.TabIndex = 17;
            this.maskedTextBox5.Text = "-";
            this.maskedTextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // maskedTextBoxResultTime
            // 
            this.maskedTextBoxResultTime.BackColor = System.Drawing.Color.Black;
            this.maskedTextBoxResultTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxResultTime.ForeColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxResultTime.Location = new System.Drawing.Point(221, 80);
            this.maskedTextBoxResultTime.Mask = "00:00:00.00";
            this.maskedTextBoxResultTime.Name = "maskedTextBoxResultTime";
            this.maskedTextBoxResultTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxResultTime.TabIndex = 18;
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar.LargeChange = 2;
            this.hScrollBar.Location = new System.Drawing.Point(294, 80);
            this.hScrollBar.Maximum = 1;
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(383, 13);
            this.hScrollBar.TabIndex = 19;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(40, 0);
            this.pictureBox.MinimumSize = new System.Drawing.Size(620, 80);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(640, 80);
            this.pictureBox.TabIndex = 12;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.Layout += new System.Windows.Forms.LayoutEventHandler(this.pictureBox_Layout);
            // 
            // SampleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.maskedTextBoxResultTime);
            this.Controls.Add(this.maskedTextBox5);
            this.Controls.Add(this.maskedTextBoxSplitEndTime);
            this.Controls.Add(this.maskedTextBoxSplitStartTime);
            this.Controls.Add(this.maskedTextBox2);
            this.Controls.Add(this.maskedTextBoxCurrentTime);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPlay);
            this.MinimumSize = new System.Drawing.Size(680, 93);
            this.Name = "SampleControl";
            this.Size = new System.Drawing.Size(680, 93);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox maskedTextBoxSplitEndTime;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxSplitStartTime;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxCurrentTime;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.MaskedTextBox maskedTextBox5;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxResultTime;
        private System.Windows.Forms.HScrollBar hScrollBar;
    }
}