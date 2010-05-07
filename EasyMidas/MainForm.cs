using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace EasyMidas
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void 读取MgtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string CurDir = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            string CurModelPath = CurDir + "\\models";
            string ModelFile = Path.Combine(CurModelPath, "model.emgb");
            if (Directory.Exists(CurModelPath) == false)//如果没有模型文件目录
            {
                Directory.CreateDirectory(CurModelPath);//创建目录
            }

            if (this.MdiChildren.Length == 0)
            {
                MessageBox.Show("请先新建模型");
                return;
            }
            else
            {
                if (File.Exists(ModelFile) == false)
                {
                    ReReadModel(ModelFile);//读取模型文件
                }
                else
                {
                    DialogResult res = MessageBox.Show("程序发现已有分析模型数据，是否重新读入mgt分析模型数据？", "注意",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                    if (res == DialogResult.Yes)
                    {
                        ReReadModel(ModelFile);
                    }
                }
            }

            
        }

        /// <summary>
        /// 重新读取mgt模型数据文件
        /// </summary>
        /// <param name="MoldeFile">模型文件存储路径</param>
        private void ReReadModel(string ModelFile)
        {
            ChildForm cf = this.ActiveMdiChild as ChildForm;

            OpenFileDialog OPD = new OpenFileDialog();
            OPD.Title = "选择Midas数据文件路径";
            OPD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//获取我的文档
            OPD.Filter = "mgt 文件(*.mgt)|*.mgt|All files (*.*)|*.*";

            if (OPD.ShowDialog() == DialogResult.OK)
            {
                cf.CurModel.ReadFromMgt(OPD.FileName);//读取mgt文件
                MidasGenModel.Application.WriteModelBinary(cf.CurModel, ModelFile);//写出二进制文件
                MessageLabel.Text="读取模型成功！节点:" + cf.CurModel.nodes.Count.ToString() + "单元:"
                    + cf.CurModel.elements.Count.ToString();
                cf.Refresh();
            }
        }

        private void 读取内力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           ChildForm cf=this.ActiveMdiChild as ChildForm;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择Midas单元内力输出文件";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            ofd.Filter = "nl 文件(*.nl)|*.nl|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                cf.CurModel.ReadElemForces(ofd.FileName);//读取内力
                int num =cf.CurModel.elemforce.Count;//单位内力数
                MessageBox.Show("读入单位内力成功！单位几力数据数：" + num.ToString());
            } 
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm ChildForm = new ChildForm();
            ChildForm.Text = "新模型";
            ChildForm.MdiParent = this;
            ChildForm.Show();
        }

        private void 钢结构验算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                MessageBox.Show("请先新建模型");
                return;
            }

            ChildForm cf = this.ActiveMdiChild as ChildForm;

            CheckSteelBeam CSB = new CheckSteelBeam();
            CSB.Owner =cf;
            CSB.ShowDialog();
        }

        private void 测试验算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                MessageBox.Show("请先新建模型");
                return;
            }
            ChildForm cf = this.ActiveMdiChild as ChildForm;
            //to do
        }
    }
}
