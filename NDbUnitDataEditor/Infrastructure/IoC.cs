using System;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using NDbUnit.Utility;
using NDbUnitDataEditor.UI;
using System.Reflection;

namespace NDbUnitDataEditor
{
	public class IoC
	{
		private static IWindsorContainer _container;
		public static void Initialize(IWindsorContainer container)
		{
			_container = container;
		}

		public static T Resolve<T>()
		{
			if (_container == null)
				throw new ArgumentNullException("You must initialize container before using it");
			return _container.Resolve<T>();
		}
	}
}
