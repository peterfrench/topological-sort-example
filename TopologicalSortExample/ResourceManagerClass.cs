using System;
using System.Collections.Generic;

namespace App
{
	public class ResourceManager
	{
		public List<Resource> resources;

		public bool autoSort;

		public ResourceManager(bool autoSort = false)
		{
			this.autoSort = autoSort;
			this.resources = new List<Resource> ();
		}

		public ResourceManager AddResource( Resource resource )
		{
			this.resources.Add (resource);
			if(this.autoSort)
				this.Sort ();
			return this;
		}

		public Resource GetResource( string name )
		{
			return this.resources.Find (i => i.name == name);
		}

		public ResourceManager Sort()
		{
			return this.TopologicalSort ();
		}

		protected ResourceManager TopologicalSort()
		{
			if (this.resources.Count > 0) {
				List<Resource> sorted = new List<Resource> ();
				List<string> resolved = new List<string> ();
				List<string> unresolved = new List<string> ();
				foreach (Resource resource in this.resources) {
					this.resolve (resource, sorted, resolved, unresolved);
				}
				this.resources = sorted;
			}
			return this;
		}

		protected void resolve(Resource resource, List<Resource> sorted, List<string> resolved, List<string> unresolved)
		{
			if (resource != null && !resolved.Contains (resource.name)) {
				unresolved.Add (resource.name);
				foreach (string dependency in resource.dependencies) {
					if (!resolved.Contains (dependency)) {
						if (unresolved.Contains (dependency)) {
							throw new InvalidOperationException ("Circular reference detected: " + resource + " <==> " + dependency);
						}
						this.resolve (this.GetResource (dependency), sorted, resolved, unresolved);
					}
				}
				sorted.Add (resource);
				unresolved.Remove (resource.name);
				resolved.Add (resource.name);
			}
		}

		public override string ToString ()
		{
			string output = "";
			foreach (Resource resource in this.resources)
				output += resource + " ";
			return output.TrimEnd ();
		}
	}
}

