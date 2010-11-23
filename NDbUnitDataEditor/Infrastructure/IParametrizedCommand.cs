using System;

namespace NDbUnitDataEditor
{
	public interface IParametrizedCommand<T>
	{
		void Execute(T data);
	}
}
