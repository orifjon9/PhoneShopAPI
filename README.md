# PhoneShopAPI

Application developed based on .NET Core WEB API

## Overview
This tutorial creates the following API:

|API	                   |   Description	                |   Request body    |   Response body       |
|:-------------------------|:-------------------------------|:------------------|:----------------------|
|GET _/api/phones_	       |Get all phone items	            |   None	        |   Array of phone items|
|GET _/api/phones/{id}_	   |Get an phone item by ID	        |   None	        |   Phone item          |
|POST _/api/phones_	       |Add a new phone item	        |   Phone item	    |   Phone item          |
|PUT _/api/phones/{id}_	   |Update an existing phone item   |  	Phone item	    |   None                |
|DELETE _/api/phones/{id}_ |Delete an phone item    	    |   None	        |   None                |

# Prerequisites
Install the following:

[.NET Core 2.1 SDK or later](https://www.microsoft.com/net/download/all)

[Visual Studio Code](https://code.visualstudio.com/download) 

[C# for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)


# Create the project

From a console, run the following commands:

```bash
dotnet new webapi -o PhoneShopAPI
code PhoneShopAPI
```

## Run project
Press Debug (F5) or _use commend_ to build and run the program. In a browser, navigate to http://localhost:5000/api/phones
```bash
dotnet run
```

## Swagger

Use below address in a browse, navigate to swagger. [Instruction for how to create](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-2.1)

http://localhost:5000/swagger/v1/swagger.json

