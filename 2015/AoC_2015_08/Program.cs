var input = File.ReadAllLines("Input.txt");
//var input = File.ReadAllLines("Sample.txt");

int size = 0;
foreach (var line in input)
{
    var length = line.Length;

    var contentLength = 0;
    for (int i = 1; i < length - 1; i++)
    {
        contentLength++;
        switch (line[i])
        {
            case '\\':
                if (line[i + 1] == 'x')
                {
                    i += 3;
                }
                else
                {
                    i += 1;
                }
                break;
        }
    }

    size += length - contentLength;
}

Console.WriteLine(size);


size = 0;
foreach (var line in input)
{
    var length = line.Length;

    var addedLength = 4;
    for (int i = 1; i < length - 1; i++)
    {
        switch (line[i])
        {
            case '\\':
                if (line[i + 1] == 'x')
                {
                    addedLength++;
                    i += 3;
                }
                else
                {
                    addedLength += 2;
                    i += 1;
                }
                break;
        }
    }

    size += addedLength;
}

Console.WriteLine(size);