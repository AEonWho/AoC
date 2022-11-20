public class Player
{
    public Dictionary<string, int> States { get; set; } = new Dictionary<string, int>();

    public int Mana { get; set; }

    public int Health { get; set; }

    public int ManaSpent { get; set; }

    public void ActivateSpell(int ManaCost)
    {
        ManaSpent += ManaCost;
        Mana -= ManaCost;
    }
}
