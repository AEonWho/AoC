int count = 1;
int idx = 0;

var testX = 2981;
var testY = 3075;

bool found = false;

while (!found)
{
    for (int xVar = idx; xVar >= 0; xVar--)
    {
        var yVar = idx - xVar;

        if (testX - 1 == xVar && testY - 1 == yVar)
        {
            found = true;
            break;
        }

        count++;
    }

    idx++;
}

long value = 20151125;
for (var x = 1; x < count; x++)
{
    value = value * 252533 % 33554393;
}

Console.WriteLine(value);
