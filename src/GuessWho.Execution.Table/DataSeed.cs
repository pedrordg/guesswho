using GuessWho.Execution.Contracts;
using GuessWho.Infra.TableStorage.Contracts;
using GuessWho.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessWho.Execution.Table
{
    public static class DataSeed
    {
        /// <summary>
        /// Seeds the specified table configurator builder.
        /// </summary>
        /// <param name="tableConfiguratorBuilder">The table configurator builder.</param>
        /// <returns></returns>
        public static ITableConfiguratorBuilder Seed(this ITableConfiguratorBuilder tableConfiguratorBuilder)
        {
            ServiceProvider serviceProvider = tableConfiguratorBuilder.Services.BuildServiceProvider();

            SeedThemes(serviceProvider);

            SeedIdols(serviceProvider);

            return tableConfiguratorBuilder;
        }

        public static void SeedThemes(ServiceProvider serviceProvider) 
        {
            ITable<ThemeEntity> themeTable = serviceProvider.GetRequiredService<ITable<ThemeEntity>>();
            IEnumerable<ThemeEntity> themes = themeTable.QueryAsync("").Result;

            if (!themes.Any(c => c.PartitionKey == Constants.OurBiasTheme))
            {
                themeTable.InsertAsync(new ThemeEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = nameof(Constants.OurBiasTheme),
                    Name = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }
        }


        public static void SeedIdols(ServiceProvider serviceProvider)
        {
            ITable<IdolEntity> idolTable = serviceProvider.GetRequiredService<ITable<IdolEntity>>();
            IEnumerable<IdolEntity> idols = idolTable.QueryAsync(FilterBuilder.CreateForPartitionKey(Constants.OurBiasTheme)).Result;

            // #1
            if (!idols.Any(c => c.Name == nameof(Constants.GDragon)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.GDragon,
                    Name = nameof(Constants.GDragon),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #2
            if (!idols.Any(c => c.Name == nameof(Constants.Chen)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Chen,
                    Name = nameof(Constants.Chen),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #3
            if (!idols.Any(c => c.Name == nameof(Constants.Lucas)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Lucas,
                    Name = nameof(Constants.Lucas),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #4
            if (!idols.Any(c => c.Name == nameof(Constants.Hongjoon)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Hongjoon,
                    Name = nameof(Constants.Hongjoon),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #5
            if (!idols.Any(c => c.Name == nameof(Constants.Ten)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Ten,
                    Name = nameof(Constants.Ten),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #6
            if (!idols.Any(c => c.Name == nameof(Constants.Hwasa)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Hwasa,
                    Name = nameof(Constants.Hwasa),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }



            // #7
            if (!idols.Any(c => c.Name == nameof(Constants.Lisa)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Lisa,
                    Name = nameof(Constants.Lisa),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #8
            if (!idols.Any(c => c.Name == nameof(Constants.Yugyeom)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Yugyeom,
                    Name = nameof(Constants.Yugyeom),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #9
            if (!idols.Any(c => c.Name == nameof(Constants.Taemin)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Taemin,
                    Name = nameof(Constants.Taemin),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #10
            if (!idols.Any(c => c.Name == nameof(Constants.Hobi)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Hobi,
                    Name = nameof(Constants.Hobi),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #11
            if (!idols.Any(c => c.Name == nameof(Constants.JB)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.JB,
                    Name = nameof(Constants.JB),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #12
            if (!idols.Any(c => c.Name == nameof(Constants.Jennie)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Jennie,
                    Name = nameof(Constants.Jennie),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #13
            if (!idols.Any(c => c.Name == nameof(Constants.RM)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.RM,
                    Name = nameof(Constants.RM),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #14
            if (!idols.Any(c => c.Name == nameof(Constants.Dooyoung)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Dooyoung,
                    Name = nameof(Constants.Dooyoung),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #15
            if (!idols.Any(c => c.Name == nameof(Constants.Baekyun)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Baekyun,
                    Name = nameof(Constants.Baekyun),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #16
            if (!idols.Any(c => c.Name == nameof(Constants.Taeyong)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Taeyong,
                    Name = nameof(Constants.Taeyong),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #17
            if (!idols.Any(c => c.Name == nameof(Constants.Solar)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Solar,
                    Name = nameof(Constants.Solar),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #18
            if (!idols.Any(c => c.Name == nameof(Constants.San)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.San,
                    Name = nameof(Constants.San),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #19
            if (!idols.Any(c => c.Name == nameof(Constants.Chanyeol)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Chanyeol,
                    Name = nameof(Constants.Chanyeol),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #20
            if (!idols.Any(c => c.Name == nameof(Constants.Seongwa)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Seongwa,
                    Name = nameof(Constants.Seongwa),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #21
            if (!idols.Any(c => c.Name == nameof(Constants.Sunmi)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Sunmi,
                    Name = nameof(Constants.Sunmi),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #22
            if (!idols.Any(c => c.Name == nameof(Constants.Mark)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Mark,
                    Name = nameof(Constants.Mark),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #23
            if (!idols.Any(c => c.Name == nameof(Constants.Kai)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Kai,
                    Name = nameof(Constants.Kai),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            // #24
            if (!idols.Any(c => c.Name == nameof(Constants.Jackson)))
            {
                idolTable.InsertAsync(new IdolEntity()
                {
                    PartitionKey = Constants.OurBiasTheme,
                    RowKey = Constants.Jackson,
                    Name = nameof(Constants.Jackson),
                    ThemeName = nameof(Constants.OurBiasTheme),
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }
        }
    }
}
