using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.ObjectSources
{
    /// <summary> Helper methods for iterating through assembly resources </summary>
    public static class AssemblyExtensions
    {
        /// <summary> Gets resource from assembly by its name </summary>
        public static string GetManifestResourceName(this Assembly assembly, string fileName)
        {
            var name = assembly.GetManifestResourceNames()
                .SingleOrDefault(n => n.EndsWith(fileName, StringComparison.InvariantCultureIgnoreCase));

            if (string.IsNullOrEmpty(name))
            {
                throw new FileNotFoundException($"Embedded file '{fileName}' could not be found in assembly '{assembly.FullName}'.", fileName);
            }

            return name;
        }

        /// <summary> Information about resource's name </summary>
        public record ResourceInfo(string FullName, string ResourceName);

        /// <summary> Get all SQL-embedded resources from folder </summary>
        public static IReadOnlyCollection<ResourceInfo> GetAllSql(this Assembly assembly, string folder)
        {
            var folderName = $"{assembly.GetName().Name}.{folder}";
            return assembly
                .GetManifestResourceNames()
                .Where(r => r.StartsWith(folderName) && r.EndsWith(".sql"))
                .Select(r => new ResourceInfo(r, r.Substring(folderName.Length + 1)))
                .ToArray();
        }

        /// <summary> Get all SQL-objects from assembly's folder </summary>
        public static IEnumerable<SqlObject> GetSqlObjects(this Assembly assembly, string folder)
        {
            var reader = new AssemblyTextFileReader(assembly);

            foreach (var sqlObject in assembly.GetAllSql(folder))
            {
                var text = reader.ReadFile(sqlObject.FullName);

                yield return new(sqlObject.ResourceName, text);
            }
        }

    }
}