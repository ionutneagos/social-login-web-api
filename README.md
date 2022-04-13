---
page_type: sample
languages:
- csharp
- angular 13
description: "This project implements a simple Google Login/Register into a WEB API solution using NET 6.0 for back-end and Angular 13 for client."
urlFragment: social-login-web-api
---

# Social Login Web Api Sample

This project implements a simple Google Login/Register into a WEB API solution using .NET 6.0 for back-end and Angular 13 for client. It demonstrates how you can integrate Social Logins and manage users in the back-end. 

## Demo
Check out the live demo here: https://social-login.azurewebsites.net/

# Getting Started - Local Development

## Prerequisites

1. Server
   * [.NET 6.0](https://dotnet.microsoft.com/download)
   * [Visual Studio 2022](https://docs.microsoft.com/en-us/visualstudio/install/update-visual-studio) or newer
2. Front End
   * [Angular CLI](https://github.com/angular/angular-cli) version 13.
   * [Node 12.22.1](https://nodejs.org/en/download/) or newer
   * [Visual Studio Code](https://code.visualstudio.com/) or your preferred editor
3. Google OAuth Configuration 
   * Navigate to Integrating Google Sign-In into your web app and select Configure a project.
   * In the Configure your OAuth client dialog, select Web server.
   * In the Authorized redirect URIs text entry box, set the redirect URI. For example, https://localhost:44329/signin-google
   * Save the Client ID and Client Secret.

   _Official guide from Google: [Authenticate with a backend server](https://developers.google.com/identity/sign-in/web/backend-auth)_

### Setup

Clone the repo
```
git clone https://github.com/ionutneagos/social-login-web-api.git
```

#### Server
Navigate to Web Api Project -> appsettings.json, and update it according to your's credentials.
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "Database": {
    "ConnectionString": "Server=[SQL Server];User ID=[SQL User];Password=[SQL Password];Initial Catalog=[DatabaseName]; Persist Security Info=True; MultipleActiveResultSets=True;"
  },
  "Authentication": {
    "Jwt": {
      "Secret": "",
      "Issuer": "WebApi.NetCore.Service",
      "Audience": "WebApi.NetCore.ClientApp",
      "Subject": "WebApi.NetCore.ClientAccess"
    },
    "Google": {
      "ClientId": "[Client Id from  Google OAuth Configuration]",
      "ClientSecret": [Client Secret from  Google OAuth Configuration]
    }
  }
}
```

>**Note: Automatic migrations are applied on application startup. You don't need to generate them manually. For doing this, I used [EFCore.AutomaticMigrations](https://www.nuget.org/packages/EFCore.AutomaticMigrations/) package.**

> *MigrateDatabaseToLatestVersion.ExecuteAsync(context).Wait()* - where context is your application context.
```
if (environment.IsDevelopment())
{
    var context = services.GetRequiredService<Infrastructure.AppDbContext>();
    MigrateDatabaseToLatestVersion.ExecuteAsync(context).Wait();
}
```
#### Front-End
The front-end is built as an Angular standalone application using npm commands. In order to remove dependencies between UI component frameworks and Angular, the front end HTML/CSS uses plain and simple Bootstrap and is built with Angular 13 using the Angular CLI. This reduces dependency restrictions on any UI component frameworks.

>For login authentication is used Angular 13 Social Login npm package, [angularx-social-login](https://www.npmjs.com/package/angularx-social-login)

Install dependencies
```
npm install
```
Navigate to Angular.Client/GoogleLogin/src/environments -> environments.ts, and update it according to yours credentials:
```
export const environment = {
  production: false,
  baseUrl: 'https://localhost:44329', //back-end address
  googleClientId: "googleClientId",  //Client Id from  Google OAuth Configuration
};
```

## Use / Test

Start your backend server by following the above instructions.

Start angular client: `ng serve`. 

Navigate to `http://localhost:4200/`.
