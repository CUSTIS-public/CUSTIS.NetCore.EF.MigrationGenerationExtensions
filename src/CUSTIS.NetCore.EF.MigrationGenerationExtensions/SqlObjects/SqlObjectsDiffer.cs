using System;
using System.Collections.Generic;
using System.Linq;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Utils;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    /// <summary> Gets SqlObjects changes </summary>
    internal class SqlObjectsDiffer : IModelDiffer
    {
        public IReadOnlyCollection<MigrationOperation> GetDiff(IRelationalModel? source, IRelationalModel? target)
        {
            var sourceViews = source?.Model.GetSqlObjects() ?? new SqlObject[0];
            var targetViews = target?.Model.GetSqlObjects() ?? new SqlObject[0];

            return GetDiff(sourceViews, targetViews);
        }

        /// <summary> Получает изменения грантов </summary>
        private IReadOnlyCollection<MigrationOperation> GetDiff(IReadOnlyCollection<SqlObject> source, IReadOnlyCollection<SqlObject> target)
        {
            var sourceDic = ToDictionary(source);
            var targetDic = ToDictionary(target);

            var newObjs = targetDic.Keys.Except(sourceDic.Keys);
            var droppedObjs = sourceDic.Keys.Except(targetDic.Keys);

            var possibleChanges = sourceDic.Keys.Intersect(targetDic.Keys);
            var changes = new List<CreateOrUpdateSqlObjectOperation>();
            foreach (var key in possibleChanges)
            {
                var sourceObj = sourceDic[key];
                var targetObj = targetDic[key];

                if (sourceObj.SqlCode != targetObj.SqlCode)
                {
                    changes.Add(new CreateOrUpdateSqlObjectOperation(targetObj));
                }
            }

            return droppedObjs.Select(g => (MigrationOperation)new DropSqlObjectOperation(sourceDic[g]))
                .Concat(newObjs.Select(g => new CreateOrUpdateSqlObjectOperation(targetDic[g])))
                .Concat(changes)
                .ToArray();
        }

        private static Dictionary<string, SqlObject> ToDictionary(IReadOnlyCollection<SqlObject> objects)
        {
            var groups = objects
                .GroupBy(s => s.Name.Trim().ToUpper()).ToArray();

            var multiObjs = groups.Where(g => g.Count() > 1).ToArray();
            if (multiObjs.Any())
            {
                throw new InvalidOperationException($"The following objects have multiple definitions: " +
                                                    $"[{multiObjs.Select(g => g.Key).JoinBySeparator("], [")}]");
            }

            return groups.ToDictionary(g => g.Key, g => g.Single());
        }
    }
}