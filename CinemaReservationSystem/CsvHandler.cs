using System.Globalization;
using CsvHelper;
using Microsoft.VisualBasic;
//linq
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

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

    //example (UserDBFilePath, "ID", 1)
    //example (UserDBFilePath, "Name", "Utku")
    public static bool FindRecordWithHeaderWithValue(string csvFile, string header, VariantType value)
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
        
        csvReader.ReadHeader();
        while (csvReader.Read())
        {
            var field = csvReader.GetField<VariantType>(header);
            //var line = csvReader.GetRecord<VariantType>();
        }

        return true;
    }

    // public static bool FindRecordWithHeaderWithValue(string csvFile, string header, VariantType value)
    // {
    //     StreamReader reader = new(csvFile);
    //     string headersString = reader.ReadLine();
    //     string[] headersArray = headersString.Split(',');
    //     List<string> headers = headersArray.ToList();
    //     reader.Close();
    //     int headerIndex = headers.FindIndex(item => item == header);
        
    //     from l in File.ReadLines(csvFile)
    //     let x = l.Split(',')
    //     select new
    //     {
    //         a = x.ToList()[headerIndex]
            
    //     }
    // }
    public static bool Write(string csvFile, int id, string header)
    {
        using StreamWriter writer = new(csvFile);
        using CsvHelper.CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture); 
        csvWriter.WriteRecord
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

   
}