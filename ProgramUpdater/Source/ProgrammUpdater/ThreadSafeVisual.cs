using System;
using System.Drawing;
using System.Windows.Forms;

class ThreadSafe
{
    public static void WriteLog(RichTextBox _logTextBox, string Text)
    {
        string Time = DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy | ");

        if (_logTextBox.InvokeRequired)
        {
            _logTextBox.Invoke(new MethodInvoker(delegate
            {
                _logTextBox.AppendText(Time + Text + "\n");
                _logTextBox.Select(_logTextBox.Text.LastIndexOf('|'), 1);
                _logTextBox.SelectionFont = new System.Drawing.Font(_logTextBox.Font.FontFamily, _logTextBox.Font.Size, FontStyle.Bold);
                _logTextBox.ScrollToCaret();
            }));
        }
        else
        {
            _logTextBox.AppendText(Time + Text + "\n");
            _logTextBox.Select(_logTextBox.Text.LastIndexOf('|'), 1);
            _logTextBox.SelectionFont = new System.Drawing.Font(_logTextBox.Font.FontFamily, _logTextBox.Font.Size, FontStyle.Bold);
            _logTextBox.ScrollToCaret();
        }
    }

    public static void SetStatusLabel(ToolStripItem _label, string Text)
    {
        if (_label.GetCurrentParent().InvokeRequired)
        {
            _label.GetCurrentParent().Invoke(new MethodInvoker(delegate
            {
                _label.Text = Text;
            }));
        }
        else
        {
            _label.Text = Text;
        }
    }

    public static void SetProgressBar(ProgressBar _progressBar, int minimum, int maximum, int current)
    {
        if (_progressBar.InvokeRequired)
        {
            _progressBar.Invoke(new MethodInvoker(delegate
            {
                _progressBar.Minimum = minimum;
                _progressBar.Maximum = maximum;
                _progressBar.Value = current;
            }));
        }
        else
        {
            _progressBar.Minimum = minimum;
            _progressBar.Maximum = maximum;
            _progressBar.Value = current;
        }
    }
}
