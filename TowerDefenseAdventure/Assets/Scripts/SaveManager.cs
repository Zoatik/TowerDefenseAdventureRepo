using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Experimental.RestService;

public static class SaveManager 
{
    //Saver
    public static void Save(Player player)
    {
        //general player infos
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/player.save";
        PlayerInfos.PlayerData playerData = player.GetPlayerInfos().playerData;
        FileStream stream = new(path, FileMode.Create);
        Debug.Log(path);

        formatter.Serialize(stream, playerData);
        stream.Close();

        //player spells
        path = Application.persistentDataPath + "/playerSpells.save";
        formatter = new();
        stream = new(path, FileMode.Create);
        formatter.Serialize(stream,player.GetPlayerInfos().possessedSpells.playerSpellData.ToArray());
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

    public static PlayerSpells.PlayerSpellData[] LoadSpells()
    {
        string path = Application.persistentDataPath + "/playerSpells.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);
            PlayerSpells.PlayerSpellData[] playerSpells = formatter.Deserialize(stream) as PlayerSpells.PlayerSpellData[];

            stream.Close();

            return playerSpells;
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
