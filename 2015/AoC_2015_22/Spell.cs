public abstract class Spell
{
    public static List<Spell> Spells { get; } = new List<Spell>
    { new Missle(),
      new Drain(),
      new Shield(),
      new Poison(),
      new Recharge()
    };
    public abstract string Name { get; }

    public abstract bool CanActivate(Player player);

    public abstract bool IsActive(Player player);

    public abstract void Activate(Player player, Boss enemy);

    public abstract void Turn(Player player, Boss enemy);
}


public class Missle : Spell
{
    public override string Name { get; } = nameof(Missle);

    public override bool IsActive(Player player) => false;

    public override void Activate(Player player, Boss enemy)
    {
        player.ActivateSpell(53);

        enemy.Health -= 4;
    }

    public override bool CanActivate(Player player)
    {
        return player.Mana >= 53;
    }

    public override void Turn(Player player, Boss enemy)
    {
        throw new NotImplementedException();
    }
}

public class Drain : Spell
{
    public override string Name { get; } = nameof(Drain);

    public override bool IsActive(Player player) => false;

    public override void Activate(Player player, Boss enemy)
    {
        player.ActivateSpell(73);
        player.Health += 2;
        enemy.Health -= 2;
    }

    public override bool CanActivate(Player player)
    {
        return player.Mana >= 73;
    }

    public override void Turn(Player player, Boss enemy)
    {
        throw new NotImplementedException();
    }
}

public class Shield : Spell
{
    public override string Name { get; } = nameof(Shield);

    public override bool IsActive(Player player) => player.States.ContainsKey(nameof(Shield)) && player.States[nameof(Shield)] > 0;

    public override void Activate(Player player, Boss enemy)
    {
        player.ActivateSpell(113);
        player.States[nameof(Shield)] = 6;
    }

    public override bool CanActivate(Player player)
    {
        return player.Mana >= 113;
    }

    public override void Turn(Player player, Boss enemy)
    {
        player.States[nameof(Shield)]--;
    }
}

public class Poison : Spell
{
    public override string Name { get; } = nameof(Poison);

    public override bool IsActive(Player player) => player.States.ContainsKey(nameof(Poison)) && player.States[nameof(Poison)] > 0;

    public override void Activate(Player player, Boss enemy)
    {
        player.ActivateSpell(173);
        player.States[nameof(Poison)] = 6;
    }

    public override bool CanActivate(Player player)
    {
        return player.Mana >= 173;
    }

    public override void Turn(Player player, Boss enemy)
    {
        player.States[nameof(Poison)]--;
        enemy.Health -= 3;
    }
}

public class Recharge : Spell
{
    public override string Name { get; } = nameof(Recharge);

    public override bool IsActive(Player player) => player.States.ContainsKey(nameof(Recharge)) && player.States[nameof(Recharge)] > 0;

    public override void Activate(Player player, Boss enemy)
    {
        player.ActivateSpell(229);
        player.States[nameof(Recharge)] = 5;
    }

    public override bool CanActivate(Player player)
    {
        return player.Mana >= 229;
    }

    public override void Turn(Player player, Boss enemy)
    {
        player.States[nameof(Recharge)]--;
        player.Mana += 101;
    }
}

