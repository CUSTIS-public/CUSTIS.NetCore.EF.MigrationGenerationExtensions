using System.Collections.Generic;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Design.Internal;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL
{
#pragma warning disable EF1001 // Internal EF Core API usage.
    internal class CustomNpgsqlAnnotationCodeGenerator : NpgsqlAnnotationCodeGenerator
    {
        public CustomNpgsqlAnnotationCodeGenerator(AnnotationCodeGeneratorDependencies dependencies) : base(dependencies)
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