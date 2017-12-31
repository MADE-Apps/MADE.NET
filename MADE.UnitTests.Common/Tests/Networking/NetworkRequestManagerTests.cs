namespace MADE.UnitTests.Common.Tests.Networking
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using MADE.Networking;
    using MADE.Networking.Requests.Json;
    using MADE.UnitTests.Common.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NetworkRequestManagerTests
    {
        private NetworkRequestManager networkRequestManager;

        public const string GetRequestUrl =
                "https://raw.githubusercontent.com/MADE-Apps/MADE-App-Components/c89333c872c9a03647d6b7fe0d91819b51505aec/MADE.UnitTests.Common/Assets/TestModels.json"
            ;

        public static Type GetRequestExpectedType => typeof(IEnumerable<TestModel>);

        [TestInitialize]
        public void Setup()
        {
            this.networkRequestManager = new NetworkRequestManager{QueueProcessPeriod = TimeSpan.FromSeconds(1)};
            this.networkRequestManager.StartProcessing();
        }

        [TestMethod]
        public void AddToQueue_NetworkRequest_AddedAndProcessed()
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            GetNetworkRequest request = new GetNetworkRequest(GetRequestUrl, GetRequestExpectedType) { IsCachingEnabled = true };

            this.networkRequestManager.AddToQueue<GetNetworkRequest, IEnumerable<TestModel>, Exception>(
                request,
                model =>
                    {
                        System.Diagnostics.Debug.WriteLine(model.ToString());
                        Assert.IsNotNull(model);

                        autoResetEvent.Set();
                    },
                ex =>
                    {
                        Assert.Fail(ex.ToString());

                        autoResetEvent.Set();
                    });

            Assert.IsTrue(autoResetEvent.WaitOne());
        }
    }
}