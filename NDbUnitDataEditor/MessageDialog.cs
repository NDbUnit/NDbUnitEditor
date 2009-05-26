using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NDbUnitDataEditor
{
    public class MessageDialog : IMessageDialog
    {
        public void Show(string message)
        {
            MessageBox.Show(message);
        }
    }
}
