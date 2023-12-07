using System.Data;

internal class Hand : IComparable<Hand>
{
    public string Input { get; }
    public Card[] Cards { get; }

    public long Bid { get; }

    public int Eval { get; set; }

    public Hand(string[] strings, bool joker)
    {
        Input = strings[0];

        Cards = strings[0].SelectMany(d => ToEnum(d, joker)).ToArray();

        Bid = long.Parse(strings[1]);

        Eval = Evaluate(joker);
    }

    private int Evaluate(bool joker)
    {
        var grp = Cards.GroupBy(d => d).OrderByDescending(d => d.Count()).ThenByDescending(d => d.First()).Select(c => c.ToList()).ToList();

        if (joker && grp.Count > 1)
        {
            var jokers = grp.FirstOrDefault(d => d[0] == Card.Joker);
            if (jokers != null)
            {
                grp.Remove(jokers);

                foreach (var j in jokers)
                {
                    grp[0].Add(grp[0][0]);
                }
            }
        }

        if (grp.Count == 1 && grp[0].Count == 5)
            return 7;

        if (grp.Count == 2 && grp[0].Count == 4)
            return 6;

        if (grp.Count == 2 && grp[0].Count == 3)
            return 5;

        if (grp[0].Count == 3)
            return 4;

        if (grp[0].Count == 2 && grp[1].Count == 2)
            return 3;

        if (grp[0].Count == 2)
            return 2;

        return 1;
    }

    public override string ToString()
    {
        return Input;
    }

    private IEnumerable<Card> ToEnum(char arg1, bool joker)
    {
        if (arg1 == 'A')
            yield return Card.Ace;
        if (arg1 == 'K')
            yield return Card.King;
        if (arg1 == 'Q')
            yield return Card.Queen;
        if (arg1 == 'J')
            yield return joker ? Card.Joker : Card.Jack;
        if (arg1 == 'T')
            yield return Card.Ten;
        if (arg1 == '9')
            yield return Card.Nine;
        if (arg1 == '8')
            yield return Card.Eight;
        if (arg1 == '7')
            yield return Card.Seven;
        if (arg1 == '6')
            yield return Card.Six;
        if (arg1 == '5')
            yield return Card.Five;
        if (arg1 == '4')
            yield return Card.Four;
        if (arg1 == '3')
            yield return Card.Three;
        if (arg1 == '2')
            yield return Card.Two;
    }

    public int CompareTo(Hand? other)
    {
        ArgumentNullException.ThrowIfNull(other);

        var eval = this.Eval.CompareTo(other.Eval);
        if (eval == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                eval = this.Cards[i].CompareTo(other.Cards[i]);
                if (eval != 0)
                    break;
            }
        }

        return eval;
    }
}