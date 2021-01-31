using Matrix.PaymentGateway.Infra.TableStorage.Contracts;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuessWho.Models
{
    public class ThemeEntity : TableEntity, IAudit
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastChangeDate { get; set; }
    }
}
