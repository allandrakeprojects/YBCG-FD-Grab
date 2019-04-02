﻿namespace YBCG_FD_Grab
{
    partial class Main_Form
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
        /// Required method for Designer support - do not modiYB
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.panel_header = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox_header = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_title = new System.Windows.Forms.Label();
            this.pictureBox_minimize = new System.Windows.Forms.PictureBox();
            this.pictureBox_close = new System.Windows.Forms.PictureBox();
            this.label_brand = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_player_last_bill_no = new System.Windows.Forms.Label();
            this.panel_cefsharp = new System.Windows.Forms.Panel();
            this.panel_landing = new System.Windows.Forms.Panel();
            this.pictureBox_landing = new System.Windows.Forms.PictureBox();
            this.label_page_count = new System.Windows.Forms.Label();
            this.label_currentrecord = new System.Windows.Forms.Label();
            this.pictureBox_loader = new System.Windows.Forms.PictureBox();
            this.timer_landing = new System.Windows.Forms.Timer(this.components);
            this.timer_close_message_box = new System.Windows.Forms.Timer(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timer_pending = new System.Windows.Forms.Timer(this.components);
            this.timer_detect_running = new System.Windows.Forms.Timer(this.components);
            this.timer_flush_memory = new System.Windows.Forms.Timer(this.components);
            this.timer_size = new System.Windows.Forms.Timer(this.components);
            this.panel_header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_header)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).BeginInit();
            this.panel_landing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_landing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_loader)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_header
            // 
            this.panel_header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(90)))), ((int)(((byte)(101)))));
            this.panel_header.Controls.Add(this.label1);
            this.panel_header.Controls.Add(this.pictureBox_header);
            this.panel_header.Controls.Add(this.panel1);
            this.panel_header.Controls.Add(this.label_title);
            this.panel_header.Controls.Add(this.pictureBox_minimize);
            this.panel_header.Controls.Add(this.pictureBox_close);
            this.panel_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_header.Location = new System.Drawing.Point(0, 0);
            this.panel_header.Name = "panel_header";
            this.panel_header.Size = new System.Drawing.Size(466, 45);
            this.panel_header.TabIndex = 1;
            this.panel_header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_header_MouseDown);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(445, 149);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // pictureBox_header
            // 
            this.pictureBox_header.Image = global::YBCG_FD_Grab.Properties.Resources.fd_header;
            this.pictureBox_header.Location = new System.Drawing.Point(21, 9);
            this.pictureBox_header.Name = "pictureBox_header";
            this.pictureBox_header.Size = new System.Drawing.Size(27, 24);
            this.pictureBox_header.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_header.TabIndex = 4;
            this.pictureBox_header.TabStop = false;
            this.pictureBox_header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_header_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(101)))), ((int)(((byte)(6)))));
            this.panel1.Location = new System.Drawing.Point(0, -5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 10);
            this.panel1.TabIndex = 1;
            this.panel1.TabStop = true;
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            this.panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDoubleClick);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // label_title
            // 
            this.label_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label_title.Location = new System.Drawing.Point(52, 0);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(235, 45);
            this.label_title.TabIndex = 2;
            this.label_title.Text = "FD Grab [Card Game]";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_title_MouseDown);
            // 
            // pictureBox_minimize
            // 
            this.pictureBox_minimize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_minimize.Image = global::YBCG_FD_Grab.Properties.Resources.minus;
            this.pictureBox_minimize.Location = new System.Drawing.Point(378, 10);
            this.pictureBox_minimize.Name = "pictureBox_minimize";
            this.pictureBox_minimize.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_minimize.TabIndex = 1;
            this.pictureBox_minimize.TabStop = false;
            this.pictureBox_minimize.Click += new System.EventHandler(this.pictureBox_minimize_Click);
            // 
            // pictureBox_close
            // 
            this.pictureBox_close.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_close.Image = global::YBCG_FD_Grab.Properties.Resources.close;
            this.pictureBox_close.Location = new System.Drawing.Point(416, 10);
            this.pictureBox_close.Name = "pictureBox_close";
            this.pictureBox_close.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_close.TabIndex = 0;
            this.pictureBox_close.TabStop = false;
            this.pictureBox_close.Click += new System.EventHandler(this.pictureBox_close_Click);
            // 
            // label_brand
            // 
            this.label_brand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_brand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_brand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(101)))), ((int)(((byte)(6)))));
            this.label_brand.Location = new System.Drawing.Point(0, 228);
            this.label_brand.Name = "label_brand";
            this.label_brand.Size = new System.Drawing.Size(468, 23);
            this.label_brand.TabIndex = 4;
            this.label_brand.Text = "Yong Bao";
            this.label_brand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_brand.Visible = false;
            this.label_brand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_brand_MouseDown);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(101)))), ((int)(((byte)(6)))));
            this.panel2.Location = new System.Drawing.Point(309, 462);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(158, 10);
            this.panel2.TabIndex = 5;
            this.panel2.TabStop = true;
            this.panel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseClick);
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            // 
            // label_player_last_bill_no
            // 
            this.label_player_last_bill_no.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_player_last_bill_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_player_last_bill_no.Location = new System.Drawing.Point(0, 197);
            this.label_player_last_bill_no.Name = "label_player_last_bill_no";
            this.label_player_last_bill_no.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label_player_last_bill_no.Size = new System.Drawing.Size(466, 23);
            this.label_player_last_bill_no.TabIndex = 8;
            this.label_player_last_bill_no.Text = "-";
            this.label_player_last_bill_no.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_player_last_bill_no.Visible = false;
            this.label_player_last_bill_no.MouseClick += new System.Windows.Forms.MouseEventHandler(this.label_player_last_bill_no_MouseClick);
            this.label_player_last_bill_no.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_player_last_bill_no_MouseDown);
            // 
            // panel_cefsharp
            // 
            this.panel_cefsharp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_cefsharp.Location = new System.Drawing.Point(10, 55);
            this.panel_cefsharp.Name = "panel_cefsharp";
            this.panel_cefsharp.Size = new System.Drawing.Size(445, 402);
            this.panel_cefsharp.TabIndex = 9;
            // 
            // panel_landing
            // 
            this.panel_landing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(90)))), ((int)(((byte)(101)))));
            this.panel_landing.Controls.Add(this.pictureBox_landing);
            this.panel_landing.Location = new System.Drawing.Point(0, 10);
            this.panel_landing.Name = "panel_landing";
            this.panel_landing.Size = new System.Drawing.Size(468, 457);
            this.panel_landing.TabIndex = 10;
            this.panel_landing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_landing_MouseDown);
            // 
            // pictureBox_landing
            // 
            this.pictureBox_landing.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_landing.Image")));
            this.pictureBox_landing.Location = new System.Drawing.Point(183, 169);
            this.pictureBox_landing.Name = "pictureBox_landing";
            this.pictureBox_landing.Size = new System.Drawing.Size(111, 113);
            this.pictureBox_landing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_landing.TabIndex = 0;
            this.pictureBox_landing.TabStop = false;
            this.pictureBox_landing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_landing_MouseDown);
            // 
            // label_page_count
            // 
            this.label_page_count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_page_count.Location = new System.Drawing.Point(11, 274);
            this.label_page_count.Name = "label_page_count";
            this.label_page_count.Size = new System.Drawing.Size(203, 23);
            this.label_page_count.TabIndex = 10;
            this.label_page_count.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_currentrecord
            // 
            this.label_currentrecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_currentrecord.Location = new System.Drawing.Point(11, 290);
            this.label_currentrecord.Name = "label_currentrecord";
            this.label_currentrecord.Size = new System.Drawing.Size(203, 23);
            this.label_currentrecord.TabIndex = 11;
            this.label_currentrecord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox_loader
            // 
            this.pictureBox_loader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_loader.Image = global::YBCG_FD_Grab.Properties.Resources.loader;
            this.pictureBox_loader.Location = new System.Drawing.Point(153, 233);
            this.pictureBox_loader.Name = "pictureBox_loader";
            this.pictureBox_loader.Size = new System.Drawing.Size(160, 74);
            this.pictureBox_loader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_loader.TabIndex = 3;
            this.pictureBox_loader.TabStop = false;
            this.pictureBox_loader.Visible = false;
            this.pictureBox_loader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_loader_MouseDown);
            // 
            // timer_landing
            // 
            this.timer_landing.Interval = 2000;
            this.timer_landing.Tick += new System.EventHandler(this.timer_landing_Tick);
            // 
            // timer_close_message_box
            // 
            this.timer_close_message_box.Enabled = true;
            this.timer_close_message_box.Tick += new System.EventHandler(this.timer_close_message_box_Tick);
            // 
            // timer
            // 
            this.timer.Interval = 15000;
            this.timer.Tick += new System.EventHandler(this.timer_TickAsync);
            // 
            // timer_pending
            // 
            this.timer_pending.Enabled = true;
            this.timer_pending.Interval = 20000;
            this.timer_pending.Tick += new System.EventHandler(this.timer_pending_TickAsync);
            // 
            // timer_detect_running
            // 
            this.timer_detect_running.Enabled = true;
            this.timer_detect_running.Interval = 60000;
            this.timer_detect_running.Tick += new System.EventHandler(this.timer_detect_running_Tick);
            // 
            // timer_flush_memory
            // 
            this.timer_flush_memory.Enabled = true;
            this.timer_flush_memory.Interval = 60000;
            this.timer_flush_memory.Tick += new System.EventHandler(this.timer_flush_memory_Tick);
            // 
            // timer_size
            // 
            this.timer_size.Tick += new System.EventHandler(this.timer_size_Tick);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(466, 468);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel_landing);
            this.Controls.Add(this.panel_cefsharp);
            this.Controls.Add(this.label_player_last_bill_no);
            this.Controls.Add(this.label_brand);
            this.Controls.Add(this.pictureBox_loader);
            this.Controls.Add(this.label_page_count);
            this.Controls.Add(this.label_currentrecord);
            this.Controls.Add(this.panel_header);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(466, 468);
            this.MinimumSize = new System.Drawing.Size(466, 168);
            this.Name = "Main_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YB FD Grab";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Form_FormClosing);
            this.Load += new System.EventHandler(this.Main_Form_Load);
            this.panel_header.ResumeLayout(false);
            this.panel_header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_header)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            this.panel_landing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_landing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_loader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_header;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.PictureBox pictureBox_minimize;
        private System.Windows.Forms.PictureBox pictureBox_close;
        private System.Windows.Forms.PictureBox pictureBox_loader;
        private System.Windows.Forms.Label label_brand;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_player_last_bill_no;
        private System.Windows.Forms.Panel panel_cefsharp;
        private System.Windows.Forms.Panel panel_landing;
        private System.Windows.Forms.PictureBox pictureBox_landing;
        private System.Windows.Forms.Label label_page_count;
        private System.Windows.Forms.Label label_currentrecord;
        private System.Windows.Forms.PictureBox pictureBox_header;
        private System.Windows.Forms.Timer timer_landing;
        private System.Windows.Forms.Timer timer_close_message_box;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timer_pending;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer_detect_running;
        private System.Windows.Forms.Timer timer_flush_memory;
        private System.Windows.Forms.Timer timer_size;
    }
}