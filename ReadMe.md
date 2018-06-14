# Featurify

### What is Featurify?

Featurify is just yet another implementation of feature flags thats super easy to setup and use. It intends to solve one and only one issue where feature flags are user specific.

### Background

Featurify is designed to be really light weight and easily integrate with .net core 2.0+ web applications. This is intended to be one part of a two piece puzzle. If you are interested in simple application settings based feature flags, this library is not for you! There are a number of other options for that purpose (refer to the credits section). 

First part of this puzzle is elaborated in the usage section. Second part of the puzzle is to design a user interface that would help administrators identify the features, and also tie them to users (step 2 in usage deals with the integration to this part of the puzzle). 

The need for this came up for my project because I found out LaunchDarkly that provides feature flags as a service, but its not free! The not free part let to this package and this would require minimal effort to achieve the same. 

### Usage

1. Add the Featurify nuget package

2. Implement a class that would provide the user information for the current user

3. Implement a class that would provide the metadata for the feature

4. Add the featurify service in Startup.cs

### Credits

* Feature Toggle - For the inspiration to eliminate magic strings (https://github.com/jason-roberts/FeatureToggle)

### Simple Feature Toggles Based on Application Settings

Check out the http://enterprisedevops.org/feature-toggle-frameworks-list page where feature toggles for various languages are listed
