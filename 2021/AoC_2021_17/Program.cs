var targetX = (79, 137);
var targetY = (-176, -117);

//var targetX = (79, 137);
//var targetY = (-176, -117);

Console.WriteLine("Stage1:" + Enumerable.Range(1, Math.Abs(targetY.Item1) - 1).Sum());

//Bruteforce mit gfreits grad ned...
HashSet<(int, int)> hitVelocities = new HashSet<(int, int)>();
for (int startX = 0; startX <= targetX.Item2; startX++)
{
    for (int startY = targetY.Item1; startY <= targetY.Item1 * -1; startY++)
    {
        var x = startX;
        var y = startY;

        var coordX = startX;
        var coordY = startY;

        while (coordX <= targetX.Item2 && coordY >= targetY.Item1)
        {
            if (coordX >= targetX.Item1 && coordX <= targetX.Item2 && coordY >= targetY.Item1 && coordY <= targetY.Item2)
            {
                hitVelocities.Add((startX, startY));
                break;
            }

            if (x > 0)
                x = x - 1;

            y = y - 1;

            coordX += x;
            coordY += y;
        }
    }
}

Console.WriteLine();