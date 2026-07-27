using System.ComponentModel.DataAnnotations;
public class User
{
    public  int Id { get; set; }

    [Required]
    public  string Name { get; set; } = "";

    [Range(1,100)]
    public int Age { get; set; }

    [EmailAddress]
    public string ? Email { get; set; }

}