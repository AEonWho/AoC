internal partial class Program
{
    private static void Main(string[] args)
    {
        List<Game> games = new List<Game>
        {
            new Game()
            {
                Character = new Player
                {
                    Health = 50,
                    Mana = 500,
                },
                Enemy = new Boss
                {
                    Health = 58,
                    Damage = 9,
                }
            }
        };

        List<Game> wonGames = new List<Game>();
        while (true)
        {
            List<Game> tmp = new List<Game>();
            foreach (Game game in games)
            {

                //Uncomment for stage1
                game.Character.Health -= 1;

                game.RoundStart();

                try
                {
                    var spells = game.GetAvailableSpells().ToList();

                    foreach (var spell in spells)
                    {
                        if (spell != null)
                        {
                            var newGame = game.CreateCopy();
                            newGame.PlayerTurn(spell);
                            tmp.Add(newGame);
                        }
                        else
                        {
                            tmp.Add(game);
                        }
                    }
                }
                catch
                {
                    // Out of Options...
                }
            }

            games = tmp;

            games.ForEach(d => d.RoundStart());

            var winningGames = games.Where(d => d.Enemy.Health <= 0).ToList();
            wonGames.AddRange(winningGames);
            games.RemoveAll(winningGames.Contains);

            games.ForEach(d => d.EnemyTurn());
            games.RemoveAll(d => d.Character.Health <= 0);

            if (wonGames.Any())
            {
                break;
            }
        }

        Console.WriteLine(wonGames.Min(d => d.Character.ManaSpent));
    }



    public class Game
    {
        public List<string> Turns { get; set; } = new List<string>();

        public Player Character { get; set; }

        public Boss Enemy { get; set; }

        public Game CreateCopy()
        {
            return new Game()
            {
                Enemy = new Boss
                {
                    Damage = Enemy.Damage,
                    Health = Enemy.Health,
                },
                Character = new Player
                {
                    Health = Character.Health,
                    Mana = Character.Mana,
                    ManaSpent = Character.ManaSpent,
                    States = new Dictionary<string, int>(Character.States),
                },
                Turns = Turns.ToList(),
            };
        }

        internal void EnemyTurn()
        {
            if (Character.States.ContainsKey(nameof(Shield)) && Character.States[nameof(Shield)] > 0)
            {
                Character.Health -= Enemy.Damage - 7;
            }
            else
            {

                Character.Health -= Enemy.Damage;
            }
        }

        internal IEnumerable<Spell> GetAvailableSpells()
        {
            var spells = Spell.Spells.Where(d => d.CanActivate(Character) && !d.IsActive(Character));

            if (spells.Any())
            {
                foreach (var spell in spells)
                {
                    yield return spell;
                }
                yield break;
            }
            else if (!Character.States.Any(d => d.Value > 0))
            {
                throw new InvalidCastException();
            }

            yield return null;

        }

        internal void PlayerTurn(Spell spell)
        {
            Turns.Add(spell?.Name ?? "Wait");

            spell.Activate(Character, Enemy);
        }

        internal void RoundStart()
        {
            var activeSpells = Spell.Spells.Where(c => c.IsActive(Character)).ToList();
            foreach (var activeSpell in activeSpells)
            {
                activeSpell.Turn(Character, Enemy);
            }
        }
    }
}
