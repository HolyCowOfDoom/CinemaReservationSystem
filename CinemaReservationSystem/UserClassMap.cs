using CsvHelper;
using CsvHelper.Configuration;

public class UserClassMap  : ClassMap<User>
{
    public UserClassMap()
    {
        Map(x => x.ID).Index(0);
        Map(x => x.Name).Index(1);
        Map(x => x.BirthDate).Index(2);
        Map(x => x.Email).Index(3);
        Map(x => x.Admin).Index(4);
        Map(x => x.Password).Index(5);
        //Map(x => x.Reservations).Index(6).TypeConverter<convert>;
        
        // {
        //     var list = new List<string>();
        //     list.Add(row.GetField( 1 ));
        //     list.Add(row.GetField( 2 ));
        //     list.Add(row.GetField( 3 ));
        //     return list;
        // }).Index(6);
    //     .Convert(row =>
    //     {
    //         var columnValue = row.Row.GetField<string>("categories");
    //         return columnValue?.Split(',').ToList() ?? new List<string>();

    //     });
    }
}

// public class JsonNodeConverter : DefaultTypeConverter
// {
//     public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
//     {
//         return JsonSerializer.Deserialize<JsonNode>(text);
//     }
// }
