namespace Model.Models;
public class Culture : EntityBaseWithTypeId<string>
{
    public Culture(string id)
    {
        Id = id;
    }

    [Required(ErrorMessage = "The {0} field is required.")]
    [StringLength(450)]
    public string Name { get; set; }

    public IList<Resource> Resources { get; set; }
}

