using System;
using System.Collections.Generic;

namespace TopologicalSortExample
{
	public class Resource
	{
		public List<string> dependencies;
		public string name;

		public Resource (string name, string[] dependencies = null)
		{
			this.name = name;
			this.dependencies = new List<string> ();
			if (dependencies != null) {
				foreach (string dependency in dependencies) {
					this.AddDependency (dependency);
				}
			}
		}

		public void AddDependency(string dependency)
		{
			this.dependencies.Add (dependency);
		}

		public override string ToString()
		{
			return this.name;
		}
	}
}

