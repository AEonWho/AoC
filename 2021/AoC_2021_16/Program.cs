var txt = File.ReadAllText("Input.txt");

string binarystring = String.Join(String.Empty,
  txt.Select(
    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
  )
);

Packet p = new Packet(binarystring);

var idx = p.Parse(binarystring[6..]);

var versions = p.Version;
List<Packet> packets = p.SubPackets;
while (packets.Count > 0)
{
    versions += packets.Sum(c => c.Version);
    packets = packets.SelectMany(d => d.SubPackets).ToList();
}

Console.WriteLine("Versions: " + versions);
var bla = p.SubPackets.Select(c => c.GetValue()).Sum();
Console.WriteLine("Value: " + p.GetValue());

public class Packet
{
    public Packet(string binarystring)
    {
        var versionStart = 0;
        var versionEnd = 3;
        var typeStart = 3;
        var typeEnd = 6;

        var versionBinary = binarystring[versionStart..versionEnd];
        Version = Convert.ToInt32(versionBinary, 2);
        var typeBinary = binarystring[typeStart..typeEnd];
        Type = Convert.ToInt32(typeBinary, 2);
    }

    public int Version { get; set; }

    public int Type { get; set; }

    public List<Packet> SubPackets { get; } = new List<Packet>();

    private long? _value;

    public long GetValue()
    {
        if (_value.HasValue)
            Console.WriteLine(_value);

        switch (Type)
        {
            case 1:
                return SubPackets.Select(c => c.GetValue()).Aggregate((d, e) => d * e);
            case 2:
                return SubPackets.Select(c => c.GetValue()).Min();
            case 3:
                return SubPackets.Select(c => c.GetValue()).Max();
            case 5:
                return SubPackets[0].GetValue() > SubPackets[1].GetValue() ? 1 : 0;
            case 6:
                return SubPackets[0].GetValue() < SubPackets[1].GetValue() ? 1 : 0;
            case 7:
                return SubPackets[0].GetValue() == SubPackets[1].GetValue() ? 1 : 0;
            default:
                return SubPackets.Select(c => c.GetValue()).Sum();
        }
    }

    public int Parse(string v)
    {
        switch (Type)
        {
            case 4:
                string binaryValue = "";
                int prefix = 0;
                while (true)
                {
                    var start = prefix + 1;
                    var end = prefix + 5;
                    var valueString = v[start..end];
                    binaryValue += valueString;

                    if (v[prefix] == '0')
                    {
                        break;
                    }
                    else
                    {
                        prefix += 5;
                    }
                }

                _value = Convert.ToInt64(binaryValue, 2);

                return prefix + 5;


            default:
                var lengthTypeBinaryValue = v[0..1];
                var lengthTypeValue = Convert.ToInt32(lengthTypeBinaryValue, 2);

                if (lengthTypeValue == 1)
                {
                    var numberPacketsBinaryValue = v[1..12];
                    var numberPacketsValue = Convert.ToInt32(numberPacketsBinaryValue, 2);

                    var idx = 12;
                    for (var i = 0; i < numberPacketsValue; i++)
                    {
                        Packet p = new Packet(v[idx..]);

                        var dataStart = idx + 6;
                        idx += 6 + p.Parse(v[dataStart..]);
                        SubPackets.Add(p);
                    }

                    return idx;
                }
                else
                {
                    var numberBitsBinaryValue = v[1..16];
                    var numberBits = Convert.ToInt32(numberBitsBinaryValue, 2);

                    var length = numberBits + 16;

                    var idx = 16;
                    while (true)
                    {
                        if (!v[idx..length].Any() || v[idx..length].All(c => c == '0'))
                            break;

                        Packet p = new Packet(v[idx..length]);

                        var dataStart = idx + 6;
                        idx += 6 + p.Parse(v[dataStart..length]);

                        SubPackets.Add(p);
                    }

                    return idx;
                }
        }
    }
}