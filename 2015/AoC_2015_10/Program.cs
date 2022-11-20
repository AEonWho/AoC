using System.Text;

var input = "1113122113";

for (int i = 0; i < 50; i++)
{
    input = Iterate(input);


    Console.WriteLine($"Iteration{i + 1}: {input.Length}");
}


string Iterate(string input)
{
    StringBuilder sb = new StringBuilder();

    var checkChar = input[0];
    var currentCount = 0;

    for (int i = 0; i < input.Length; i++)
    {
        if (checkChar == input[i])
        {
            currentCount++;
        }
        else
        {
            sb.Append($"{currentCount}{checkChar}");
            checkChar = input[i];
            currentCount = 1;
        }

    }
    sb.Append($"{currentCount}{checkChar}");

    return sb.ToString();
}