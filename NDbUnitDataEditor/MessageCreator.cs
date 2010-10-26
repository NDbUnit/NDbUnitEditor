using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NDbUnitDataEditor
{
    public class MessageCreator : IMessageCreator
    {
        public void Show(string message)
        {
            MessageBox.Show(message);
        }

        public void ShowError(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowError(string message)
        {
            ShowError(message, "Error!");
        }

        public void ShowWarning(string message)
        {
            ShowWarning(message, "Warning!");
        }

        public void ShowWarning(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

		public bool AskUser(string message)
		{
			if (MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				return true;
			return false;

		}
	}



}
