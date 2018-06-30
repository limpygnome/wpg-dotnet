<h1 align="center">
  Worldpay Gateway .NET SDK
</h1>

<h4 align="center">
  Use this SDK on your server-side to communicate with the <a href="https://www.worldpay.com/global/products/gateway-services">Worldpay Gateway (WPG)</a>.
</h4>

<p align="center">
  <img src="https://img.shields.io/badge/.NET%20Standard-1.2+-blue.svg" alt=".NET Standard 1.2" />
  <img src="https://img.shields.io/badge/.NET%20Core-1.0+-blue.svg" alt=".NET Core 1.0" />
  <img src="https://img.shields.io/badge/.NET%20Framework-4.5.1+-blue.svg" alt=".NET Standard 1.2" />
  <br />
  <a href="license.md">
    <img src="https://img.shields.io/badge/license-MIT-blue.svg" alt="MIT License" />
  </a>
</p>

## Features
- Payments
  - Cards __(includes tokenisation)__
  - Tokenised cards
  - 3ds authentication
  - Hosted Payment Pages
  - PayPal __(includes tokenisation)__
- Order inquiries
- Order notifications (XML)
- Tokenisation
  - Payments
  - Fetch
  - Fetch by shopper
  - Delete

## Examples
- [Examples](wpg-examples/examples)

## Getting Started
Download the binary from the [releases](../../releases) page, or use NuGet.

Command line:

````
nuget install wpg-sdk-dotnet
````

From within Visual Studio:
- Open up and select your project.
- Right-click your project, _Add_, _Add NuGet Packages..._.
- Search for `wpg-sdk-dotnet`.

## Requirements
Currently [supports](https://docs.microsoft.com/en-us/dotnet/standard/net-standard):
- .NET Standard 1.2+
- .NET Core 1.0+
- .NET Framework 4.5.1+
- Mono 4.6+
- Windows 8.1+

## Docs
- [Contribute & Support](docs/contribute-and-support.md)
- [Worldpay XML API Docs](http://support.worldpay.com/support/kb/gg/corporate-gateway-guide/content/home.htm)
- [Payment Service XML DTD](http://dtd.worldpay.com/v1/)
