# Featurify

### What is Featurify?

Featurify is just yet another implementation of feature flags thats super easy to setup and use. It intends to solve one and only one issue where feature flags are user specific.

### Background

Featurify is designed to be really light weight and easily integrate with .net core 2.0+ web applications. This is intended to be 
one part of a two piece puzzle. If you are interested in simple application settings based feature flags, this library is not for 
you! There are a number of other options for that purpose (refer to various sections at the end of this file). 

First part of this puzzle is elaborated in the usage section. Second part of the puzzle is to design a user interface that would 
help administrators identify the features, and also tie them to users (step 2 in usage deals with the integration to this part of the puzzle). 

The need for this came up for my project because I found out LaunchDarkly that provides feature flags as a service, but its not 
free! The not free part let to this package and this would require minimal effort to achieve the same. 

### Usage

1. Add the `Featurify` nuget package using the nuget package manager. The `Featurify.Contract` nuget package is also added automatically. If you want to have a seperate project to define `IUserInfoStrategy`, `IToggleMetadataFinder` and other associated classes, you can just add the `Featurify.Contracts` nuget package to that.

2. Create a class (implementing `IUserInfoStrategy`) that would provide the user information for the current user. 

```csharp
public class DemoAppUserFinderStrategy : IUserInfoStrategy
{
   private readonly IHttpContextAccessor accessor;

   public DemoAppUserFinderStrategy(IHttpContextAccessor accessor)
   {
      this.accessor = accessor;
   }

   public async Task<string> GetCurrentUserId()
   {
      // This is just an illustration. In real life you would ideally use the instance of IHttpContextAccessor
      //      to get the logged in user's user id or email address
   
      await Task.CompletedTask;
      return "b0486d0f-9114-41a7-a095-e4e92201a41e";
   }
}
```
3. (Optional) Create a class that (implementing `IFeatureNameTransformer`) will dictate the format of feature names. Default is `Featurify.{featureName}`

4. Create a class  (implementing `IToggleMetadataFinder`) that would provide the metadata for the feature for a/all specific user(s)

```csharp
public class DemoAppFeatureMetadataFinder: IToggleMetadataFinder
{
    private readonly IAppDbContext dbContext;

    public DemoAppFeatureMetadataFinder(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IToggleMetadata> FindToggleStatus(string featureName, string userId)
    {
        // This is just an illustration. In real life you would use a data context to identify the feature toggle
        //      status for the user
        //   featureName => the transformed feature name based on your initial setup
	//                  default is "Featurify.{featureName}"
        //   userId => this will be logged in user's id identified by your IUserInfoStrategy instance
    
        await Task.CompletedTask;
        var metadata = new Toggle
        {
            Name = featureName,
            Value = featureName.Contains("ImportFeature") ? true : false,
            UserId = "?" // Indicates any user, see step 5 for more details
        };
        return metadata;
     }
}

public class Toggle : IToggleMetadata
{
     public string Name { get; set; }
     public bool Value { get; set; }
     public string UserId { get; set; }
}
```

5. Add the featurify service in Startup.cs

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();

    services.AddFeaturify<DemoAppFeatureMetadataFinder, DemoAppUserFinderStrategy>(options =>
    {
	// Identifier if a feature is common for all users
        options.AnyUserVerifier = "?"; 
	// If this is set to true, and a match is not found through IToggleMetadataFinder.FindToggleStatus, an exception will be thrown
        options.UseStrict = false; 
    });
}
```

6. You can now use the package as shown below:

***Using it in the controller***

```csharp
// Create a class to represent your feature
public class ImportFeature : IFeatureToggle
{
}

public class HomeController : Controller
{
     private readonly IFeaturifyServer server;

     public HomeController(IFeaturifyServer server)
     {
        this.server = server;
     }

     public async Task<IActionResult> Contact()
     {
        ViewData["Message"] = "Your contact page.";
        var model = new ContactViewModel
           {
               CanImport = await server.Is<ImportFeature>().Enabled() // Verify if the feature is enabled
           };
         return View(model);
      }
}
```

   (or)

***Use it in the views***

```csharp
// Create a class to represent your feature
public class ExportFeature : IFeatureToggle
{
}
```

Use it in a view by injecting an instance of `IFeaturifyServer`

```html
@using Featurify
@using Featurify.Contracts
@using Featurify.Demo.Features

@inject IFeaturifyServer Featurify

@if (await Featurify.Is<ExportFeature>().Enabled())
{
    <button class="btn btn-success">Export Users</button>
}
else
{
    <button class="btn btn-danger" disabled>Export Users</button>
}
```

In both the cases listed above the `FeaturifyServer` first uses the user strategy to get the unique identifier for the logged in user.
If a custom feature name transformer is defined, that is used to transform the feature name. Then, the metadata finder assigned is 
used to find the metadata by passing in the feature name and the user id. In this case, for demo purposes, import feature is turned on
and the export feature is turned off.

### Note

There is also a syntactic sugar that you can use when verifying if a feature is enabled for the logged in user. Here is an example:

```html
@using Featurify
@using Featurify.Contracts
@using Featurify.Demo.Features

@inject IFeaturifyServer Featurify

@if (await Featurify.Enabled<ExportFeature>())
{
    <button class="btn btn-success">Export Users</button>
}
else
{
    <button class="btn btn-danger" disabled>Export Users</button>
}
```


### Credits

* Feature Toggle - For the inspiration to eliminate magic strings (https://github.com/jason-roberts/FeatureToggle)

### Simple Feature Toggles Based on Application Settings

If you are looking for an implementation of feature flags in .net which can be used out of the box with the help of the application settings file, 
check out http://enterprisedevops.org/feature-toggle-frameworks-list, where feature toggles for various languages are listed.
