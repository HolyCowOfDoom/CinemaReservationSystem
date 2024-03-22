using Newtonsoft.Json;
using System.Linq;

public interface ObjectHasID
{
    int ID { get; }
}

public static class JsonHandler
{
    // Writes given object list to given json file.
    public static void Write<T>(List<T> dataToWrite, string jsonFile)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(jsonFile))
            {
                string stringToWrite = JsonConvert.SerializeObject(dataToWrite);
                writer.Write(stringToWrite);
            }
        }
        catch (JsonWriterException ex)
        {
            Console.WriteLine($"Error reading JSON: {ex.Message}");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"JSON file not found: {ex.Message}");
        }
    }

    // Reads the given JSON file and return the appropriate list, if JSON is empty return a new empty list.
    public static List<T> Read<T>(string jsonFile)
    {
        List<T>? listOfObjects = new List<T>();
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
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"JSON file not found: {ex.Message}");
        }
        return listOfObjects == null ? new List<T>() : listOfObjects;
    }

    // Removes object based on object.ID, works with all json related classes.
    public static void Remove<T>(int removalID, string jsonFile) where T: ObjectHasID
    {
        T? objectToRemove = Get<T>(removalID, jsonFile);
        
        if(objectToRemove != null)
        {
            List<T> listOfObjects = Read<T>(jsonFile);
            listOfObjects.Remove(objectToRemove);
            Write<T>(listOfObjects, jsonFile);
            return;
        }
    }

    // returns T object based on object.ID, returns null if object does not exist in JSON.
    public static T? Get<T>(int objectID, string jsonFile) where T : ObjectHasID
    {
        List<T> listOfObjects = Read<T>(jsonFile);
        foreach (T item in listOfObjects)
        {
            if (item.ID == objectID)
            {
                return item;
            }
        }
        return default;
    }

    // gets list from json, adds item, writes list back to json.
    public static void Append<T>(T objectToAppend, string jsonFile)
    {
        List<T> listOfObjects = Read<T>(jsonFile);
        listOfObjects.Add(objectToAppend);
        Write<T>(listOfObjects, jsonFile);
    }


    public static void Update<T>(T objectToUpdate, string jsonFile) where T : ObjectHasID
    {
        List<T> listOfObjects = Read<T>(jsonFile);

        T? objectInList = Get<T>(objectToUpdate.ID, jsonFile);
        if (objectInList != null)
        {
            listOfObjects[listOfObjects.IndexOf(objectInList)] = objectToUpdate;
            Write<T>(listOfObjects, jsonFile);
            return;
        }
        Append<T>(objectToUpdate, jsonFile);
    }
}