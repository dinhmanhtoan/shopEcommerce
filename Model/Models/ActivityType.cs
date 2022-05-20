namespace Model.Models;
public class ActivityType : EntityBase
{
    public ActivityType(long id)
    {
        Id = id;
    }

    [Required(ErrorMessage = "The {0} field is required.")]
    [StringLength(450)]
    public string Name { get; set; }
}

