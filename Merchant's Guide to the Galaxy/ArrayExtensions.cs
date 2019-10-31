using System;
using System.Collections.Generic;
using System.Text;

namespace Merchant_s_Guide_to_the_Galaxy
{
    static class ArrayExtensions
    {
        public static void ReplaceAll(this string[] items, string oldValue, string newValue)
        {
            for (int index = 0; index < items.Length; index++)
                if (items[index] == oldValue)
                    items[index] = newValue;
        }

        public static void TrimAll(this string[] items)
        {
            for (int index = 0; index < items.Length; index++)
                items[index].Trim();
        }
    }
}
