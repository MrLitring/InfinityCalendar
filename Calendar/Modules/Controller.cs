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
using System.Drawing;
using System.Text;

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

        ContextMenuStrip calendae_cms = null;
        ContextMenuStrip event_cms = null;




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
            try
            {
                if (IsCanWork() == false) throw new Exception("Отсутвует возможность работать корректно");

                eventModel.dateTime = calendarModel.dateTime;

                mainView.ShowOtherForm(calendarView as Form, 0);
                mainView.ShowOtherForm(eventView as Form, 1);

                FullLoadDataInDataGridView();
                TextUpdate();

                AddLiesten();
                EventInvoke();
                calendarModel.LastCellPoint = CellPoint.SearchValueOfTable(calendarModel.dateTime.Day, tables.DateTable);
                calendarView.CellFocus(calendarModel.LastCellPoint);

                calendae_cms = calendarView.GetCMS();
                event_cms = eventView.GetCMS();

                CMSEventAdd(calendae_cms);
                CMSEventAdd(event_cms);
            }
            catch (Exception ex)
            {
                Notifity.Invoke("Main - \n" + ex.Message.ToString(), Properties.Resources.imageres_98);
            }
        }

        public Form GetMainViewForm { get { return (mainView as Form); } }


        // Подписываемся как слушатель на события
        private void AddLiesten()
        {
            EventBus.OnIncreaseDate += () => IncreaseData();
            EventBus.OnDecreaseDate += () => IncreaseData(-1);
            EventBus.OnCalendarCellRightMouseClick += (CellPoint cellPoint) => { calendarView.CMSShow(calendae_cms); };
            EventBus.OnEventCellRightMouseClick += (CellPoint cellPoint) => { eventModel.cellPoint = cellPoint; eventView.CMSShow(event_cms); };

            EventBus.OnEditModeChange += EditChange;
            EventBus.OnCalendarCellLeftMouseClick += LeftClick;

            EventBus.OnRestartDate += ResetDateTime;
            EventBus.OnSimulateDate += EventInvoke;
        }

        private void EventInvoke()
        {
            Notifity.Invoke(eventModel.dayEvents.Count > 0? Notifity.EventContent(eventModel.dayEvents) : "На сегодня нету никаких событий");
        }

        private void LeftClick(CellPoint cellPoint)
        {
            calendarModel.LastCellPoint = cellPoint;


            if (cellPoint.IsNotOutBorder(cellPoint) && tables.DateTable.Rows[cellPoint.row][cellPoint.column] != null)
            {
                object obj = calendarModel.GetValueDataTable(cellPoint);
                if (obj == null || string.IsNullOrEmpty(obj.ToString())) return;
                calendarModel.DataChange((int)obj);
                TextUpdate();
                FullLoadDataInDataGridView();

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
            eventModel.DataChange(calendarModel.dateTime);
            tables = NewDataTable();
            calendarView.KeepTable(tables);
            eventView.KeepTable(tables);


            string[] dateParts_1 = calendarModel.GetCurrentDate.Split(' ');
            string[] dateParts_2 = calendarModel.GetNowDate.Split(' ');

            if (calendarModel.EditMode == Calendar.CalendarModel.CalendarModeType.DayMode)
            {
                if (dateParts_1[1] == dateParts_2[1] && dateParts_1[2] == dateParts_2[2])
                {
                    CellPoint now = CellPoint.SearchValueOfTable(dateParts_2[0], tables.DateTable);
                    calendarView.CellPaint(now, Config.Settings.Today, Color.White);
                }
            }

            calendarView.CellFocus(calendarModel.LastCellPoint);
        }


        private bool IsCanWork()
        {
            if (mainView == null || calendarView == null || eventView == null || calendarModel == null || eventModel == null) return false;
            return true;
        }

        private CustomDataTable NewDataTable()
        {
            CustomDataTable dataTable = new CustomDataTable();
            dataTable.DateTable = calendarModel.GetDataTable(out bool isVisibleHeader);
            dataTable.isVisibleHeader = isVisibleHeader;
            dataTable.EventTable = eventModel.GetEventTable(dataTable.DateTable);
            dataTable.ListEventTable = eventModel.GetDayDataTable();

            return dataTable;
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



        private void ResetDateTime()
        {
            if (calendarModel.EditMode != Calendar.CalendarModel.CalendarModeType.DayMode) return;

            calendarModel.ResetDate();
            FullLoadDataInDataGridView();
            TextUpdate();

            calendarView.CellFocus(CellPoint.SearchValueOfTable(calendarModel.dateTime.Day, tables.DateTable));
        }



        private void CMSEventAdd(ContextMenuStrip cms)
        {
            foreach (var elem in cms.Items)
            {
                if (elem is ToolStripMenuItem)
                {
                    ToolStripMenuItem tool = elem as ToolStripMenuItem;

                    if (tool.Name == "btnAddEvent") tool.Click += ToolCMS_Add;
                    else if (tool.Name == "btnDeleteEvent") tool.Click += ToolCMS_Delete;
                    else if (tool.Name == "btnReplaceEvent") tool.Click += ToolCMS_Replace;
                }
            }
        }

        private void ToolCMS_Replace(object sender, EventArgs e)
        {
            CellPoint cellPoint = eventModel.cellPoint;
            EventCreateController eventCreateController = new EventCreateController(
                EventCreateModel.Mode.Edit, eventModel.events[cellPoint.row]);


            FullLoadDataInDataGridView();
        }

        private void ToolCMS_Delete(object sender, EventArgs e)
        {
            CellPoint cellPoint = eventModel.cellPoint;
            SQLEventItem item = null;
            if (cellPoint.IsNotOutBorder(cellPoint))
            {
                if (cellPoint.row <= eventModel.dayEvents.Count)
                {
                    item = new SQLEventItem(eventModel.dayEvents[cellPoint.row], SQLEventItem.DataOperation.Delete);
                    item.Execute();
                }
            }

            FullLoadDataInDataGridView();
        }

        private void ToolCMS_Add(object sender, EventArgs e)
        {
            EventCreateController eventCreateController = new EventCreateController(
                EventCreateModel.Mode.Create, new EventItem(EventItem.NewEmpty(SQLEventItem.Max() + 1, calendarModel.dateTime)));

            FullLoadDataInDataGridView();
        }

    }
}
