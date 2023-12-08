using AoC_Common;

var input = File.ReadAllLines("Input.txt");

var instructions = input[0].ToCharArray();

Dictionary<string, string> LeftMap = new Dictionary<string, string>();
Dictionary<string, string> RightMap = new Dictionary<string, string>();
for (int i = 2; i < input.Length; i++)
{
    var splitted = input[i].Split(new[] { '=', ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
    LeftMap.Add(splitted[0], splitted[1]);
    RightMap.Add(splitted[0], splitted[2]);
}

{
    var instructionCounter = 0;
    var node = "AAA";
    while (node != "ZZZ")
    {
        if (instructions[instructionCounter % instructions.Length] == 'L')
        {
            node = LeftMap[node];
        }
        else
        {
            node = RightMap[node];
        }

        instructionCounter++;
    }

    Console.WriteLine(instructionCounter);
}

{
    var nodeA = LeftMap.Keys.Where(d => d[2] == 'A');

    List<long> kgv = new List<long>();
    foreach (var nodea in nodeA)
    {
        var instructionCounter = 0;
        var node = nodea;
        while (node[2] != 'Z')
        {
            if (instructions[instructionCounter % instructions.Length] == 'L')
            {
                node = LeftMap[node];
            }
            else
            {
                node = RightMap[node];
            }

            instructionCounter++;
        }
        kgv.Add(instructionCounter);
    }

    Console.WriteLine(AoC_Math.GetSmallestCommonMultiple(kgv.ToArray()));
}