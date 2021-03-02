using GuessWho.Infra.TableStorage.Contracts;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace GuessWho.Models
{
    public class PlayerEntity : TableEntity, IAudit
    {
        public DateTime CreationDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime LastChangeDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
