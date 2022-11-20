using System.Security.Cryptography;
using System.Text;

var input = "yzbqklnj";

var algo = MD5.Create();

int i = 1;
while (true)
{
    var resByte = algo.ComputeHash(Encoding.ASCII.GetBytes($"{input}{i}"));
    var resStr = BitConverter.ToString(resByte);

    if (resStr.StartsWith("00-00-0"))
    {
        Console.WriteLine($"Input: {input}{i} results in 5 leading zeros");
    }

    if (resStr.StartsWith("00-00-00"))
    {
        Console.WriteLine($"Input: {input}{i} results in 6 leading zeros");
        break;
    }

    i++;
}

Console.ReadLine();