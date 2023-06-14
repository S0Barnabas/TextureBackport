namespace TextureBackport.Api.Logging;

public class Logger : ILogger
{
    public event Logged? OnLogged;
    
    public void Log(LogLevel logLevel, string message)
    {
        OnLogged?.Invoke(logLevel, message);
    }
}