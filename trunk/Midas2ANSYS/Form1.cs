using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MidasGenModel.model;

namespace Midas2ANSYS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 1;
            //this.Text = "MIDAS to ANSYS (" ++ ")";
            CollapseForm();//������Ϣ��
        }

        private void bn_loadmgt_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPD = new OpenFileDialog();
            OPD.Title = "��Midas�������ļ�";
            OPD.InitialDirectory = Directory.GetCurrentDirectory();
            OPD.Filter = "mgt �ļ�(*.mgt)|*.mgt|All files (*.*)|*.*";
            //OPD.FilterIndex = 2;
            OPD.RestoreDirectory = true;
            if (OPD.ShowDialog() == DialogResult.OK)
            {
                tb_mgt.Text = OPD.FileName;
            }
        }

        private void bn_loadinp_Click(object sender, EventArgs e)
        {
            SaveFileDialog SPD = new SaveFileDialog();
            SPD.Title = "ѡ��ANSYS������·��";
            SPD.InitialDirectory = Directory.GetCurrentDirectory();
            SPD.Filter = "inp �ļ�(*.inp)|*.inp|All files (*.*)|*.*";
            if (SPD.ShowDialog() == DialogResult.OK)
            {
                tb_inp.Text = SPD.FileName;
            }
        }

        private void bt_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_run_Click(object sender, EventArgs e)
        {
            if (tb_mgt.Text == "" || tb_inp.Text == "")
            {
                MessageBox.Show("����ָ��ת�����ļ�·����");
            }
            else
            {
                Bmodel modelinfo=new Bmodel ();//�ֲ����������ڴ洢ģ������
                modelinfo.ReadFromMgt (tb_mgt.Text);
                if (modelinfo.WriteToInp(tb_inp.Text, comboBox1.SelectedIndex + 1) == true)
                {
                    if (splitContainer1.Panel2Collapsed)
                        CollapseForm();
                    tb_Out.AppendText(Environment.NewLine+"��ϲ��ת���ɹ����^_^");
                    tb_Out.AppendText(Environment.NewLine+"ģ�ͽڵ�����"+modelinfo.nodes.Count.ToString()+
                        "  ��Ԫ��:"+modelinfo.elements.Count.ToString());
                    tb_Out.AppendText(Environment.NewLine+"inp�ļ��ɹ������ڣ�"+tb_inp.Text);
                }

                //�������ṹ�����ִ��
                if (cb_MacroGroup.Checked)
                {
                    if (modelinfo.WriteAnsysComponents(Path.GetDirectoryName(tb_mgt.Text)))
                    {
                        tb_Out.AppendText(Environment.NewLine + "[��]�ṹ����Ϣת����Components�ɹ�!");
                    }
                }
            }
        }
        /// <summary>
        /// ���������ַ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.lubanren.com/");
        }

        //�Զ��������ļ�·��
        private void tb_mgt_TextChanged(object sender, EventArgs e)
        {
            if (tb_mgt.Text.EndsWith(".mgt")||tb_mgt.Text.EndsWith(".mct"))
            {
                tb_inp.Text = Path.ChangeExtension(tb_mgt.Text,".inp");
            }           
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string modelpath = Path.ChangeExtension(tb_mgt.Text, ".ga1");
            string path =Path.ChangeExtension(tb_mgt.Text,".nl");
            MidasGenModel.model.Bmodel mm = new Bmodel();
            //MidasGenModel.Application.ReadModelBinary(modelpath,ref mm);
            mm.ReadFromMgt(tb_mgt.Text);
            mm.ReadElemForces(path);

            //�洢��������ģ��
            string modelpath2=Path.ChangeExtension(modelpath,".ga2");
            MidasGenModel.Application.WriteModelBinary(mm, modelpath2);

            //�����������
            bool tt = mm.LoadCombTable.ContainsKey("gStr1");
            BLoadComb comb = mm.LoadCombTable["gStr1"];
            ElemForce ef = mm.CalElemForceComb(comb, 4);
            MessageBox.Show("OK");
        }

        private void Tx_Click(object sender, EventArgs e)
        {
            CollapseForm();
        }
        //չ��������Ϣ��
        private void CollapseForm()
        {
            bool isCollapse = splitContainer1.Panel2Collapsed;
            if (!isCollapse)
            {
                int Hp2 = splitContainer1.Panel2.Height;
                splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
                this.Height -= Hp2;
                Tx.Text = "չ��";
            }
            else
            {
                splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
                int Hp2 = splitContainer1.Panel2.Height;
                this.Height += Hp2;
                Tx.Text = "����";
            }
        }

    }
    
}