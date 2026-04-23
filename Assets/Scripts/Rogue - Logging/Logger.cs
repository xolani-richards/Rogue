using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
public struct Message
{
    public Component sender;
    public string message;
    public DateTime timeStamp;
    public LogLevel logLevel;

    public Message(LogLevel level, Component sender, string message)
    {
        this.logLevel = level;
        this.sender = sender;
        this.message = message;
        this.timeStamp = DateTime.Now;
    }
}

public static class Logger
{
    // public static List<Message> logs = new ();
    private static readonly LogLevel printLevel = LogLevel.Debug;
    
    public static void Error(Component sender, string message) => LogMessage(LogLevel.Error, sender, message);
    public static void Warn(Component sender, string message) => LogMessage(LogLevel.Warn, sender, message);
    public static void Info(Component sender, string message) => LogMessage(LogLevel.Info, sender, message);
    public static void Log(Component sender, string message) => LogMessage(LogLevel.Info, sender, message);
    public static void Debug(Component sender, string message) => LogMessage(LogLevel.Debug, sender, message);

    public static void LogMessage(LogLevel logLevel, Component sender, string message)
    {
        Message log = new Message(logLevel, sender, message);
        if(!CanShowMessage(log)) return;
        UnityEngine.Debug.Log($"{logLevel} | {log.timeStamp} | {sender} | {message}");
    }

    private static bool CanShowMessage(Message log)
    {
        bool result = false;
        switch(printLevel)
        {
            case LogLevel.Info:
                if (log.logLevel == LogLevel.Info) result = true;
                if (log.logLevel == LogLevel.Warn) result = true;
                if (log.logLevel == LogLevel.Error) result = true;
                break;
            case LogLevel.Debug:
                result = true;
                break;
            default:
                result = true;
                break;
        }
        return result;
    }
}