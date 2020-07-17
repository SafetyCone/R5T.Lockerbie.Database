using System;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;

using R5T.Dacia;


namespace R5T.Lockerbie.Database
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="DatabaseLocalFileInfoRepository{TDbContext}"/> implementation of <see cref="ILocalFileInfoRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddLocalFileInfoRepository<TDbContext>(this IServiceCollection services)
            where TDbContext: DbContext, ILocalFileInfoDbContext
        {
            services.AddSingleton<ILocalFileInfoRepository, DatabaseLocalFileInfoRepository<TDbContext>>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="DatabaseLocalFileInfoRepository{TDbContext}"/> implementation of <see cref="ILocalFileInfoRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<ILocalFileInfoRepository> AddLocalFileInfoRepositoryAction<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext, ILocalFileInfoDbContext
        {
            var serviceAction = ServiceAction.New<ILocalFileInfoRepository>(() => services.AddLocalFileInfoRepository<TDbContext>());
            return serviceAction;
        }
    }
}
