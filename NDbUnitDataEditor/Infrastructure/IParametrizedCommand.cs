using System;
using Castle.MicroKernel;
using Rhino.Commons;
using System.Collections.Generic;
using System.Linq;

namespace NDbUnitDataEditor
{
	public interface IParametrizedCommand<T>
	{
		void Execute<T>(T data);
	}
}
