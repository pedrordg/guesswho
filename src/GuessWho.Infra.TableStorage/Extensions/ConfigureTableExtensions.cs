// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureTableExtensions.cs" company="Five Degrees">
// Copyright (c) Five Degrees. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Matrix.PaymentGateway.Infra.TableStorage.Configuration;
using Matrix.PaymentGateway.Infra.TableStorage.Contracts;
using Matrix.PaymentGateway.Infra.TableStorage.Setup;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Matrix.PaymentGateway.Infra.TableStorage.Extensions
{
    public static class ConfigureTableExtensions
    {
        /// <summary>
        /// Configures the storage table.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static ITableConfiguratorBuilder ConfigureStorageTable(this IServiceCollection services)
        {
            services.AddTransient<IAuditSigner, AuditSigner>();
            services.AddSingleton<IConfigureOptions<TableOptions>, ConfigureTableOptions>();
            services.AddScoped(provider =>
            {
                IOptions<TableOptions> tableOptions = provider.GetRequiredService<IOptions<TableOptions>>();
                var uriString = tableOptions.Value.StorageUrl ?? throw new Exception("TableStorageUrl needs to be configured!");
                var accountName = tableOptions.Value.AccountName ?? throw new Exception("AccountName needs to be configured!");
                var accountKey = tableOptions.Value.AccountKey ?? throw new Exception("AccountKey needs to be configured!");

                return new CloudTableClient(new Uri(uriString), new StorageCredentials(accountName, accountKey));
            });

            return new TableConfiguratorBuilder(services);
        }

        /// <summary>
        /// Adds the table.
        /// </summary>
        /// <typeparam name="TTableEntity">The type of the table entity.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">builder</exception>
        public static ITableConfiguratorBuilder AddTable<TTableEntity>(this ITableConfiguratorBuilder builder, string tableName)
                where TTableEntity : class, ITableEntity, IAudit, new()
        {
            if (builder == null) throw new Exception(nameof(builder));

            builder.Services.AddScoped<ITable<TTableEntity>>(provider => 
            {
                var auditSigner = provider.GetRequiredService<IAuditSigner>();
                if(auditSigner == null)
                {
                    throw new Exception("AuditSigner not properly injected");
                }

                var table = new StorageTable<TTableEntity>(provider.GetRequiredService<CloudTableClient>(), tableName, auditSigner);
                table.CreateIfNotExists();
                return table;
            });

            return builder;
        }
    }
}
