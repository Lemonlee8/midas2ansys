using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MidasGenModel.model;

namespace EasyMidas
{
    public partial class ChildForm : Form
    {
        public Bmodel CurModel;
        public ChildForm()
        {
            InitializeComponent();
            CurModel = new Bmodel();
        }

        private void ChildForm_Paint(object sender, PaintEventArgs e)
        {
            label1.Text = "当前模型信息：节点" + CurModel.nodes.Count.ToString() +
                "单元" + CurModel.elements.Count.ToString();
        }
    }
}
