﻿namespace SimpleAudioEditor.PeachStudio {
    partial class ProjectControl {
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
            this.pbWaveViewer = new System.Windows.Forms.PictureBox();
            this.bPlayPause = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.maskedTextBoxCurrentTime = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbWaveViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // pbWaveViewer
            // 
            this.pbWaveViewer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbWaveViewer.BackColor = System.Drawing.Color.Transparent;
            this.pbWaveViewer.Location = new System.Drawing.Point(40, 0);
            this.pbWaveViewer.Name = "pbWaveViewer";
            this.pbWaveViewer.Size = new System.Drawing.Size(620, 80);
            this.pbWaveViewer.TabIndex = 0;
            this.pbWaveViewer.TabStop = false;
            this.pbWaveViewer.Paint += new System.Windows.Forms.PaintEventHandler(this.pbWaveViewer_Paint);
            this.pbWaveViewer.Layout += new System.Windows.Forms.LayoutEventHandler(this.pbWaveViewer_Layout);
            // 
            // bPlayPause
            // 
            this.bPlayPause.BackColor = System.Drawing.Color.DarkOrange;
            this.bPlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPlayPause.Location = new System.Drawing.Point(0, 0);
            this.bPlayPause.Name = "bPlayPause";
            this.bPlayPause.Size = new System.Drawing.Size(40, 40);
            this.bPlayPause.TabIndex = 1;
            this.bPlayPause.Text = ">";
            this.bPlayPause.UseVisualStyleBackColor = false;
            this.bPlayPause.Click += new System.EventHandler(this.bPlayPause_Click);
            // 
            // bStop
            // 
            this.bStop.BackColor = System.Drawing.Color.DarkOrange;
            this.bStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStop.Location = new System.Drawing.Point(0, 40);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(40, 40);
            this.bStop.TabIndex = 2;
            this.bStop.Text = "■";
            this.bStop.UseVisualStyleBackColor = false;
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.BackColor = System.Drawing.Color.DarkOrange;
            this.bSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSave.Location = new System.Drawing.Point(660, 0);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(40, 40);
            this.bSave.TabIndex = 3;
            this.bSave.Text = "button3";
            this.bSave.UseVisualStyleBackColor = false;
            // 
            // bDelete
            // 
            this.bDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bDelete.BackColor = System.Drawing.Color.DarkOrange;
            this.bDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDelete.Location = new System.Drawing.Point(660, 40);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(40, 40);
            this.bDelete.TabIndex = 4;
            this.bDelete.Text = "button4";
            this.bDelete.UseVisualStyleBackColor = false;
            // 
            // maskedTextBoxCurrentTime
            // 
            this.maskedTextBoxCurrentTime.BackColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxCurrentTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxCurrentTime.Location = new System.Drawing.Point(40, 67);
            this.maskedTextBoxCurrentTime.Mask = "00:00:00.00";
            this.maskedTextBoxCurrentTime.Name = "maskedTextBoxCurrentTime";
            this.maskedTextBoxCurrentTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxCurrentTime.TabIndex = 14;
            // 
            // ProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.maskedTextBoxCurrentTime);
            this.Controls.Add(this.pbWaveViewer);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bPlayPause);
            this.Controls.Add(this.bStop);
            this.MinimumSize = new System.Drawing.Size(700, 80);
            this.Name = "ProjectControl";
            this.Size = new System.Drawing.Size(700, 80);
            this.Load += new System.EventHandler(this.ProjectControl_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ProjectControl_Layout);
            ((System.ComponentModel.ISupportInitialize)(this.pbWaveViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbWaveViewer;
        private System.Windows.Forms.Button bPlayPause;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxCurrentTime;
    }
}
