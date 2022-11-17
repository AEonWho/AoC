var items = new List<Item>()
{
    new Item{ Type = ItemType.Weapon, Name = "Dagger", Cost = 8, Damage = 4},
    new Item{ Type = ItemType.Weapon, Name = "Shortsword", Cost = 10, Damage = 5},
    new Item{ Type = ItemType.Weapon, Name = "Warhammer", Cost = 25, Damage = 6},
    new Item{ Type = ItemType.Weapon, Name = "Longsword", Cost = 40, Damage = 7},
    new Item{ Type = ItemType.Weapon, Name = "Greataxe", Cost = 74, Damage = 8},

    new Item { Type = ItemType.Armor, Name="NoArmor" },
    new Item { Type = ItemType.Armor, Name="Leather", Cost = 13, Armor = 1 },
    new Item { Type = ItemType.Armor, Name="Chainmail", Cost = 31, Armor = 2 },
    new Item { Type = ItemType.Armor, Name="Splintmail", Cost = 53, Armor = 3 },
    new Item { Type = ItemType.Armor, Name="Bandedmail", Cost = 75, Armor = 4 },
    new Item { Type = ItemType.Armor, Name="Platemail", Cost = 102, Armor = 5 },

    new Item { Type = ItemType.Ring, Name="NoRing" },
    new Item { Type = ItemType.Ring, Name="NoRing" },
    new Item { Type = ItemType.Ring, Name="Damage1", Cost = 25, Damage = 1 },
    new Item { Type = ItemType.Ring, Name="Damage2", Cost = 50, Damage = 2 },
    new Item { Type = ItemType.Ring, Name="Damage3", Cost = 100, Damage = 3 },
    new Item { Type = ItemType.Ring, Name="Armor1", Cost = 20, Armor = 1 },
    new Item { Type = ItemType.Ring, Name="Armor2", Cost = 40, Armor = 2 },
    new Item { Type = ItemType.Ring, Name="Armor3", Cost = 80, Armor = 3 },
};

var weapons = items.Where(c => c.Type == ItemType.Weapon).ToList();
var armors = items.Where(c => c.Type == ItemType.Armor).ToList();
var rings = items.Where(c => c.Type == ItemType.Ring).ToList();

List<(Item, Item, Item, Item)> tmp = new List<(Item, Item, Item, Item)>();
foreach (var weapon in weapons)
{
    foreach (var armor in armors)
    {
        foreach (var ring1 in rings)
        {
            foreach (var ring2 in rings)
            {
                if (ring1 == ring2)
                    continue;

                tmp.Add((weapon, armor, ring1, ring2));
            }
        }
    }
}

foreach (var entry in tmp.OrderBy(GetCoins))
{
    int playerHP = PlayGame(entry);

    if (playerHP > 0)
    {
        Console.WriteLine($"CheapestWin: {GetCoins(entry)}");
        break;
    }
}

foreach (var entry in tmp.OrderByDescending(GetCoins))
{
    int playerHP = PlayGame(entry);

    if (playerHP <= 0)
    {
        Console.WriteLine($"ExpensiveLoss: {GetCoins(entry)}");
        break;
    }
}

int PlayGame((Item, Item, Item, Item) entry)
{
    var playerHP = 100;
    var playerDamage = entry.Item1.Damage + entry.Item2.Damage + entry.Item3.Damage + entry.Item4.Damage;
    var playerArmor = entry.Item1.Armor + entry.Item2.Armor + entry.Item3.Armor + entry.Item4.Armor;

    var bossHP = 109;
    var bossDamage = 8;
    var bossArmor = 2;

    int i = 0;
    while (true)
    {
        if (i % 2 == 0)
        {
            var damage = playerDamage - bossArmor;
            if (damage < 1)
                damage = 1;

            bossHP -= damage;
            if (bossHP <= 0)
                break;
        }
        else
        {
            var damage = bossDamage - playerArmor;
            if (damage < 1)
                damage = 1;

            playerHP -= damage;
            if (playerHP <= 0)
                break;
        }

        i++;
    }

    return playerHP;
}

static int GetCoins((Item, Item, Item, Item) entry)
{
    return entry.Item1.Cost + entry.Item2.Cost + entry.Item3.Cost + entry.Item4.Cost;
}

enum ItemType
{
    Weapon,
    Armor,
    Ring,
}

class Item
{
    public ItemType Type { get; set; }

    public string Name { get; set; }

    public int Cost { get; set; }

    public int Damage { get; set; }

    public int Armor { get; set; }
}