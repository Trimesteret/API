namespace API.Models.Authentication;

public class Admin : User
{
    private readonly DBContext _context;

    public Admin(DBContext context)
    {
        _context = context;
    }

    override
    public string GetClassName()
    {
        return "Admin";
    }

}
