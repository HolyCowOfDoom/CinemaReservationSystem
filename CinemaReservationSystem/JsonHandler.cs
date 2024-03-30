using Newtonsoft.Json;
using System.Linq;
using System.Text.Json;


// Necessary interface for the ID system to fuction.
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
                JsonSerializerSettings settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                string stringToWrite = JsonConvert.SerializeObject(dataToWrite, settings);
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
        int objectIndex = GetIndex<T>(removalID, jsonFile);
        
        if(objectIndex != -1)
        {
            List<T> listOfObjects = Read<T>(jsonFile);
            listOfObjects.Remove(listOfObjects[objectIndex]);
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

    // Gets index of item in read list, returns -1 if item was not found.
    public static int GetIndex<T>(int objectID, string jsonFile) where T : ObjectHasID
    {
        List<T> listOfObjects = Read<T>(jsonFile);
        foreach (T item in listOfObjects)
        {
            if (item.ID == objectID)
            {
                return listOfObjects.IndexOf(item);
            }
        }
        return -1;
    }

    // gets list from json, adds item, writes list back to json.
    public static void Append<T>(T objectToAppend, string jsonFile)
    {
        List<T> listOfObjects = Read<T>(jsonFile);
        listOfObjects.Add(objectToAppend);
        Write<T>(listOfObjects, jsonFile);
    }

    // updates the json with given object, gets object id; gets list; writes list. If object id is not found, appends object.
    public static void Update<T>(T objectToUpdate, string jsonFile) where T : ObjectHasID
    {
        int objectIndex = GetIndex<T>(objectToUpdate.ID, jsonFile);
        if (objectIndex != -1)
        {
            List<T> listOfObjects = Read<T>(jsonFile);
            listOfObjects[objectIndex] = objectToUpdate;
            Write<T>(listOfObjects, jsonFile);
            return;
        }
        Append<T>(objectToUpdate, jsonFile);
    }
}