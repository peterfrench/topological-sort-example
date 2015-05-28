using System;

namespace TopologicalSortExample
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// Setup Resources
			ResourceManager manager = new ResourceManager ();
			manager.AddResource (new Resource("core.js", new[] { "jquery.js","bootstrap.js" }));
			manager.AddResource (new Resource("nav.js", new[] { "core.js" }));
			manager.AddResource (new Resource("jquery.js"));
			manager.AddResource (new Resource("bootstrap.js", new[] { "jquery.js" }));
			manager.Sort();

			// Output results
			Console.WriteLine ("Sorting resources...");
			foreach (Resource resource in manager.resources) {
				string depends = "";
				foreach (string dependency in resource.dependencies) {
					depends += "'"+dependency+"',";
				}
				depends = depends.TrimEnd (',', ' ');
				Console.WriteLine (("   "+resource.name).PadRight(20) + (depends.Length > 0 ? " (requires "+depends+")" : ""));
			}
			Console.WriteLine ("Resources have been sorted!");
		}
	}
}
