var lines = File.ReadAllLines("Input.txt");

var root = new Directory("/", null);
var currentDirectory = root;
bool listMode = false;
foreach (var line in lines)
{
    var split = line.Split(' ');
    if (split[0] == "$")
    {
        listMode = false;
        switch (split[1])
        {
            case "cd":
                {
                    if (split[2] == "/")
                    {
                        currentDirectory = root;
                    }
                    else if (split[2] == "..")
                    {
                        currentDirectory = currentDirectory.ParentDirectory ?? root;
                    }
                    else
                    {
                        var dict = currentDirectory.Directories.FirstOrDefault(d => d.Name == split[2]);
                        if (dict == null)
                        {
                            dict = new Directory(split[2], currentDirectory);
                        }

                        currentDirectory = dict;
                    }

                    break;
                }

            case "ls":
                {
                    listMode = true;
                }
                break;
        }
    }
    else if (listMode)
    {
        if (split[0] == "dir")
        {
            var dict = currentDirectory.Directories.FirstOrDefault(d => d.Name == split[1]);
            if (dict == null)
            {
                dict = new Directory(split[1], currentDirectory);
            }
        }
        else if (int.TryParse(split[0], out var size))
        {
            currentDirectory.Files.Add((size, split[1]));
        }
    }
}

var dicts = root.GetDirectories();

var result = dicts.Where(d => d.GetSize() <= 100000).Sum(d => d.GetSize());

Console.WriteLine("Stage1: " + result);

var rootSize = root.GetSize();
var availableSIze = 70000000 - rootSize;

var needed = 30000000 - availableSIze;

var bla = dicts.OrderBy(d => d.GetSize()).Where(d => d.GetSize() >= needed).FirstOrDefault();
Console.WriteLine("Stage2: " + bla.GetSize());


public class Directory
{
    public Directory(string name, Directory parentDirectory)
    {
        Name = name;
        ParentDirectory = parentDirectory;
        ParentDirectory?.Directories.Add(this);
    }

    public string Name { get; set; }

    public Directory ParentDirectory { get; set; }

    public List<Directory> Directories { get; } = new List<Directory>();

    public List<(int, string)> Files { get; } = new List<(int, string)>();

    public int GetSize()
    {
        var dicts = GetDirectories();

        var size = dicts.SelectMany(d => d.Files).Sum(d => d.Item1);

        return size;
    }

    public IEnumerable<Directory> GetDirectories()
    {
        yield return this;

        foreach (var entry in Directories.SelectMany(c => c.GetDirectories()))
        {
            yield return entry;
        }
    }
}