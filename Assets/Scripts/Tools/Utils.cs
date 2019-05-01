using System;
public class Utils
{
    public static string TimeNow()
    {
        return $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}-{DateTime.Now.Hour}:{DateTime.Now.Minute}";
    }
}
