﻿namespace Microsoft.Extensions.DependencyInjection;

public interface IDouanierBuilder
{
    /// <summary>
    /// Gets the services.
    /// </summary>
    /// <value>
    /// The services.
    /// </value>
    IServiceCollection Services { get; }
}