using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{ 
    public static void SavePlayer(PlayerInfo Player)
    {
        BinaryFormatter Formatter = new BinaryFormatter();
        string Path = Application.persistentDataPath + "/Save.cbr";
        FileStream Stream = new FileStream(Path, FileMode.Create);

        Save Save = new Save(Player);
        Formatter.Serialize(Stream, Save);
        Stream.Close();
    }

    public static Save LoadPlayer()
    {
        string Path = Application.persistentDataPath + "/Save.cbr";
        if (File.Exists(Path))
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            FileStream Stream = new FileStream(Path, FileMode.Open);

            Save Player = Formatter.Deserialize(Stream) as Save;
            Stream.Close();
            return Player;
        }
        else
        {
            Debug.LogError("File not found in " + Path);
            return null;
        }
    }
}
