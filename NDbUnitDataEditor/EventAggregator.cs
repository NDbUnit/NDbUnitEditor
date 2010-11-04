using System;
using Castle.MicroKernel;
using Rhino.Commons;
using System.Collections.Generic;
using System.Linq;

namespace NDbUnitDataEditor
{
	public interface IEventAggregator
	{
		void Publish<T>(T message);
		void Subscribe<T>(Action<T> handler);
	}

    public class EventAggregator : IEventAggregator
	{
		private IList<Delegate> _registeredHandlers = new List<Delegate>();

		public void Publish<T>(T message)
		{
			var handlers = _registeredHandlers.Where(l => l is Action<T>).Select(h => h as Action<T>);
			if (!handlers.Any())
				return;
			foreach (var handler in handlers)
			{
				handler(message);
			}
		}

		public void Subscribe<T>(Action<T> handler)
		{
			_registeredHandlers.Add(handler);
		}
	}
}
