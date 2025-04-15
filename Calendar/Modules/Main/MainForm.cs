using Calendare.LibClasses.Interfaces;
using Calendare.LibClasses.ViewHelper;
using System;
using System.Windows.Forms;
using Calendare.LibClasses.DataBase;

namespace Calendare.Modules.Main
{
    public partial class MainForm : Form, IFormShower
    {
        private Control[] controlShowers;


        public MainForm()
        {
            InitializeComponent();

            controlShowers = new Control[]
            {
                splitContainer1.Panel1,
                splitContainer1.Panel2
            };

            new Calendare.LibClasses.WindowsDragController(toolStrip1);
        }

        void IFormShower.ShowOtherForm(Form form, int index)
        {
            if (index < 0 || index > controlShowers.Length)
                throw new Exception("Не верно выбрано количество показывающих контролов");


            FormViewHelper.ShowFormInPanel(controlShowers[index], form);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            EventBus.OnExit?.Invoke();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            EventBus.OnDBExport?.Invoke();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            EventBus.OnDBImport?.Invoke();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            EventBus.OnSimulateDate?.Invoke();
        }
    }
}
