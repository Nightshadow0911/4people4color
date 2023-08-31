using System.Text;

public class CharacterData
{
    int Index { get; }
    Character Player { get; }

    public CharacterData(int index, Character player)
    {
        Index = index;
        Player = player;
    }

    public string GetFileName()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(Index);
        sb.Append("_");
        sb.Append(Player.Name);
        sb.Append(".dat");

        return sb.ToString();
    }

    public int GetIndex()
    {
        return Index;
    }
    public Character GetCharacter()
    {
        return Player;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(Index.ToString());
        sb.Append(" | ");
        sb.Append(Player.Name.PadLeft(10, ' '));
        sb.Append(" | ");
        sb.Append("LV.");
        sb.Append(Player.Level.ToString());

        return sb.ToString();
    }
}
