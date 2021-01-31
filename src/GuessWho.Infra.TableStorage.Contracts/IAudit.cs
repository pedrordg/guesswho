// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAudit.cs" company="Five Degrees">
// Copyright (c) Five Degrees. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Matrix.PaymentGateway.Infra.TableStorage.Contracts
{
    public interface IAudit
    {
        DateTime CreationDate { get; set; }
        DateTime LastChangeDate { get; set; }
    }
}
