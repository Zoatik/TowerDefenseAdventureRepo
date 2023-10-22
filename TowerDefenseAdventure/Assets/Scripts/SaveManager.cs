using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Experimental.RestService;

public static class SaveManager 
{
    //Saver
    public static void Save(Player player)
    {
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new(path, FileMode.Create);

        PlayerInfos.PlayerData playerData = player.GetPlayerInfos().playerData;

        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static void Save(KeyBindings.KeyBinding[] keyBindings)
    {
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/keyBindings.save";
        FileStream stream = new(path, FileMode.Create);

        formatter.Serialize(stream, keyBindings);
        stream.Close();
    }

    //Loader
    public static PlayerInfos.PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);
            PlayerInfos.PlayerData playerData = formatter.Deserialize(stream) as PlayerInfos.PlayerData;

            stream.Close();

            return playerData;
        }
        else
            return null;
    }

    public static KeyBindings.KeyBinding[] LoadKeyBindings()
    {
        string path = Application.persistentDataPath + "/keyBindings.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);
            KeyBindings.KeyBinding[] keyBindings = formatter.Deserialize(stream) as KeyBindings.KeyBinding[];

            stream.Close();

            return keyBindings;
        }
        else
            return null;
    }
}
