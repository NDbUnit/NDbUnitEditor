using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Castle.Windsor;
using Castle.Core.Interceptor;
using Castle.MicroKernel.Registration;
using Castle.Core;
using Rhino.Commons;

namespace Tests
{
    [TestFixture]
    public class ContainerTests
    {
        [FixtureSetUp]
        public void _TestFixtureSetup()
        {
        }

        [SetUp]
        public void _TestSetup()
        {
            RegisterComponents();

        }

        [Test]
        public void Test1()
        {
            ITestClass1 instance1 = IoC.Resolve<ITestClass1>();
            ITestClass2 instance2 = IoC.Resolve<ITestClass2>();
            instance1.Method1();
            try
            {
                instance1.Method2();
            }
            catch (Exception)
            {
                Console.WriteLine("Test output: Exception was captured");
            }
            instance2.Method1();
            instance2.Method2();

        }

        private void RegisterComponents()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Register(
                Component.For<TestClassInterceptor>(),
                Component.For<ITestClass1>().ImplementedBy<TestClass1>()
                .Interceptors(InterceptorReference.ForType<TestClassInterceptor>()).First,
                Component.For<ITestClass2>().ImplementedBy<TestClass2>()
                .Interceptors(InterceptorReference.ForType<TestClassInterceptor>()).First);

            IoC.Initialize(container);
        }

    }

    public class TestClassInterceptor : IInterceptor
    {

        int i = 0;

        public void Intercept(IInvocation invocation)
        {
            string className = invocation.Method.ReflectedType.Name;
            Console.WriteLine(string.Format("Method {0} from {1} is about to be invoked", invocation.Method.Name, className));
            i++;
            try
            {
                invocation.Proceed();
            }
            catch (Exception)
            {
                Console.WriteLine(string.Format("Method {0} from {1} have thrown an exception", invocation.Method.Name, className));
                throw;
            }

            Console.WriteLine(string.Format("Intercepted {0} method calls", i));
        }

    }

    public interface ITestClass1
    {
        void Method1();
        void Method2();
    }
    public class TestClass1 : ITestClass1
    {

        public TestClass1()
        {

        }

        public void Method1()
        {
            Console.WriteLine("Call from Method1");
        }

        public void Method2()
        {
            throw new NotImplementedException();
        }
    }

    public interface ITestClass2
    {
        void Method1();
        void Method2();
    }
    public class TestClass2 : ITestClass2
    {
        public TestClass2()
        {

        }

        public void Method1()
        {
            Console.WriteLine("Call from Method1");
        }

        public void Method2()
        {
            Console.WriteLine("Call from Method2");
        }
    }
}
