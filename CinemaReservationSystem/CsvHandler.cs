using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.VisualBasic;
//linq
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Collections.Concurrent;
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
        };
        using StreamReader reader = new(csvFile);
        using CsvHelper.CsvReader csvReader = new(reader, config);
        csvReader.Context.RegisterClassMap<UserClassMap>();
        return csvReader.GetRecords<T>().ToList();
    }

     public static bool Write<T>(string csvFile, List<T> objectsList)
    {
        using StreamWriter writer = new(csvFile);
        using CsvHelper.CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture); 
        csvWriter.Context.RegisterClassMap<UserClassMap>();
        csvWriter.WriteRecords(objectsList);
        return true;
    }
 
    public static bool Append(string csvFile, List<object> records){

        bool fileEmpty = false;
        if(new FileInfo(csvFile).Length == 0) fileEmpty = true; //checks is file is empty
        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            // Don't write the header again.
            HasHeaderRecord = fileEmpty ? true : false, //write header if file is empty
        };
        
        //Console.WriteLine(csvFile);
        StreamWriter writer = new(csvFile, true);
        CsvHelper.CsvWriter csvWriter = new(writer, config); 
        csvWriter.Context.RegisterClassMap<UserClassMap>();
        csvWriter.WriteRecords(records);
        writer.Close();

        StreamReader reader = new(csvFile);
        //Console.WriteLine(reader.ReadToEnd());
        reader.Close();
        return true;
    }

    //example (UserDBFilePath, "ID", 1) -> returns User obj with ID 1
    //example (UserDBFilePath, "Name", "Utku") -> returns User obj with Name Utku
    public static T? GetRecordWithValue<T>(string csvFile, string header, object value) 
    {
         var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLowerInvariant(),
            //https://stackoverflow.com/questions/49521193/csvhelper-ignore-case-for-header-names
        };
        using StreamReader reader = new(csvFile);
        using CsvHelper.CsvReader csvReader = new(reader, config);
        csvReader.Context.RegisterClassMap<UserClassMap>();
        
        csvReader.Read();
        csvReader.ReadHeader();
        while (csvReader.Read())
        {
            object field = csvReader.GetField(value.GetType(), header); 
            if(value.Equals(field))
            {
                T records = csvReader.GetRecord<T>();
                return records;
            }
        }
        return default;
    }

    public static bool UpdateRecordOfID<T>(string csvFile, string id, T newRecord)  //the newRecord MUST be instantiated using old ID in constructor!
    //OR use the copy constructor and change the field you want to change

    {
        List<T> records = Read<T>(csvFile);
        for(int i = 0; i < records.Count; i++)
        {
            object property = MyGetProperty<string, T>(records[i], "ID");
            if(property != null){
                string record_ID = (string)property;
                if(record_ID == id){
                    records[i] = newRecord;
                    if (Write(csvFile, records)){
                        Console.WriteLine("record changed succesfully");
                        return true;
                    }
                    else Console.WriteLine("writing to file failed, somewhow");
                }
            }
            else
            {
                Console.WriteLine("No property ID was found in record"); 
                //break ?
            }
        }
        return false;
    }


    public static bool UpdateRecordWithValue<T>(string csvFile, T record, string header, object value)// where T : IEquatable<T>
    {
        // var method = typeof(CsvHandler).GetMethod("UpdateRecordWithValueExtension");
        // var CsvHandlerRef = method.MakeGenericMethod(typeof(T), typeof(object));
        // return (bool)CsvHandlerRef.Invoke(null, new object[] {csvFile, record, header, value});

        // //change field of newRecord. using reflection because it's type T
        // var constructor = typeof(T).GetConstructor(new[] {typeof(T)}); //gets copy constructor of record
        // T newRecord = (T)constructor.Invoke(new object[] {record}); //creates T object using said constructor
        // MethodInfo method = typeof(CsvHandler).GetMethod("MySetProperty"); //gets SetField method
        // var genericmethod = method.MakeGenericMethod(typeof(T), typeof(J)); //makes it a generic method(glues <T, J> to it)
        // genericmethod.Invoke(null, new object[] {newRecord, header, value}); //calls method with parameters
        // //the SetField method sets newRecord.header = value

        List<T> records = Read<T>(csvFile);
        for(int i = 0; i < records.Count; i++)
        {
            if(records[i].Equals(record))
            {
                object propertyObject = MyGetProperty<object, T>(records[i], header);
                if(propertyObject == null) break;
                //if(propertyObject.GetType() == typeof(List<>))
                if(propertyObject is IEnumerable) //if object associated with header is e.g. a List
                {
                    //Type myListElementType = propertyObject.GetType().GetGenericArguments().Single();
                    //https://stackoverflow.com/questions/4452590/c-sharp-get-the-item-type-for-a-generic-list
                    ((List<object>)propertyObject).Add(value); //add to List
                }
                else
                {
                    // Type propertyType = propertyObject.GetType();
                    // var property= Convert.ChangeType(propertyObject, propertyType);

                    MySetProperty(records[i], header, value);
                    Console.WriteLine($"Property {header} of record changed succesfully");
                    return true;
                }
            }
            
        }
        Console.WriteLine("No record found with property matching value");
        return false;
    }


    public static void MySetProperty<T, J>(T obj, string propertyToChange, J value)
    {
        PropertyInfo? property = typeof(T).GetProperty(propertyToChange);
        if(property != null) property.SetValue(obj, value);
        else Console.WriteLine($"property {propertyToChange} remains unchanged");
    } 

    public static J MyGetProperty<J, T>(T record, string propertyToGet)
    {
        PropertyInfo? propertyInfo = typeof(T).GetProperty(propertyToGet);
        if(propertyInfo != null)
            {
            J property = (J)propertyInfo.GetValue(record);
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

    public static int CountRecords(string csvFile)
    {
        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLowerInvariant(),
            //https://stackoverflow.com/questions/49521193/csvhelper-ignore-case-for-header-names
        };
        using StreamReader reader = new(csvFile);
        using CsvHelper.CsvReader csvReader = new(reader, config);
        try{
            csvReader.Read();
            csvReader.ReadHeader();
        }
        catch (CsvHelper.ReaderException e)
        {
            //if there is no header
            return 0;
        }
        

        int count = 0;
        while (csvReader.Read())
        {
            count++;
        }
        return count;
    }
}