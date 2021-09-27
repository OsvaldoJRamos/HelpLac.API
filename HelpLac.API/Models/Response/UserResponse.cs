namespace HelpLac.API.Models.Response
{
    public class UserResponse
    {
        public string UserName { get; set; }

        public UserResponse(string userName)
        {
            UserName = userName;
        }
    }
}
