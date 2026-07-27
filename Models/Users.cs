using System.ComponentModel.DataAnnotations;
public class User
{
    public  int Id { get; set; }

    [Required]
     [StringLength(100)]
    public  string Name { get; set; } = "";

    [Range(1,100)]
    public int Age { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

}