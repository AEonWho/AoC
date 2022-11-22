int[] playerPos = new int[2] { 7, 9 };
int[] playerValue = new int[2] { 0, 0 };

int turn = 0;
while (playerValue.All(d => d < 1000))
{
    var playerIdx = turn % 2;

    for (int rollCount = 1; rollCount <= 3; rollCount++)
    {
        var rollValue = (turn * 3 % 100) + rollCount;
        playerPos[playerIdx] = (playerPos[playerIdx] + rollValue) % 10;
    }

    playerValue[playerIdx] += playerPos[playerIdx] + 1;
    turn++;
}

Console.WriteLine($"Stage1: {turn * 3 * playerValue.Min()}");

Dictionary<int, int> rollDistribution = new Dictionary<int, int>();
for (int firstRoll = 1; firstRoll <= 3; firstRoll++)
{
    for (int secondRoll = 1; secondRoll <= 3; secondRoll++)
    {
        for (int thirdRoll = 1; thirdRoll <= 3; thirdRoll++)
        {
            var roll = firstRoll + secondRoll + thirdRoll;
            if (!rollDistribution.ContainsKey(roll))
                rollDistribution[roll] = 0;
            rollDistribution[roll]++;
        }
    }
}

long P1Won = 0;
long P2Won = 0;
var universes = new Dictionary<(int p1, int p2, int v1, int v2, int idx), long>();
universes.Add((7, 9, 0, 0, 0), 1);

while (universes.Any())
{
    var newUniverses = new Dictionary<(int p1, int p2, int v1, int v2, int idx), long>();

    foreach (var universe in universes)
    {
        foreach (var valuegroup in rollDistribution)
        {
            var p1 = universe.Key.p1;
            var p2 = universe.Key.p2;
            var v1 = universe.Key.v1;
            var v2 = universe.Key.v2;
            var idx = universe.Key.idx;

            if (idx == 0)
            {
                idx = 1;
                p1 = (p1 + valuegroup.Key) % 10;
                v1 += p1 + 1;
            }
            else
            {
                idx = 0;
                p2 = (p2 + valuegroup.Key) % 10;
                v2 += p2 + 1;
            }

            if (v1 >= 21)
            {
                P1Won += universe.Value * valuegroup.Value;
                continue;
            }
            else if (v2 >= 21)
            {
                P2Won += universe.Value * valuegroup.Value;
                continue;
            }

            var universeKey = (p1, p2, v1, v2, idx);
            if (!newUniverses.ContainsKey(universeKey))
            {
                newUniverses[universeKey] = 0;
            }
            newUniverses[universeKey] += universe.Value * valuegroup.Value;
        }
    }

    universes = newUniverses;
}

Console.WriteLine();