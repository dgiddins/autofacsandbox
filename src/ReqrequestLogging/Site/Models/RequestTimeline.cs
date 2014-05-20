using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class RequestTimeline
    {
        public Guid IdGuid { get; set; }
        public string SetBy { get; set; }
        public string MessageSent { get; set; }
    }

    public class SendOnlyBusLogger
    {
        private readonly RequestTimeline _timeline;

        public SendOnlyBusLogger(RequestTimeline timeline)
        {
            _timeline = timeline;
        }

        public string GetId()
        {
            return _timeline.IdGuid.ToString();
        }
    }
}