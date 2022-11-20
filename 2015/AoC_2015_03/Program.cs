using System.Collections.Concurrent;

var input = File.ReadAllText("Input.txt");

var dict = new ConcurrentDictionary<(int x, int y), int>();

int santaX = 0;
int santaY = 0;

int roboSantaX = 0;
int roboSantaY = 0;

dict.AddOrUpdate((santaX, santaY), 1, (coord, val) => val++);
dict.AddOrUpdate((roboSantaX, roboSantaY), 1, (coord, val) => val++);

for (int d = 0; d < input.Length; d+=2)
{
    var c = input[d];
    switch (c)
    {
        case '^':
            santaY++;
            break;
        case '<':
            santaX--;
            break;
        case '>':
            santaX++;
            break;
        case 'v':
            santaY--;
            break;
    }

    dict.AddOrUpdate((santaX, santaY), 1, (coord, val) => val++);

    c = input[d+1];
    switch (c)
    {
        case '^':
            roboSantaY++;
            break;
        case '<':
            roboSantaX--;
            break;
        case '>':
            roboSantaX++;
            break;
        case 'v':
            roboSantaY--;
            break;
    }

    dict.AddOrUpdate((roboSantaX, roboSantaY), 1, (coord, val) => val++);
}

Console.WriteLine(dict.Count);

