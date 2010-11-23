using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel;
using NDbUnitDataEditor;
using MbUnit.Framework;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Rhino.Mocks;
using Castle.Facilities.TypedFactory;

namespace Tests
{

	public class CommandLogger
	{
		public string Log { get; set; }
	}
	public class Command1 : ICommand
	{
		private CommandLogger _logger;
        /// <summary>
		/// Initializes a new instance of the Command1 class.
		/// </summary>
		public Command1(CommandLogger logger)
		{
			_logger = logger;
			
		}
		public void Execute()
		{
			_logger.Log = "Command1";
		}
	}

	public class Command2 : ICommand
	{
		private CommandLogger _logger;
        /// <summary>
		/// Initializes a new instance of the Command2 class.
		/// </summary>
		public Command2(CommandLogger logger)
		{
			_logger = logger;
			
		}
		public void Execute()
		{
			_logger.Log = "Command2";
		}
	}

	public class ParametrizedCommand1 : IParametrizedCommand<string>
	{
		private CommandLogger _logger;
        /// <summary>
		/// Initializes a new instance of the ParametrizedCommand1 class.
		/// </summary>
		public ParametrizedCommand1(CommandLogger logger)
		{
			_logger = logger;
			
		}

		public void Execute(string text)
		{
			_logger.Log = text;
		}
	}

	public class ParametrizedCommand2 : IParametrizedCommand<int>
	{
		private CommandLogger _logger;
		/// <summary>
		/// Initializes a new instance of the ParametrizedCommand1 class.
		/// </summary>
		public ParametrizedCommand2(CommandLogger logger)
		{
			_logger = logger;

		}

		public void Execute(int number)
		{
			_logger.Log = number.ToString();
		}
	}

	[TestFixture]
	public class ApplicationControllerTests
	{
		[Test]
		public void ShouldExecuteCommand()
		{
			string log="";
			var eventAggregator = MockRepository.GenerateStub<IEventAggregator>();
			var container = new WindsorContainer();
			container.AddFacility<TypedFactoryFacility>();
			container.Register(
				Component.For<ICommandFactory>().AsFactory(),
				Component.For<IApplicationController>().ImplementedBy<ApplicationController>(),
				Component.For<IEventAggregator>().Instance(eventAggregator),

				Component.For<CommandLogger>(),
				Component.For<Command1>(),
				Component.For<Command2>()
				);
			var controller = container.Resolve<IApplicationController>();
			var logger = container.Resolve<CommandLogger>();
			controller.ExecuteCommand<Command1>();
			Assert.AreEqual("Command1", logger.Log);
			controller.ExecuteCommand<Command2>();
			Assert.AreEqual("Command2", logger.Log);
		}

		[Test]
		public void ShouldCreateParametrizedCommands()
		{
			var eventAggregator = MockRepository.GenerateStub<IEventAggregator>();
			var container = new WindsorContainer();
			container.AddFacility<TypedFactoryFacility>();
			container.Register(
				Component.For<ICommandFactory>().AsFactory(),
				Component.For<IApplicationController>().ImplementedBy<ApplicationController>(),
				Component.For<IEventAggregator>().Instance(eventAggregator),

				Component.For<CommandLogger>(),
				Component.For<IParametrizedCommand<string>>().ImplementedBy<ParametrizedCommand1>(),
				Component.For<IParametrizedCommand<int>>().ImplementedBy<ParametrizedCommand2>()
				);

			var controller = container.Resolve<IApplicationController>();

			var logger = container.Resolve<CommandLogger>();
			controller.Execute("Command1");
			Assert.AreEqual("Command1", logger.Log);
			controller.Execute(2);
			Assert.AreEqual("2", logger.Log);
		}
	}
}
