using Calendare.LibClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calendare.LibClasses;
using Calendare.LibClasses.ViewHelper;
using System.Data.Common;

namespace Calendare.Modules.Event
{
    public partial class EventForm : Form, IFormEvent
    {
        public EventForm()
        {
            InitializeComponent();
        }

        void IDGV.KeepTable(CustomDataTable tables) => DGVDataUpdate(tables);

        private void DGVDataUpdate(CustomDataTable tables)
        {
            dataGridView1.DataTableSource(tables.ListEventTable, 1);
            dataGridView1.GenerallDesign(Config.Settings.BackGroundColor, Config.Settings.ForeColor);

            for(int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                if ((string)tables.ListEventTable.Rows[row][2] == "Да")
                {
                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Config.Settings.AllertColor;
                }
            }

            dataGridView1.Columns[0].Width = 5;
            dataGridView1.Columns[3].Width = 50;
            FrozenColumn(0);

            dataGridView1.Columns[1].Width = dataGridView1.ClientSize.Width / 2;


            dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGridView1.ScrollBars = ScrollBars.Vertical;
        }

        private void FrozenColumn(int column)
        {
            dataGridView1.Columns[column].Frozen = true;
            dataGridView1.Columns[column].DividerWidth = 0;
            dataGridView1.Columns[column].Resizable = DataGridViewTriState.False;
            
        }

        private void EventForm_Load(object sender, EventArgs e)
        {

        }

        void IFormEvent.CMSShow(ContextMenuStrip cms) { cms.Show(Cursor.Position); }

        ContextMenuStrip IFormEvent.GetCMS() { return contextMenuStrip1; }


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CellPoint cellPoint = new CellPoint(e.RowIndex, e.ColumnIndex);
            //if (cellPoint.row < 0 || cellPoint.column < 0) return;


            if (e.Button == MouseButtons.Left)
            {
                dataGridView1.CellFocus(cellPoint);
                EventBus.OnEventCellLeftMouseClick?.Invoke(cellPoint);
            }

            if (e.Button == MouseButtons.Right)
            {
                EventBus.OnEventCellRightMouseClick?.Invoke(cellPoint);

            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count > 0)
            {
                if(dataGridView1.CurrentCell?.ColumnIndex == 0)
                dataGridView1.CurrentCell.Selected = false;
            }
        }

        private void RestartRow_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    row.Height = 20;
            }
        }
    }
}
