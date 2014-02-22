using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Text;

namespace Serenity
{
    public static class Invariants
    {
       /// <summary>
       ///   Number format information for invariant culture</summary>
       public static readonly NumberFormatInfo NumberFormat;

       /// <summary>
       ///   Date time format information for invariant culture</summary>
       public static readonly DateTimeFormatInfo DateTimeFormat;

       /// <summary>
       ///   Statik DataHelper contructor'ı. Varsayılan bağlantı string'i ve bağlantı kültürü parametlerini
       ///   initialize eder.
       /// </summary>
       static Invariants()
       {
           NumberFormat = NumberFormatInfo.InvariantInfo;
           DateTimeFormat = DateTimeFormatInfo.InvariantInfo;
       }

       public static string ToInvariant(this int value)
       {
           return value.ToString(Invariants.NumberFormat);
       }

       public static string ToInvariant(this Int64 value)
       {
           return value.ToString(Invariants.NumberFormat);
       }

       public static string ToInvariant(this Double value)
       {
           return value.ToString(Invariants.NumberFormat);
       }

       public static string ToInvariant(this Decimal value)
       {
           return value.ToString(Invariants.NumberFormat);
       }
   }
}