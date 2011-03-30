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
using NDbUnit.Utility;
using System.Threading;

namespace Tests.DataEditorPresenterTests
{

	[TestFixture]
	public class WhenNavigatingTables: PresenterTestBase
	{

		[SetUp]
		public void TestSetup()
		{
			GenerateStubs();
		}

		[Test]
		public void ShouldOpenNewTabWhenDblClickedOnTreeNode()
		{
			var selectedTableName = "Customers";
			var selectedTable = new DataTable();
			datasetProvider.Stub(d => d.GetTable(selectedTableName)).Return(selectedTable);
			var presenter = new DataEditorPresenter(applicationController,view, null, null, null, null, datasetProvider, null);

			RaiseTreeNodeDblClicked("Customers");
			view.AssertWasCalled(v => v.OpenTableView(selectedTable));
		}

		[Test]
		public void ShouldNotOpenNewTableWhenTableDoesNotExists()
		{
			var selectedTableName = "Customers";
			datasetProvider.Stub(d => d.GetTable(selectedTableName)).Return(null);
			var presenter = new DataEditorPresenter(applicationController,view, null, null, null, null, datasetProvider, null);

			RaiseTreeNodeDblClicked("Customers");
			view.AssertWasNotCalled(v => v.OpenTableView(null));
		}

		[Test]
		public void ShouldUpdateStatusBarWithRowCountWhenTabIsOpened()
		{
			var selectedTableName = "Customers";
			var selectedTableInfo = new DataTableInfo { NumberOfRows = 12 };
			var expectedStatusLabel = String.Format("Rows: {0}", selectedTableInfo.NumberOfRows);
			datasetProvider.Stub(d => d.GetTableInfo(selectedTableName)).Return(selectedTableInfo);
			var presenter = new DataEditorPresenter(applicationController, view, null, null, null, null, datasetProvider, null);
			RaiseTabSelected("Customers");

			Assert.AreEqual(expectedStatusLabel, view.StatusLabel);
		}

		private void RaiseTabSelected(string tabName)
		{
			IEventRaiser eventRaiser = view.GetEventRaiser(v => v.TabSelected += null);
			eventRaiser.Raise(tabName);
		}

		private void RaiseTreeNodeDblClicked(string tableName)
		{
			IEventRaiser eventRaiser = view.GetEventRaiser(v => v.TableTreeNodeDblClicked += null);
			eventRaiser.Raise(tableName);
		}



	}
}
