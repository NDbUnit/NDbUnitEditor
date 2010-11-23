using System;

namespace NDbUnitDataEditor
{
	public interface ICommandFactory
	{
		T Create<T>() where T : ICommand;
		IParametrizedCommand<T> CreateParametrized<T>();
	}

    public class ApplicationController : IApplicationController
	{
		private IEventAggregator _events;
		private ICommandFactory _factory;
		/// <summary>
		/// Initializes a new instance of the ApplicationController class.
		/// </summary>
		public ApplicationController(ICommandFactory factory, IEventAggregator events)
		{
			_factory = factory;
            _events = events;
		}

		public void ExecuteCommand<T>() where T:ICommand
		{
			ICommand command = _factory.Create<T>();
			command.Execute();
		}

		public void Execute<T>(T data)
		{

			IParametrizedCommand<T> command = _factory.CreateParametrized<T>();
			command.Execute(data);
		}

		public void Publish<T>(T message)
		{
			_events.Publish(message);
		}

		public void Subscribe<T>(Action<T> handler)
		{
			_events.Subscribe(handler);
		}


	}
}
