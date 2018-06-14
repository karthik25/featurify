using Featurify.Contracts;
using Featurify.Transformers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Featurify
{
    public static class FeaturifyServiceCollectionExtensions
    {
        public static IServiceCollection AddFeaturify<TMetaDataFinder, TUserFinder>(this IServiceCollection services, Action<FeaturifyOptions> options)
            where TMetaDataFinder : class, IToggleMetadataFinder
            where TUserFinder : class, IUserInfoStrategy
        {
            return services.AddFeaturify<TMetaDataFinder, TUserFinder, DefaultFeatureNameTransformer>(options);
        }

        public static IServiceCollection AddFeaturify<TMetaDataFinder, TUserFinder, TNameTransformer>(this IServiceCollection services, Action<FeaturifyOptions> options)
            where TMetaDataFinder : class, IToggleMetadataFinder
            where TUserFinder : class, IUserInfoStrategy
            where TNameTransformer : class, IFeatureNameTransformer
        {
            services.Configure(options);

            services.AddScoped<IToggleMetadataFinder, TMetaDataFinder>();
            services.AddScoped<IUserInfoStrategy, TUserFinder>();
            services.AddScoped<IFeatureNameTransformer, TNameTransformer>();
            services.AddScoped<IFeaturifyServer, FeaturifyServer>();

            return services;
        }
    }
}
