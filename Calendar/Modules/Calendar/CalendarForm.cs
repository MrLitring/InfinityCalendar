using Calendare.LibClasses;
using ViewHelper = Calendare.LibClasses.ViewHelper;
using Calendare.LibClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calendare.LibClasses.ViewHelper;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace Calendare.Modules.Calendar
{
    public partial class CalendarForm : Form, IFormCalendare
    {
        private bool isHeaderView = false;
        private Label[] labels;


        public CalendarForm()
        {
            InitializeComponent();

            labels = new Label[]
            {
                label1,
                label2
            };

            InitEvent();
        }

        void IDGV.KeepTable(LibClasses.CustomDataTable dataTable)
        {
            isHeaderView = dataTable.isVisibleHeader;
            DGVUpdateData(dataTable);
            DGVEventUpdate(dataTable.EventTable);
            RowResize();

        }

        void IFormCalendare.CellFocus(CellPoint cellPoint) { dataGridView1.CellFocus(cellPoint); }

        void IFormCalendare.LabelTextUpdate(int index, string str) { LBLTextUpdate(index, str); }

        ContextMenuStrip IFormCalendare.GetCMS() { return contextMenuStrip1; }

        void IFormCalendare.CMSShow(ContextMenuStrip cms) { cms.Show(Cursor.Position); }

        void IFormCalendare.CellPaint(Calendare.LibClasses.CellPoint cellPoint, System.Drawing.Color backColor, System.Drawing.Color foreColor)
        {
            dataGridView1.Rows[cellPoint.row].Cells[cellPoint.column].Style.BackColor = backColor;
            dataGridView1.Rows[cellPoint.row].Cells[cellPoint.column].Style.ForeColor = foreColor;
        }

        private void InitEvent()
        {
            labels[0].MouseClick += (s, e) => { EventBus.OnRestartDate?.Invoke(); };
            labels[1].MouseClick += (s, e) => { EventBus.OnEditModeChange?.Invoke(); };

            btnIncreaseYear.MouseClick += (s, e) => { EventBus.OnIncreaseDate?.Invoke(); };
            btnDecreaseYear.MouseClick += (s, e) => { EventBus.OnDecreaseDate?.Invoke(); };

            dataGridView1.Resize +=(s, e) => dataGridView1.RowResize(isHeaderView);
        }



        private void DGVUpdateData(CustomDataTable dataTable)
        {
            dataGridView1.Reset();

            //dataGridView1.DataSource = dataTable.DateTable;

            dataGridView1.DataTableSource(dataTable.DateTable);
            dataGridView1.GenerallDesign(Config.Settings.BackGroundColor, Config.Settings.ForeColor);

            dataGridView1.ColumnHeadersVisible = dataTable.isVisibleHeader;

        }

        private void DGVEventUpdate(DataTable dataTable)
        {
            DataGridView dgv = dataGridView1;

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    if (int.Parse(dataTable.Rows[row][col].ToString()) == 1)
                        dgv.Rows[row].Cells[col].Style.ForeColor = Config.Settings.ExistEvent;
                    
                }
            }

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CellPoint cellPoint = new CellPoint(e.RowIndex, e.ColumnIndex);
            //if (cellPoint.row < 0 || cellPoint.column < 0) return;


            if(e.Button == MouseButtons.Left)
            {
                dataGridView1.CellFocus(cellPoint);
                EventBus.OnCalendarCellLeftMouseClick?.Invoke(cellPoint);
            }

            if(e.Button == MouseButtons.Right)
            {
                EventBus.OnCalendarCellRightMouseClick?.Invoke(cellPoint);

            }
        }

        private void RowResize() { this.dataGridView1.RowResize(isHeaderView); }

        private void LBLTextUpdate(int index, string str) { labels[index].Text = str; }

    }
}

