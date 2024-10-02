using CSharpFunctionalExtensions;

namespace ProductAssemblySystem.UserManagement.Domain.Entities
{
    public class Role
    {
        public int Id { get; }
        public string Name { get; } = string.Empty;

        private List<Permission> _permissions = [];
        public IReadOnlyList<Permission> Permissions => _permissions;

        private List<User> _users = [];
        public IReadOnlyList<User> Users => _users;

        private Role()
        {
        }

        private Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Result<Role> Create(int id, string name)
        {
            Role validRole = new(id, name);

            return Result.Success(validRole);
        }
    }
}
