﻿using GuessWho.Infra.TableStorage.Contracts;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace GuessWho.Models
{
    //partitionkey is playerId, aka oid from auth token
    //rowkey is ?!?! random for now
    public class PlayerEntity : TableEntity, IAudit
    {
        public string Name { get; set; }
        public string Friends { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastChangeDate { get; set; }
    }
}  
