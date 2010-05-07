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
        public  Bmodel CurModel;
        public ChildForm()
        {
            InitializeComponent();
            CurModel = new Bmodel();
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
                CodeCheck.WriteElemCheckRes(ref CurModel, sfd.FileName, 4);
            }
        }

    }
}
