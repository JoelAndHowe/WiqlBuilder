param(
    [string]$LibraryProjectPath = "src/WiqlBuilder/WiqlBuilder.csproj",
    [string]$PackageId = "WiqlBuilder",
    [string]$Author = "Your Name",
    [string]$Company = "Your Org",
    [string]$RepositoryUrl = "https://github.com/your-org/your-repo",
    [string]$Description = "A fluent, strongly-typed WIQL query builder for .NET.",
    [string]$PackageTags = "wiql azure-devops query-builder dotnet",
    [string]$ReadmePath = "README.md",
    [string]$LicensePath = "LICENSE",
    [string]$WorkflowPath = ".github/workflows/publish-nuget.yml"
)

$ErrorActionPreference = "Stop"

if (-not (Test-Path $LibraryProjectPath)) {
    throw "Could not find project file at '$LibraryProjectPath'."
}

$projectDirectory = Split-Path $LibraryProjectPath -Parent
$readmeFileName = Split-Path $ReadmePath -Leaf

if (-not (Test-Path ".github/workflows")) {
    New-Item -ItemType Directory -Path ".github/workflows" -Force | Out-Null
}

@"
MIT License

Copyright (c) 2026 $Author

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the ""Software""), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
"@ | Set-Content -Path $LicensePath -Encoding UTF8

@"
# $PackageId

$Description

## Features

- Fluent WIQL builder API
- Strongly typed field references
- Typed helpers for common WIQL concepts
- Support for custom fields when needed

## Install

```powershell
dotnet add package $PackageId
```

## Example

```csharp
using NhsWales.Pcmh.$PackageId.Builders;
using NhsWales.Pcmh.$PackageId.Fields;
using NhsWales.Pcmh.$PackageId.Syntax;

var wiql = WiqlQueryBuilder.Create()
    .Select(SystemFields.Id, SystemFields.Title, SystemFields.State)
    .FromWorkItems()
    .Where(w => w
        .AndWorkItemTypeIs(WorkItemType.Task)
        .AndStateIsOneOf(WorkItemState.Active, WorkItemState.Resolved))
    .Build();
```

## Build and test

```powershell
dotnet build
dotnet test
```

## NuGet publishing

This repository includes a GitHub Actions workflow for publishing to NuGet.

Add a repository secret named `NUGET_API_KEY` before using the workflow.

## License

MIT. See [LICENSE](LICENSE).
"@ | Set-Content -Path $ReadmePath -Encoding UTF8

@"
name: publish-nuget

on:
  release:
    types: [published]
  workflow_dispatch:

jobs:
  publish:
    runs-on: ubuntu-latest
    permissions:
      contents: read

    steps:
      - name: Checkout
        uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: 8.0.x

      - name: Restore
        run: dotnet restore

      - name: Build
        run: dotnet build --configuration Release --no-restore

      - name: Test
        run: dotnet test --configuration Release --no-build

      - name: Pack
        run: dotnet pack $LibraryProjectPath --configuration Release --no-build --output ./artifacts

      - name: Publish to NuGet
        run: dotnet nuget push "./artifacts/*.nupkg" --api-key "${{ secrets.NUGET_API_KEY }}" --source https://api.nuget.org/v3/index.json --skip-duplicate
"@ | Set-Content -Path $WorkflowPath -Encoding UTF8

[xml]$xml = Get-Content $LibraryProjectPath
$propertyGroup = $xml.Project.PropertyGroup | Select-Object -First 1

if (-not $propertyGroup) {
    $propertyGroup = $xml.CreateElement("PropertyGroup")
    $null = $xml.Project.AppendChild($propertyGroup)
}

function Add-OrUpdateProperty {
    param([xml]$XmlDoc, $Group, [string]$Name, [string]$Value)

    $existing = $Group.SelectSingleNode($Name)
    if ($existing -eq $null) {
        $node = $XmlDoc.CreateElement($Name)
        $node.InnerText = $Value
        $null = $Group.AppendChild($node)
    }
    else {
        $existing.InnerText = $Value
    }
}

Add-OrUpdateProperty -XmlDoc $xml -Group $propertyGroup -Name "PackageId" -Value $PackageId
Add-OrUpdateProperty -XmlDoc $xml -Group $propertyGroup -Name "Authors" -Value $Author
Add-OrUpdateProperty -XmlDoc $xml -Group $propertyGroup -Name "Company" -Value $Company
Add-OrUpdateProperty -XmlDoc $xml -Group $propertyGroup -Name "Description" -Value $Description
Add-OrUpdateProperty -XmlDoc $xml -Group $propertyGroup -Name "PackageTags" -Value $PackageTags
Add-OrUpdateProperty -XmlDoc $xml -Group $propertyGroup -Name "RepositoryUrl" -Value $RepositoryUrl
Add-OrUpdateProperty -XmlDoc $xml -Group $propertyGroup -Name "PackageProjectUrl" -Value $RepositoryUrl
Add-OrUpdateProperty -XmlDoc $xml -Group $propertyGroup -Name "RepositoryType" -Value "git"
Add-OrUpdateProperty -XmlDoc $xml -Group $propertyGroup -Name "PackageLicenseExpression" -Value "MIT"
Add-OrUpdateProperty -XmlDoc $xml -Group $propertyGroup -Name "PackageReadmeFile" -Value $readmeFileName

$noneGroup = $xml.Project.ItemGroup | Where-Object { $_.None } | Select-Object -First 1
if (-not $noneGroup) {
    $noneGroup = $xml.CreateElement("ItemGroup")
    $null = $xml.Project.AppendChild($noneGroup)
}

$readmeExists = $false
foreach ($node in $noneGroup.None) {
    if ($node.Include -eq $readmeFileName -or $node.Include -eq "..\..\README.md" -or $node.Include -eq "README.md") {
        $readmeExists = $true
    }
}

if (-not $readmeExists) {
    $noneNode = $xml.CreateElement("None")
    $includeAttr = $xml.CreateAttribute("Include")
    if ($projectDirectory -like "src\*") {
        $includeAttr.Value = "..\..\README.md"
    } else {
        $includeAttr.Value = "README.md"
    }
    $null = $noneNode.Attributes.Append($includeAttr)

    $packNode = $xml.CreateElement("Pack")
    $packNode.InnerText = "true"
    $null = $noneNode.AppendChild($packNode)

    $packagePathNode = $xml.CreateElement("PackagePath")
    $packagePathNode.InnerText = "\"
    $null = $noneNode.AppendChild($packagePathNode)

    $null = $noneGroup.AppendChild($noneNode)
}

$xml.Save((Resolve-Path $LibraryProjectPath))

Write-Host "Updated:"
Write-Host " - $LibraryProjectPath"
Write-Host " - $ReadmePath"
Write-Host " - $LicensePath"
Write-Host " - $WorkflowPath"
