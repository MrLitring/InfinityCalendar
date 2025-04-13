using Calendare.LibClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }


    }
}
