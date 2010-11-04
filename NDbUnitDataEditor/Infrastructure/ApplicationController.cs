using System;
using Castle.MicroKernel;
using Rhino.Commons;
using System.Collections.Generic;
using System.Linq;

namespace NDbUnitDataEditor
{
    public class ApplicationController : IApplicationController
	{
		private IKernel _kernel;
		private IEventAggregator _events;
		/// <summary>
		/// Initializes a new instance of the ApplicationController class.
		/// </summary>
		public ApplicationController(IKernel kernel, IEventAggregator events)
		{
			_events = events;
            _kernel = kernel;
		}

		public void ExecuteCommand<T>() where T:ICommand
		{
			ICommand command = _kernel.Resolve<T>();
			command.Execute();
		}

		public void Execute<T>(T data)
		{
			IParametrizedCommand<T> command = _kernel.Resolve<IParametrizedCommand<T>>();
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
