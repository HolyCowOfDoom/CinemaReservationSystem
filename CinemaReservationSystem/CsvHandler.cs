using System.Globalization;
using CsvHelper;
using Microsoft.VisualBasic;
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
    public static bool Write(string csvFile, int id, string header)
    {
        using StreamWriter writer = new(csvFile);
        using CsvHelper.CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture); 
        // csvWriter.WriteRecords()
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