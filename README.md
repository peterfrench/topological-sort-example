## Simple example of Topological Sorting

The ResourceManager class uses topological sorting to sort a list of resources based on their dependencies.

## Example

```
ResourceManager manager = new ResourceManager();
manager.AddResource(new Resource("core.js",new string[] {"jquery.js"}));
manager.AddResource(new Resource("jquery.js"));
manager.Sort();
```

With the above code `jquery.js` will be the first resource for the ResourceManager since `core.js` depends on it.