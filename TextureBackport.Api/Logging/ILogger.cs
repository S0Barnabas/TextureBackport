namespace TextureBackport.Api.Logging;

public delegate void Logged(LogLevel logLevel, string message);

public interface ILogger
{
    public event Logged? OnLogged;

    public void Log(LogLevel logLevel, string message);
}