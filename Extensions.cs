using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Common
{
    public static class Extensions
    {
        /// <summary>
        /// Determine whether a class has a particular property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool HasProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }


        /// <summary>
        /// Remove all whitespace characters from a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string TrimAll(this string text)
        {
            var len = text.Length;
            var src = text.ToCharArray();
            int dstIdx = 0;
            for (int i = 0; i < len; i++)
            {
                var ch = src[i];
                if (!ch.IsWhiteSpace())
                    src[dstIdx++] = ch;
            }

            return new string(src, 0, dstIdx);
        }

        /// <summary>
        /// Whitespace detection method: very fast, a lot faster than Char.IsWhiteSpace
        /// NOTE: If this is not inlined then it will be really slow!!!
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsWhiteSpace(this char ch)
        {
            // this is surprisingly faster than the equivalent if statement
            switch (ch)
            {
                case '\u0009': case '\u000A': case '\u000B': case '\u000C': case '\u000D':
                case '\u0020': case '\u0085': case '\u00A0': case '\u1680': case '\u2000':
                case '\u2001': case '\u2002': case '\u2003': case '\u2004': case '\u2005':
                case '\u2006': case '\u2007': case '\u2008': case '\u2009': case '\u200A':
                case '\u2028': case '\u2029': case '\u202F': case '\u205F': case '\u3000':
                    return true;
                default:
                    return false;
            }
        }


        /// <summary>
        /// Handle apostrophes for SQL statements -- duplicate apostrophe(s)
        /// </summary>
        /// <param name="text">string value for SQL statement</param>
        /// <returns></returns>
        public static string ToSqlString(this string text)
        {
            return string.IsNullOrEmpty(text) ? "" : text.Replace("'", "''");
        }

        /// <summary>
        /// Handle apostrophes for SQL statements -- remove duplicate apostrophe(s)
        /// </summary>
        /// <param name="obj">string value from SQL statement</param>
        /// <returns></returns>
        public static string FromSqlString(this object obj)
        {
            string result;

            if (obj == DBNull.Value || obj == null)
            {
                result = "";
            }
            else
            {
                var text = (string)obj;
                result = text.Replace("''", "'");

            }

            return result;
        }

        /// <summary>
        /// Trim milliseconds from DateTime
        /// </summary>
        /// <param name="dt">Datetime from which to trim the milliseconds</param>
        /// <returns></returns>
        public static DateTime TrimMilliseconds(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0, dt.Kind);
        }

        /// <summary>
        /// Convert ObservableCollection<T> to List<T>
        /// </summary>
        /// <typeparam name="T">Type of objects in the collection</typeparam>
        /// <param name="col">Collection to be converted to a List</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this ObservableCollection<T> col)
        {
            var list = new List<T>();

            foreach (var obj in col)
            {
                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// Clone ObservableCollection<T> to new ObservableCollection<T>
        /// </summary>
        /// <typeparam name="T">Type of the objects in the collection</typeparam>
        /// <param name="col">Collection to be cloned</param>
        /// <returns></returns>
        public static ObservableCollection<T> Clone<T>(this ObservableCollection<T> col)
        {
            var list = new ObservableCollection<T>();

            foreach (var obj in col)
            {
                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// Implementation to allow Insert(index, object) on ObservableCollection<T>
        /// </summary>
        /// <typeparam name="T">Type of objects in the collection</typeparam>
        /// <param name="col">The collection to receive the new object</param>
        /// <param name="idx">Index where the object should be inserted</param>
        /// <param name="obj">Object to be inserted</param>
        /// <returns></returns>
        public static ObservableCollection<T> InsertAt<T>(this ObservableCollection<T> col, int idx, T obj)
        {
            var list = col.ToList();
            list.Insert(idx, obj);

            return new ObservableCollection<T>(list);
        }

        /// <summary>
        /// Convert a List<T> into a DataTable<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (var item in items)
            {
                var values = new object[properties.Length];
                for (var i = 0; i < properties.Length; i++)
                {
                    //inserting property values to data table rows
                    values[i] = properties[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check data table
            return dataTable;
        }
    }
}
