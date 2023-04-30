using System;
using System.Collections.Generic;

static class ListExtensions
{
    private static Random newRandom = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int i = list.Count;
        while (i > 1)
        {
            i--;
            int j = newRandom.Next(i + 1);
            T value = list[j];
            list[j] = list[i];
            list[i] = value;
        }
    }
}