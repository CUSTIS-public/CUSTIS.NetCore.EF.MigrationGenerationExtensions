using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation
{
    /// <summary> Custom migration operation </summary>
    /// <remarks> All custom operation must inherit it </remarks>
    public abstract class CustomMigrationOperation : MigrationOperation
    {
    }
}