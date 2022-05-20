namespace Model.Models;
public class AppSetting : EntityBaseWithTypeId<string>
{
    public AppSetting(string id)
    {
        Id = id;
    }

    [StringLength(450)]
    public string Value { get; set; }
    public bool IsVisibleInCommonSettingPage { get; set; }
}

