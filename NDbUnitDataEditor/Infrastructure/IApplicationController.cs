using System;

namespace NDbUnitDataEditor
{
	public interface IApplicationController
	{
		void Subscribe<T>(Action<T> handler);
		void Publish<T>(T message);
		void Execute<T>(T data);
		void ExecuteCommand<T>() where T : ICommand;
	}
}
