using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Web.Http.Dependencies;
using Autofac.Integration.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Site.HttpModules;
using Site.Models;

namespace UnitTestProject1
{
    public class FakeDependencyScope : IDependencyScope
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object GetService(Type serviceType)
        {
            return new RequestTimeline();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class UnitTest1 : Starter
    {
        //Todo : pass an inner handler.... should make it possible to tests these handler without starting up the site....
        [TestMethod]
        public void TestMethod1()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost/api/values"));
            httpRequestMessage.Properties.Add("MS_DependencyScope", new FakeDependencyScope());
            
            var response = SendAsync(httpRequestMessage, new CancellationToken()).Result;

        }
    }
}
