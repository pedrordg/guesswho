using GuessWho.Infra.TableStorage.Contracts;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace GuessWho.Models
{
    public class IdolEntity : TableEntity, IAudit
    {
        public string Name { get; set; }
        public string ThemeName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastChangeDate { get; set; }
    }
}
