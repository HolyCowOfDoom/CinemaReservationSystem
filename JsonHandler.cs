using Newtonsoft.Json;

public static class JsonHandler
{
    public static bool Write<T>(List<T> dataToWrite, string jsonFile)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(jsonFile))
            {
                string dataToWrite = JsonConvert.SerializeObject<List<T>>(dataToWrite);
                writer.Write(dataToWrite);
            }
        }
        catch (JsonWriterException ex)
        {
            Console.WriteLine($"Error reading JSON: {ex.Message}");
            return false;
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"JSON file not found: {ex.Message}");
            return false;
        }

        return true;
    }

    public static bool Append<T>(T objectToAppend, string jsonFile)
    {
        List<T> listOfObjects = Read<T>(jsonFile);
        listOfObjects.Add(objectToAppend);
        Write<T>(listOfObjects);
        return true;
    }

    public static List<T>? Read<T>(string jsonFile)
    {
        List<T> listOfObject = new List<T>();
        try
        {
            using (StreamReader reader = new StreamReader(jsonFile))
            {
                string fileData = reader.ReadToEnd();
                listOfObjects = JsonConvert.DeserializeObject<List<T>>(fileData);
            }
        }
        catch (JsonReaderException ex)
        {
            Console.WriteLine($"Error reading JSON: {ex.Message}");
            return null;
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"JSON file not found: {ex.Message}");
            return null;
        }

        return listOfObjects;
    }


}