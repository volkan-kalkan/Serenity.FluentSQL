﻿using System;

namespace Serenity.Data
{
    public partial class SqlQuery
    {
        /// <summary>
        ///   MSSQL'de full text araması için gerekli join'i oluşturur.</summary>
        /// <param name="searchTable">
        ///   Arama yapılacak alanları içeren tablo adı (zorunlu).</param>
        /// <param name="searchFields">
        ///   Arama yapılacak alanlar (zorunlu).</param>
        /// <param name="searchQuery">
        ///   Aranan kelime, ya da kelime grubu (zorunlu). Kelimeler virgülle ayrılmalı.</param>
        /// <param name="searchTableAlias">
        ///   Arama yapılacak tabloya, sorgunun FROM kısmında atanmış olan alias (zorunlu, ör. T0).</param>
        /// <param name="searchTableKey">
        ///   Arama yapılacak tablonun anahtar (ID) sahası (zorunlu).</param>
        /// <param name="containsAlias">
        ///   Bağlama yapılan contains table'a atanacak alias (zorunlu, ör. CT).</param>
        /// <returns>
        ///   SqlSelect nesnesinin kendisi.</returns>
        public SqlQuery FullTextSearchJoin(
            string searchTable, string searchFields, string searchQuery,
            string searchTableAlias, string searchTableKey, string containsAlias)
        {
            if (String.IsNullOrEmpty(searchTable))
                throw new ArgumentNullException("searchTable");
            if (String.IsNullOrEmpty(searchFields))
                throw new ArgumentNullException("searchFields");
            if (String.IsNullOrEmpty(searchQuery))
                throw new ArgumentNullException("searchQuery");
            if (String.IsNullOrEmpty(searchTableAlias))
                throw new ArgumentNullException("searchTableAlias");
            if (String.IsNullOrEmpty(searchTableKey))
                throw new ArgumentNullException("searchTableKey");
            if (String.IsNullOrEmpty(containsAlias))
                throw new ArgumentNullException("containsAlias");

            cachedQuery = null;

            if (from.Length > 0)
                from.Append(" \n");

            from.Append("INNER JOIN CONTAINSTABLE(");

            from.AppendFormat(
                "{0}, ({1}), '{2}') AS {5} ON ({5}.[key] = {3}.{4})",
                searchTable, searchFields, searchQuery.Replace("'", "''"), searchTableAlias, searchTableKey, containsAlias);

            return this;
        }
    }
}