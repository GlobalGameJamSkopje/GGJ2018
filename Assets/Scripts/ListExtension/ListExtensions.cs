﻿using System;
using System.Collections.Generic;

public static class ListExtensions
{
    private static readonly Random Random = new Random();

    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = Random.Next(n + 1);
            var value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }
}
