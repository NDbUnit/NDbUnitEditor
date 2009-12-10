using System;
using System.Collections.Generic;
using System.Text;
using Castle.Windsor;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Rhino.Commons;
using NDbUnit.Utility;
using NDbUnitDataEditor.UI;

namespace NDbUnitDataEditor
{
    public class ComponentRegistrar
    {
        public virtual void RegisterComponents()
        {
            IWindsorContainer container = new WindsorContainer();

            container.Register(Component.For<IDataEditorView>().ImplementedBy<DataEditor>().LifeStyle.Transient);
            container.Register(Component.For<DataEditorPresenter>().LifeStyle.Transient);
            container.Register(Component.For<IDialogFactory>().ImplementedBy<DialogFactory>().LifeStyle.Transient);
            container.Register(Component.For<IUserSettings>().ImplementedBy<UserSettings>()
                .Parameters(Parameter.ForKey("configFileType").Eq(UserSettings.Config.PrivateFile.ToString()))
                .Parameters(Parameter.ForKey("applicationName").Eq("NDbUnitEditor")));
            container.Register(Component.For<IDataSetFromDatabaseView>().ImplementedBy<DataSetFromDatabase>().LifeStyle.Transient);
            container.Register(Component.For<DataSetFromDatabasePresenter>().LifeStyle.Singleton);
            container.Register(Component.For<NDbUnitFacade>().LifeStyle.Transient);
            IoC.Initialize(container);

        }

    }
}
