using System;
using System.Collections.Generic;
using System.Text;

using Castle.Windsor;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Rhino.Commons;
using NDbUnit.Utility;


namespace NDbUnitDataEditor
{
    public class ComponentManager
    {
        public virtual void RegisterComponents()
        {
            IWindsorContainer container = new WindsorContainer();

            container.Register(Component.For<IDataEditorView>().ImplementedBy<DataEditor>().LifeStyle.Transient);
            container.Register(Component.For<DataEditorPresenter>().ImplementedBy<DataEditorPresenter>().LifeStyle.Transient);
            container.Register(Component.For<IMessageDialog>().ImplementedBy<MessageDialog>().LifeStyle.Transient);
            container.Register(Component.For<IUserSettings>().ImplementedBy<UserSettings>().Parameters(Parameter.ForKey("configFileType").Eq(UserSettings.Config.PrivateFile.ToString())).Parameters(Parameter.ForKey("applicationName").Eq("NDbUnitEditor")));
            IoC.Initialize(container);
            
        }
    }
}
