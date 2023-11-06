namespace API.Dtos;

public class SignupDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int DesiredRole { get; set; }
}
