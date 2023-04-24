using EnsureThat;

namespace PowerShellExecution.Domain
{
    public sealed class PowerShellScript
    {
        public string Content { get; }
        private PowerShellScript(string scriptContent)
        {
            EnsureArg.IsNotNullOrEmpty(scriptContent);
            Content = scriptContent;
        }

        public static explicit operator PowerShellScript(string scriptContent)
            => new(scriptContent);
    }
}
