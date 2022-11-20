using System.Text.Json;
using System.Text.Json.Nodes;

var input = File.ReadAllText("Input.txt");

var data = JsonDocument.Parse(input);

var resStage1 = GetElementCount(data.RootElement, false);
var resStage2 = GetElementCount(data.RootElement, true);

Console.WriteLine(resStage1);
Console.WriteLine(resStage2);

int? GetElementCount(JsonElement element, bool filter)
{
    int count = 0;
    if (element.ValueKind == JsonValueKind.Array)
    {
        foreach (var tmp in element.EnumerateArray())
        {
            count += GetElementCount(tmp, filter) ?? 0;
        }
    }
    else if (element.ValueKind == JsonValueKind.Object)
    {
        foreach (var tmp in element.EnumerateObject())
        {
            var t = GetElementCount(tmp.Value, filter);
            if (t == null && tmp.Value.ValueKind == JsonValueKind.String)
                return null;
            else
            {
                count += t ?? 0;
            }

        }
    }
    else if (element.ValueKind == JsonValueKind.String && filter)
    {
        if (element.GetString() == "red")
            return null;
    }
    else if (element.ValueKind == JsonValueKind.Number)
    {
        count = element.GetInt32();
    }

    return count;
}