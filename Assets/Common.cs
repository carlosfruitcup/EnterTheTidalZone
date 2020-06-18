/// <summary>Contains commonly used functions, methods, etc.
/// <para>This is a static class. </para>
/// <seealso cref="Settings"/>
/// </summary>
public static class Common
{
    public static int BoolToInt(bool boolean)
    {
        if(boolean) return 1;
        else return 0;
    }
    public static bool IntToBool(int integer)
    {
        if(integer == 0) return false;
        else return true;
    }
}
