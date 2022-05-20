namespace Model.Models;
public class Vender : EntityBase
{
    public Vender()
    {
        CreatedOn = DateTimeOffset.Now;
    }
    [Required(ErrorMessage = "The {0} field is required.")]
    [StringLength(450)]
    public string Name { get; set; }
    [Required(ErrorMessage = "The {0} field is required.")]
    [StringLength(450)]
    public string Slug { get; set; }
    [StringLength(450)]
    public string Email { get; set; }
    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedOn { get; set; }

    public DateTimeOffset LatestUpdatedOn { get; set; }

    public string Descriptions { get; set; }

    public IList<User> Users { get; set; } = new List<User>();
}

