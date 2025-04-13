using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendare.LibClasses;

namespace Calendare
{
    public static class EventBus
    {
        public static Action OnIncreaseDate;
        public static Action OnDecreaseDate;

        public static Action OnEditModeChange;
        public static Action OnRestartDate;

        public static Action<CellPoint> OnCalendarCellLeftMouseClick;
        public static Action<CellPoint> OnCalendarCellRightMouseClick;

        public static Action<CellPoint> OnEventMouseClick;

        public static Action<CellPoint> OnEventCellLeftMouseClick;
        public static Action<CellPoint> OnEventCellRightMouseClick;

        public static Action OnNewEvent;
        public static Action OnEditEvent;
        public static Action OnAddEvent;
        public static Action OnRemoveEvent;


        public static Action OnDBExport;
        public static Action OnDBImport;
        
    }
}
