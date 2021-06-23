using System;
using System.Collections.Generic;

namespace Bewise.Phone
{

    internal class StringWithIndexComparer : IComparer<object>
    {
        static bool ExtractInt(string s, out int i)
        {
            string temp = "";
            for (int index = 0; index < s.Length; index++)
            {
                if (char.IsNumber(s, index))
                    temp += s[index];
                else
                {
                    break;
                }
            }

            if (string.IsNullOrEmpty(temp))
            {
                i = 0;
                return false;
            }

            return int.TryParse(temp, out i);
        }

        public int Compare(object x, object y)
        {
            string strX = x.ToString();
            string strY = y.ToString();

            int a, b;

            if (ExtractInt(strX, out a) && ExtractInt(strY, out b))
            {
                if (a < b)
                    return -1;
                if (b > a)
                    return 1;
                return 0;
            }

            return String.Compare(x.ToString(), y.ToString());
        }
    }

}
