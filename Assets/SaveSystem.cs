using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string path = Application.persistentDataPath + "/sop.save";

    public static void Save(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        //Debug.Log("Guarda" + data.playerHealth);
        stream.Close();
    }
    
    public static GameData Load()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            //Debug.Log("Carga" + data.playerHealth);
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
        }
        return null;
    }
}
