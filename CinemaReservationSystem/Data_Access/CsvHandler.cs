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
        while (csvReader.Read())
        {
            object csvField = csvReader.GetField(hasValue.GetType(), header); 
            if(csvField is ICollection)
            {
                //reverse of //https://stackoverflow.com/questions/2837063/cast-object-to-generic-list
                //casts generic (Reservation) list to object list, value is object
                List<object> propertyList = (csvField as IEnumerable<object>).Cast<object>().ToList();
                for(int i = 0; i < propertyList.Count; i++)
                {
                    if(hasValue.Equals(propertyList[i])) 
                    //if (propertyList[i].Equals((object)value))
                    {
                        T record = csvReader.GetRecord<T>();
                        return record; //will never be null due to if statement
                    }
                }
            }
            else if(csvField.Equals(hasValue))
            {
                T record = csvReader.GetRecord<T>();
                return record; //will never be null due to if statement
            }
        }
        Console.WriteLine($"Couldn't get record with {header} matching {hasValue}, ");
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
                object propertyObject = MyGetProperty<object, T>(records[i], header); //get property of record in DB using header
                if(propertyObject == null) break;

                if(propertyObject is ICollection) //if object associated with header is e.g. a List
                {
                    //https://stackoverflow.com/questions/2837063/cast-object-to-generic-list
                    //List<J> propertyList = (propertyObject as IEnumerable<J>).Cast<J>().ToList();
    
                    MySetProperty(records[i], header, newValue);
                    Write(csvFile, records);
                    Console.WriteLine($"{header} was set to new list {newValue}");
                    return true;
                }
                //Console.WriteLine("propertyObject: ", propertyObject);
               
                // Type propertyType = propertyObject.GetType();
                // var property= Convert.ChangeType(propertyObject, propertyType);
                else
                {
                    MySetProperty(records[i], header, newValue);
                    Write(csvFile, records);
                    Console.WriteLine($"Property {header} of record changed succesfully");
                    return true;
                }
                
                
            }
        }
        Console.WriteLine($"in UpdateRecordWithValue(): No record found with property {header} matching value {newValue}");
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
                    Console.WriteLine($"{addValue} was added to list of Property {header} ");
                    return true;
                }
                else
                {
                    Console.WriteLine("Attempted to add a value to a non-list property!");
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
        else Console.WriteLine($"property {propertyToChange} remains unchanged");
    } 

    //must specify return type J in call because compiler can't figure out return type
    public static J MyGetProperty<J, T>(T record, string propertyToGet)
    {
        PropertyInfo? propertyInfo = typeof(T).GetProperty(propertyToGet);
        if(propertyInfo != null)
            {
            J property = (J)propertyInfo.GetValue(record); //even with no reservations. this should not be returning null, but it is!!1
            Console.WriteLine($"property {propertyToGet} was found! it is {property}");
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