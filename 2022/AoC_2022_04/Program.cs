var lines = File.ReadAllLines("Input.txt").ToList();

int count1 = 0;
int count2 = 0;

for (int i = 0; i < lines.Count; i++)
{
    var line = lines[i];
    var splitted = line.Split(',', '-').Select(int.Parse).ToArray();

    if ((splitted[0] >= splitted[2] && splitted[1] <= splitted[3]) || (splitted[2] >= splitted[0] && splitted[3] <= splitted[1]))
        count1++;

    if (splitted[1] >= splitted[2] && splitted[0] <= splitted[3])
        count2++;
}

Console.WriteLine("Stage1: " + count1);
Console.WriteLine("Stage2: " + count2);