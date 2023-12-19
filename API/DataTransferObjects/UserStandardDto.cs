using API.Enums;

namespace API.DataTransferObjects;

public class UserStandardDto
{
    public int? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public bool? SignedUp { get; set; }
    public Role Role { get; set; }
}
