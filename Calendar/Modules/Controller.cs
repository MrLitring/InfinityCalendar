using Calendare.LibClasses;
using Calendare.LibClasses.Interfaces;
using ViewHelper = Calendare.LibClasses.ViewHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Calendare.LibClasses.DataBase;
using Calendare.Modules.EventCreate;

namespace Calendare.Modules
{
    /// <summary>
    /// Контроллер, отвечаюший за взаимодействие между предстапвлением и бизнес логикой.
    /// Паттерны программирования: MVC + Наблюдатель
    /// </summary>
    public class Controller
    {
        private readonly IFormShower mainView;
        private readonly IFormCalendare calendarView;
        private readonly IFormEvent eventView;

        private readonly Modules.Calendar.CalendarModel calendarModel;
        private readonly Modules.Event.EventModel eventModel;

        private CustomDataTable tables;


        public Controller(IEnumerable<object> args)
        {
            mainView = args.OfType<IFormShower>().FirstOrDefault();
            calendarView = args.OfType<IFormCalendare>().FirstOrDefault();
            eventView = args.OfType<IFormEvent>().FirstOrDefault();

            calendarModel = args.OfType<Modules.Calendar.CalendarModel>().FirstOrDefault();
            eventModel = args.OfType<Modules.Event.EventModel>().FirstOrDefault();

        }

        public void Main()
        {
            if (IsCanWork() == false) throw new Exception("Отсутвует возможность работать корректно");

            eventModel.dateTime = calendarModel.dateTime;

            mainView.ShowOtherForm(calendarView as Form, 0);
            mainView.ShowOtherForm(eventView as Form, 1);

            tables = NewDataTable();
            FullLoadDataInDataGridView ();
            TextUpdate();

            AddLiesten();
            CMSEvent();

        }

        public Form GetMainViewForm { get { return (mainView as Form); } }


        // Подписываемся как слушатель на события
        private void AddLiesten()
        {
            EventBus.OnIncreaseDate += () => IncreaseData();
            EventBus.OnDecreaseDate += () => IncreaseData(-1);
            EventBus.OnCalendarCellRightMouseClick += (CellPoint cellPoint) => { CMSInvoke(); };

            EventBus.OnEditModeChange += EditChange;
            EventBus.OnCalendarCellLeftMouseClick +=  LeftClick;

            EventBus.OnRestartDate += ResetDateTime;
        }


        private void LeftClick(CellPoint cellPoint)
        {
            if (cellPoint.IsNotOutBorder(cellPoint) && tables.DateTable.Rows[cellPoint.row][cellPoint.column] != null)
            {
                calendarView.CellFocus(cellPoint);
                object obj = calendarModel.GetValueDataTable(cellPoint);
                if (obj == null || string.IsNullOrEmpty(obj.ToString())) return;
                calendarModel.DataChange((int)obj);
                TextUpdate();
            }
        }

        private void EditChange()
        {
            calendarModel.SwitchEditMode();
            TextUpdate();
            FullLoadDataInDataGridView();
        }

        private void FullLoadDataInDataGridView()
        {
            tables = NewDataTable();
            CalendarDataGridViewUpdate();
            EventDataGridViewUpdate();

            calendarView.CellFocus(tables.SearchValueOfTable(calendarModel.GetCurrentDate.Split(' ')[(int)calendarModel.EditMode]));
        }

        private void CalendarDataGridViewUpdate()
        {
            calendarView.DataGridViewUpdate(tables);
        }

        private void EventDataGridViewUpdate()
        {
            eventView.DataGridViewUpdate(tables);
        }


        private bool IsCanWork()
        {
            if (mainView == null || calendarView == null || eventView == null || calendarModel == null || eventModel == null) return false;
            return true;
        }

        private CustomDataTable NewDataTable()
        { 
            DataTable dateTable = calendarModel.GetDataTable(out bool isVisibleHeader);
            DataTable eventTable = eventModel.GetDataTable(dateTable);

            return new CustomDataTable(dateTable, isVisibleHeader, eventTable);
        }

        private void TextUpdate()
        {
            calendarView.LabelTextUpdate(0, calendarModel.GetNowDate);
            calendarView.LabelTextUpdate(1, calendarModel.GetFromEditMode);
        }

        private void IncreaseData(int value = 1)
        {
            calendarModel.IncreaseDate(value);
            FullLoadDataInDataGridView();
            TextUpdate();
        }

        private void CMSInvoke()
        {
            bool isHasEvent = false;
            ContextMenuStrip cms = calendarView.GetCMS();
            calendarView.CMSShow(ViewHelper.ContexMenuStripExtends.CMSCheckForDesign(cms, isHasEvent));
        }

        private void CMSEvent()
        {
            ContextMenuStrip cms = calendarView.GetCMS();


            foreach (var elem in cms.Items)
            {
                if (elem is ToolStripMenuItem)
                {
                    ToolStripMenuItem tool = elem as ToolStripMenuItem;

                    if (tool.Name == "btnAddEvent") tool.Click += ToolAdd_Click;
                }
            }
        }

        private void ToolAdd_Click(object sender, EventArgs e)
        {
            EventCreateController eventCreateController = new EventCreateController(
                EventCreateModel.Mode.Create, new EventItem(EventItem.NewEmpty(SQLEventItem.Max() + 1, calendarModel.dateTime)) );
        }

        private void ResetDateTime()
        {
            calendarModel.ResetDate();
            FullLoadDataInDataGridView();
            TextUpdate();
        }

    }
}
