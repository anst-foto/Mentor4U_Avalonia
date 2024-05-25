namespace Mentor4U_Avalonia.Models;

public class NegativeNumberException(string message) : Exception(message);

public class EmptyStringException(string message) : Exception(message);