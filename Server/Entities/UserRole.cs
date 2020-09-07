namespace Server.Entities
{
    public class UserRole
    {
        public string UserAccount { get; set; }

        public UserEntity User { get; set; }

        public string RoleName { get; set; }

        public Role Role { get; set; }
    }
}