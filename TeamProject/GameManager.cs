using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class GameManager
{
    private static GameManager instance;
    string FolderName = "Data";

    public static GameManager Instance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }

    public List<CharacterData> GetFileNameList()
    {
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);

        List<CharacterData> list = new List<CharacterData>();
        foreach (System.IO.FileInfo File in di.GetFiles())
        {
            if (File.Extension.ToLower().CompareTo(".dat") == 0)
            {
                string fileName = File.Name.Split(".")[0];
                int index = Int32.Parse(fileName);
                Character player = LoadData(index);

                list.Add(new CharacterData(index, player));
            }
        }
        return list;
    }
    public Character LoadData(int index)
    {
        string filePath = FolderName + "/" + index + ".dat";

        using (StreamReader reader = new StreamReader(filePath))
            return JsonConvert.DeserializeObject<Character>(reader.ReadToEnd());

    }
    public void SaveData(int index, Character player)
    {
        using (StreamWriter writer = new StreamWriter("Data/" + index + "_" + player.Name + ".dat"))
            writer.Write(JsonConvert.SerializeObject(player, Formatting.Indented));
    }
}
