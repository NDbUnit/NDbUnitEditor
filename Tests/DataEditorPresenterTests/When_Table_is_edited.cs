using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using NDbUnitDataEditor;
using NDbUnitDataEditor.UI;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Tests.DataEditorPresenterTests
{
    [TestFixture]
    public class When_Table_is_edited
    {
        MockRepository _mocks = new MockRepository();
        [Test]
        public void Table_Name_Is_Passed_When_DataViewChanged_Event_Is_Fired()
        {
            IDataEditorView view = _mocks.DynamicMock<IDataEditorView>();
            IEventRaiser eventRaiser = null;
            DataSet dataSet = CreateDataSet();
            using (_mocks.Record())
            {
                view.DataViewChanged += null;
                eventRaiser = LastCall.IgnoreArguments().GetEventRaiser();
                Expect.Call(() => view.MarkTabAsEdited("Tab1")).Repeat.Once();
                
                Expect.Call(view.Data).Return(dataSet);
            }
            _mocks.ReplayAll();
            DataEditorPresenter presenter = new DataEditorPresenter(view, null,null);
            TableViewEventArguments eventArguments = new TableViewEventArguments("Tab1");
            eventRaiser.Raise(eventArguments);
            _mocks.VerifyAll();
        }

        private DataSet CreateDataSet()
        {
            var dataset = new DataSet();
            DataTable table = dataset.Tables.Add("Tab1");
            table.Columns.Add("Id");
            table.Columns.Add("Name");
            DataRow row = table.NewRow();
            row["Id"] = 1;
            table.Rows.Add(row);
            //table.AcceptChanges();
            return dataset;

        }
    }
}
