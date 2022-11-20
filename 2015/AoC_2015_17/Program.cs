using System.Globalization;

var containers = File.ReadAllLines("Input.txt").Select((d, idx) => new Container(idx, int.Parse(d)));

var container = CheckContainers(containers, 150).ToList();

container = container.GroupBy(c => string.Join(" - ", c.Select(d => d.ContainerId))).Select(c => c.First()).ToList();

Console.WriteLine($"Container Combinations: {container.Count}");

var idealContainerCombinations = container.GroupBy(d => d.Count()).OrderBy(c => c.Key).First().Count();

Console.WriteLine($"Ideal Container Combinations: {idealContainerCombinations}");

IEnumerable<List<Container>> CheckContainers(IEnumerable<Container> containers, int eggnog)
{
    var possibleContainers = containers.Where(c => c.Size <= eggnog).ToList();
    foreach (var container in possibleContainers)
    {
        if (container.Size == eggnog)
            yield return new List<Container> { container };
        else
        {
            foreach (var c in CheckContainers(possibleContainers.Where(c => c != container), eggnog - container.Size))
            {
                c.Add(container);
                yield return c.OrderBy(c => c.ContainerId).ToList();
            }
        }
    }
}

internal class Container
{
    public int ContainerId { get; }
    public int Size { get; }

    public Container(int containerId, int size)
    {
        ContainerId = containerId;
        Size = size;
    }
}