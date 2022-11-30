using GL.Kit.Log;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HyEye.WForm
{
    public partial class FormLog : DockContent
    {
        readonly DisplayAdapter m_log;
        LogLevel m_level = LogLevel.All;

        public FormLog(DisplayAdapter log)
        {
            InitializeComponent();

            m_log = log;

            m_log.NewLog += Log_NewLog;
            m_log.ClearCurrent += Log_AutoClear;
        }

        private void Log_NewLog(LogInfo log)
        {
            if (Visible)
            {
                rtbLogs.AsyncAction((c) =>
                {
                    Color color = GetLogColor(log.Level);

                    RichTextBox rtb = (RichTextBox)c;
                    rtb.AppendLine(log.ToString(LogFormat.General), color);
                    rtb.ScrollToCaret();
                });
            }
        }

        private void Log_AutoClear()
        {
            rtbLogs.AsyncAction((c) =>
            {
                (c as RichTextBox).Clear();
            });
        }

        readonly Color InfoColor = SystemColors.WindowText;
        readonly Color WarnColor = Color.FromArgb(0xFF, 0xA5, 0x00);
        readonly Color ErrorColor = Color.Red;

        private Color GetLogColor(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Info:
                    return InfoColor;
                case LogLevel.Warn:
                    return WarnColor;
                case LogLevel.Error:
                case LogLevel.Fatal:
                    return ErrorColor;
                default:
                    return InfoColor;
            }
        }

        private void LogLevelDisplayedChanged(object sender, EventArgs e)
        {
            if (tbLogLevel.SelectedIndex == 0)
            {
                m_level = LogLevel.All;
            }
            else if (tbLogLevel.SelectedIndex == 1)
            {
                m_level = LogLevel.Info;
            }
            else if (tbLogLevel.SelectedIndex == 2)
            {
                m_level = LogLevel.Warn;
            }
            else if (tbLogLevel.SelectedIndex == 3)
            {
                m_level = LogLevel.Error;
            }

            m_log.LevelChanged(m_level);
        }

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            m_log.Clear(m_level);
        }
    }
}
