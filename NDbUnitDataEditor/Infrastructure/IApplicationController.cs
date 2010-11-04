using System;
using Castle.MicroKernel;
using Rhino.Commons;
using System.Collections.Generic;
using System.Linq;

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
