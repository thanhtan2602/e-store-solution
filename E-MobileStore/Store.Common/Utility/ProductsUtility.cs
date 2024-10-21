﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Common.Utility
{
    public static class ProductsUtility
    {
        public static string ToUrl(string name)
        {
            name = name.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();
            foreach (var c in name)
            {
                var unicodeCategory= CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString()
                .Normalize(NormalizationForm.FormC)
                .ToLower()
                .Replace(" ", "-")
                .Replace("'", "")
                .Replace("/", "-")
                .Replace("[", "")
                .Replace("]", "")
                .Replace("(", "")
                .Replace(")", "");
        }
    }
}
