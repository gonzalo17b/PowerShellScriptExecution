using PowerShellClient.ConsoleHelper;
using PowerShellExecution.Domain;
using System.Reflection;

namespace PowerShellClient
{
    public class PowerShellConsoleExecutor
    {
        private readonly Dictionary<ConsoleAction, Func<Task>> _requestcodeToAction = new()
        {
            { ConsoleAction.ScriptString, ExecuteScriptString },
            { ConsoleAction.ScriptStringWithParams, ExecuteScriptStringWithParams },
            { ConsoleAction.ScriptFile, ExecuteScriptFile },
            { ConsoleAction.ScriptFileWithParams, ExecuteScriptFileWithParams },
            { ConsoleAction.ThrowError, ScriptThrowError },
            { ConsoleAction.Stop, () => Task.FromResult(0) }
        };

        public async Task RunAsync()
        {
            ConsoleManager.WriteLine("POWERSHELL EXECUTION PROJECT STARTED");
            ConsoleManager.WriteEmptyLine(2);

            var exit = false;

            while (!exit)
            {
                var action = ConsoleManager.GetAction(_requestcodeToAction);
                try
                {
                    await action();
                }
                catch (Exception ex)
                {
                    ConsoleManager.WriteLine(ex.Message, ConsoleLevel.Error);
                }

                ConsoleManager.WriteEmptyLine(3);
                exit = action == _requestcodeToAction[ConsoleAction.Stop];
            }
        }

        private static async Task ExecuteScriptString()
        {
            var scriptWithNoParams =
                @"
                    Write-Host '¡Hola desde PowerShell content sin ningún parámetro!'
                ";

            var result = await new PowerShellRunner((PowerShellScript)scriptWithNoParams).RunAsync();
            ConsoleManager.WriteLine(result, ConsoleLevel.Sucess);
        }

        private static async Task ExecuteScriptStringWithParams()
        {
            var name = "Gonzalo";
            var surname = "Bermejo";

            var scriptWithParams =
                $@"                    
                    Write-Host '¡Hola desde PowerShell content $name $surname!'
                ";

            var result = await new PowerShellRunner((PowerShellScript)scriptWithParams)
                .WithParam(PowerShellScriptParam.FromKeyValue($"${nameof(name)}", name))
                .WithParam(PowerShellScriptParam.FromKeyValue($"${nameof(surname)}", surname))
                .RunAsync();
            ConsoleManager.WriteLine(result, ConsoleLevel.Sucess);
        }

        private static async Task ExecuteScriptFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var path = Path.GetDirectoryName(assembly.Location);
            var scriptPath = Path.Combine(path, "Ps1Scripts", "powershellScript.ps1");

            var result = await new PowerShellRunner(PowerShellScriptPath.FromFilePath(scriptPath)).RunAsync();
            ConsoleManager.WriteLine(result, ConsoleLevel.Sucess);
        }

        private static async Task ExecuteScriptFileWithParams()
        {
            var name = "Gonzalo";
            var surname = "Bermejo";

            var assembly = Assembly.GetExecutingAssembly();
            var path = Path.GetDirectoryName(assembly.Location);
            var paramsScriptPath = Path.Combine(path, "Ps1Scripts", "powershellScriptWithParams.ps1");

            var result = await new PowerShellRunner(PowerShellScriptPath.FromFilePath(paramsScriptPath))
                .WithParam(PowerShellScriptParam.FromKeyValue($"${nameof(name)}", name))
                .WithParam(PowerShellScriptParam.FromKeyValue($"${nameof(surname)}", surname))
                .RunAsync();
            ConsoleManager.WriteLine(result, ConsoleLevel.Sucess);
        }

        private static async Task ScriptThrowError()
        {
            var scriptWithNoParams =
                @"
                Error in the script
            ";

            var result = await new PowerShellRunner((PowerShellScript)scriptWithNoParams).RunAsync();
            ConsoleManager.WriteLine(result, ConsoleLevel.Sucess);
        }
    }
}