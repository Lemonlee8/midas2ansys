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
using SerializerProgress;
using MidasGenModel.model;

namespace EasyMidas
{
    public partial class MainForm : Form
    {
        private static ChildForm ModelForm;//模型主视图窗口
        private BackgroundWorker _BackWorker;//后台单独进程
        private string _tempFileName;//模型文件存储路径
        public MainForm()
        {
            InitializeComponent();

            _BackWorker = new BackgroundWorker();//创建后台进程
            _BackWorker.WorkerReportsProgress = true;//可以报告进程更新
            _BackWorker.WorkerSupportsCancellation = false;//进程不支持取消

            string CurDir = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            string CurModelPath = CurDir + "\\models";
            _tempFileName = Path.Combine(CurModelPath, "model.emgb");//取得模型文件默认存储路径

            stusProgressBar.Visible = false;//先不显示进度条
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

            if (ModelForm==null||ModelForm.IsDisposed)
            {
                MessageBox.Show("请先新建模型","提示",MessageBoxButtons.OK,MessageBoxIcon.Stop);
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
                    DialogResult res = MessageBox.Show("导入MGT将冲掉现有的模型信息，是否确定？", "提示",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
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
            OpenFileDialog OPD = new OpenFileDialog();
            OPD.Title = "选择Midas数据文件路径";
            OPD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//获取我的文档
            OPD.Filter = "mgt 文件(*.mgt)|*.mgt|All files (*.*)|*.*";

            if (OPD.ShowDialog() == DialogResult.OK)
            {
                ModelForm.CurModel = new MidasGenModel.model.Bmodel();
                ModelForm.CurModel.ReadFromMgt(OPD.FileName);//读取mgt文件
                //MidasGenModel.Application.WriteModelBinary(ModelForm.CurModel, ModelFile);//写出二进制文件
                MessageLabel.Text = "读取模型成功！";
                ModelForm.Refresh();
                this.Refresh();
                ModelForm.InitContral();//更新控件
            }
        }

        private void 读取内力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择Midas单元内力输出文件";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            ofd.Filter = "nl 文件(*.nl)|*.nl|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ModelForm.CurModel.ReadElemForces(ofd.FileName);//读取内力
                int num = ModelForm.CurModel.elemforce.Count;//单位内力数
                MessageBox.Show("读入单位内力成功！单位内力数据数：" + num.ToString());
                this.Refresh();
            } 
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ModelForm == null || ModelForm.IsDisposed)
            {
                ModelForm = new ChildForm();
                ModelForm.Text = "新模型";
                ModelForm.MdiParent = this;
                ModelForm.WindowState = FormWindowState.Maximized;//最大化窗口
                ModelForm.Show();
                this.Refresh();//刷新界面
            }
            else
            {
                DialogResult res = MessageBox.Show("现有模型数据将被初始化，是否确认对现有模型进行初始化？", "注意",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (res == DialogResult.Yes)
                {
                    ModelForm.Dispose();
                    ModelForm = new ChildForm();
                    ModelForm.Text = "新模型";
                    ModelForm.MdiParent = this;
                    ModelForm.WindowState = FormWindowState.Maximized;//窗口最大化
                    ModelForm.Show();//激活
                    this.Refresh();//刷新界面
                }
            }          
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

        private void statusStrip_Paint(object sender, PaintEventArgs e)
        {
            if (ModelForm == null || ModelForm.IsDisposed)
                return;
            else
            {
                this.ModelInfoLabel.Text = " 节点:" + ModelForm.CurModel.nodes.Count.ToString() +
                     "单元:" + ModelForm.CurModel.elements.Count.ToString() + "--有内力的单元数:" +
                     ModelForm.CurModel.elemforce.Count.ToString();
                this.UnitLabel.Text = " 单位:" + ModelForm.CurModel.unit.Force + "," +
                    ModelForm.CurModel.unit.Length + "," + ModelForm.CurModel.unit.Temper;
            }
        }

        private void 保存模型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string CurDir = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            string CurModelPath = CurDir + "\\models";
            string ModelFile = Path.Combine(CurModelPath, "model.emgb");
            if (Directory.Exists(CurModelPath) == false)//如果没有模型文件目录
            {
                Directory.CreateDirectory(CurModelPath);//创建目录
            }

            if (ModelForm == null || ModelForm.IsDisposed)
            {
                MessageBox.Show("请先新建模型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                MidasGenModel.Application.WriteModelBinary(ModelForm.CurModel, ModelFile);
                ModelForm.Text = ModelFile;
            }
        }

        private void 重读缓存模型ToolStripMenuItem_Click(object sender, EventArgs e)
        {;
            string ModelFile = _tempFileName;
            if (ModelForm == null || ModelForm.IsDisposed)
            {
                MessageBox.Show("请先新建模型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (File.Exists(ModelFile) == false)
            {
                MessageBox.Show("缓存文件不存在!请先保存模型...","提示",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            else 
            {
                //ModelForm.CurModel=MidasGenModel.Application.ReadModelBinary(ModelFile);
                _BackWorker.ProgressChanged += delegate(object sender1, ProgressChangedEventArgs e1)
                {
                    stusProgressBar.Value = e1.ProgressPercentage;
                };
                _BackWorker.DoWork+=new DoWorkEventHandler(Deserialize);
                _BackWorker.RunWorkerCompleted += delegate
                {
                    MessageLabel.Text = "读取缓存模型完成!";
                    ModelForm.Text = _tempFileName;
                    ModelForm.InitContral();//初始化控件
                    stusProgressBar.Visible = false;
                };
                stusProgressBar.Visible = true;//显示状态进度栏
                _BackWorker.RunWorkerAsync();//开始后台读取操作
            }
        }

        private void 存储验算结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string CurDir = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            string CurModelPath = CurDir + "\\models";
            string ModelFile = Path.Combine(CurModelPath, "model.ga");
            if (Directory.Exists(CurModelPath) == false)//如果没有模型文件目录
            {
                Directory.CreateDirectory(CurModelPath);//创建目录
            }

            if (ModelForm == null || ModelForm.IsDisposed)
            {
                MessageBox.Show("请先新建模型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                MidasGenModel.Application.WriteCheckBinary(ModelForm.CheckTable,ModelFile);
                MessageLabel.Text="存储验算结果成功！";
            }
        }

        private void 重读取验算结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string CurDir = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            string CurModelPath = CurDir + "\\models";
            string ModelFile = Path.Combine(CurModelPath, "model.ga");
            if (Directory.Exists(CurModelPath) == false)//如果没有模型文件目录
            {
                Directory.CreateDirectory(CurModelPath);//创建目录
            }

            if (ModelForm == null || ModelForm.IsDisposed)
            {
                MessageBox.Show("请先新建模型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (File.Exists(ModelFile) == false)
            {
                MessageBox.Show("缓存文件不存在!请先保存验算结果...", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                ModelForm.CheckTable = MidasGenModel.Application.ReadCheckBinary(ModelFile);
                MessageLabel.Text = "重读验算结果成功!";
            }
        }

        /// <summary>
        /// 反序列化文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deserialize(object sender, DoWorkEventArgs e)
        {
            using (FileStream fs = File.OpenRead(_tempFileName))
            {
                Bmodel result = Utilities.Deserialize<Bmodel>(fs,
                    delegate(object sender2, ProgressChangedEventArgs e2)
                    {
                        _BackWorker.ReportProgress(e2.ProgressPercentage);
                    });
                ModelForm.CurModel = result;//存储到当前模型中
            }
        }
    }
}
