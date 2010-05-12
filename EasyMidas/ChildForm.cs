using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MidasGenModel.model;
using MidasGenModel.Design;

namespace EasyMidas
{
    public partial class ChildForm : Form
    {
        public  Bmodel CurModel;//模型数据
        public CheckRes CheckTable;//截面验算结果表
        public ChildForm()
        {
            InitializeComponent();
            CurModel = new Bmodel();
            CheckTable = new CheckRes();
            InitContral();//初始化控件
            comboBox1.SelectedIndex = 1;
        }

        private void ChildForm_Paint(object sender, PaintEventArgs e)
        {
            label1.Text = "当前模型信息：节点" + CurModel.nodes.Count.ToString() +
                "单元" + CurModel.elements.Count.ToString()+"--有内力的单元数"+
                CurModel.elemforce.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "请输入结果文件存储位置";
            sfd.Filter="txt 文件(*.txt)|*.txt|All files (*.*)|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                int num = Convert.ToInt32(textBox12.Text);
                UpdataDesignPara(num);//更新单元设计参数
                CodeCheck.WriteElemCheckRes(ref CurModel, ref CheckTable,sfd.FileName,num );
            }
        }
        //初始化控件
        public  void InitContral()
        {
            cb_secs.Items.Clear();
            if (CurModel.sections.Count > 0)
            {
                foreach (BSections sec in CurModel.sections.Values)
                {
                    cb_secs.Items.Add(sec.Num+" "+sec.Name);
                }
            }
            else
            {
                cb_secs.Items.Add("无");
            }
            cb_secs.SelectedIndex = 0;

            lb_changdu.Text = CurModel.unit.Length;
            lb_changdu2.Text = CurModel.unit.Length;
            lb_qiangdu.Text = CurModel.unit.Force + "/" + CurModel.unit.Length + "2";
        }

        /// <summary>
        /// 按单无更新截面设计参数
        /// </summary>
        /// <param name="iEle">单元号</param>
        public void UpdataDesignPara(int iEle)
        {
            int num = iEle;//单元号
            double len_y = Convert.ToDouble(tb_leng1.Text);
            double len_z = Convert.ToDouble(tb_leng2.Text);
            double Net_r = Convert.ToDouble(tb_Net_r.Text);
            double Gamma_y = Convert.ToDouble(tb_gamma1.Text);
            double Gamma_z = Convert.ToDouble(tb_gamma2.Text);
            double Betla_my = Convert.ToDouble(tb_betla1.Text);
            double Betal_mz = Convert.ToDouble(tb_betla2.Text);
            double Betal_ty = Convert.ToDouble(tb_betla3.Text);
            double Betal_tz = Convert.ToDouble(tb_betla4.Text);
            double Phi_by = Convert.ToDouble(tb_phibx.Text);
            double Phi_bz = Convert.ToDouble(tb_phiby.Text);
            double F = Convert.ToDouble(tb_f.Text);//强度设计值
            SecCategory cat=SecCategory.b;

            switch(comboBox1.SelectedIndex)
            {
                case 0:cat=SecCategory.a;break;
                case 1:cat=SecCategory.b;break;
                case 2:cat=SecCategory.c;break;
                case 3:cat=SecCategory.d;break;
                default:break;
            }

            //更新长细比
            CodeCheck.CalDesignPara_lemda(ref CurModel, num, len_y, len_z);
            //更新受压稳定系数
            CodeCheck.CalDesignPara_phi(ref CurModel, num, 1, cat);
            //更新其它参数
            FrameElement fele = CurModel.elements[num] as FrameElement;
            fele.DPs.Ratio_Anet = Net_r;
            fele.DPs.Gamma_y = Gamma_y;
            fele.DPs.Gamma_z = Gamma_z;
            fele.DPs.Belta_my = Betla_my;
            fele.DPs.Belta_mz = Betal_mz;
            fele.DPs.Belta_ty = Betal_ty;
            fele.DPs.Belta_tz = Betal_tz;
            fele.DPs.fy = F;//强度设计值
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string Cursec = cb_secs.SelectedItem.ToString();
            int iSec = 5;
            if (Cursec.Contains(" "))
            {
                string temp = Cursec.Remove(Cursec.IndexOf(' '));
                iSec = Convert.ToInt32(temp);//取得截面号
            }
            //List<int> eles = CurModel.getElemBySec(iSec);
            //foreach (int ele in eles)
            //{
            //    UpdataDesignPara(ele);//更新单元设计参数
            //}
            CheckTable.CheckElemBySec(ref CurModel, iSec);

            MessageBox.Show(Cursec + "截面验算完成！");
            return;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Cursec = cb_secs.SelectedItem.ToString();
            int iSec = 5;
            if (Cursec.Contains(" "))
            {
                string temp = Cursec.Remove(Cursec.IndexOf(' '));
                iSec = Convert.ToInt32(temp);//取得截面号
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "请输入结果文件存储位置";
            sfd.Filter = "txt 文件(*.txt)|*.txt|All files (*.*)|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
               CodeCheck.WriteSecCheckRes(ref CurModel,ref CheckTable,sfd.FileName,iSec);
            }
        }

        //输出所有截面验算结果
        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "请输入结果文件存储位置";
            sfd.Filter = "txt 文件(*.txt)|*.txt|All files (*.*)|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                CodeCheck.WriteAllCheckRes(ref CurModel,ref CheckTable,sfd.FileName);
            }
        }
        //指定验算参数
        private void tb_putpara_Click(object sender, EventArgs e)
        {
            string Cursec = cb_secs.SelectedItem.ToString();
            int iSec = 5;
            if (Cursec.Contains(" "))
            {
                string temp = Cursec.Remove(Cursec.IndexOf(' '));
                iSec = Convert.ToInt32(temp);//取得截面号
            }
            List<int> eles = CurModel.getElemBySec(iSec);
            foreach (int ele in eles)
            {
                UpdataDesignPara(ele);//更新单元设计参数
            }

            MessageBox.Show(Cursec+"截面验算参数指定成功！");
        }

    }
}
