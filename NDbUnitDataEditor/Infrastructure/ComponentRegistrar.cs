using System;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using NDbUnit.Utility;
using NDbUnitDataEditor.UI;
using System.Reflection;
using Castle.Facilities.TypedFactory;

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
			_container.AddFacility<TypedFactoryFacility>();
            _container.Register
            (
				Component.For<ICommandFactory>().AsFactory(),
                Component.For<IDataEditorView>().ImplementedBy<DataEditor>(),
				Component.For<IDataSetProvider>().ImplementedBy<DataSetProvider>(),
                Component.For<DataEditorPresenter>(),
                Component.For<IFileDialogCreator>().ImplementedBy<FileDialogCreator>().LifeStyle.Transient,
				Component.For<IMessageCreator>().ImplementedBy<MessageCreator>().LifeStyle.Transient,
                Component.For<IDataSetFromDatabaseView>().ImplementedBy<DataSetFromDatabase>().LifeStyle.Transient,
				Component.For<IFileService>().ImplementedBy<FileService>(),
                Component.For<DataSetFromDatabasePresenter>(),
				Component.For<IApplicationController>().ImplementedBy<ApplicationController>(),
				Component.For<IEventAggregator>().ImplementedBy<EventAggregator>()
            );

            //settings persistennce and retrieval
            _container.Register
            (
                Component.For<IUserSettingsRepository>().ImplementedBy<UserSettingsRepository>()
                    .Parameters(Parameter.ForKey("configFileType").Eq(UserSettingsRepository.Config.PrivateFile.ToString()))
                    .Parameters(Parameter.ForKey("applicationName").Eq("NDbUnitEditor")),
                Component.For<IProjectRepository>().ImplementedBy<ProjectRepository>().LifeStyle.Transient
            );

			_container.Register(
				AllTypes.FromAssembly(Assembly.GetExecutingAssembly()).BasedOn<ICommand>());

            //NDbUnit interaction
            _container.Register
            (
                Component.For<NDbUnitFacade>().LifeStyle.Transient
            );

            IoC.Initialize(_container);

        }

    }
}
