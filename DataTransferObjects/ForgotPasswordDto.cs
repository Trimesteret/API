namespace API.DataTransferObjects;

public class ForgotPasswordDto
{
    public string Email { get; set; }
    public string? Password { get; set; }
    public string? NewPasswordOne { get; set; }
    public string? NewPasswordTwo { get; set; }
}
