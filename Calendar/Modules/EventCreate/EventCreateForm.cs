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
        private const string tab = "    ";

        public EventCreateForm()
        {
            InitializeComponent();
            new Calendare.LibClasses.WindowsDragController(this);
            foreach(var c in this.Controls)
            {
                if(c is Label) new Calendare.LibClasses.WindowsDragController(c as Control);
            }
        }

        public void ShowData(EventItem item)
        {
            label_ID.Text = $"id: {item.Id}";
            label_ID.Text += $"\n{tab}Дата: {item.dateTime.ToString("d.mm.y", ' ')}";
            textBox1.Text = item.Name;
            textBox2.Text = item.Description;
            checkBox1.Checked = item.IsAllert;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label_Count1.Text = textBox1.Text.Length + "/" + textBox1.MaxLength;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label_Count2.Text = textBox2.Text.Length + "/" + textBox2.MaxLength;
        }

        private void EventCreateForm_Load(object sender, EventArgs e)
        {
            label_Count1.Text = textBox1.Text.Length + "/" + textBox1.MaxLength;
            label_Count2.Text = textBox2.Text.Length + "/" + textBox2.MaxLength;

            this.BackColor = Config.Settings.BackGroundColor;
            this.ForeColor = Config.Settings.ForeColor;
        }
    }
}
