using System;
using System.Collections.Generic;

namespace Serenity.Data
{
    public class JoinAliasLocator
    {
        public static HashSet<string> Locate(string expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            HashSet<string> aliases = null;
            EnumerateAliases(expression, s =>
            {
                aliases = aliases ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                aliases.Add(s);
            });

            return aliases;
        }

        public static HashSet<string> LocateOptimized(string expression, out string singleAlias)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            HashSet<string> aliases = null;
            string alias = null;
            EnumerateAliases(expression, s =>
            {
                if (aliases == null && (alias == null || (aliases == null && alias == s)))
                    alias = s;
                else
                {
                    alias = null;
                    aliases = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {alias, s};
                }
            });

            singleAlias = alias;
            return aliases;
        }

        public static bool EnumerateAliases(string expression, Action<string> alias)
        {
            bool inQuote = false;
            int startIdent = -1;
            for (var i = 0; i < expression.Length; i++)
            {
                var c = expression[i];

                if (inQuote)
                {
                    if (c == '\'')
                    {
                        inQuote = false;
                    }
                }
                else
                {
                    if (c == '\'')
                    {
                        inQuote = true;
                        startIdent = -1;
                    }
                    else if (c == '_' || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                    {
                        if (startIdent < 0)
                            startIdent = i;
                    }
                    else if (c >= '0' && c <= '9')
                    {
                    }
                    else if (c == '.')
                    {
                        if (startIdent >= 0 && startIdent < i)
                        {
                            alias(expression.Substring(startIdent, i - startIdent));
                        }
                        startIdent = -1;
                    }
                    else
                        startIdent = -1;
                }
            }

            return true;
        }
    }
}