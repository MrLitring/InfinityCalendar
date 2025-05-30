﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calendare.Modules;

namespace Calendare
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Config.Settings.Initilization();
            Calendare.LibClasses.DataBase.SqlController.OpenOrCreateConnection();

            Modules.Main.Controller mainFormShower = new Modules.Main.Controller(new Modules.Main.MainForm());

            Modules.Controller controller = new Controller(new object[] {
               mainFormShower.form,
               new Modules.Calendar.CalendarForm(),
               new Modules.Event.EventForm(),
               new Modules.Event.EventModel(),
               new Modules.Calendar.CalendarModel()
                }
             );

            controller.Main();
            Application.Run(controller.GetMainViewForm);

        }
    }
}
