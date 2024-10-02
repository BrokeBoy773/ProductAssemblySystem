using CSharpFunctionalExtensions;

namespace ProductAssemblySystem.UserManagement.Domain.Entities
{
    public class Permission
    {
        public int Id { get; }
        public string Name { get; } = string.Empty;

        private List<Role> _roles = [];
        public IReadOnlyList<Role> Roles => _roles;

        private Permission()
        {
        }

        private Permission(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Result<Permission> Create(int id, string name)
        {
            Permission validPermission = new(id, name);

            return Result.Success(validPermission);
        }
    }
}
