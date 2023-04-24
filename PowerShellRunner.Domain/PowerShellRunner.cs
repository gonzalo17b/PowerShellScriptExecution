using EnsureThat;
using System.Diagnostics;

namespace PowerShellExecution.Domain
{
    public class PowerShellRunner
    {
        private string _scriptContent;
        private readonly List<PowerShellScriptParam> _params = new();

        public PowerShellRunner(PowerShellScript script)
        {
            EnsureArg.IsNotNull(script);
            _scriptContent = script.Content;
        }

        public PowerShellRunner(PowerShellScriptPath scriptFile)
        {
            EnsureArg.IsNotNull(scriptFile);
            _scriptContent = scriptFile.Content;
        }

        public PowerShellRunner WithParam(PowerShellScriptParam scriptParam)
        {
            EnsureArg.IsNotNull(scriptParam);

            var paramAlreadyAdded = _params.Any(x => x.Key == scriptParam.Key);
            EnsureArg.IsFalse(paramAlreadyAdded);

            var paramExistInScript = _scriptContent.Contains(scriptParam.Key);
            EnsureArg.IsTrue(paramExistInScript);

            _params.Add(scriptParam);
            return this;
        }

        public async Task<string> RunAsync()
        {
            _params.ForEach(param => _scriptContent = _scriptContent.Replace($"{param.Key}", param.Value));

            using var process = new Process { StartInfo = ProcessStartInfo(_scriptContent) };
            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                throw new Exception($"El script de PowerShell ha finalizado con un código de salida {process.ExitCode}. Error: {error}");

            return output;
        }

        private static ProcessStartInfo ProcessStartInfo(string script)
            => new()
            {
                FileName = "powershell.exe",
                Arguments = $"-ExecutionPolicy Bypass -Command \"{script}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
    }
}
