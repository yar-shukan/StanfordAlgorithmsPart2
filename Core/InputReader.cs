using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Core
{
    public static class InputReader
    {
        public static T[] ReadAsArray<T>(string fileName, params char[] separator)
        {
            using (var stream = new StreamReader(fileName))
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof (T));
                return stream.ReadToEnd()
                             .Split(separator, StringSplitOptions.RemoveEmptyEntries)
                             .Select(s => (T)converter.ConvertFrom(s))
                             .ToArray();
            }
        }

        public static int[] SplitAsInts(this string s)
        {
            return s.Split(' ').Select(int.Parse).ToArray();
        }
    }
}