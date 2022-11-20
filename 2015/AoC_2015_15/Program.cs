using System.ComponentModel;

var lines = File.ReadAllLines("Input.txt");

var Ingredients = lines.Select(c => new Ingredient(c)).ToList();

int score = 0;
int scoreCalories = 0;

int[] ingredientFaktor = new int[4];

for (ingredientFaktor[0] = 1; ingredientFaktor[0] < 98; ingredientFaktor[0]++)
{
    for (ingredientFaktor[1] = 1; ingredientFaktor[1] < 99 - ingredientFaktor[0]; ingredientFaktor[1]++)
    {
        for (ingredientFaktor[2] = 1; ingredientFaktor[2] < 100 - ingredientFaktor[0] - ingredientFaktor[1]; ingredientFaktor[2]++)
        {
            ingredientFaktor[3] = 100 - ingredientFaktor[0] - ingredientFaktor[1] - ingredientFaktor[2];

            var capacity = Ingredients.Select((c, i) => c.Capacity * ingredientFaktor[i]).Sum();
            var durability = Ingredients.Select((c, i) => c.Durability * ingredientFaktor[i]).Sum();
            var flavor = Ingredients.Select((c, i) => c.Flavor * ingredientFaktor[i]).Sum();
            var texture = Ingredients.Select((c, i) => c.Texture * ingredientFaktor[i]).Sum();
            var calories = Ingredients.Select((c, i) => c.Calories * ingredientFaktor[i]).Sum();

            if (capacity > 0 && durability > 0 && flavor > 0 && texture > 0)
            {
                var val = capacity * durability * flavor * texture;
                if (val > score)
                {
                    score = val;
                }

                if (calories == 500)
                {
                    if (val > scoreCalories)
                    {
                        scoreCalories = val;
                    }
                }
            }
        }
    }
}

Console.WriteLine($"Highest Score: {score}");
Console.WriteLine($"Highest Score (500 cal): {scoreCalories}");

class Ingredient
{
    public Ingredient(string line)
    {
        var x = line.Split(new char[] { ' ', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
        Name = x[0];

        Capacity = int.Parse(x[2]);

        Durability = int.Parse(x[4]);

        Flavor = int.Parse(x[6]);

        Texture = int.Parse(x[8]);

        Calories = int.Parse(x[10]);
    }

    public string Name { get; }
    public int Capacity { get; }
    public int Durability { get; }
    public int Flavor { get; }
    public int Texture { get; }
    public int Calories { get; }
}