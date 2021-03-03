using GuessWho.Infra.TableStorage.Contracts;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace GuessWho.Models
{
    //partitionkey is playerId
    //rowkey is friendId
    public class PlayerRelationEntity : TableEntity, IAudit
    {
        public DateTime CreationDate { get; set; }
        public DateTime LastChangeDate { get; set; }
    }
}  
