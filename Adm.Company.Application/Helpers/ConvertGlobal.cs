﻿using System.Text;

namespace Adm.Company.Application.Helpers;

public static class ConvertGlobal
{
    public static byte[]? ConverterStringParaBytes(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;

        return Encoding.UTF8.GetBytes(value);
    }

    public static string? ConverterBytesParaString(this byte[]? value)
    {
        if (value == null) return null;

        return Encoding.UTF8.GetString(value);
    }
}
