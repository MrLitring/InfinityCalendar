using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendare.LibClasses
{
    public class WindowsDragController
    {
        private readonly Control dragControl;
        private bool isDragging;
        Point dragStartPosition;

        public WindowsDragController(Control dragControl)
        {
            this.dragControl = dragControl;
            dragControl.MouseDown += OnMouseDown;
            dragControl.MouseMove += MouseMove;
            dragControl.MouseUp += MouseUp;
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if(isDragging)
            {
                Form form = dragControl.FindForm();
                if(form != null)
                {
                    Point newPosition = form.PointToScreen(e.Location);
                    form.Location = new Point(
                        newPosition.X - dragStartPosition.X,
                        newPosition.Y - dragStartPosition.Y
                        );
                }
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPosition = e.Location;
            }
        }
    }
}
