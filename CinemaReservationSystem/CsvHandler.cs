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
    public static T? FindRecordWithHeaderWithValue<T>(string csvFile, string header, object value)
    {
        //Type J = value.GetType();
        //J.MakeGenericType();
        var method = typeof(CsvHandler).GetMethod("FindRecordWithHeaderWithValue2");
        var CsvHandlerRef = method.MakeGenericMethod(typeof(T), value.GetType());
        return (T)CsvHandlerRef.Invoke(null, new object[] {csvFile, header, value});
        //https://stackoverflow.com/questions/3957817/calling-generic-method-with-type-variable
        
        //this doesn't work!
        //FindRecordWithHeaderWithValue2<T, value.GetType()>(csvFile, header, value);
    }
    public static T? FindRecordWithHeaderWithValue2<T, J>(string csvFile, string header, J value)
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

    public static bool Write(string csvFile, int id, string header)
    {
        using StreamWriter writer = new(csvFile);
        using CsvHelper.CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture); 
        //csvWriter.WriteRecord
        // csvWriter.WriteField()
        //writer.Close();
        return true;
    }

    public static bool Read<T>(string csvFile, out List<T> objectsList)
    {
        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLowerInvariant(),
            //https://stackoverflow.com/questions/49521193/csvhelper-ignore-case-for-header-names
        };
        using StreamReader reader = new(csvFile);
        using CsvHelper.CsvReader csvReader = new(reader, config);
        objectsList = csvReader.GetRecords<T>().ToList();

        reader.Close();
        return true;
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
        csvReader.Read();
        csvReader.ReadHeader();

        int count = 0;
        while (csvReader.Read())
        {
            count++;
        }
        return count;
    }

   
}