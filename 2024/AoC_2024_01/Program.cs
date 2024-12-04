
var lines = File.ReadAllLines("Input.txt").ToList();

List<int> list1 = [];
List<int> list2 = [];

foreach (var l in lines)
{
    var split = l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    list1.Add(split[0]);
    list2.Add(split[1]);
}

list1 = list1.OrderBy(d => d).ToList();
list2 = list2.OrderBy(d => d).ToList();

var sum = 0;
for (int i = 0; i < list1.Count; i++)
{
    sum += Math.Abs(list2[i] - list1[i]);
}

Console.WriteLine("Level1: " + sum);

sum = 0;
foreach (var num in list1)
{
    sum += num * list2.Where(d => d == num).Count();
}
Console.WriteLine("Level2: " + sum);