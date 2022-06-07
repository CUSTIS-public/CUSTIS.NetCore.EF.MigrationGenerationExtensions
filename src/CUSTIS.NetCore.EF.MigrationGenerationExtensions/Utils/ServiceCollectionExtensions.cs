using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Utils
{
    /// <summary> Расширения для <see cref="IServiceCollection"/> </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary> Удаляет сервис </summary>
        public static void RemoveService<TService>(this IServiceCollection services) where TService : class
        {
            var descriptors = services.Where(s => s.ServiceType == typeof(TService)).ToArray();

            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }
        }

        /// <summary> Заменяет зарегистрированный ранее сервис на singleton </summary>
        public static void ReplaceBySingleton<TService, TNewService>(this IServiceCollection services)
            where TService : class
            where TNewService : class, TService
        {
            services.RemoveService<TService>();
            services.AddSingleton<TService, TNewService>();
        }
    }
}