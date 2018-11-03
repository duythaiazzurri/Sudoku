using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudokuu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            btnNew.Click += btnNew_Click;
            DataGridView1.Paint += DataGridView1_Paint;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataGridView1.Rows.Add(9);
            ComboBox1.SelectedIndex = 0;
            btnNew.PerformClick();
        }
        private void DataGridView1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 75, 0, 75, 228);
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 150, 0, 150, 228);
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 0, 66, 228, 66);
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 0, 132, 228, 132);
        }
        
        private void btnNew_Click(object sender, EventArgs e)
        {

        }
    }
}
