﻿namespace SimpleAudioEditor.PeachStudio.View
{
    partial class PeachEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PeachEditor));
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.panelSample = new System.Windows.Forms.Panel();
            this.panelSupport = new System.Windows.Forms.Panel();
            this.buttonAddSample = new System.Windows.Forms.Button();
            this.projectControl = new SimpleAudioEditor.PeachStudio.ProjectControl();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panelSample.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(11, 133);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(165, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 20;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // panelSample
            // 
            this.panelSample.AllowDrop = true;
            this.panelSample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSample.AutoScroll = true;
            this.panelSample.BackColor = System.Drawing.Color.Transparent;
            this.panelSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSample.Controls.Add(this.panelSupport);
            this.panelSample.Location = new System.Drawing.Point(12, 156);
            this.panelSample.MinimumSize = new System.Drawing.Size(700, 302);
            this.panelSample.Name = "panelSample";
            this.panelSample.Size = new System.Drawing.Size(700, 425);
            this.panelSample.TabIndex = 3;
            this.panelSample.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.panelSample_ControlAdded);
            this.panelSample.Layout += new System.Windows.Forms.LayoutEventHandler(this.panelSample_Layout);
            // 
            // panelSupport
            // 
            this.panelSupport.BackColor = System.Drawing.Color.Transparent;
            this.panelSupport.Location = new System.Drawing.Point(0, 0);
            this.panelSupport.Name = "panelSupport";
            this.panelSupport.Size = new System.Drawing.Size(0, 304);
            this.panelSupport.TabIndex = 0;
            // 
            // buttonAddSample
            // 
            this.buttonAddSample.BackColor = System.Drawing.Color.DarkGreen;
            this.buttonAddSample.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddSample.Location = new System.Drawing.Point(12, 98);
            this.buttonAddSample.Name = "buttonAddSample";
            this.buttonAddSample.Size = new System.Drawing.Size(75, 23);
            this.buttonAddSample.TabIndex = 4;
            this.buttonAddSample.Text = "Add";
            this.buttonAddSample.UseVisualStyleBackColor = false;
            this.buttonAddSample.Click += new System.EventHandler(this.buttonAddSample_Click);
            // 
            // projectControl
            // 
            this.projectControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectControl.BackColor = System.Drawing.Color.Transparent;
            this.projectControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.projectControl.CurrentProject = null;
            this.projectControl.Location = new System.Drawing.Point(12, 12);
            this.projectControl.MinimumSize = new System.Drawing.Size(700, 80);
            this.projectControl.Name = "projectControl";
            this.projectControl.Size = new System.Drawing.Size(700, 80);
            this.projectControl.TabIndex = 5;
            // 
            // PeachEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = global::SimpleAudioEditor.Properties.Resources.peaches_pattern;
            this.ClientSize = new System.Drawing.Size(732, 593);
            this.Controls.Add(this.projectControl);
            this.Controls.Add(this.buttonAddSample);
            this.Controls.Add(this.panelSample);
            this.Controls.Add(this.trackBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(740, 480);
            this.Name = "PeachEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PeachEditor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PeachEditor_FormClosed);
            this.Load += new System.EventHandler(this.PeachEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panelSample.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Panel panelSample;
        private System.Windows.Forms.Button buttonAddSample;
        private System.Windows.Forms.Panel panelSupport;
        private ProjectControl projectControl;
    }
}