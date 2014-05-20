using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Site.Models;

namespace Site.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly RequestTimeline _timeline;
        private readonly SendOnlyBusLogger _sendOnlyBusLogger;

        public ValuesController(RequestTimeline timeline, SendOnlyBusLogger sendOnlyBusLogger)
        {
            _timeline = timeline;
            _sendOnlyBusLogger = sendOnlyBusLogger;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            //if (_timeline.IdGuid == Guid.Empty)
            //    _timeline.IdGuid = Guid.NewGuid();

            var same = _sendOnlyBusLogger.GetId();

            return new string[] { _timeline.IdGuid.ToString(), same, _timeline.SetBy };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
