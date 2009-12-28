using System;
using MbUnit.Framework;
using System.Collections;
using Rhino.Mocks;
using NDbUnitDataEditor;
using NDbUnitDataEditor.UI;
using NDbUnit.Utility;
using System.IO;


namespace Tests.DataEditorPresenterTests
{
    public class When_saving_restoring_application_settings
    {
        [Test]
        public void CanSaveApplicationSettings()
        {
            var settingsFileName = "ndbUserSettings.xml";
            var view = MockRepository.GenerateStub<IDataEditorView>();
            var dialogFactory = MockRepository.GenerateStub<IDialogFactory>();
            var userSettings = MockRepository.GenerateStub<IUserSettings>();
            var presenter = new DataEditorPresenter(view, dialogFactory, userSettings);

            presenter.SaveSettings(settingsFileName);

            Assert.IsTrue(File.Exists(settingsFileName));
        }
    }
}
