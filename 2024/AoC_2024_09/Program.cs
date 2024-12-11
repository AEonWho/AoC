using System.Text;

var input = File.ReadAllText("Input.txt");

var length = input.Select(c => int.Parse(c + ""));

int?[] diskPart1 = new int?[length.Sum()];
int?[] diskPart2 = new int?[length.Sum()];

BuildDisk();

Level1();

Level2();

void Level2()
{
    var lastIdx = diskPart2.Length - 1;
    var size = 1;
    while (lastIdx >= 1)
    {
        if (diskPart2[lastIdx - 1] == diskPart2[lastIdx] && diskPart2[lastIdx] != null)
        {
            size++;
        }
        else if(diskPart2[lastIdx] != null)
        {
            var startIdx = 0;
            var emptySize = 0;
            while (startIdx < lastIdx)
            {
                if (diskPart2[startIdx] == null)
                {
                    emptySize++;

                    if (emptySize == size)
                    {
                        for (int i = 0; i < size; i++)
                        {
                            diskPart2[startIdx - size + i+1] = diskPart2[lastIdx + i];
                            diskPart2[lastIdx + i] = null;
                        }

                        //Move
                        break;
                    }
                }
                else
                {
                    emptySize = 0;
                }

                startIdx++;
            }

            size = 1;
        }
        lastIdx = lastIdx - 1;
    }

    var lvl2 = diskPart2.Select((d, i) => (long)((d ?? 0) * i)).Sum();

    Console.WriteLine("Level 2: " + lvl2);
}

void Level1()
{
    var lastIdx = diskPart1.Length - 1;
    var firstIdx = 0;
    while (firstIdx < lastIdx)
    {
        if (diskPart1[firstIdx] == null && diskPart1[lastIdx] != null)
        {
            diskPart1[firstIdx] = diskPart1[lastIdx];
            diskPart1[lastIdx] = null;
        }

        if (diskPart1[firstIdx] != null)
        {
            firstIdx++;
        }

        if (diskPart1[lastIdx] == null)
        {
            lastIdx--;
        }
    }

    var lvl1 = diskPart1.Select((d, i) => (long)((d ?? 0) * i)).Sum();

    Console.WriteLine("Level 1: " + lvl1);
}

void BuildDisk()
{

    bool file = true;
    var idx = 0;
    var fileCount = 0;
    foreach (var bla in length)
    {
        for (int i = 0; i < bla; i++)
        {
            if (file)
            {
                if (i == 0)
                {
                    fileCount++; ;
                }

                diskPart1[idx + i] = fileCount - 1;
                diskPart2[idx + i] = fileCount - 1;
            }
        }

        idx += bla;

        file = !file;
    }
}