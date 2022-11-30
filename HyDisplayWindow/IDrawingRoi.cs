using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;






namespace HyDisplayWindow
{
    public interface IDrawingRoi
    {

        void OnMouseDoubleClick(MouseEventArgs e);

        void OnMouseDown(MouseEventArgs e, PointF ImagePoint);

        void OnMouseUp(MouseEventArgs e, PointF ImagePoint);

        //void OnMouseWheel(MouseEventArgs e);

        void OnMouseMove(MouseEventArgs e, PointF ImagePoint);

        void DisplayHyRoi(Graphics Canvas);

    }
}
