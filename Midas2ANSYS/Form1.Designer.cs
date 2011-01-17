namespace Midas2ANSYS
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Tx = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_cancle = new System.Windows.Forms.Button();
            this.bt_run = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bn_loadinp = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_inp = new System.Windows.Forms.TextBox();
            this.bn_loadmgt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_mgt = new System.Windows.Forms.TextBox();
            this.tb_Out = new System.Windows.Forms.TextBox();
            this.cb_MacroGroup = new System.Windows.Forms.CheckBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Tx);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.bt_cancle);
            this.splitContainer1.Panel1.Controls.Add(this.bt_run);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tb_Out);
            this.splitContainer1.Size = new System.Drawing.Size(478, 384);
            this.splitContainer1.SplitterDistance = 240;
            this.splitContainer1.TabIndex = 5;
            // 
            // Tx
            // 
            this.Tx.Location = new System.Drawing.Point(138, 202);
            this.Tx.Name = "Tx";
            this.Tx.Size = new System.Drawing.Size(50, 23);
            this.Tx.TabIndex = 10;
            this.Tx.Text = "收起";
            this.Tx.UseVisualStyleBackColor = true;
            this.Tx.Click += new System.EventHandler(this.Tx_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(11, 207);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(53, 12);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "博客支持";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cb_MacroGroup);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(11, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(454, 62);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出选项";
            // 
            // comboBox1
            // 
            this.comboBox1.AllowDrop = true;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Beam 44",
            "Beam 188",
            "Beam 189"});
            this.comboBox1.Location = new System.Drawing.Point(90, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(87, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "梁单元类型";
            // 
            // bt_cancle
            // 
            this.bt_cancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_cancle.Location = new System.Drawing.Point(390, 202);
            this.bt_cancle.Name = "bt_cancle";
            this.bt_cancle.Size = new System.Drawing.Size(75, 23);
            this.bt_cancle.TabIndex = 7;
            this.bt_cancle.Text = "关 闭";
            this.bt_cancle.UseVisualStyleBackColor = true;
            this.bt_cancle.Click += new System.EventHandler(this.bt_cancle_Click);
            // 
            // bt_run
            // 
            this.bt_run.Location = new System.Drawing.Point(290, 202);
            this.bt_run.Name = "bt_run";
            this.bt_run.Size = new System.Drawing.Size(75, 23);
            this.bt_run.TabIndex = 6;
            this.bt_run.Text = "转 换";
            this.bt_run.UseVisualStyleBackColor = true;
            this.bt_run.Click += new System.EventHandler(this.bt_run_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bn_loadinp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_inp);
            this.groupBox1.Controls.Add(this.bn_loadmgt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_mgt);
            this.groupBox1.Location = new System.Drawing.Point(11, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 120);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "转换设置";
            // 
            // bn_loadinp
            // 
            this.bn_loadinp.Location = new System.Drawing.Point(368, 86);
            this.bn_loadinp.Name = "bn_loadinp";
            this.bn_loadinp.Size = new System.Drawing.Size(72, 23);
            this.bn_loadinp.TabIndex = 5;
            this.bn_loadinp.Text = ">>指 定";
            this.bn_loadinp.UseVisualStyleBackColor = true;
            this.bn_loadinp.Click += new System.EventHandler(this.bn_loadinp_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "ANSYS命令流文件输出:";
            // 
            // tb_inp
            // 
            this.tb_inp.BackColor = System.Drawing.Color.PaleGreen;
            this.tb_inp.Location = new System.Drawing.Point(23, 86);
            this.tb_inp.Name = "tb_inp";
            this.tb_inp.Size = new System.Drawing.Size(331, 21);
            this.tb_inp.TabIndex = 3;
            this.tb_inp.DoubleClick += new System.EventHandler(this.bn_loadinp_Click);
            // 
            // bn_loadmgt
            // 
            this.bn_loadmgt.Location = new System.Drawing.Point(368, 38);
            this.bn_loadmgt.Name = "bn_loadmgt";
            this.bn_loadmgt.Size = new System.Drawing.Size(72, 23);
            this.bn_loadmgt.TabIndex = 2;
            this.bn_loadmgt.Text = ">>加 载";
            this.bn_loadmgt.UseVisualStyleBackColor = true;
            this.bn_loadmgt.Click += new System.EventHandler(this.bn_loadmgt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "mgt文件位置:";
            // 
            // tb_mgt
            // 
            this.tb_mgt.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tb_mgt.Location = new System.Drawing.Point(23, 38);
            this.tb_mgt.Name = "tb_mgt";
            this.tb_mgt.Size = new System.Drawing.Size(331, 21);
            this.tb_mgt.TabIndex = 0;
            this.tb_mgt.TextChanged += new System.EventHandler(this.tb_mgt_TextChanged);
            this.tb_mgt.DoubleClick += new System.EventHandler(this.bn_loadmgt_Click);
            // 
            // tb_Out
            // 
            this.tb_Out.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Out.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_Out.Location = new System.Drawing.Point(11, 3);
            this.tb_Out.Multiline = true;
            this.tb_Out.Name = "tb_Out";
            this.tb_Out.ReadOnly = true;
            this.tb_Out.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_Out.Size = new System.Drawing.Size(454, 125);
            this.tb_Out.TabIndex = 5;
            // 
            // cb_MacroGroup
            // 
            this.cb_MacroGroup.AutoSize = true;
            this.cb_MacroGroup.Location = new System.Drawing.Point(194, 28);
            this.cb_MacroGroup.Name = "cb_MacroGroup";
            this.cb_MacroGroup.Size = new System.Drawing.Size(96, 16);
            this.cb_MacroGroup.TabIndex = 2;
            this.cb_MacroGroup.Text = "【宏】结构组";
            this.cb_MacroGroup.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(478, 384);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Midas2ANSYS (V1.0.0.52)";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_cancle;
        private System.Windows.Forms.Button bt_run;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bn_loadinp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_inp;
        private System.Windows.Forms.Button bn_loadmgt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_mgt;
        private System.Windows.Forms.TextBox tb_Out;
        private System.Windows.Forms.Button Tx;
        private System.Windows.Forms.CheckBox cb_MacroGroup;

    }
}

