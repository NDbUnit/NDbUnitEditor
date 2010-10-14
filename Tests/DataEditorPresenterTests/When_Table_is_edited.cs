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
        public void Tab_Is_Marked_As_Changed_When_DataViewChanged_Event_Is_Fired_And_DataSet_Contains_Changes()
        {
			var tabName = "Tab1";
			IDataEditorView view = MockRepository.GenerateStub<IDataEditorView>();
			IDataSetProvider dataSetProvider = MockRepository.GenerateStub<IDataSetProvider>();
			dataSetProvider.Stub(d => d.HasTableChanged(tabName)).Return(true);
			view.Stub(v => v.DataSetHasChanges()).Return(true);
			IEventRaiser eventRaiser = view.GetEventRaiser(v => v.DataViewChanged+=null);
           
			DataEditorPresenter presenter = CreatePresenter(view, dataSetProvider);
            eventRaiser.Raise(tabName);
			view.AssertWasCalled(v => v.MarkTabAsEdited(tabName), o => o.Repeat.Once());
        }


        [Test]
        public void Tab_Is_Not_Marked_As_Changed_When_DataViewChanged_Event_Is_Fired_And_DataSet_Contains_No_Changes()
        {
			var tabName = "Tab1";
			IDataEditorView view = MockRepository.GenerateStub<IDataEditorView>();
			IDataSetProvider dataSetProvider = MockRepository.GenerateStub<IDataSetProvider>();
			dataSetProvider.Stub(d => d.HasTableChanged(tabName)).Return(false);
			view.Stub(v => v.DataSetHasChanges()).Return(false);
			IEventRaiser eventRaiser = view.GetEventRaiser(v => v.DataViewChanged += null);

			DataEditorPresenter presenter = CreatePresenter(view, dataSetProvider);
			eventRaiser.Raise(tabName);
			view.AssertWasNotCalled(v => v.MarkTabAsEdited(tabName));
        }

		private DataEditorPresenter CreatePresenter(IDataEditorView view, IDataSetProvider datasetProvider)
		{
			var presenter = new DataEditorPresenter(view, null, null, null, datasetProvider);
			return presenter;
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
            return dataset;

        }
    }
}
