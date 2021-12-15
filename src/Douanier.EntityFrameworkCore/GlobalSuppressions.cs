// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S4457:Parameter validation in \"async\"/\"await\" methods should be wrapped", Justification = "<Pending>", Scope = "member", Target = "~M:Douanier.EntityFrameworkCore.Permissions.Stores.PermissionGrantStore.FindAsync``1(Douanier.Abstractions.Permissions.Stores.PermissionGrantFilter)~System.Threading.Tasks.Task{System.Collections.Generic.IEnumerable{Douanier.Permissions.Models.PermissionGrant}}")]
[assembly: SuppressMessage("Major Code Smell", "S4457:Parameter validation in \"async\"/\"await\" methods should be wrapped", Justification = "<Pending>", Scope = "member", Target = "~M:Douanier.EntityFrameworkCore.Permissions.Stores.PermissionGrantStore.FindOneAsync``1(Douanier.Permissions.Models.Permission,System.String,System.String)~System.Threading.Tasks.Task{Douanier.Permissions.Models.PermissionGrant}")]
[assembly: SuppressMessage("Major Code Smell", "S4457:Parameter validation in \"async\"/\"await\" methods should be wrapped", Justification = "<Pending>", Scope = "member", Target = "~M:Microsoft.Extensions.DependencyInjection.DouanierApplicationBuilderExtensions.UseDouanierEntityFramework(Microsoft.AspNetCore.Builder.IApplicationBuilder)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Builder.IApplicationBuilder}")]
