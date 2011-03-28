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
	public class Event1
	{
		public string Message { get; set; }
	}

	public class Event2
	{
		public string Message { get; set; }
	}

	public interface ITestListener
	{
		void OnStepCompletedHandler1(Event1 eventData);
		void OnStepCompletedHandler2(Event1 eventData);
		void OnStepStartedHandler1(Event2 eventData);
		void OnStepStartedHandler2(Event2 eventData);
	}

	[TestFixture]
	public class EventAggregatorTests
	{
		EventAggregator events;
		ITestListener listener;

		[SetUp]
		public void TestSetup()
		{
			events = new EventAggregator();
			listener = MockRepository.GenerateStrictMock<ITestListener>();			
		}

		[Test]
		public void ShouldHandlePublishedEventWhenSubscribed()
		{		
			var data = new Event1{Message="step completed"};

			listener.Expect(l => l.OnStepCompletedHandler1(data));
			events.Subscribe<Event1>(listener.OnStepCompletedHandler1);
			events.Publish(data);
			listener.VerifyAllExpectations();
		}

		[Test]
		public void ShouldNotHandleWhenNotSubscribed()
		{
			var data1 = new Event1 { Message = "step completed" };
			events.Publish(data1);
			listener.VerifyAllExpectations();
		}

		[Test]
		public void ShouldHandleWhenTwoHandlersSubscribedToOneEvent()
		{
			var data1 = new Event1 { Message = "step completed" };

			events.Subscribe<Event1>(listener.OnStepCompletedHandler1);
			events.Subscribe<Event1>(listener.OnStepCompletedHandler2);

			listener.Expect(l => l.OnStepCompletedHandler1(data1));
			listener.Expect(l => l.OnStepCompletedHandler2(data1));

			events.Publish<Event1>(data1);
			listener.VerifyAllExpectations();

		}
	}
}
