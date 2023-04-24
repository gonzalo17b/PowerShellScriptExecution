# Running PowerShell Scripts:
This project contains a class to run PowerShell scripts from a C# console application.

## Requirements

- .NET 5.0 SDK or later

## Usage

The console application provides a menu to select the type of script to run.

There are six options available:
1.  Run PowerShell script without parameters (code in the same file).
2.  Execute PowerShell script with parameters (code in the same file).
3.  Execute PowerShell script without parameters (external file).
4.  Execute PowerShell script with parameters (external file).
5.  Execute PowerShell command that throws an error to see how it is captured by console.
6.  Exit.

### Types of PowerShell scripts

There are two types of PowerShell scripts that can be run with the `PowerShellRunner` class:

#### PowerShell script as a string.

A PowerShell script can be provided as a string. In this case, the constructor of the `PowerShellRunner` class accepts an object of type `PowerShellScript`.

    var powerShellScript = new PowerShellScript("Write-Host 'Hello from PowerShell!'");
    var result = new PowerShellRunner(powerShellScript).RunAsync();

#### PowerShell Script in a file

A PowerShell script can also be provided in a file. In this case, the constructor of the `PowerShellRunner` class accepts an object of type `PowerShellScriptPath`.

    var powerShellScriptPath = PowerShellScriptPath.FromFilePath("script.ps1");
    var result = new PowerShellRunner(powerShellScriptPath).RunAsync();

#### Input parameters
`PowerShellScriptParam` contains the name and value of the parameter.

    new PowerShellRunner(PowerShellScriptPath.FromFilePath("script.ps1"))
    .WithParam(PowerShellScriptParam.FromKeyValue("name", "Gonzalo"))
    .WithParam(PowerShellScriptParam.FromKeyValue("lastName", "Bermejo"))
    .RunAsync();

#### Examples of scripts

In the `Ps1Scripts` folder you will find some examples of PowerShell scripts that you can use with this application.

## Contribute
If you want to contribute to this project, you are more than welcome! You can do it in the following way:

Make a fork of the repository.

Make your changes and tests.

Create a pull request and describe the changes you have made.

Wait for someone from the team to review your pull request.
