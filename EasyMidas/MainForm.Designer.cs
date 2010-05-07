namespace EasyMidas
{
    partial class MainForm
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.MessageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tm_ReadMgt = new System.Windows.Forms.ToolStripMenuItem();
            this.前处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.读取内力ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.荷载组合编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.后处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.钢结构验算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.前于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MessageLabel,
            this.toolStripProgressBar1});
            this.statusStrip.Location = new System.Drawing.Point(0, 496);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(654, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // MessageLabel
            // 
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(41, 17);
            this.MessageLabel.Text = "无模型";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始ToolStripMenuItem,
            this.前处理ToolStripMenuItem,
            this.后处理ToolStripMenuItem,
            this.帮助ToolStripMenuItem,
            this.插件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(654, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 开始ToolStripMenuItem
            // 
            this.开始ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.tm_ReadMgt});
            this.开始ToolStripMenuItem.Name = "开始ToolStripMenuItem";
            this.开始ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.开始ToolStripMenuItem.Text = "文件";
            // 
            // tm_ReadMgt
            // 
            this.tm_ReadMgt.Name = "tm_ReadMgt";
            this.tm_ReadMgt.Size = new System.Drawing.Size(152, 22);
            this.tm_ReadMgt.Text = "导入Mgt文件";
            this.tm_ReadMgt.Click += new System.EventHandler(this.读取MgtToolStripMenuItem_Click);
            // 
            // 前处理ToolStripMenuItem
            // 
            this.前处理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.读取内力ToolStripMenuItem,
            this.荷载组合编辑ToolStripMenuItem});
            this.前处理ToolStripMenuItem.Name = "前处理ToolStripMenuItem";
            this.前处理ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.前处理ToolStripMenuItem.Text = "前处理";
            // 
            // 读取内力ToolStripMenuItem
            // 
            this.读取内力ToolStripMenuItem.Name = "读取内力ToolStripMenuItem";
            this.读取内力ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.读取内力ToolStripMenuItem.Text = "读取Midas内力";
            this.读取内力ToolStripMenuItem.Click += new System.EventHandler(this.读取内力ToolStripMenuItem_Click);
            // 
            // 荷载组合编辑ToolStripMenuItem
            // 
            this.荷载组合编辑ToolStripMenuItem.Name = "荷载组合编辑ToolStripMenuItem";
            this.荷载组合编辑ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.荷载组合编辑ToolStripMenuItem.Text = "荷载组合编辑";
            // 
            // 后处理ToolStripMenuItem
            // 
            this.后处理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.钢结构验算ToolStripMenuItem});
            this.后处理ToolStripMenuItem.Name = "后处理ToolStripMenuItem";
            this.后处理ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.后处理ToolStripMenuItem.Text = "后处理";
            // 
            // 钢结构验算ToolStripMenuItem
            // 
            this.钢结构验算ToolStripMenuItem.Name = "钢结构验算ToolStripMenuItem";
            this.钢结构验算ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.钢结构验算ToolStripMenuItem.Text = "钢结构验算";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.前于ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 前于ToolStripMenuItem
            // 
            this.前于ToolStripMenuItem.Name = "前于ToolStripMenuItem";
            this.前于ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.前于ToolStripMenuItem.Text = "关于";
            // 
            // 插件ToolStripMenuItem
            // 
            this.插件ToolStripMenuItem.Name = "插件ToolStripMenuItem";
            this.插件ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.插件ToolStripMenuItem.Text = "插件";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(654, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新建ToolStripMenuItem.Text = "新建模型";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 518);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "EasyMidas";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem 开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 前处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 后处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tm_ReadMgt;
        private System.Windows.Forms.ToolStripStatusLabel MessageLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem 读取内力ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 荷载组合编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 钢结构验算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 前于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
    }
}

