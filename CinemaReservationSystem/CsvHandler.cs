using System.Globalization;
using CsvHelper;
using Microsoft.VisualBasic;
//linq
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Collections.Concurrent;

public static class CsvHandler{

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
        var method = typeof(CsvHandler).GetMethod("GetRecordWithValueExtension");
        var CsvHandlerRef = method.MakeGenericMethod(typeof(T), value.GetType());
        return (T)CsvHandlerRef.Invoke(null, new object[] {csvFile, header, value});
        //https://stackoverflow.com/questions/3957817/calling-generic-method-with-type-variable
        
        //this doesn't work!
        //FindRecordWithHeaderWithValue2<T, value.GetType()>(csvFile, header, value);
    }
    public static T? GetRecordWithValueExtension<T, J>(string csvFile, string header, J value) // invoke above wn't work if private?
    {
        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLowerInvariant(),
            
            //https://stackoverflow.com/questions/49521193/csvhelper-ignore-case-for-header-names
        };
        using StreamReader reader = new(csvFile);
        using CsvHelper.CsvReader csvReader = new(reader, config);

        // string headersString = reader.ReadLine();
        // List<string> headers = headersString.Split(',').ToList();
        // int headerIndex = headers.FindIndex(item => item == header);
        
        csvReader.Read();
        csvReader.ReadHeader();

        while (csvReader.Read())
        {
            J field = csvReader.GetField<J>(header);
            if(value.Equals(field))
            {
                T records = csvReader.GetRecord<T>();
                return records;
            }
            //var line = csvReader.GetRecord<VariantType>();
        }

        return default;
    }

    public static bool UpdateRecordOfID<T>(string csvFile, string id, T newRecord) where T : ObjectHasID //the newRecord MUST be instantiated using old ID in constructor!
                                                                                //OR use the copy constructor and change the field you want to change
    {
        //this is not enough, as I can't just write to that location.
        //T record = GetRecordWithValue<T>(csvFile, "ID", id); //get record with matching ID

        // var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        // {
        //     PrepareHeaderForMatch = args => args.Header.ToLowerInvariant(),
        //     //https://stackoverflow.com/questions/49521193/csvhelper-ignore-case-for-header-names
        // };
        // using StreamReader reader = new(csvFile);
        // using CsvHelper.CsvReader csvReader = new(reader, config);

        // //read to get index of record in db, re-uses some code of GetRecordWithValue so consider if it's possible to abstract.
        // int indexOfRecord = 0; //start at 1 to include Header line
        // csvReader.Read();
        // csvReader.ReadHeader(); 
        // //T recordinDB;
        // while (csvReader.Read()) 
        // {
        //     indexOfRecord++;
        //     var field = csvReader.GetField("ID");
        //     //recordinDB = csvReader.GetRecord<T>(); //stores the record in DB with the matcing ID
        //     if(field.Equals(id))
        //     {
        //         break;
        //     }
        // }
        // reader.Close();
        
        // var options = new FileStreamOptions();
        // options.Access = FileAccess.Write;
        // options.Mode = FileMode.Open;
        // using StreamWriter writer = new(csvFile, options);
        // writer.BaseStream.Position = 0;
        // using CsvHelper.CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture);
        // while(indexOfRecord > 1)
        // {
        //     csvWriter.NextRecord(); //move writer 'cursor' to the correct line using index
        //     indexOfRecord--;
        // }
        // csvWriter.WriteRecord(newRecord);
        // csvWriter.Flush();

        List<T> recordsList = Read<T>(csvFile);
        for(int i = 0; i < recordsList.Count; i++)
        {
            if(recordsList[i].ID == id)
            {
                recordsList[i] = newRecord;
            }
        }

        Write(csvFile, recordsList);













        return true;
    }

    // private static bool WriteValueToRecordExtension<T, J>(string csvFile, T newRecord)
    // {
    //     var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
    //     {
    //         PrepareHeaderForMatch = args => args.Header.ToLowerInvariant(),
    //         //https://stackoverflow.com/questions/49521193/csvhelper-ignore-case-for-header-names
    //     };
    //     using StreamReader reader = new(csvFile);
    //     using CsvHelper.CsvReader csvReader = new(reader, config);

    //     //read to get index of record in db, re-uses some code of GetRecordWithValue so consider if it's possible to abstract.
    //     int indexOfRecord = 1; //start at 1 to include Header line
    //     csvReader.Read();
    //     csvReader.ReadHeader(); 
    //     T recordinDB;
    //     while (csvReader.Read()) 
    //     {
    //         indexOfRecord++;
    //         J field = csvReader.GetField<J>(header);
    //         recordinDB = csvReader.GetRecord<T>();
    //         if(recordinDB.Equals(record))
    //         {
    //             break;
    //         }
    //         //var line = csvReader.GetRecord<VariantType>();
    //     }
        
    //     using StreamWriter writer = new(csvFile);
    //     using CsvHelper.CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture);
    //     while(indexOfRecord > 1)
    //     {
    //         csvWriter.NextRecord(); //move writer 'cursor' to the correct line using index
    //         indexOfRecord--;
    //     }
    //     record.
    //     csvWriter.
        

    //     return true;
    // }

   

    

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