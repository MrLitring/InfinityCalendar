using Calendare.LibClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendare.Modules.Main
{
    internal class Controller
    {
        public IFormShower form { get; private set; }


        private MainFormModel model;



        public Controller(IFormShower form) 
        {
            this.form = form;

            this.model = new MainFormModel();
            

            Liestener();
        }


        private void Liestener()
        {
            EventBus.OnExit += Exit;
            EventBus.OnDBExport += ExportDB;
            EventBus.OnDBImport += ImportDB;
        }

        private void ImportDB()
        {
            string path = string.Empty;

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = Config.Settings.GetImportPathOfXmlDocument;
                dialog.Filter = "xml file (*.xml)|*.xml";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.FileName;
                }
            }

            if (path == string.Empty) return;

            model.ImportDataBase(path);
        }

        private void ExportDB()
        {
            model.ExportDataBase();
        }

        private void Exit()
        {
            Application.Exit();
        }

    }
}
