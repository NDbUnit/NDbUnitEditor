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

namespace Tests
{

    [TestFixture]
    public class ComponentRegistrarTests
    {

        [TestFixture]
        public class When_Creating_Container
        {
            [Test]
            public void All_Components_Can_Be_Created()
            {
                var registrar = new ComponentRegistrar();
                var handlers = new List<IHandler>();

                registrar.Container.Kernel.ComponentRegistered += (name, handler) => handlers.Add(handler);
                registrar.RegisterComponents();
                // If the container cannot resolve the instance  
                // an exception will be thrown and the test will fail!
                handlers.ForEach(handler =>
                {
                    if (!IsFromCastleNamespace(handler) && handler.CurrentState == HandlerState.WaitingDependency)
                        registrar.Container.Kernel.Resolve(handler.ComponentModel.Name, handler.Service);
                });
            }

			private bool IsFromCastleNamespace(IHandler handler)
			{
				if (handler.ComponentModel.Service.Namespace.StartsWith("Castle"))
					return true;
				return false;
			}
        }

    }
}
