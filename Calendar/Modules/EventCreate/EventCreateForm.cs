using Calendare.LibClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendare.Modules.EventCreate
{
    public partial class EventCreateForm : Form
    {
        public EventCreateForm()
        {
            InitializeComponent();
        }

        public void ShowData(EventItem item)
        {
            label1.Text += item.Id;
            textBox2.Text = item.Name;
            textBox3.Text = item.Description;
            checkBox1.Checked = item.IsAllert;
        }
    }
}
