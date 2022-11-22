using System.Collections.Generic;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL
{
#pragma warning disable EF1001 // Internal EF Core API usage.
    internal class CustomSqlServerAnnotationCodeGenerator : SqlServerAnnotationCodeGenerator
    {
        public CustomSqlServerAnnotationCodeGenerator(AnnotationCodeGeneratorDependencies dependencies) : base(dependencies)
        {
        }
#pragma warning restore EF1001 // Internal EF Core API usage.

        /// <inheritdoc />
        public override IEnumerable<IAnnotation> FilterIgnoredAnnotations(IEnumerable<IAnnotation> annotations)
        {
            return base.FilterIgnoredAnnotations(annotations).FilterCustomIgnoredAnnotations();
        }
    }
}