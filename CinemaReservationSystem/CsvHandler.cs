using System.Globalization;
using CsvHelper;
using Microsoft.VisualBasic;
//linq
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Collections.Concurrent;
using System.Reflection;

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
        return csvReader.GetRecords<T>().ToList();
    }

     public static bool Write<T>(string csvFile, List<T> objectsList)
    {
        using StreamWriter writer = new(csvFile);
        using CsvHelper.CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture); 
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

    public static bool UpdateRecordOfID<T>(string csvFile, int id, T newRecord)
    //the newRecord MUST be instantiated using old ID in constructor!
    //OR use the copy constructor and change the field you want to change like new User(oldUser){Name = newName}
    {
        List<T> records = Read<T>(csvFile);
        for(int i = 0; i < records.Count; i++)
        {
            object property = MyGetProperty(records[i], "ID");
            if(property != null){
                int record_ID = (int)property;
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


    public static bool UpdateRecordWithValue<T>(string csvFile, T record, string header, object value)
    {
        // var method = typeof(CsvHandler).GetMethod("UpdateRecordWithValueExtension");
        // var CsvHandlerRef = method.MakeGenericMethod(typeof(T), typeof(object));
        // return (bool)CsvHandlerRef.Invoke(null, new object[] {csvFile, record, header, value});

        List<T> records = Read<T>(csvFile);
        for(int i = 0; i < records.Count; i++)
        {
            object propertyObject = MyGetProperty(records[i], header);
            if(propertyObject != null){
                
                // Type propertyType = propertyObject.GetType();
                // var property= Convert.ChangeType(propertyObject, propertyType);
                if(propertyObject == value){
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

    public static object MyGetProperty<T>(T record, string propertyToGet)
    {
        PropertyInfo? propertyInfo = typeof(T).GetProperty(propertyToGet);
        if(propertyInfo != null)
            {
            var property = propertyInfo.GetValue(record);
            if(property != null) return property;
            else
            {
                Console.WriteLine($"property {propertyToGet} was not found or is null (2)");
                return null;
            }
        }
        else 
        {
            Console.WriteLine($"property {propertyToGet} was not found or is null (1)");
            return null;
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