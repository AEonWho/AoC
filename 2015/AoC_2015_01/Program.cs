var txt = File.ReadAllText("Input.txt");

var x = 0;

for (int idx = 0; idx < txt.Length; idx++)
{
    var c = txt[idx];

    switch (c)
    {
        case '(':
            x++;
            break;
        case ')':
            x--;

            if (x == -1)
            {
                Console.WriteLine($"Santa is in Basement at idx {idx+1}");
            }
            break;
    }
}

Console.WriteLine($"Santa ends on Floor {x}");