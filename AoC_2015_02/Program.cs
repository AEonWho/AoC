var lines = File.ReadAllLines("Input.txt");

var packages = lines.Select(c => new Package(c));

var wrap = packages.Sum(d => d.WrappingPaperNeeded);
var ribbon = packages.Sum(c => c.Ribbon);
Console.WriteLine("WrappingPaper: "+ wrap);
Console.WriteLine("Ribbon: "+ ribbon);