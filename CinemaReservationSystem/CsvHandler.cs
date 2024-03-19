using CsvHelper;
public static class CsvHandler{
 
    public static bool Write(string csvFile, int id, string header)
    {
        StreamWriter writer = new(csvFile);
        
       
        CsvHelper.CsvWriter csvWriter = new(writer, CsvHelper.CultureInfo.InvariantCulture); 
        return true;
    }

    public static bool Read(string csvFile)
    {
        return true;
    }

   
}