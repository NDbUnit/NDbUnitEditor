using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NDbUnitDataEditor
{
    public interface IMessageCreator
    {
        void Show(string message);
        void ShowWarning(string message, string caption);
        void ShowWarning(string message);
        void ShowError(string message, string caption);
        void ShowError(string message);
		bool AskUser(string message);
        
    }
}
