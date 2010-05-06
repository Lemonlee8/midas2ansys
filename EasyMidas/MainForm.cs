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

        /// <summary>
        /// 重新读取mgt模型数据文件
        /// </summary>
        /// <param name="MoldeFile">模型文件存储路径</param>
        private void ReReadModel(string ModelFile)
        {
            MidasGenModel.model.Bmodel midasModel = new MidasGenModel.model.Bmodel();//定义模型对像
            OpenFileDialog OPD = new OpenFileDialog();
            OPD.Title = "选择Midas数据文件路径";
            OPD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//获取我的文档
            OPD.Filter = "mgt 文件(*.mgt)|*.mgt|All files (*.*)|*.*";

            if (OPD.ShowDialog() == DialogResult.OK)
            {
                midasModel.ReadFromMgt(OPD.FileName);//读取mgt文件
                MidasGenModel.Application.WriteModelBinary(midasModel, ModelFile);//写出二进制文件
                MessageLabel.Text="读取模型成功！节点:" + midasModel.nodes.Count.ToString() + "单元:"
                    + midasModel.elements.Count.ToString();
            }
        }
    }
}
