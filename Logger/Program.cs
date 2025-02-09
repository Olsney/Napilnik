namespace Logger;

class Program
{
    static void Main(string[] args)
    {
        PathFinder fileLogPathFinder = CreateFileLogPathFinder();
        fileLogPathFinder.Find();

        PathFinder consoleLogPathFinder = CreateConsoleLogPathFinder();
        consoleLogPathFinder.Find();

        PathFinder fridayFileLogPathFinder = CreateFridayFileLogPathFinder();
        fridayFileLogPathFinder.Find();

        PathFinder fridayConsoleLogPathFinder = CreateFridayConsoleLogPathFinder();
        fridayConsoleLogPathFinder.Find();

        List<ILogger> loggers = new List<ILogger>
        {
            new ConsoleLogWriter(),
            new SecureConsoleLogWriter(
                new FileLogWriter())
        };

        PathFinder combinedLogWriterPathFinder = CreateCombinedLogWriterPathFinder(loggers);
        combinedLogWriterPathFinder.Find();
    }

    private static PathFinder CreateFileLogPathFinder() =>
        new PathFinder(new FileLogWriter());

    private static PathFinder CreateConsoleLogPathFinder() =>
        new PathFinder(new ConsoleLogWriter());

    private static PathFinder CreateFridayFileLogPathFinder() =>
        new PathFinder(
            new SecureConsoleLogWriter(
                new FileLogWriter()));

    private static PathFinder CreateFridayConsoleLogPathFinder() =>
        new PathFinder(
            new SecureConsoleLogWriter(
                new ConsoleLogWriter()));

    private static PathFinder CreateCombinedLogWriterPathFinder(List<ILogger> loggers) =>
        new(new CombinedLogWriter(loggers));
}

interface ILogger
{
    void WriteError(string message);
}

class PathFinder
{
    private readonly ILogger _logger;

    public PathFinder(ILogger logger) =>
        _logger = logger;

    public void Find()
    {
        string messageToSend = "I'm logging info";

        WriteError(messageToSend);
    }

    private void WriteError(string messageToSend) =>
        _logger.WriteError(messageToSend);
}

class SecureConsoleLogWriter : ILogger
{
    private const DayOfWeek Friday = DayOfWeek.Friday;

    private readonly ILogger _logger;

    public SecureConsoleLogWriter(ILogger logger) =>
        _logger = logger;

    public void WriteError(string message)
    {
        if (IsFriday())
            _logger.WriteError(message);
    }

    private static bool IsFriday() =>
        DateTime.Now.DayOfWeek == Friday;
}

class FileLogWriter : ILogger
{
    private const string FileLog = "log.txt";

    public void WriteError(string message) =>
        File.WriteAllText(FileLog, message);
}

class ConsoleLogWriter : ILogger
{
    public void WriteError(string message) =>
        Console.WriteLine(message);
}

class CombinedLogWriter : ILogger
{
    private readonly List<ILogger> _loggers;

    public CombinedLogWriter(List<ILogger> loggers) =>
        _loggers = loggers;

    public void WriteError(string message)
    {
        foreach (ILogger logger in _loggers)
            logger.WriteError(message);
    }
}