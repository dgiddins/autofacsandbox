using System.Collections;
using System.Collections.Generic;
using Autofac;

namespace Site.Models
{
    public class NServiceBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new NServiceBus(new NServiceBusConfig())).As<IBus>().SingleInstance();
        }
    }

    public interface IBus
    {
        void Send(object message);
        object Recieve();
    }

    public class NServiceBusConfig
    {
        public string QueueName { get; set; }
    }

    public class NServiceBus : IBus
    {
        private readonly NServiceBusConfig _config;

        public NServiceBus(NServiceBusConfig config)
        {
            _config = config;
        }

        public void Send(object message)
        {
        }

        public object Recieve()
        {
            return new object();
        }
    }

    public interface ISendOnlyBus
    {
        void Send(object message);
    }

    public class NServiceBusWrapper : ISendOnlyBus
    {
        private readonly IBus _bus;

        public NServiceBusWrapper(IBus bus)
        {
            _bus = bus;
        }

        public void Send(object message)
        {
            _bus.Send(message);
        }
    }

    public class BusLogger : NServiceBusWrapper
    {
        private readonly RequestTimeline _requestTimeline;

        public BusLogger(IBus bus,RequestTimeline requestTimeline) : base(bus)
        {
            _requestTimeline = requestTimeline;
        }

        public new void Send(object message)
        {
            _requestTimeline.MessageSent = message.GetType().Name;
            base.Send(message);
        }
    }
}