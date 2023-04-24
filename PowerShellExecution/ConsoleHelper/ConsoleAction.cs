namespace PowerShellClient.ConsoleHelper
{
    public enum ConsoleAction
    {
        ScriptString = 1,
        ScriptStringWithParams = 2,
        ScriptFile = 3,
        ScriptFileWithParams = 4,
        ThrowError = 5,
        Stop = 9
    }
}
