using System.Collections.Generic;
using System.Linq;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation
{
    /// <summary> Extensions necessary for code generation </summary>
    public static class GenerationExtensions
    {
        /// <summary> Filters annotations </summary>
        public static IEnumerable<IAnnotation> FilterCustomIgnoredAnnotations(this IEnumerable<IAnnotation> annotations)
        {
            return annotations
                .Where(a => a.Name != SqlObjectsModelExtensions.SqlObjectsData);
        }
    }
}