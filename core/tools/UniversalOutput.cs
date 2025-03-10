using Godot;
using System;
using System.Diagnostics;

internal static class SM
{
    public static void Log(object message)
    {
        try
        {
            GD.Print(message);
            Console.WriteLine(message);
            Debug.WriteLine(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    public static void Error(object message)
    {
        try
        {
            GD.PrintErr(message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Debug.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
