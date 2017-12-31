namespace MADE.UnitTests.Common.Tests.Networking.Requests.Json
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using MADE.Data.Caching;
	using MADE.Data.Caching.FileSystem;
	using MADE.Networking.Requests.Json;
	using MADE.UnitTests.Common.Models;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class GetNetworkRequestTests
	{
		private GetNetworkRequest request;

		private IDataCacheProvider networkCache;

		[TestInitialize]
		public void Setup()
		{
		    this.networkCache = new FileDataCacheProvider(FileDataCacheProvider.DefaultApplicationFolderName, "NetworkCache");

			this.request =
				new GetNetworkRequest(NetworkRequestManagerTests.GetRequestUrl, NetworkRequestManagerTests.GetRequestExpectedType)
					{
						CacheProvider
							= this
								.networkCache,
						IsCachingEnabled
							= true
					};
		}

		[TestMethod]
		public async Task SendAsync_ValidJsonResponse_ReturnsValidObject()
		{
			object obj = await this.request.SendAsync();

			Assert.IsNotNull(obj);
			Assert.IsInstanceOfType(obj, NetworkRequestManagerTests.GetRequestExpectedType);
		}

		[TestMethod]
		public async Task SendAsync_ValidJsonResponse_ReturnsValidObjectOfCorrectType()
		{
			IEnumerable<TestModel> obj = await this.request.SendAsync<IEnumerable<TestModel>>();

			Assert.IsNotNull(obj);
			Assert.IsTrue(obj.Count() == 4);
		}
	}
}