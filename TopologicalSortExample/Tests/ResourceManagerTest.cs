using NUnit.Framework;
using System;

namespace TopologicalSortExample.Tests
{
	[TestFixture ()]
	public class ResourceManagerTest
	{
		[Test ()]
		public void TestCase_NoDependencies ()
		{
			// Arrange
			ResourceManager manager = new ResourceManager();
			manager.AddResource (new Resource ("a"));
			manager.AddResource (new Resource ("b"));
			manager.AddResource (new Resource ("c"));
			// Act
			manager.Sort();
			// Assert
			Assert.AreEqual ("a",manager.resources[0].name);
			Assert.AreEqual ("b",manager.resources[1].name);
			Assert.AreEqual ("c",manager.resources[2].name);
		}

		[Test ()]
		public void TestCase_SingleDependency ()
		{
			// Arrange
			ResourceManager manager = new ResourceManager();
			manager.AddResource (new Resource ("a",new[] {"b"}));
			manager.AddResource (new Resource ("b"));
			manager.AddResource (new Resource ("c"));
			// Act
			manager.Sort();
			// Assert
			Assert.AreEqual ("b",manager.resources[0].name);
			Assert.AreEqual ("a",manager.resources[1].name);
			Assert.AreEqual ("c",manager.resources[2].name);
		}

		[Test ()]
		public void TestCase_MultipleDependencies ()
		{
			// Arrange
			ResourceManager manager = new ResourceManager();
			manager.AddResource (new Resource ("a",new[] {"b","d"}));
			manager.AddResource (new Resource ("b"));
			manager.AddResource (new Resource ("c"));
			manager.AddResource (new Resource ("d", new[] {"c","e"}));
			manager.AddResource (new Resource ("e"));
			manager.AddResource (new Resource ("f"));
			// Act
			manager.Sort();
			// Assert
			Assert.AreEqual ("b",manager.resources[0].name);
			Assert.AreEqual ("c",manager.resources[1].name);
			Assert.AreEqual ("e",manager.resources[2].name);
			Assert.AreEqual ("d",manager.resources[3].name);
			Assert.AreEqual ("a",manager.resources[4].name);
			Assert.AreEqual ("f",manager.resources[5].name);
			Assert.AreEqual ("b c e d a f", manager.ToString ());
		}

		[Test ()]
		[ExpectedException( "System.InvalidOperationException" )]
		public void TestCase_CircularDependency ()
		{
			// Arrange
			ResourceManager manager = new ResourceManager();
			manager.AddResource (new Resource ("a",new[] {"b"}));
			manager.AddResource (new Resource ("b",new[] {"a"}));
			manager.AddResource (new Resource ("c"));
			// Act
			manager.Sort();
			// Assert
			// Expected Exception "System.InvalidOperationException"
		}

		[Test ()]
		public void TestCase_AutoSort ()
		{
			// Arrange
			ResourceManager manager = new ResourceManager(true);
			// Act
			manager.AddResource (new Resource ("a",new[] {"b","d"}));
			manager.AddResource (new Resource ("b"));
			manager.AddResource (new Resource ("c"));
			manager.AddResource (new Resource ("d", new[] {"c","e"}));
			manager.AddResource (new Resource ("e"));
			manager.AddResource (new Resource ("f"));
			// Assert
			Assert.AreEqual ("b",manager.resources[0].name);
			Assert.AreEqual ("c",manager.resources[1].name);
			Assert.AreEqual ("e",manager.resources[2].name);
			Assert.AreEqual ("d",manager.resources[3].name);
			Assert.AreEqual ("a",manager.resources[4].name);
			Assert.AreEqual ("f",manager.resources[5].name);
			Assert.AreEqual ("b c e d a f", manager.ToString ());
		}
	}
}

