using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts
{
    /// <summary> Находит разницу в моделях </summary>
    public interface IModelDiffer
    {
        /// <summary> Найти разницу в моделях </summary>
        IReadOnlyCollection<MigrationOperation> GetDiff(IRelationalModel? source, IRelationalModel? target);
    }
}