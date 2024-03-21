using Newtonsoft.Json;
using System.Linq;

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
        if (listOfObjects == null) listOfObjects = new List<T>();
        listOfObjects.Add(objectToAppend);
        bool writeSucces = Write<T>(listOfObjects);

        return writeSucces;
    }

    public static bool Update<T>(T objectToUpdate, string jsonFile)
    {
        List<T>? listOfObjects = Read<T>(jsonFile);
        bool updated = false;

        T objectInList = Get<T>(objectToUpdate.ID, jsonFile);
        if (objectInList != null)
        {
            listOfObjects[listOfObjects.IndexOf(objectInList)] = objectToUpdate;
            updated = Write<T>(listOfObjects, jsonFile);
            return updated;
        }
       
        Append<T>(objectToUpdate, jsonFile);
        updated = true;
        
        return updated;
    }

    public static List<T>? Read<T>(string jsonFile)
    {
        List<T> listOfObjects = new List<T>();
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

    public static bool Remove<T>(T objectToRemove, string jsonFile)
    {
        List<T>? listOfObjects = Read<T>(jsonFile);
        if (listOfObjects != null && listOfObjects.Contains(objectToRemove))
        {
            listOfObjects.Remove(objectToRemove);
            bool writeSucces = Write<T>(listOfObjects, jsonFile);
            return writeSucces;
        }
        return false;
    }

    public static T Get<T>(int objectID, string jsonFile)
    {
        List<T> listOfObjects = Read<T>(jsonFile);
        foreach (T object in listOfObjects)
        {
            if (object.ID == objectID)
            {
                return object;
            }
        }
        return null;
    }
}