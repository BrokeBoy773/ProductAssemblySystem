namespace ProductAssemblySystem.UserManagement.Domain.Entities
{
    public class UserRole
    {
        public Guid UserId { get; private set; }
        public int RoleId { get; private set; }
    }
}
