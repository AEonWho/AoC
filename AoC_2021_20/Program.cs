var txt = File.ReadAllLines("Input.txt");
var txt2 = File.ReadAllLines("Sample.txt");

var map = new FloorMap(txt.Skip(2).ToArray(), txt[0].ToCharArray());
var map2 = new FloorMap(txt2.Skip(2).ToArray(), txt2[0].ToCharArray());

for(int iteration = 1;iteration<=50;iteration++)
{
    map = map.NextIteration();
    map2 = map2.NextIteration();

    if(iteration == 2 || iteration == 50)
    {
        var str = map.GetString();
        var result = str.Count(d => d == '#');
        var str2 = map2.GetString();
        var result2 = str2.Count(d => d == '#');
        Console.WriteLine($"SampleData ({iteration}): {result2}");
        Console.WriteLine($"FinalData ({iteration}): {result}");
        Console.WriteLine();
    }
}

Console.ReadLine();