// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Logger;

namespace Mentor4U_Avalonia.Models;

public class NegativeNumberException(string message) : Exception(message);

public class EmptyStringException(string message) : Exception(message);

public static class ExceptionExtensions
{
    public static void LoggingIfException(ILogger logger, Action action, string moduleName, string methodName)
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            logger.Error($"Module: {moduleName}. Method: {methodName}. Message:  {e.Message}");
            throw;
        }
    }
}
