using EnsureThat;

namespace PowerShellExecution.Domain
{
    public sealed class PowerShellScriptParam
    {
        public string Key { get; }
        public string Value { get; }

        public PowerShellScriptParam(string key, string value)
        {
            EnsureArg.IsNotNullOrEmpty(key);
            EnsureArg.StartsWith(key, "$");
            Key = key;

            EnsureArg.IsNotNullOrEmpty(value);
            Value = value;
        }

        public static PowerShellScriptParam FromKeyValue(string key, string value)
            => new(key, value);
    }
}
