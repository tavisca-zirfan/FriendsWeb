using Infrastructure.Model;

namespace BusinessDomain.DomainObjects
{
    public class Role:EntityBase<string>
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
