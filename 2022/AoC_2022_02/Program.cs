var lines = File.ReadAllLines("Input.txt").ToList();

var games = new List<(string, string)>();

foreach (var line in lines)
{
    var data = line.Split(' ');

    var val1 = data[0];
    var val2 = data[1];

    games.Add((val1, val2));
}

var score = 0;
foreach (var game in games)
{
    score += GetGameScore(game);
}

Console.WriteLine(score);

score = 0;
foreach (var game in games)
{

    var play = "";
    switch (game.Item2)
    {
        case "Y" when game.Item1 == "A":
        case "X" when game.Item1 == "B":
        case "Z" when game.Item1 == "C":
            play = "X";
            break;
        case "Y" when game.Item1 == "B":
        case "X" when game.Item1 == "C":
        case "Z" when game.Item1 == "A":
            play = "Y";
            break;
        case "Y" when game.Item1 == "C":
        case "X" when game.Item1 == "A":
        case "Z" when game.Item1 == "B":
            play = "Z";
            break;
    }

    score += GetGameScore((game.Item1, play));
}

Console.WriteLine(score);

int GetGameScore((string, string) game)
{
    int score = 0;
    if (game.Item2 == "X")
    {
        score += 1;
    }
    else if (game.Item2 == "Y")
    {
        score += 2;
    }
    else if (game.Item2 == "Z")
    {
        score += 3;
    }

    if (game.Item1[0] == game.Item2[0] - 23)
    {
        score += 3;
    }
    else if (game.Item1 == "A" && game.Item2 == "Y")
    {
        score += 6;
    }
    else if (game.Item1 == "B" && game.Item2 == "Z")
    {
        score += 6;
    }
    else if (game.Item1 == "C" && game.Item2 == "X")
    {
        score += 6;
    }

    return score;
}