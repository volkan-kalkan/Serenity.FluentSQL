﻿using Serenity.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Serenity.Testing.Test
{
    public class TestSqlHelper
    {
        public static string Normalize(string script)
        {
            if (script == null)
                return null;

            script = script.Trim();

            StringBuilder sb = new StringBuilder();
            bool insideQuote = false;
            char prior = '\0';
            foreach (var x in script)
            {
                char c = x;
                if (c == '\'')
                    insideQuote = !insideQuote;
                else if (insideQuote)
                {
                }
                else if (c == '\r')
                    continue;
                else if (c == '\n' || c == ' ')
                {
                    if (prior == '\n' || prior == ' ' || prior == ';')
                        continue;

                    c = ' ';
                }

                prior = c;
                sb.Append(c);
            }

            return sb.ToString().Trim();
        }

        public static string GenerateInsertStatements(IDataReader reader, string table)
        {
            StringBuilder sbAll = new StringBuilder();

            while (reader.Read())
            {
                var sb = new StringBuilder();
                sb.Append("INSERT INTO ");
                sb.Append(table);
                sb.Append(" (");

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    if (i > 0)
                        sb.Append(", ");

                    sb.Append(reader.GetName(i));
                }
                sb.Append(")\r\nVALUES (");

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    if (i > 0)
                        sb.Append(", ");

                    sb.Append("/*");
                    sb.Append(reader.GetName(i));
                    sb.Append(":*/ ");
                    var value = reader.GetValue(i);
                    if (value == null ||
                        value == DBNull.Value)
                        sb.Append("NULL");
                    else if (value is DateTime)
                        sb.Append(((DateTime)value).ToSql());
                    else if (value is Int32 || value is Int16 || value is Boolean || value is Int64)
                        sb.Append(Serenity.Invariants.ToInvariant(Convert.ToInt64(value)));
                    else if (value is Double || value is Decimal || value is float)
                        sb.Append(Serenity.Invariants.ToInvariant(Convert.ToDecimal(value)));
                    else if (value is String)
                        sb.Append(((string)value).ToSql());
                    else
                        sb.Append(value.ToString().ToSql());
                }

                sb.Append(");");
                sbAll.AppendLine(sb.ToString());
                sbAll.AppendLine();

            }

            return sbAll.ToString();
        }
    }
}