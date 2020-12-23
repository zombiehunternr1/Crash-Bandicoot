using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static Save;

public static class SaveSystem
{ 
    public static void SaveProgress(PlayerInfo Player, Level Level)
    {
        BinaryFormatter Formatter = new BinaryFormatter();
        string Path = Application.persistentDataPath + "/Save.cbr";
        FileStream Stream = new FileStream(Path, FileMode.Create);

        Save Progress = new Save(Player);
        Formatter.Serialize(Stream, Progress);
        Stream.Close();
    }

    public static Save LoadProgress()
    {
        string Path = Application.persistentDataPath + "/Save.cbr";
        if (File.Exists(Path))
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            FileStream Stream = new FileStream(Path, FileMode.Open);

            Save Progress = Formatter.Deserialize(Stream) as Save;
            Stream.Close();
            return Progress;
        }
        else
        {
            Debug.LogError("File not found in " + Path);
            return null;
        }
    }
}
