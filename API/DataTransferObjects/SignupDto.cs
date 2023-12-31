using API.Enums;

namespace API.DataTransferObjects;

public class SignupDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string RepeatPassword { get; set; }
}
