// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AgencyPro.Core.Extensions
{
    public static class GuidExtensions
    {
        private static Regex rxDigits = new Regex(@"[\d]+");

        public static string GetNumericIdFromGuid(this Guid guid)
        {
            var s = guid.ToString();
            StringBuilder sb = new StringBuilder();
            for (Match m = rxDigits.Match(s); m.Success; m = m.NextMatch())
            {
                sb.Append(m.Value);
            }
            string cleaned = sb.ToString();
            return cleaned.Substring(0, 8);
        }
    }
}