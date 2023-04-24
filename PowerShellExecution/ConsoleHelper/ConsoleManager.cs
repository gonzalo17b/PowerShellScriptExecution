namespace PowerShellClient.ConsoleHelper
{
    public static class ConsoleManager
    {
        public static void WriteLine(string message) => Console.WriteLine(message);
        public static void WriteLine(string message, ConsoleLevel messageLevel)
        {
            Console.ForegroundColor = messageLevel.Color;
            WriteLine(message);
            Console.ForegroundColor = ConsoleLevel.Info.Color;
        }

        public static void WriteEmptyLine(int num = 1)
        {
            for (int i = 0; i < num; i++) 
            {
                Console.WriteLine("");
            }
        }

        public static T GetEnumFromConsole<T>() where T : Enum
        {
            WriteLine($"Por favor, seleccione una de las siguientes opciones:");
            var allowedKeys = new List<int>();
            foreach (int key in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine($"{key} - {Enum.GetName(typeof(T), key)}");
                allowedKeys.Add(key);
            }

            WriteEmptyLine();

            var typeIntroduced = Console.ReadLine();
            var typeIsCorrect = int.TryParse(typeIntroduced, out var urlTypeSelected) && allowedKeys.Contains(urlTypeSelected);
            if (typeIsCorrect) return (T)(object)urlTypeSelected;

            WriteLine($"El parametro no coincide con los posibles valores. Inténtelo de nuevo", ConsoleLevel.Warning);
            return GetEnumFromConsole<T>();
        }

        public static Func<Task> GetAction<T>(Dictionary<T, Func<Task>> actionDictionary) where T : Enum
        {
            var codeSelected = GetEnumFromConsole<T>();
            return actionDictionary[codeSelected];
        }
    }
}
