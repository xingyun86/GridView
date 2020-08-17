using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GridView
{
    public partial class GridViewForm : Form
    {
        class LinesPoint
        {
            public Point s;
            public Point e;
            public LinesPoint(Point _s, Point _e)
            {
                s = _s;e = _e;
            }
        }
        List<LinesPoint> linesPointList = new List<LinesPoint>();
        
        public GridViewForm()
        {
            InitializeComponent();

            ///////////////////////////////////////////////////
            
            /*
            //包含Header所有的单元格的背景色 DataGridView1.DefaultCellStyle.BackColor
            //包含Header所有的单元格的前景色 DataGridView1.DefaultCellStyle.ForeColor
            //包含Header所有的单元格的背景色 DataGridView1.DefaultCellStyle.BackColor
            // Header以外所有的单元格的背景色 DataGridView1.RowsDefaultCellStyle.BackColor
            //奇数行的单元格的背景色 DataGridView1.AlternatingRowsDefaultCellStyle.BackColor
            //列Header的背景色 DataGridView1.ColumnHeadersDefaultCellStyle.BackColor
            //行Header的背景色 DataGridView1.RowHeadersDefaultCellStyle.BackColor
            //边框线的颜色 DataGridView1.GridColor
            //如果想让标题的边框线和颜色生效还需要如下设置
            */
            this.dataGridView1.Paint += DataGridView1_Paint;
            this.dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.GridColor = Color.LightBlue;
            this.dataGridView1.BackgroundColor = Color.LightBlue;
            this.dataGridView1.DefaultCellStyle.BackColor = Color.AliceBlue;
            this.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.AliceBlue;
             this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.dataGridView1.RowHeadersWidth = 80;
            this.dataGridView1.RowTemplate.Height =80;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;

            linesPointList.Add(new LinesPoint(new Point(0, 0), new Point(1, 2)));
            linesPointList.Add(new LinesPoint(new Point(1, 2), new Point(2, 2)));

            DataGridViewTextBoxColumn dataGridViewCol1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dataGridViewCol2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dataGridViewCol3 = new DataGridViewTextBoxColumn();
            dataGridViewCol1.Name = "dataGridViewCol1";
            dataGridViewCol1.HeaderText = "Name";
            dataGridViewCol2.Name = "dataGridViewCol2";
            dataGridViewCol2.HeaderText = "Count";
            dataGridViewCol3.Name = "dataGridViewCol3";
            dataGridViewCol3.HeaderText = "Count";
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dataGridViewCol1, dataGridViewCol2, dataGridViewCol3 });
        }

        private void DataGridView1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;

            //e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(0, 0, this.dataGridView1.Width - 1, this.dataGridView1.Height - 1));

            // 单元格间连线
            Point ptStart;
            Point ptEnd;
            int row = 0, col = 0;
            int CellLeft = 0;
            int CellTop = 0;
            int CellBottom = 0;
            int CellRight = 0;
            int CellWidth = 0;
            int CellHeight = 0;
            foreach (var it in linesPointList)
            {
                col = it.s.Y; row = it.s.X;
                CellLeft = this.dataGridView1.GetCellDisplayRectangle(col, row, true).Left;
                CellTop = this.dataGridView1.GetCellDisplayRectangle(col, row, true).Top;
                CellBottom = this.dataGridView1.GetCellDisplayRectangle(col, row, true).Bottom;
                CellRight = this.dataGridView1.GetCellDisplayRectangle(col, row, true).Right;
                CellWidth = this.dataGridView1.GetCellDisplayRectangle(col, row, true).Width;
                CellHeight = this.dataGridView1.GetCellDisplayRectangle(col, row, true).Height;
                ptStart = new Point(CellLeft + CellWidth / 2, CellTop + CellHeight / 2);
                {
                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);//画刷
                    e.Graphics.FillEllipse(myBrush, new Rectangle(CellLeft, CellTop, CellWidth/2, CellWidth / 2));//画实心椭圆
                }

                col = it.e.Y; row = it.e.X;
                CellLeft = this.dataGridView1.GetCellDisplayRectangle(col, row, true).Left;
                CellTop = this.dataGridView1.GetCellDisplayRectangle(col, row, true).Top;
                CellBottom = this.dataGridView1.GetCellDisplayRectangle(col, row, true).Bottom;
                CellRight = this.dataGridView1.GetCellDisplayRectangle(col, row, true).Right;
                CellWidth = this.dataGridView1.GetCellDisplayRectangle(col, row, true).Width;
                CellHeight = this.dataGridView1.GetCellDisplayRectangle(col, row, true).Height;
                ptEnd = new Point(CellLeft + CellWidth / 2, CellTop + CellHeight / 2);
                {
                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);//画刷
                    e.Graphics.FillEllipse(myBrush, new Rectangle(CellLeft, CellTop, CellWidth / 2, CellWidth / 2));//画实心椭圆
                }
                e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Cyan), 2.0f), ptStart, ptEnd);
            }

        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            int index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = "1";
            this.dataGridView1.Rows[index].Cells[1].Value = "2";
            this.dataGridView1.Rows[index].Cells[2].Value = "3";
        }
    }
}
