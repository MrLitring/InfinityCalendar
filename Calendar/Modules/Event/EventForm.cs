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

namespace Calendare.Modules.Event
{
    public partial class EventForm : Form, IFormEvent
    {
        public EventForm()
        {
            InitializeComponent();
        }

        void IFormEvent.DataGridViewUpdate(CustomDataTable tables) => DGVDataUpdate(tables);

        private void DGVDataUpdate(CustomDataTable tables)
        {
            this.dataGridView1.DataSource = tables.CopyTextTable;
        }

        private void EventForm_Load(object sender, EventArgs e)
        {

        }
    }
}
