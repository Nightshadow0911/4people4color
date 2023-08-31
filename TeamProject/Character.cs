public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }
    public float Crit { get; set; }
    public float Evade { get; set; }
    public int Gold { get; }
    public int Exp { get; set; }

    public Character(string name, string job, int level, int atk, int def, int hp, float crit, float evade, int gold, int exp)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Crit = crit;
        Evade = evade;
        Gold = gold;
        Exp = exp;
    }
}