using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public static class BigIntegerFormatter
{
    private static readonly string[] _units = { "", "a", "b", "c", "d", "e", "f", "g", 
        "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D" };

    public static string FormatBigInteger(BigInteger value)
    {
        int unitIndex = 0;
        BigInteger threshold = 1000;

        while (value >= threshold && unitIndex < _units.Length - 1)
        {
            value /= 1000;
            unitIndex++;
        }

        return $"{value}{_units[unitIndex]}";
    }
}
