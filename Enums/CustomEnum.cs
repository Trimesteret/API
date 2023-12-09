namespace API.Enums;

public class CustomEnum
{
    public int Id { get; set; }
    public EnumType EnumType { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }

    /// <summary>
    /// Empty constructor for Entity Framework
    /// </summary>
    public CustomEnum()
    {

    }

    public CustomEnum(EnumType enumType, string key, string value)
    {
        EnumType = enumType;
        Key = key;
        Value = value;
    }
}
