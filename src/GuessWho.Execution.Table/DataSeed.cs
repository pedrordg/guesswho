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

            SeedPlayers(serviceProvider);

            SeedThemes(serviceProvider);

            SeedIdols(serviceProvider);

            return tableConfiguratorBuilder;
        }

        public static void SeedPlayers(ServiceProvider serviceProvider)
        {
            ITable<PlayerEntity> playerTable = serviceProvider.GetRequiredService<ITable<PlayerEntity>>();

            IEnumerable<PlayerEntity> steph1 = playerTable.QueryAsync(FilterBuilder.CreateForPartitionKey("82a79a3c-2ec4-4b74-9a8a-52d5ba502454")).Result;
            if (!steph1.Any())
            {
                playerTable.InsertAsync(new PlayerEntity()
                {
                    PartitionKey = "82a79a3c-2ec4-4b74-9a8a-52d5ba502454",
                    RowKey = Guid.NewGuid().ToString(),
                    Name = "Stephanie",
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            IEnumerable<PlayerEntity> steph2 = playerTable.QueryAsync(FilterBuilder.CreateForPartitionKey("f2c190f2-705b-47bf-a342-ef23ce4795d7")).Result;
            if (!steph2.Any())
            {
                playerTable.InsertAsync(new PlayerEntity()
                {
                    PartitionKey = "f2c190f2-705b-47bf-a342-ef23ce4795d7",
                    RowKey = Guid.NewGuid().ToString(),
                    Name = "Steph",
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            IEnumerable<PlayerEntity> pedro1 = playerTable.QueryAsync(FilterBuilder.CreateForPartitionKey("37bdd8c2-4315-4e70-8470-aae61805ba1e")).Result;
            if (!pedro1.Any())
            {
                playerTable.InsertAsync(new PlayerEntity()
                {
                    PartitionKey = "37bdd8c2-4315-4e70-8470-aae61805ba1e",
                    RowKey = Guid.NewGuid().ToString(),
                    Name = "Pedro",
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            IEnumerable<PlayerEntity> pedro2 = playerTable.QueryAsync(FilterBuilder.CreateForPartitionKey("40a63e1c-c39f-4265-b022-e5340a9c59c6")).Result;
            if (!pedro2.Any())
            {
                playerTable.InsertAsync(new PlayerEntity()
                {
                    PartitionKey = "40a63e1c-c39f-4265-b022-e5340a9c59c6",
                    RowKey = Guid.NewGuid().ToString(),
                    Name = "Pedro giga1 email",
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            ITable<PlayerRelationEntity> playerRelationTable = serviceProvider.GetRequiredService<ITable<PlayerRelationEntity>>();
            //stephs friends
            IEnumerable<PlayerRelationEntity> stephsfriends = playerRelationTable.QueryAsync(FilterBuilder.CreateForPartitionKey("82a79a3c-2ec4-4b74-9a8a-52d5ba502454")).Result;
            if (!stephsfriends.Any(c => c.RowKey == "37bdd8c2-4315-4e70-8470-aae61805ba1e"))
            {
                //pedro1
                playerRelationTable.InsertAsync(new PlayerRelationEntity()
                {
                    PartitionKey = "82a79a3c-2ec4-4b74-9a8a-52d5ba502454",
                    RowKey = "37bdd8c2-4315-4e70-8470-aae61805ba1e",
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            //pedro1 friends
            IEnumerable<PlayerRelationEntity> pedro1Friends = playerRelationTable.QueryAsync(FilterBuilder.CreateForPartitionKey("37bdd8c2-4315-4e70-8470-aae61805ba1e")).Result;
            if (!pedro1Friends.Any(c => c.RowKey == "82a79a3c-2ec4-4b74-9a8a-52d5ba502454"))
            {
                //steph1
                playerRelationTable.InsertAsync(new PlayerRelationEntity()
                {
                    PartitionKey = "37bdd8c2-4315-4e70-8470-aae61805ba1e",
                    RowKey = "82a79a3c-2ec4-4b74-9a8a-52d5ba502454",
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }
            if (!pedro1Friends.Any(c => c.RowKey == "f2c190f2-705b-47bf-a342-ef23ce4795d7"))
            {
                //steph2
                playerRelationTable.InsertAsync(new PlayerRelationEntity()
                {
                    PartitionKey = "37bdd8c2-4315-4e70-8470-aae61805ba1e",
                    RowKey = "f2c190f2-705b-47bf-a342-ef23ce4795d7",
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }
            if (!pedro1Friends.Any(c => c.RowKey == "40a63e1c-c39f-4265-b022-e5340a9c59c6"))
            {
                //pedro2
                playerRelationTable.InsertAsync(new PlayerRelationEntity()
                {
                    PartitionKey = "37bdd8c2-4315-4e70-8470-aae61805ba1e",
                    RowKey = "40a63e1c-c39f-4265-b022-e5340a9c59c6",
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }

            //pedro2 friends
            IEnumerable<PlayerRelationEntity> pedro2friends = playerRelationTable.QueryAsync(FilterBuilder.CreateForPartitionKey("40a63e1c-c39f-4265-b022-e5340a9c59c6")).Result;
            if (!pedro2friends.Any(c => c.RowKey == "37bdd8c2-4315-4e70-8470-aae61805ba1e"))
            {
                //pedro1
                playerRelationTable.InsertAsync(new PlayerRelationEntity()
                {
                    PartitionKey = "40a63e1c-c39f-4265-b022-e5340a9c59c6",
                    RowKey = "37bdd8c2-4315-4e70-8470-aae61805ba1e",
                    CreationDate = DateTime.UtcNow,
                    LastChangeDate = DateTime.UtcNow
                }).Wait();
            }
        }

        public static void SeedThemes(ServiceProvider serviceProvider) 
        {
            ITable<ThemeEntity> themeTable = serviceProvider.GetRequiredService<ITable<ThemeEntity>>();
            IEnumerable<ThemeEntity> themes = themeTable.QueryAsync(FilterBuilder.CreateForPartitionKey(Constants.OurBiasTheme)).Result;

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
