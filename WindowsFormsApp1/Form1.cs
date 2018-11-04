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
        int[,] map = new int[9, 9]; // matrix sudoku, this is solution
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            DataGridView1.Paint += DataGridView1_Paint;
            ComboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            btnSolution.Click += btnSolution_Click;


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
            createMap();
            int colMax = DataGridView1.Columns.Count;
            if (ComboBox1.SelectedIndex == 0)
                for (int u = 0; u < 9; u++)
                    for (int y = 0; y < 4; y++)
                    {
                        int column = r.Next(0, colMax); //get the random column index
                        DataGridView1.Rows[u].Cells[column].Value = "";
                        DataGridView1.Rows[u].Cells[column].Style.ForeColor = Color.Black;

                    }
            if (ComboBox1.SelectedIndex == 1)
                for (int u = 0; u < 9; u++)
                    for (int y = 0; y < 5; y++)
                    {
                        int column = r.Next(0, colMax); //get the random column index
                        DataGridView1.Rows[u].Cells[column].Value = "";
                        DataGridView1.Rows[u].Cells[column].Style.ForeColor = Color.Black;

                    }
            if (ComboBox1.SelectedIndex == 2)
                for (int u = 0; u < 9; u++)
                    for (int y = 0; y < 6; y++)
                    {
                        int column = r.Next(0, colMax); //get the random column index
                        DataGridView1.Rows[u].Cells[column].Value = "";
                        DataGridView1.Rows[u].Cells[column].Style.ForeColor = Color.Black;

                    }
        }
        
        private void createMap()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            List<int> row, column, square, tmp, tmp1, tmp2;
            bool restart = false;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    map[i, j] = 0;
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    row = getRowAvailableValues(i);
                    column = getColumnAvailableValues(j);
                    square = getSquareAvailableValues(i, j);
                    tmp1 = row.Intersect(column).ToList();
                    tmp2 = row.Intersect(square).ToList();
                    tmp = tmp1.Intersect(tmp2).ToList();
                    if (tmp.Count == 0)
                    {
                        restart = true;
                        break;
                    }
                    map[i, j] = tmp[rnd.Next(tmp.Count)];
                    DataGridView1.Rows[i].Cells[j].Value = map[i,j];
                    DataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                }
                if (restart)
                {
                    break;
                }
            }

            if (restart)
                createMap();

            return;
        }

        private List<int> getRowAvailableValues(int y)
        {
            int[] a = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] b = new int[9];
            for (int i = 0; i < 9; i++)
            {
                b[i] = map[y, i];
            }
            return a.Except(b).ToList();
        }

        private List<int> getColumnAvailableValues(int x)
        {
            int[] a = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] b = new int[9];
            for (int i = 0; i < 9; i++)
            {
                b[i] = map[i, x];
            }
            return a.Except(b).ToList();
        }

        private List<int> getSquareAvailableValues(int x, int y)
        {
            int[] a = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] b = new int[9];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    b[i* 3 + j] = map[(x / 3) * 3 + i, (y / 3) * 3 + j];
                }
            }
            return a.Except(b).ToList();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void ComboBox1_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            btnNew.PerformClick();
        }

        private Random r = new Random();
        private void btnSolution_Click(object sender, EventArgs e)
        {
            game_ShowSolution(map);
        }
        public void game_ShowSolution(int[,] map)
        {
            for (int y = 0; y <= 8; y++)
            {
                for (int x = 0; x <= 8; x++)
                {
                    if (DataGridView1.Rows[y].Cells[x].Style.ForeColor == Color.Black)
                    {
                        if (string.IsNullOrEmpty(DataGridView1.Rows[y].Cells[x].Value.ToString()))
                        {
                            DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Blue;
                            DataGridView1.Rows[y].Cells[x].Value = map[y,x];
                        }
                        else
                        {
                            if (map[y,x].ToString() != DataGridView1.Rows[y].Cells[x].Value.ToString())
                            {
                                DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Blue;
                                DataGridView1.Rows[y].Cells[x].Value = map[y,x];
                            }
                        }
                    }
                }
            }
        }
        
    }
}
