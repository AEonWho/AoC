Dictionary<(int x, int y), char> initGameMap = new Dictionary<(int x, int y), char>();

var parkPositions = new (int x, int y)[] { (1, 1), (2, 1), (4, 1), (6, 1), (8, 1), (10, 1), (11, 1) };

List<(int x, int y)> finishPositionA;
List<(int x, int y)> finishPositionB;
List<(int x, int y)> finishPositionC;
List<(int x, int y)> finishPositionD;

InitStage2();

Print(initGameMap);

List<Game> games = new List<Game>() { new Game(initGameMap, 0) };
Game bestGame = null;

List<Game> temp = new List<Game>();

while (games.Any())
{
    List<Game> newGames = new List<Game>();

    foreach (var g in games)
    {
        if (bestGame != null && g.Cost >= bestGame.Cost)
            continue;

        var moves = GetMoveablePieces(g.Map).ToList();

        foreach (var move in moves)
        {
            var bla = g.Map.ToDictionary(d => d.Key, d => d.Value);
            var c = bla[move.Item1];
            bla.Remove(move.Item1);
            bla[move.Item2] = c;

            var cost = g.Cost + GetCost(move) * GetMultiplier(c);
            if (bestGame == null || cost < bestGame.Cost)
                newGames.Add(new Game(bla, cost));
        }
    }

    var newBestGame = newGames.Where(IsGameDone).OrderBy(c => c.Cost).FirstOrDefault();

    if (newBestGame != null && (bestGame == null || bestGame.Cost > newBestGame.Cost))
        bestGame = newBestGame;

    games = newGames.Take(10000).ToList();
    temp.AddRange(newGames.Skip(10000));

    if (games.Count == 0)
    {
        games.AddRange(temp.Take(10000));
        temp = temp.Skip(10000).ToList();
    }

    //Console.WriteLine(games.Count);

    Console.Clear();
    if (games.Any())
        Print(games[0].Map);

}

Console.WriteLine(bestGame.Cost);

int GetMultiplier(char c)
{
    switch (c)
    {
        case 'A':
            return 1;
        case 'B':
            return 10;
        case 'C':
            return 100;
        case 'D':
            return 1000;
    }

    throw new InvalidOperationException();
}

int GetCost(((int x, int y), (int x, int y)) move)
{
    var x = move.Item1.x > move.Item2.x ? move.Item1.x - move.Item2.x : move.Item2.x - move.Item1.x;

    if ((move.Item1.y == 1 && move.Item2.y != 1) || (move.Item1.y != 1 && move.Item2.y == 1))
    {
        var y = move.Item1.y > move.Item2.y ? move.Item1.y - move.Item2.y : move.Item2.y - move.Item1.y;

        return x + y;
    }
    else
    {
        var y = move.Item1.y + move.Item2.y - 2;

        return x + y;
    }
}

IEnumerable<((int x, int y), (int x, int y))> GetMoveablePieces(Dictionary<(int x, int y), char> map)
{
    foreach (var position in map)
    {
        var idx = finishPositionA.IndexOf(position.Key);
        if (idx != -1 && idx < finishPositionA.Count - 1 && map.ContainsKey(finishPositionA[idx + 1]))
        {
            continue;
        }

        idx = finishPositionB.IndexOf(position.Key);
        if (idx != -1 && idx < finishPositionB.Count - 1 && map.ContainsKey(finishPositionB[idx + 1]))
        {
            continue;
        }

        idx = finishPositionC.IndexOf(position.Key);
        if (idx != -1 && idx < finishPositionC.Count - 1 && map.ContainsKey(finishPositionC[idx + 1]))
        {
            continue;
        }

        idx = finishPositionD.IndexOf(position.Key);
        if (idx != -1 && idx < finishPositionD.Count - 1 && map.ContainsKey(finishPositionD[idx + 1]))
        {
            continue;
        }

        if (IsPieceDone(map, position.Key, position.Value))
        {
            continue;
        }

        var bla = MoveToFinish(map, position.Key, position.Value);

        if (bla != null)
        {
            yield return (position.Key, bla.Value);
            continue;
        }

        if (parkPositions.Contains(position.Key))
            continue;

        foreach (var entry in parkPositions)
        {
            if (map.ContainsKey(entry))
                continue;

            if (entry.x > position.Key.x)
            {
                if (map.Keys.Any(c => c.y == 1 && c.x > position.Key.x && c.x < entry.x))
                    continue;

                yield return (position.Key, entry);
            }
            else
            {
                if (map.Keys.Any(c => c.y == 1 && c.x < position.Key.x && c.x > entry.x))
                    continue;

                yield return (position.Key, entry);
            }
        }

    }
    yield break;
}

(int x, int y)? MoveToFinish(Dictionary<(int x, int y), char> map, (int x, int y) position, char character)
{
    switch (character)
    {
        case 'A':
            {
                var checkPark = parkPositions;
                if (position.x > 3)
                {
                    checkPark = checkPark.Where(c => c.x < position.x && c.x >= 3).ToArray();
                }
                else
                {

                    checkPark = checkPark.Where(c => c.x > position.x && c.x <= 3).ToArray();
                }

                if (checkPark.Any(map.ContainsKey))
                    return null;

                if (finishPositionA.Any(d => map.TryGetValue(d, out var character) && character != 'A'))
                    return null;


                return finishPositionA.First(d => !map.ContainsKey(d));
            }
        case 'B':
            {
                var checkPark = parkPositions;
                if (position.x > 5)
                {
                    checkPark = checkPark.Where(c => c.x < position.x && c.x >= 5).ToArray();
                }
                else
                {

                    checkPark = checkPark.Where(c => c.x > position.x && c.x <= 5).ToArray();
                }

                if (checkPark.Any(map.ContainsKey))
                    return null;

                if (finishPositionB.Any(d => map.TryGetValue(d, out var character) && character != 'B'))
                    return null;


                return finishPositionB.First(d => !map.ContainsKey(d));
            }
        case 'C':
            {
                var checkPark = parkPositions;
                if (position.x > 7)
                {
                    checkPark = checkPark.Where(c => c.x < position.x && c.x >= 7).ToArray();
                }
                else
                {

                    checkPark = checkPark.Where(c => c.x > position.x && c.x <= 7).ToArray();
                }

                if (checkPark.Any(map.ContainsKey))
                    return null;

                if (finishPositionC.Any(d => map.TryGetValue(d, out var character) && character != 'C'))
                    return null;


                return finishPositionC.First(d => !map.ContainsKey(d));
            }
        case 'D':
            {
                var checkPark = parkPositions;
                if (position.x > 9)
                {
                    checkPark = checkPark.Where(c => c.x < position.x && c.x >= 9).ToArray();
                }
                else
                {

                    checkPark = checkPark.Where(c => c.x > position.x && c.x <= 9).ToArray();
                }

                if (checkPark.Any(map.ContainsKey))
                    return null;

                if (finishPositionD.Any(d => map.TryGetValue(d, out var character) && character != 'D'))
                    return null;


                return finishPositionD.First(d => !map.ContainsKey(d));
            }
    }

    return null;
}

bool IsPieceDone(Dictionary<(int x, int y), char> map, (int x, int y) position, char character)
{
    switch (character)
    {
        case 'A':
            var idxA = finishPositionA.IndexOf(position);
            if (idxA != -1 && finishPositionA.Take(idxA + 1).All(c => map.TryGetValue(c, out var character) && character == 'A'))
                return true;
            break;
        case 'B':
            var idxB = finishPositionB.IndexOf(position);
            if (idxB != -1 && finishPositionB.Take(idxB + 1).All(c => map.TryGetValue(c, out var character) && character == 'B'))
                return true;
            break;
        case 'C':
            var idxC = finishPositionC.IndexOf(position);
            if (idxC != -1 && finishPositionC.Take(idxC + 1).All(c => map.TryGetValue(c, out var character) && character == 'C'))
                return true;
            break;
        case 'D':
            var idxD = finishPositionD.IndexOf(position);
            if (idxD != -1 && finishPositionD.Take(idxD + 1).All(c => map.TryGetValue(c, out var character) && character == 'D'))
                return true;
            break;
    }

    return false;
}

void Print(Dictionary<(int x, int y), char> map)
{
    for (int y = 0; y < 7; y++)
    {
        for (int x = 0; x < 13; x++)
        {
            var key = (x, y);
            if (map.ContainsKey(key))
            {
                Console.Write(map[key]);
            }
            else if (parkPositions.Contains(key) || finishPositionA.Contains(key) || finishPositionB.Contains(key) || finishPositionC.Contains(key) || finishPositionD.Contains(key))
            {
                Console.Write('.');
            }
            else if (key == (3, 1) || key == (5, 1) || key == (7, 1) || key == (9, 1))
            {
                Console.Write('.');
            }
            else
            {
                Console.Write('#');
            }
        }
        Console.WriteLine();
    }
}

bool IsGameDone(Game g)
{
    if (!finishPositionA.All(d => g.Map.TryGetValue(d, out var test) && test == 'A'))
        return false;

    if (!finishPositionB.All(d => g.Map.TryGetValue(d, out var test) && test == 'B'))
        return false;

    if (!finishPositionC.All(d => g.Map.TryGetValue(d, out var test) && test == 'C'))
        return false;

    if (!finishPositionD.All(d => g.Map.TryGetValue(d, out var test) && test == 'D'))
        return false;

    return true;
}

void InitStage1()
{
    initGameMap.Add((3, 2), 'B');
    initGameMap.Add((5, 2), 'B');
    initGameMap.Add((7, 2), 'C');
    initGameMap.Add((9, 2), 'D');

    initGameMap.Add((3, 3), 'D');
    initGameMap.Add((5, 3), 'C');
    initGameMap.Add((7, 3), 'A');
    initGameMap.Add((9, 3), 'A');


    finishPositionA = new (int x, int y)[2] { (3, 3), (3, 2) }.ToList();
    finishPositionB = new (int x, int y)[2] { (5, 3), (5, 2) }.ToList();
    finishPositionC = new (int x, int y)[2] { (7, 3), (7, 2) }.ToList();
    finishPositionD = new (int x, int y)[2] { (9, 3), (9, 2) }.ToList();
}

void InitStage2()
{
    initGameMap.Add((3, 2), 'B');
    initGameMap.Add((5, 2), 'B');
    initGameMap.Add((7, 2), 'C');
    initGameMap.Add((9, 2), 'D');

    initGameMap.Add((3, 3), 'D');
    initGameMap.Add((5, 3), 'C');
    initGameMap.Add((7, 3), 'B');
    initGameMap.Add((9, 3), 'A');

    initGameMap.Add((3, 4), 'D');
    initGameMap.Add((5, 4), 'B');
    initGameMap.Add((7, 4), 'A');
    initGameMap.Add((9, 4), 'C');

    initGameMap.Add((3, 5), 'D');
    initGameMap.Add((5, 5), 'C');
    initGameMap.Add((7, 5), 'A');
    initGameMap.Add((9, 5), 'A');


    finishPositionA = new (int x, int y)[4] { (3, 5), (3, 4), (3, 3), (3, 2) }.ToList();
    finishPositionB = new (int x, int y)[4] { (5, 5), (5, 4), (5, 3), (5, 2) }.ToList();
    finishPositionC = new (int x, int y)[4] { (7, 5), (7, 4), (7, 3), (7, 2) }.ToList();
    finishPositionD = new (int x, int y)[4] { (9, 5), (9, 4), (9, 3), (9, 2) }.ToList();
}

class Game
{
    public Game(Dictionary<(int, int), char> map, int cost)
    {
        Map = map;
        Cost = cost;
    }

    public Dictionary<(int x, int y), char> Map { get; }

    public int Cost { get; }
}