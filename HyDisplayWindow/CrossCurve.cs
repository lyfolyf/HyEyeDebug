using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;



namespace HyDisplayWindow
{
    public class CrossCurve
    {

        private Control Parent;


        public CrossCurve(Control control)
        {
            Parent = control;
        }

        private bool _visible = false;
        public bool Visible 
        { 
            get 
            { 
                return _visible; 
            }
            set 
            {
                if (_visible != value)
                {
                    _visible = value;
                    Parent.Invalidate();
                }               
            }
        }


        public int LineWidth { get; set; } = 1;

        public int Opacity { get; set; } = 255;

        public Color CrossColor { get; set; } = Color.Blue;

        public int LineX { get; set; }

        public int LineY { get; set; }


        public void DrawCross(Graphics Canvas ,int LengthX, int LengthY)
        {
            if (Visible)
            {
                Pen CrossPen = new Pen(Color.FromArgb(Opacity, CrossColor), LineWidth);

                Canvas.DrawLine(CrossPen, new Point(0, LineX), new Point(LengthX, LineX));
                Canvas.DrawLine(CrossPen, new Point(LineY, 0), new Point(LineY, LengthY));

                CrossPen.Dispose();
            }
        }

    

    }
}
