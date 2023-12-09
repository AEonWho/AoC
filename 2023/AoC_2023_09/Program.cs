
var input = File.ReadAllLines("Input.txt");

List<DataReading> readings = new List<DataReading>();
for (int i = 0; i < input.Length; i++)
{
    var values = input[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
    readings.Add(new DataReading(values.ToArray()));
}

Console.WriteLine("Level1: " + readings.Sum(d => d.NextVal));
Console.WriteLine("Level2: " + readings.Sum(d => d.PrevVal));

internal class DataReading
{
    public DataReading(int[] values)
    {
        Values = values;
        var newOffsets = new List<int[]>();
        while (newOffsets.Count == 0 || newOffsets[^1].Any(d => d != 0))
        {
            var data = newOffsets.Count == 0 ? Values : newOffsets[^1];

            List<int> offset = new List<int>();
            for (int i = 0; i < data.Length - 1; i++)
            {
                offset.Add(data[i + 1] - data[i]);
            }

            newOffsets.Add(offset.ToArray());
        }

        Offsets = newOffsets.ToArray();

        var nextVal = 0;
        var prevVal = 0;
        foreach (var bla in Offsets.Reverse())
        {
            nextVal += bla[^1];
            prevVal = bla[0] - prevVal;
        }

        NextVal = Values[^1] + nextVal;
        PrevVal = Values[0] - prevVal;
    }

    public int[] Values { get; }

    public int[][] Offsets { get; }
    public int NextVal { get; }
    public int PrevVal { get; }
}