using System.Text;

internal static class Utils
{
    public static byte[] HexToBytes(this string hex)
    {
        return Enumerable.Range(0, hex.Length / 2).Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16)).ToArray();
    }

     public static string BytesToHex(this byte[] bytes)
    {
        return BitConverter.ToString(bytes).Replace("-", string.Empty);
    }

    public static T[] Concat<T>(this T[] x, T[] y)
    {
        var z = new T[x.Length + y.Length];
        x.CopyTo(z, 0);
        y.CopyTo(z, x.Length);
        return z;
    }

    public static long ToTimestamp(this DateTime d)
    {
        DateTime _jan1St1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (long)(d.ToUniversalTime() - _jan1St1970).TotalSeconds;
    }

    public static byte[] ToBytes(this string data)
    {
        return Encoding.UTF8.GetBytes(data);
    }

    public static string ToBase64(this byte[] data)
    {
        return Convert.ToBase64String(data);
    }
}
