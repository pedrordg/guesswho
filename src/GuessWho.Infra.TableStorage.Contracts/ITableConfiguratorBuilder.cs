// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITableConfiguratorBuilder.cs" company="Five Degrees">
// Copyright (c) Five Degrees. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace Matrix.PaymentGateway.Infra.TableStorage.Contracts
{
    public interface ITableConfiguratorBuilder
    {
        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        IServiceCollection Services { get; }
    }
}
