namespace NetIdentityModel.Models.AccountDto
{
    public class UserClaimsDto
    {
        public UserClaimsDto()
        {
            Claims = new List<UserClaim>();
        }

        public string? UserId { get; set; }
        public List<UserClaim> Claims { get; set; }
    }
}
