using Calendare.Modules.Event;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace Calendare.LibClasses
{
    public static class Notifity
    {
        private const int imageSize = 94;
        private const int padding = 10;
        private const int HeaderHeight = 1;


        public static void Invoke(string content, Image image = null)
        {
            var notify = new PopupNotifier();
            Notifity_Design(notify);

            notify.Image = image;
            notify.ImageSize = new System.Drawing.Size(imageSize, imageSize);

            notify.ContentText = content;
            notify.Popup();

        }

        static void Notifity_Design(PopupNotifier notify)
        {
            notify.BodyColor = Config.Settings.BackGroundColor;
            notify.ContentColor = Config.Settings.ForeColor;
            notify.HeaderHeight = HeaderHeight;

            notify.TitleText = "InfinityCalendar";

            notify.ContentPadding = new Padding(padding);
        }

        public static string EventContent(List<EventItem> items)
        {
            StringBuilder content = new StringBuilder();

            foreach (EventItem item in items)
            {
                if (item.IsAllert)
                {
                    content.AppendLine(item.Name + "\t" + item.Description);
                }
            }

            string text = content.ToString();

            return text;
        }
    }
}
