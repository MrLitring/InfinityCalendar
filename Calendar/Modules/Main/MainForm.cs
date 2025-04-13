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
        private string path = "\\InfinityCalendar\\calendar.xml";


        public MainForm()
        {
            InitializeComponent();

            controlShowers = new Control[]
            {
                splitContainer1.Panel1,
                splitContainer1.Panel2
            };

        }

        void IFormShower.ShowOtherForm(Form form, int index)
        {
            if (index < 0 || index > controlShowers.Length)
                throw new Exception("Не верно выбрано количество показывающих контролов");


            FormViewHelper.ShowFormInPanel(controlShowers[index], form);
        }

        private void btnDBExport_Click(object sender, EventArgs e)
        {
            EventBus.OnDBExport?.Invoke();
        }

        private void btnDBImport_Click(object sender, EventArgs e)
        {
            EventBus.OnDBImport?.Invoke();
        }

        private void splitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
        //    if(splitContainer1.Panel1.Height <= splitContainer1.Panel1MinSize) splitContainer1.Panel1Collapsed = true;
        //    else splitContainer1.Panel1Collapsed = false;
        }
    }
}
