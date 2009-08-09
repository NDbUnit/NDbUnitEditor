using System;

namespace NDbUnitDataEditor
{
    public class TableViewEventArguments : EventArgs
    {
        private string _tabName = null;

        public TableViewEventArguments(string tabName)
        {
            _tabName = tabName;
        }

        public string TabName
        {
            get
            {
                return _tabName;
            }
        }

    }
}