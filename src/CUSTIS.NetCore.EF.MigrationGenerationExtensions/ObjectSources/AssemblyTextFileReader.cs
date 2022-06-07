using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.ObjectSources
{
    /// <summary> Reads resources as strings </summary>
    public class AssemblyTextFileReader
    {
        private readonly Assembly _assembly;

        /// <summary> Reads resources as strings </summary>
        public AssemblyTextFileReader(Assembly assembly)
        {
            _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }

        /// <summary> Reads resource <paramref name="resourceName"/> as string </summary>
        public async Task<string> ReadFileAsync(string resourceName)
        {
            await using var stream = _assembly.GetManifestResourceStream(resourceName)
                                     ?? throw new InvalidOperationException($"Assembly {_assembly.FullName} doesn't contain a resource {resourceName}");

            using var reader = new StreamReader(stream);

            return await reader.ReadToEndAsync();
        }

        /// <summary> Reads resource <paramref name="resourceName"/> as string </summary>
        public string ReadFile(string resourceName)
        {
            using var stream = _assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException($"Assembly {_assembly.FullName} doesn't contain a resource {resourceName}");

            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}