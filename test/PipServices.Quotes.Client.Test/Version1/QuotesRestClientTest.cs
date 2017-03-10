using Microsoft.VisualStudio.TestTools.UnitTesting;
using PipServices.Runtime;
using PipServices.Runtime.Config;
using PipServices.Runtime.Run;

namespace PipServices.Quotes.Client.Version1
{
    [TestClass]
    public class QuotesRestClientTest
    {
        private static readonly ComponentConfig RestOptions = ComponentConfig.FromTuples(
            "descriptor.type", "rest",
            "endpoint.type", "http",
            "endpoint.host", "localhost",
            "endpoint.port", 8002
        );

        private QuotesRestClient Client;
        private ComponentSet Components;
        private QuotesClientFixture Fixture;

        public QuotesRestClientTest()
        {
            Client = new QuotesRestClient();
            Client.Configure(RestOptions);

            Components = ComponentSet.FromComponents(Client);
            Fixture = new QuotesClientFixture(Client);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            LifeCycleManager.LinkAndOpen(Components);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            LifeCycleManager.Close(Components);
        }

        [TestMethod]
        public void TestCrudOperations()
        {
            Fixture.TestCrudOperations();
        }
    }
}
