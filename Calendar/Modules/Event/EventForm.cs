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
            dataGridView1.DataTableSource(tables.ListEventTable);
            dataGridView1.GenerallDesign(Config.Settings.BackGroundColor, Config.Settings.ForeColor);

            dataGridView1.Columns[0].Width = dataGridView1.ClientSize.Width / 2;
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
    }
}
