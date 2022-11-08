using System.Numerics;

var file = File.ReadAllLines("Input.txt");

List<Scanner> list = GetScannerValues(file);

List<Quaternion> rotations = GetRotations().ToList();

foreach (var entry in list)
{
    foreach (var entry2 in list)
    {
        if (entry == entry2)
            continue;

        List<(BeaconPoint, BeaconPoint)> overlaps = new List<(BeaconPoint, BeaconPoint)>();
        foreach (var b1 in entry.Beacons)
        {
            var length1 = b1.Distances.Select(c => c.Value.LengthSquared()).ToList();
            foreach (var b2 in entry2.Beacons)
            {
                var length2 = b2.Distances.Select(c => c.Value.LengthSquared()).ToList();

                var intersect = length1.Intersect(length2).Count();
                if (intersect >= 11)
                {
                    overlaps.Add((b1, b2));
                    break;
                }
            }
        }

        if (overlaps.Count >= 11)
        {
            if (!entry.Overlap.ContainsKey(entry2))
                entry.Overlap.Add(entry2, overlaps);
        }
    }
}

list[0].RotationSet = true;
foreach (var entry in list[0].Beacons)
{
    entry.SetNormalizedPosition();
}

var toDo = list.Where(c => !c.RotationSet);
while (toDo.Any())
{
    foreach (var currentEntry in toDo.ToList())
    {
        var tmp = currentEntry.Overlap.Keys.FirstOrDefault(c => c.RotationSet);
        if (tmp != null)
        {
            var bla = currentEntry.Overlap[tmp].Skip(1).FirstOrDefault();

            var positionFixed = bla.Item2;
            var positionCurrent = bla.Item1;

            foreach (var vektorFixed in positionFixed.Distances)
            {
                foreach (var vektorNew in positionCurrent.Distances)
                {
                    if (vektorFixed.Value.LengthSquared() == vektorNew.Value.LengthSquared())
                    {
                        Quaternion tmpBlaa = new Quaternion();
                        foreach (var entry in rotations)
                        {
                            var checkVektor = Vector3.Transform(vektorNew.Value, entry);

                            var newVek = new Vector3((int)Math.Round(checkVektor.X), (int)Math.Round(checkVektor.Y), (int)Math.Round(checkVektor.Z));

                            if (newVek == vektorFixed.Value)
                            {
                                tmpBlaa = entry;
                                break;
                            }
                        }

                        if (positionFixed.Scanner.Rotation != null)
                        {
                            tmpBlaa = Quaternion.Concatenate(tmpBlaa, positionFixed.Scanner.Rotation.Value);
                        }

                        var vectorFromProbe = new Vector3(positionCurrent.Position.X, positionCurrent.Position.Y, positionCurrent.Position.Z);
                        vectorFromProbe = Vector3.Transform(vectorFromProbe, tmpBlaa);
                        vectorFromProbe = new Vector3((int)Math.Round(vectorFromProbe.X), (int)Math.Round(vectorFromProbe.Y), (int)Math.Round(vectorFromProbe.Z));

                        vectorFromProbe = Vector3.Negate(vectorFromProbe);

                        currentEntry.Rotation = tmpBlaa;
                        currentEntry.NormalizedPosition = Vector3.Add(positionFixed.NormalizedPosition, vectorFromProbe);

                        positionCurrent.SetNormalizedPosition();

                        foreach (var entry in currentEntry.Beacons)
                        {
                            entry.SetNormalizedPosition();
                        }

                        currentEntry.RotationSet = true;
                        break;
                    }
                }

                if (currentEntry.RotationSet)
                    break;
            }
        }
    }
}

var entries = list.SelectMany(d => d.Beacons).Select(c => c.NormalizedPosition).ToList();
var entries2 = entries.Distinct().ToList();

double length = 0;

foreach (var l1 in list)
{
    foreach (var l2 in list)
    {
        var length2 = Math.Abs(l1.NormalizedPosition.X - l2.NormalizedPosition.X);
        length2 += Math.Abs(l1.NormalizedPosition.Y - l2.NormalizedPosition.Y);
        length2 += Math.Abs(l1.NormalizedPosition.Z - l2.NormalizedPosition.Z);

        if (length2 > length)
        {
            length = length2;
        }
    }
}
Console.WriteLine(length);


static IEnumerable<Quaternion> GetRotations()
{
    foreach (var yaw in new[] { ConvertDegreesToRadians(0), ConvertDegreesToRadians(90), ConvertDegreesToRadians(180), ConvertDegreesToRadians(270) })
        foreach (var pitch in new[] { ConvertDegreesToRadians(0), ConvertDegreesToRadians(90), ConvertDegreesToRadians(180), ConvertDegreesToRadians(270) })
            foreach (var roll in new[] { ConvertDegreesToRadians(0), ConvertDegreesToRadians(90), ConvertDegreesToRadians(180), ConvertDegreesToRadians(270) })
                yield return Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
}

static List<Scanner> GetScannerValues(string[] file)
{
    Scanner sc = null;
    List<Scanner> list = new List<Scanner>();
    foreach (var tmp in file)
    {
        if (tmp.StartsWith("---"))
        {
            sc = new Scanner(tmp);
            list.Add(sc);
        }
        else
        {
            var coords = tmp.Split(',');
            if (coords.Length == 3)
            {
                sc.AddBeacon(coords);
            }
        }
    }

    list.ForEach(d => d.CalculateDistances());

    return list;
}

static float ConvertDegreesToRadians(float degrees)
{
    float radians = (float)((Math.PI / 180) * degrees);
    return radians;
}