using AoC_Common.PathFinding;

var lines = File.ReadAllLines("Input.txt").ToArray();

var sizeY = lines.Length;
var sizeX = lines[0].Length;

var map = new Dictionary<MapCoordinate, char>();

for (int y = 0; y < sizeY; y++)
{
    for (int x = 0; x < sizeX; x++)
    {
        map[(x, y)] = lines[y][x];
    }
}


var fields = new List<Field>();

foreach (var m in map.Keys)
{
    if (fields.Any(c => c.Area.Contains(m)))
        continue;

    var field = new Field { FieldId = map[m] };
    fields.Add(field);
    field.Area.Add(m);
    var neighbours = m.GetNeighbors().Where(d => map.ContainsKey(d) && map[d] == field.FieldId).ToList();
    while (neighbours.Count != 0)
    {
        neighbours.ForEach(c => field.Area.Add(c));

        neighbours = neighbours.SelectMany(c => c.GetNeighbors()).Where(d => map.ContainsKey(d) && map[d] == field.FieldId).Except(field.Area).ToList();
    }
}

var sumLevel1 = 0;
var sumLevel2 = 0;
foreach (var f in fields)
{
    var perimeter = f.Area.Select(d => d.GetNeighbors().Except(f.Area)).Sum(c => c.Count());
    var area = f.Area.Count;

    sumLevel1 += area * perimeter;


    var perimeter2 = f.Area.Select(d => d.GetNeighborsWithDirection().Where(d => !f.Area.Contains(d.Item1))).SelectMany(d => d);

    var north = perimeter2.Where(d => d.Item2 == MapDirection.N).GroupBy(d => d.Item1.Y);
    var south = perimeter2.Where(d => d.Item2 == MapDirection.S).GroupBy(d => d.Item1.Y);
    var west = perimeter2.Where(d => d.Item2 == MapDirection.W).GroupBy(d => d.Item1.X);
    var east = perimeter2.Where(d => d.Item2 == MapDirection.E).GroupBy(d => d.Item1.X);

    var sides = 0;
    foreach (var grp in north.Concat(south))
    {
        var bla = grp.OrderBy(d => d.Item1.X).Select(c => c.Item1.X).ToList();

        sides++;
        for (int i = 1; i < bla.Count; i++)
        {
            if (bla[i] != bla[i - 1] + 1)
                sides++;
        }
    }

    foreach (var grp in west.Concat(east))
    {

        var bla = grp.OrderBy(d => d.Item1.Y).Select(c => c.Item1.Y).ToList();

        sides++;
        for (int i = 1; i < bla.Count; i++)
        {
            if (bla[i] != bla[i - 1] + 1)
                sides++;
        }
    }



    sumLevel2 += area * sides;

}

Console.WriteLine(sumLevel1);
Console.WriteLine(sumLevel2);