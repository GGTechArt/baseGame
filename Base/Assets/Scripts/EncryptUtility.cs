using System;
using System.Text;

public static class EncryptUtility
{
    public static string StringToHexString(string input)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
        return BitConverter.ToString(bytes).Replace("-", string.Empty);
    }

    public static string HexStringToString(string hexString)
    {
        int numberChars = hexString.Length;
        byte[] bytes = new byte[numberChars / 2];

        for (int i = 0; i < numberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
        }

        return Encoding.UTF8.GetString(bytes);
    }
}
