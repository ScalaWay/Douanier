namespace Douanier.Abstractions.Permissions.Managers
{
    public interface IPermissionGroupManager<TPermissionGroup, TKey>
    {
        Task<TPermissionGroup> CreateAsync(TPermissionGroup group);

        Task<IReadOnlyList<TPermissionGroup>> GetAllAsync();

        Task<DouanierResult> UpdateAsync(TPermissionGroup permission);
    }
}