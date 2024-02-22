Console.WriteLine();

#region What is an Exception, and Why is it Necessary?
/*
    => Exceptions represent unexpected events and situations that disrupt the normal flow of a program during its execution.
    => In C#, exceptional situations are handled by a series of classes, each defining a specific type of error. These classes are used with try-catch blocks to handle errors.

    => Exception handling is crucial for Error Handling and Recovery, Program Stability, Error Tracking and Analysis, User Experience, Error Monitoring, and Logging, etc.
*/
#endregion

#region How to Create a Custom Exception Class

try
{
    int a = 1, b = 0;
    Console.WriteLine(a / b);
}
catch (Exception ex)
{
    throw new CustomException();
}

class CustomException : Exception
{
    public CustomException() : base("Custom error!")
    {

    }
    public CustomException(string message) : base(message) { }
}
#endregion