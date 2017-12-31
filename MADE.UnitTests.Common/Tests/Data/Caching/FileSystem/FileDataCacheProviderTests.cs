namespace MADE.UnitTests.Common.Tests.Data.Caching.FileSystem
{
	using System;

	using MADE.Data.Caching;
	using MADE.Data.Caching.FileSystem;
	using MADE.UnitTests.Common.Models;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class FileDataCacheProviderTests
	{
		private TestModel model;

		private IDataCacheProvider provider;

		[TestInitialize]
		public void Setup()
		{
			this.provider = new FileDataCacheProvider();

			this.model = new TestModel
				             {
					             Boolean = true,
					             Byte = byte.MinValue,
					             Char = char.MinValue,
					             Decimal = decimal.One,
					             Double = double.Epsilon,
					             Float = float.Epsilon,
					             Int = int.MinValue,
					             Long = long.MinValue,
					             Object = new TestModel(),
					             SByte = sbyte.MinValue,
					             Short = short.MinValue,
					             String = "Hello, World!",
					             UInt = uint.MinValue,
					             ULong = ulong.MinValue,
					             UShort = ushort.MinValue
				             };
		}

		[TestMethod]
		public void AddOrUpdate_AddModel_Exists()
		{
			string key = Guid.NewGuid().ToString();

			this.provider.AddOrUpdate(key, this.model);

			Assert.IsTrue(this.provider.Contains(key));
		}

		[TestMethod]
		public void AddOrUpdate_AddNull_DoesNotExist()
		{
			string key = Guid.NewGuid().ToString();

			this.provider.AddOrUpdate<TestModel>(key, null);

			Assert.IsFalse(this.provider.Contains(key));
		}

		[TestMethod]
		public void Get_ExistingKey_ModelNotNull()
		{
			string key = Guid.NewGuid().ToString();

			this.provider.AddOrUpdate(key, this.model);

			Assert.IsTrue(this.provider.Contains(key));

			TestModel item = this.provider.Get<TestModel>(key);

			Assert.IsNotNull(item);
		}

		[TestMethod]
		public void Get_NonExistingKey_ModelNull()
		{
			string key = Guid.NewGuid().ToString();

			TestModel item = this.provider.Get<TestModel>(key);

			Assert.IsNull(item);
		}

		[TestMethod]
		public void Remove_ExistingKey_ModelRemoved()
		{
			string key = Guid.NewGuid().ToString();

			this.provider.AddOrUpdate(key, this.model);

			Assert.IsTrue(this.provider.Contains(key));

			TestModel item = this.provider.Get<TestModel>(key);

			Assert.IsNotNull(item);

			this.provider.Remove(key);

			Assert.IsFalse(this.provider.Contains(key));
		}

		[TestMethod]
		public void Weed_ExistingModelWithinWeedDate_Days_NotRemoved()
		{
			string key = Guid.NewGuid().ToString();

			this.provider.AddOrUpdate(key, this.model);

			Assert.IsTrue(this.provider.Contains(key));

			TestModel item = this.provider.Get<TestModel>(key);

			Assert.IsNotNull(item);

			this.provider.Weed(1);

			Assert.IsTrue(this.provider.Contains(key));
		}

		[TestMethod]
		public void Weed_ExistingModelWithinWeedDate_TimeSpan_NotRemoved()
		{
			string key = Guid.NewGuid().ToString();

			this.provider.AddOrUpdate(key, this.model);

			Assert.IsTrue(this.provider.Contains(key));

			TestModel item = this.provider.Get<TestModel>(key);

			Assert.IsNotNull(item);

			this.provider.Weed(TimeSpan.FromDays(1));

			Assert.IsTrue(this.provider.Contains(key));
		}

		[TestMethod]
		public void Weed_ExistingModelWithinWeedDate_DateOnly_Removed()
		{
			string key = Guid.NewGuid().ToString();

			this.provider.AddOrUpdate(key, this.model);

			Assert.IsTrue(this.provider.Contains(key));

			TestModel item = this.provider.Get<TestModel>(key);

			Assert.IsNotNull(item);

			this.provider.Weed(DateTime.UtcNow.AddDays(1));

			Assert.IsFalse(this.provider.Contains(key));
		}

		[TestMethod]
		public void Weed_ExistingModelWithinWeedDate_DateAndTime_Removed()
		{
			string key = Guid.NewGuid().ToString();

			this.provider.AddOrUpdate(key, this.model);

			Assert.IsTrue(this.provider.Contains(key));

			TestModel item = this.provider.Get<TestModel>(key);

			Assert.IsNotNull(item);

			this.provider.Weed(DateTime.UtcNow.AddMinutes(10), true);

			Assert.IsFalse(this.provider.Contains(key));
		}

		[TestMethod]
		public void Weed_ExistingModelNotWithinWeedDate_DateOnly_NotRemoved()
		{
			string key = Guid.NewGuid().ToString();

			this.provider.AddOrUpdate(key, this.model);

			Assert.IsTrue(this.provider.Contains(key));

			TestModel item = this.provider.Get<TestModel>(key);

			Assert.IsNotNull(item);

			this.provider.Weed(DateTime.UtcNow.AddDays(-1), true);

			Assert.IsTrue(this.provider.Contains(key));
		}

		[TestMethod]
		public void Weed_ExistingModelNotWithinWeedDate_DateAndTime_NotRemoved()
		{
			string key = Guid.NewGuid().ToString();

			this.provider.AddOrUpdate(key, this.model);

			Assert.IsTrue(this.provider.Contains(key));

			TestModel item = this.provider.Get<TestModel>(key);

			Assert.IsNotNull(item);

			this.provider.Weed(DateTime.UtcNow.AddMinutes(-10), true);

			Assert.IsTrue(this.provider.Contains(key));
		}
	}
}