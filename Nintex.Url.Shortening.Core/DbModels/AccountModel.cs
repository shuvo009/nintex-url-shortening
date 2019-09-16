namespace Nintex.Url.Shortening.Core.DbModels
{
    public class AccountModel : BaseModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}