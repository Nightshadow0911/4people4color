using Newtonsoft.Json;

public class GameManager
{
    private static GameManager instance;
    private int Index = 0;
    string FolderName = "Data";


    public static GameManager Instance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }

    public void SetIndex(int index)
    {
        Index = index;
    }

    public List<CharacterData> GetFileNameList()
    {
        DirectoryInfo di = new DirectoryInfo(FolderName);

        if (!di.Exists)
            return null;

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
    private Character LoadData(int index)
    {
        string filePath = FolderName + "/" + index + ".dat";

        using (StreamReader reader = new StreamReader(filePath))
            return JsonConvert.DeserializeObject<Character>(reader.ReadToEnd());

    }
    public void SaveData(Character player)
    {
        if (!Directory.Exists(FolderName))
            Directory.CreateDirectory(FolderName);

        using (StreamWriter writer = new StreamWriter("Data/" + (Index == 0 ? GetFileNameList().Count + 1 : Index) + ".dat"))
            writer.Write(JsonConvert.SerializeObject(player, Formatting.Indented));


    }
}
