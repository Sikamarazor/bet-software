namespace BETSoftware.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public byte[] PassHash { get; set; }

        public byte[] PassSalt { get; set; }
    }
}