public class UserDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
}
public class EmailDTO
{
    public string Email { get; set; }
}
public class ChangePasswordDTO : EmailDTO
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}