using System.Globalization;
using System.Reflection;
using System.Collections;

public static class CsvHandler
{
    public static List<T> Read<T>(string csvFile)
    {
        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            
            PrepareHeaderForMatch = args => args.Header.ToLowerInvariant(),
            //https://stackoverflow.com/questions/49521193/csvhelper-ignore-case-for-header-names
            //is this still necessary?
        };
        using StreamReader reader = new(csvFile);
        using CsvHelper.CsvReader csvReader = new(reader, config);
        csvReader.Context.TypeConverterCache.AddConverter<List<Reservation>>(new ReservationConverter());
        return csvReader.GetRecords<T>().ToList();
    }

    public static bool Write<T>(string csvFile, List<T> recordsList)
    {
        using StreamWriter writer = new(csvFile);
        using CsvHelper.CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture);
        csvWriter.Context.TypeConverterCache.AddConverter<List<Reservation>>(new ReservationConverter()); 
        csvWriter.WriteRecords(recordsList);
        return true;
    }
 
    public static bool Append<T>(string csvFile, List<T> records)
    {
        bool fileEmpty = false;
        if(new FileInfo(csvFile).Length == 0) fileEmpty = true; //checks is file is empty
        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            // Don't write the header again.
            HasHeaderRecord = fileEmpty ? true : false, //write header if file is empty
        };
        using StreamWriter writer = new(csvFile, true);
        using CsvHelper.CsvWriter csvWriter = new(writer, config);
        csvWriter.Context.TypeConverterCache.AddConverter<List<Reservation>>(new ReservationConverter());  
        csvWriter.WriteRecords(records);
        return true;
    }

    //example (UserDBFilePath, "ID", 1) -> returns User obj with ID 1
    //example (UserDBFilePath, "Name", "Utku") -> returns User obj with Name Utku`
    //T specifies return type!
    public static T GetRecordWithValue<T>(string csvFile, string header, object hasValue) 
    {
        if(hasValue is null){
            Console.WriteLine("hasValue was null, it can't be null!");
            return default;
        }
        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLowerInvariant(),
            //https://stackoverflow.com/questions/49521193/csvhelper-ignore-case-for-header-names
        };
        using StreamReader reader = new(csvFile);
        using CsvHelper.CsvReader csvReader = new(reader, config);
        csvReader.Context.TypeConverterCache.AddConverter<Reservation>(new ReservationConverter()); 
        csvReader.Context.TypeConverterCache.AddConverter<List<Reservation>>(new ReservationConverter()); 
        
        csvReader.Read();
        csvReader.ReadHeader();
        object csvField = null;
        while (csvReader.Read())
        {
            csvField = csvReader.GetField(hasValue.GetType(), header); 
            if(csvField is ICollection && hasValue is not ICollection) //for example searching specific Reservation object in Reservations List
            {
                //reverse of //https://stackoverflow.com/questions/2837063/cast-object-to-generic-list
                //casts generic (Reservation) list to object list, value is object
                List<object> propertyList = (csvField as IEnumerable<object>).Cast<object>().ToList();
                //dynamic propertyList = Convert.ChangeType(csvField, csvField.GetType());
                for(int i = 0; i < propertyList.Count; i++)
                {
                    if(hasValue.Equals(propertyList[i])) 
                    //if (propertyList[i].Equals((object)value))
                    {
                        //Console.WriteLine($"found record with property {header} containing value {hasValue}");
                        T record = csvReader.GetRecord<T>();
                        return record; //will never be null due to if statement
                    }
                }
            }
            //some debugging information
            // if(csvField is ICollection)
            // {
            //     List<object> valueList = (csvField as IEnumerable<object>).Cast<object>().ToList();
            //     Console.WriteLine($"csvfield {csvField} is a collection containing: {ListInfo(valueList)}");
            // }
            // else Console.WriteLine($"csvfield: {csvField}");
            // if(hasValue is ICollection) 
            // {
            //     //reverse of //https://stackoverflow.com/questions/2837063/cast-object-to-generic-list
            //     //casts generic (Reservation) list to object list, value is object
            //     List<object> valueList = (hasValue as IEnumerable<object>).Cast<object>().ToList();
            //     Console.WriteLine($"searched value {hasValue} is a collection containing: {ListInfo(valueList)}");
            // }
            //else Console.WriteLine($"searched value: {hasValue}");
            if(csvField is ICollection && hasValue is ICollection) //if they're Lists, Equals() wont work, SequenceEqual will
            {
                //https://stackoverflow.com/questions/972636/casting-a-variable-using-a-type-variable
                //basically converts csvField (which is just 'object') back to it's actual type (List<Reservation>)
                //List<Reservation> csvFieldList would defeat the purpose of using generics
                //var csvFieldList doesn't work with SequenceEqual() below
                //dynamic DOES work with SequenceEqual() below
                dynamic csvFieldList = Convert.ChangeType(csvField, csvField.GetType());
                dynamic hasValueList = Convert.ChangeType(hasValue, csvField.GetType());
                //ICollection inherits from IEnumerable, we only want to call one method from Enumerable so it's safe.
                //"public interface ICollection<T> : System.Collections.Generic.IEnumerable<T>"
               // Console.WriteLine($"csvFieldList: {csvFieldList.ToString()}");
                //Console.WriteLine($"hasValueList: {hasValueList.ToString()}");
                if(Enumerable.SequenceEqual(csvFieldList, hasValueList)) //SequenceEqual needs the objects to be Enumerable, so cast to List will do
                {
                    //Console.WriteLine("the lists are equal");
                    T record = csvReader.GetRecord<T>();
                    return record; //will never be null due to if statement
                }
            }
            else if(csvField.Equals(hasValue))
            {
                //Console.WriteLine($"found record with property value ({csvField}) matching searched value ({hasValue})");
                T record = csvReader.GetRecord<T>();
                return record; //will never be null due to if statement
            }
        }
        Console.WriteLine($"in GetRecordWithValue(): Couldn't find record with property {header} matching value {hasValue} in {csvFile}.");
        // if(csvField is ICollection)
        // {
        //     List<object> valueList = (csvField as IEnumerable<object>).Cast<object>().ToList();
        //     Console.WriteLine($"csvfield {csvField} is a collection containing: {ListInfo(valueList)}");
        // }
        // else Console.WriteLine($"csvfield: {csvField}");
        // if(hasValue is ICollection) 
        // {
        //     //reverse of //https://stackoverflow.com/questions/2837063/cast-object-to-generic-list
        //     //casts generic (Reservation) list to object list, value is object
        //     List<object> valueList = (hasValue as IEnumerable<object>).Cast<object>().ToList();
        //     Console.WriteLine($"searched value {hasValue} is a collection containing: {ListInfo(valueList)}");
        // }
        // else Console.WriteLine($"searched value: {hasValue}");
        return default;
    }

    //"J" can't be replaced with "object" as MySetProperty needs List<J> rather than List<object>
    public static bool UpdateRecordWithValue<T, J>(string csvFile, T record, string header, J newValue)
    {
        List<T> records = Read<T>(csvFile);
        // for(int i = 0; i < records.Count; i++)
        // {
        //     //object propertyObject = MyGetProperty<object, T>(records[i], header);
        //     // List<Reservation> reservations = (List<Reservation>)MyGetProperty<object, T>(records[i], "Reservations");
        //     // if(reservations is null) reservations = new();
        //     string ID = (string)MyGetProperty<string, T>(records[i], "ID");
        //     Console.WriteLine("ID: " + ID);
        //     foreach(Reservation reservation in reservations)
        //     {
        //         Console.WriteLine($"SeatIDs: {reservation.SeatIDs}; ScreeningID: {reservation.ScreeningID}; TotalPrice: {reservation.ScreeningID}");
        //     }
        // }
        for(int i = 0; i < records.Count; i++) //for each record in DB
        {
            //Console.WriteLine(records[i]);
           // Console.WriteLine(record);
            if(records[i].Equals(record)) //if record in DB matches given record
            {
                object propertyValue = MyGetProperty<object, T>(records[i], header); //get property of record in DB using header
                if(propertyValue == null) 
                {
                    //Console.WriteLine($"A record matching {record} was found in the database, but it has no property named {header}");
                    return false; //each record SHOULD be unique, so there's no point in continuing search if a match is already found.
                    //break; 
                }

                //we don't HAVE to check if it's an ICollection (List) anymore, as it's now set the same way as a regular variable now
                //but it's better to keep them seperate to allow for more detailed debugging
                if(propertyValue is ICollection || newValue is ICollection) //if object associated with header is e.g. a List
                {
                    if(propertyValue is ICollection && newValue is ICollection)
                    {
                        MySetProperty(records[i], header, newValue);
                        Write(csvFile, records);
                        //Console.WriteLine($"Property {header} of record {record} was set to new ICollection (probably a list): {newValue}");
                        return true;
                    }
                    else if(propertyValue is not ICollection){
                        //Console.WriteLine("Attempting to set a non-ICollection property to an ICollection value");
                        return false;
                    }
                    else if(newValue is not ICollection){
                       // Console.WriteLine("Attempting to set an ICollection property to a non-ICollection value");
                        return false;
                    }
                    
                }
                else //if value of property is not an ICollection, continue setting it regularly
                {
                    MySetProperty(records[i], header, newValue);
                    Write(csvFile, records);
                   // Console.WriteLine($"Property {header} of record {record} changed succesfully to {newValue}");
                    return true;
                }
            }
        }
        Console.WriteLine($"in UpdateRecordWithValue(): No record found in db matching {record}");
        return false;
    }

    public static bool AddValueToRecord<T, J>(string csvFile, T record, string header, J addValue)
    {
        List<T> records = Read<T>(csvFile);
        for(int i = 0; i < records.Count; i++) //for each record in DB
        {
            if(records[i].Equals(record)) //if record in DB matches given record
            {
                object propertyObject = MyGetProperty<object, T>(records[i], header); //get property of record in DB using header
                if(propertyObject == null) break;
                //Console.WriteLine("propertyObject: ", propertyObject);
                if(propertyObject is ICollection) //if object associated with header is e.g. a List
                {
                    //https://stackoverflow.com/questions/2837063/cast-object-to-generic-list
                    List<J> propertyList = (propertyObject as IEnumerable<J>).Cast<J>().ToList();
                    propertyList.Add(addValue);
                    MySetProperty(records[i], header, propertyList);
                    Write(csvFile, records);
                    //Console.WriteLine($"{addValue} was added to list of Property {header} ");
                    return true;
                }
                else
                {
                    //Console.WriteLine("Attempted to add a value to a non-list property!");
                    return false;
                }
            }
        }
        Console.WriteLine($"in AddValueToRecord(): No record found with property {header} matching value {addValue}");
        return false;
    }

    private static void MySetProperty<T>(T objToChange, string propertyToChange, object newValue)
    {
        PropertyInfo? property = typeof(T).GetProperty(propertyToChange);
        if(property != null) property.SetValue(objToChange, newValue);
        //else Console.WriteLine($"property {propertyToChange} remains unchanged");
    } 

    //must specify return type J in call because compiler can't figure out return type
    public static J MyGetProperty<J, T>(T record, string propertyToGet)
    {
        PropertyInfo? propertyInfo = typeof(T).GetProperty(propertyToGet);
        if(propertyInfo != null)
            {
            J property = (J)propertyInfo.GetValue(record);
           // Console.WriteLine($"property {propertyToGet} was found! it is {property}");
            if(property != null) return property;
            else
            {
                Console.WriteLine($"property {propertyToGet} was not found or is null (2)");
                return default;
            }
        }
        else 
        {
            Console.WriteLine($"property {propertyToGet} was not found or is null (1)");
            return default;
        }
    }

    public static string ListInfo(List<object> aList)
    {
        string data = "{";
        for(int i = 0; i < aList.Count; i++) //for each object in the list
        {
            //if type is user-defined
            //https://stackoverflow.com/questions/23793845/how-do-i-determine-if-a-property-is-a-user-defined-type-in-c/23794004#23794004
            if(aList[i].GetType().Assembly.GetName().Name != "System.Private.CoreLib") {
                PropertyInfo[] propertyInfo = aList[i].GetType().GetProperties(); //get all the objects properties
                List<(string, object)> propertydata = new(); //designate tuple list for structure (property Name, property value)
                for(int j = 0; j < propertyInfo.Length; j++) //for each property
                {
                    int indexedparamsCount = propertyInfo[j].GetIndexParameters().Length;
                    var propertyValue = propertyInfo[j].GetValue(aList[i]);
                    
                    //if property is a list, recursively get data from that list too //cast 
                    if(propertyValue is IList)
                    {
                        List<object> castvalues = (propertyValue as IEnumerable<object>).Cast<object>().ToList();
                        propertydata.Add((propertyInfo[j].Name, ListInfo(castvalues)));
                    }
                    //propertyInfo does not contain a value, just attributes and metadata about the property,
                    //we can get the value of the property in a certain object using GetValue()
                    //Console.WriteLine($"property {propertyInfo[j]} has value {propertyValue}");

                    else propertydata.Add((propertyInfo[j].Name, propertyValue)); //adds tuple(property Name, property value) to data list
                    
                }
                data += $"\n {aList[i].GetType()}: {(string.Join(", ", propertydata))}";
            }
            //if type is NOT user-defined
            else{
                data += $"{aList[i].GetType()}: {aList[i]}";
            }
        }
        data += "}";
        return data;
    }
    
    // public static void CreateTestFile(string fileName)
    // {
    //     StreamReader reader = new(fileName);
        
    //    File.Create(fileName); //overwrites file with same name, to avoid duplicates
    // }

    // public static int CountRecords(string csvFile)
    // {
    //     var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
    //     {
    //         //PrepareHeaderForMatch = args => args.Header.ToLowerInvariant(),
    //         //https://stackoverflow.com/questions/49521193/csvhelper-ignore-case-for-header-names
    //     };
    //     using StreamReader reader = new(csvFile);
    //     using CsvHelper.CsvReader csvReader = new(reader, config);
    //     try{
    //         csvReader.Read();
    //         csvReader.ReadHeader();
    //     }
    //     catch (CsvHelper.ReaderException e)
    //     {
    //         //if there is no header
    //         return 0;
    //     }
        

    //     int count = 0;
    //     while (csvReader.Read())
    //     {
    //         count++;
    //     }
    //     return count;
    // }
}