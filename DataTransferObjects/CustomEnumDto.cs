using API.Enums;

namespace API.DataTransferObjects;

public class CustomEnumDto
{
    public int? Id { get; set; }
    public EnumType EnumType { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}
