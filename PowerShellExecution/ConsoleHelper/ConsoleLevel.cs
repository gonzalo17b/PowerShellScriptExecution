namespace PowerShellClient.ConsoleHelper
{
    public sealed class ConsoleLevel
    {
        public readonly static ConsoleLevel Info = new(ConsoleColor.White);
        public readonly static ConsoleLevel Warning = new(ConsoleColor.Yellow);
        public readonly static ConsoleLevel Error = new(ConsoleColor.Red);
        public readonly static ConsoleLevel Sucess = new(ConsoleColor.Green);

        public ConsoleColor Color { get; }
        private ConsoleLevel(ConsoleColor color)
        {
            Color = color;
        }
    }
}
