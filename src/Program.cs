using System;

internal class Program
{
    public static void Main(string[] args)
    {
        Window window = new Window(640, 480, "Default Title", true);
        window.SetWindowActivity(new TestActivity());
        window.Run();
    }
}