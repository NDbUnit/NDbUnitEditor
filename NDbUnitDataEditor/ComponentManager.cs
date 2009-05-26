using System;
using System.Collections.Generic;
using System.Text;

using Castle.Windsor;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Rhino.Commons;


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
            IoC.Initialize(container);
        }
    }
}
