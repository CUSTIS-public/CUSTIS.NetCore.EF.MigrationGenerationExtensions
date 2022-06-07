using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL;

namespace TestEntryPoint
{
    /// <summary> Да, ef core ищет эти сервисы в точке входа... </summary>
    internal class DbDesignTimeServices : CustomNpgsqlDesignTimeServices
    {
    }
}