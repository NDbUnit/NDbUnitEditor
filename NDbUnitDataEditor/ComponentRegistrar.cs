using System;
using System.Collections.Generic;
using System.Text;
using Castle.Windsor;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Rhino.Commons;
using NDbUnit.Utility;
using NDbUnitDataEditor.UI;
using NDbUnitDataEditor.Abstractions;
using NDbUnitDataEditor.ZeusSchemaBuilder;

namespace NDbUnitDataEditor
{
    public class ComponentRegistrar
    {
        private IWindsorContainer _container;

        /// <summary>
        /// Initializes a new instance of the ComponentRegistrar class.
        /// </summary>
        public ComponentRegistrar()
        {
            _container = new WindsorContainer();
        }

        public IWindsorContainer Container
        {
            get { return _container; }
        }

        public virtual void RegisterComponents()
        {

            //presenters and views
            _container.Register
            (
                Component.For<IDataEditorView>().ImplementedBy<DataEditor>().LifeStyle.Transient,
                Component.For<DataEditorPresenter>().LifeStyle.Transient,
                Component.For<IDialogFactory>().ImplementedBy<DialogFactory>().LifeStyle.Transient,
                Component.For<IDataSetFromDatabaseView>().ImplementedBy<DataSetFromDatabase>().LifeStyle.Transient,
                Component.For<DataSetFromDatabasePresenter>().LifeStyle.Singleton
            );

            //settings persistennce and retrieval
            _container.Register
            (
                Component.For<IUserSettings>().ImplementedBy<UserSettings>()
                    .Parameters(Parameter.ForKey("configFileType").Eq(UserSettings.Config.PrivateFile.ToString()))
                    .Parameters(Parameter.ForKey("applicationName").Eq("NDbUnitEditor")),
                Component.For<INdbUnitEditorSettingsManager>().ImplementedBy<NdbUnitEditorSettingsManager>().LifeStyle.Transient
            );

            //NDbUnit interaction
            _container.Register
            (
                Component.For<NDbUnitFacade>().LifeStyle.Transient
            );

            //MyGeneration Schema-generation components
            _container.Register
            (
                Component.For<ISchemaBuilder>().ImplementedBy<SchemaBuilder>().LifeStyle.Transient,
                Component.For<ConnectionStringValidator>().LifeStyle.Transient,
                Component.For<ConnectionStringProviderBuilder>().LifeStyle.Transient                
            );

            IoC.Initialize(_container);

        }

    }
}
