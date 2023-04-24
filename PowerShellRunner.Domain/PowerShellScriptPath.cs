using EnsureThat;

namespace PowerShellExecution.Domain
{
    public sealed class PowerShellScriptPath
    {
        public string Path { get; }
        public string Content { get; }

        private PowerShellScriptPath(string scriptPath)
        {
            EnsureArg.IsNotNullOrEmpty(scriptPath);
            Path = scriptPath;

            var exists = File.Exists(Path);
            EnsureArg.IsTrue(exists);

            using StreamReader reader = new(Path);
            var content = reader.ReadToEnd();
            EnsureArg.IsNotNullOrEmpty(content);
            Content = content;
        }

        public static PowerShellScriptPath FromFilePath(string filePath)
            => new(filePath);
    }
}
