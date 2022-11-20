var startPW = "vzbxkghb".ToArray();

var invalidChars = new[] { 'i', 'o', 'l' };

GetNextPassword(startPW, invalidChars);

Console.WriteLine("Nächstes PW: " + new string(startPW));

GetNextPassword(startPW, invalidChars);

Console.WriteLine("Nächstes PW: " + new string(startPW));

static void GetNextPassword(char[] startPW, char[] invalidChars)
{
    while (true)
    {
        for (int i = startPW.Length - 1; i >= 0; i--)
        {
            if (startPW[i] == 'z')
            {
                startPW[i] = 'a';
            }
            else
            {
                startPW[i]++;

                if (invalidChars.Contains(startPW[i]))
                {
                    startPW[i]++;
                }

                break;
            }
        }

        if (!startPW.Any(invalidChars.Contains))
        {
            List<char> pairs = new List<char>();
            bool hasOrder = false;
            for (int i = 1; i < startPW.Length; i++)
            {
                if (i > 2 && startPW[i - 2] + 2 == startPW[i] && startPW[i - 1] + 1 == startPW[i])
                {
                    hasOrder = true;
                }

                if (startPW[i - 1] == startPW[i])
                {
                    pairs.Add(startPW[i]);
                }
            }

            if (hasOrder && pairs.Distinct().Count() >= 2)
            {
                break;
            }
        }
    }
}