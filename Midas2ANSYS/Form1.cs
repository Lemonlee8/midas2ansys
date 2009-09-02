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
        }

        private void bn_loadmgt_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPD = new OpenFileDialog();
            OPD.Title = "打开Midas命令流文件";
            OPD.InitialDirectory = Directory.GetCurrentDirectory();
            OPD.Filter = "mgt 文件(*.mgt)|*.mgt|All files (*.*)|*.*";
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
            SPD.Title = "选择ANSYS命令流路径";
            SPD.InitialDirectory = Directory.GetCurrentDirectory();
            SPD.Filter = "inp 文件(*.inp)|*.inp|All files (*.*)|*.*";
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
                MessageBox.Show("请先指定转换的文件路径！");
            }
            else
            {
                //To do:执行文件转换
                Bmodel modelinfo=new Bmodel ();//局部变量，用于存储模型数据
                modelinfo.ReadFromMgt (tb_mgt.Text);
                //if (WriteInp(tb_inp.Text, modelinfo) == true)
                if (modelinfo.WriteToInp(tb_inp.Text,comboBox1.SelectedIndex+1) == true)
                    MessageBox.Show("恭喜，转换完成^_^","完成情况",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
        }
        /// <summary>
        /// 访问网络地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.lubanren.com/weblog/");
        }

        //自动完成输出文件路径
        private void tb_mgt_TextChanged(object sender, EventArgs e)
        {
            if (tb_mgt.Text.EndsWith(".mgt")||tb_mgt.Text.EndsWith(".mct"))
            {
                tb_inp.Text = tb_mgt.Text.Remove(tb_mgt.Text.LastIndexOf('.') + 1) + "inp";
            }           
        }
    
    }
    
}