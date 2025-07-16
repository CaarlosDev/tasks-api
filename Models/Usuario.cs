namespace tasks_api.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PwdHash { get; set; }
        public byte[] PwdSalt { get; set; }
    }
}