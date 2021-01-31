﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableConfiguratorBuilder.cs" company="Five Degrees">
// Copyright (c) Five Degrees. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Matrix.PaymentGateway.Infra.TableStorage.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Matrix.PaymentGateway.Infra.TableStorage.Setup
{
    public class TableConfiguratorBuilder : ITableConfiguratorBuilder
    {
        /// <inheritdoc />
        public TableConfiguratorBuilder(IServiceCollection services)
        {
            this.Services = services;
        }

        /// <inheritdoc />
        public IServiceCollection Services { get; }
    }

}