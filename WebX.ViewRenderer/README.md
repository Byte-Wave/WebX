# WebX.ViewRender
## Allows you to quickly generate secure passwords and tokens in three steps

### Installation:
NuGet Package:
| Description |  Link |
| ------ | ------ |
| NuGet Page | https://www.nuget.org/packages/CryptX/ |
| Download NuGet Package | https://www.nuget.org/api/v2/package/CryptX |

### Usage:
> #### 1st Option: (Quick and Easy)
```C#
// Step one: Create a partial view under Views folder

// Step two: In the Startup.cs, under ConfigureServices add this line in order to use dependancy injection
```
services.AddTransient<IPartialViewRenderer, PartialViewRenderer>();;
```

// Step three: In the Controller in which you want to use this view render add this field this line in order to use dependancy injection
```
private readonly IPartialViewRenderer _renderer;
```

// Step four: In the Controller constructor add this parameter to the constructor
```
IRazorPartialToStringRenderer renderer
```

// Step four: In the Controller constructor add this line to map the parameter to the field
```
_renderer = renderer;
```

// Step five: When you want to use the Render use the line specified below
// In my case I passed the url(string) as a model to the view, but it can use any viewmodel in the conventional manner
```
string myHostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

var url = $"{myHostUrl}/Account/ActivateEmployeeAccount?key={encodedKey}";

// THIS LINE
var body = await _renderer.RenderToStringAsync("EmailViews/_ActivateYourAccount", url);
```
