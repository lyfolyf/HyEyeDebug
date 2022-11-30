using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace HyDisplayWindow
{
    public class MessageManager
    {

        private List<MessageInfo> Messages = new List<MessageInfo>();


        public void AddMessage(MessageInfo NewMessage)
        {
            Messages.Add(NewMessage);
        }

        public void RemoveMessage(MessageInfo InputMessage)
        {
            Messages.Remove(InputMessage);
        }

        public void ClearMessage()
        {
            Messages.Clear();
        }

        public void SetMessageVisible(bool visible)
        {
            foreach (MessageInfo Info in Messages)
            {
                Info.Visible = visible;
            }
        }

        public void SetMessageFont(Font TextFont)
        {
            foreach (MessageInfo Info in Messages)
            {
                Info.TextFont = TextFont;
            }
        }

        public void SetMessageOpacity(int Opacity)
        {
            if (Opacity < 0)
            {
                Opacity = 0;
            }
            if (Opacity > 255)
            {
                Opacity = 255;
            }

            foreach (MessageInfo Info in Messages)
            {
                Info.Opacity = Opacity;
            }

        }

        public void DisplayMessages(Graphics Canvas)
        {
            foreach (MessageInfo Info in Messages)
            {
                if (Info.Visible)
                {
                    Brush brush = new SolidBrush(Color.FromArgb(Info.Opacity, Info.TextColor));

                    if (Info.ShadingVisible)
                    {
                        SizeF sizeF = Canvas.MeasureString(Info.Text, Info.TextFont);
                        Brush BlackBrush = new SolidBrush(Color.FromArgb(Info.Opacity, Color.Gray));
                        Canvas.FillRectangle(BlackBrush, new RectangleF(Info.TextLocation, sizeF));
                    }
                  
                    Canvas.DrawString(Info.Text, Info.TextFont, brush, Info.TextLocation);
                }
            }
        }

   
    
    }




    public class MessageInfo
    {

        public MessageInfo()
        {

        }

        public MessageInfo(string Message) : this()
        {
            this.Text = Message;
        }

        public MessageInfo(string Message, Point Location) : this(Message)
        {
            this.TextLocation = Location;
        }



        public string Text { get; set; }

        public Point TextLocation { get; set; } = new Point(0, 0);

        public Font TextFont { get; set; } = new Font("Arial", 50);

        public Color TextColor { get; set; } = Color.Lime;

        public int Opacity { get; set; } = 200;

        public bool Visible { get; set; } = true;

        public bool ShadingVisible { get; set; } = true;

    }



}
