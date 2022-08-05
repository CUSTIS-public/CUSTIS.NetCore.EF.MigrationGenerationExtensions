using System;
using System.Collections.Generic;
using System.Reflection;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.ObjectSources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    /// <summary> Extensions for usage of sql objects </summary>
    public static class SqlObjectsModelExtensions
    {
        /// <summary> Key of data about sql objects </summary>
        internal const string SqlObjectsData = "___SqlObjects";

        internal static IReadOnlyCollection<SqlObject> GetSqlObjects(this IModel model)
        {
            return model[SqlObjectsData] as List<SqlObject> as IReadOnlyCollection<SqlObject> ?? Array.Empty<SqlObject>();
        }

        /// <summary> Adds sql objects </summary>
        public static void AddSqlObjects(this ModelBuilder modelBuilder, IEnumerable<SqlObject> objects)
        {
            modelBuilder.Model.AddSqlObjects(objects);
        }

        /// <summary> Adds sql objects </summary>
        public static void AddSqlObjects(this ModelBuilder modelBuilder, params SqlObject[] objects)
        {
            modelBuilder.Model.AddSqlObjects(objects);
        }

        /// <summary> Adds sql objects from assembly's folder </summary>
        public static void AddSqlObjects(this ModelBuilder modelBuilder, Assembly assembly, string folder)
        {
            var sqlObjects = assembly.GetSqlObjects(folder);
            modelBuilder.Model.AddSqlObjects(sqlObjects);
        }

        /// <summary> Adds sql objects </summary>
        public static void AddSqlObjects(this IMutableModel model, IEnumerable<SqlObject> objects)
        {
            List<SqlObject> list;

            if (model[SqlObjectsData] is List<SqlObject> objList)
            {
                list = objList;
            }
            else
            {
                list = new List<SqlObject>();
                model[SqlObjectsData] = list;
            }

            list.AddRange(objects);
        }
    }
}