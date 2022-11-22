var lines = File.ReadAllLines("Input.txt").Take(20);

List<HashSet<(int x, int y, int z)>> onCubes = new List<HashSet<(int x, int y, int z)>>();

foreach (var line in lines)
{
    var data = line.Split(new string[] { " x=", ",y=", ",z=", ".." }, StringSplitOptions.RemoveEmptyEntries);

    var coords = data.Skip(1).Select(int.Parse).ToArray();

    HashSet<(int, int, int)> cubeList = new HashSet<(int, int, int)>();
    for (int x = coords[0]; x <= coords[1]; x++)
    {
        for (int y = coords[2]; y <= coords[3]; y++)
        {
            for (int z = coords[4]; z <= coords[5]; z++)
            {
                if (data[0] == "on")
                {
                    cubeList.Add((x, y, z));
                }
                else
                {
                    onCubes.ForEach(d => d.Remove((x, y, z)));
                }
            }
        }
    }
    if (cubeList.Any())
    {
        onCubes.Add(cubeList);
    }
}

Console.WriteLine(onCubes.SelectMany(c=>c).ToHashSet().Count());