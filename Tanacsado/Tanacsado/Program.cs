using System.Globalization;
using TanacsadoLib;

var ugyfelek = BeolvasUgyfelek("ugyfel.csv");
var tanacsadok = BeolvasTanacsadok("tanacsado.csv");
var talalkozok = BeolvasTalalkozok("talalkozo.csv");
var szakteruletek = BeolvasSzakteruletek("szakterulet.csv");

// 4. feladat: találkozó árának kiszámítása
Console.WriteLine("4. feladat:");
foreach (var talalkozo in talalkozok)
{
    var tanacsado = tanacsadok.First(t => t.TanacsadoId == talalkozo.TanacsadoId);
    var ar = talalkozo.Idotartam * tanacsado.Oradij;
    Console.WriteLine($"Találkozó ID: {talalkozo.TalalkozoId}, Ár: {ar} Ft");
}

// 5. feladat: Legalább 3 órás találkozók száma
Console.WriteLine("5. feladat:");
var hosszuTalalkozokSzama = talalkozok.Count(t => t.Idotartam >= 3);
Console.WriteLine($"Legalább 3 órás találkozók száma: {hosszuTalalkozokSzama}");

// 6. feladat: Tanácsadó elérhetőségei
Console.WriteLine("6. feladat:");
Console.Write("Kérem a tanácsadó nevét: ");
string keresettNev = Console.ReadLine();
var tanacsadoKereso = tanacsadok.FirstOrDefault(t => t.Nev.Equals(keresettNev, StringComparison.OrdinalIgnoreCase));

if (tanacsadoKereso != null)
{
    var szakterulet = szakteruletek.First(s => s.SzakteruletId == tanacsadoKereso.SzakteruletId);
    Console.WriteLine($"Telefonszám: {tanacsadoKereso.Telefon}, Email: {tanacsadoKereso.Email}, Szakterület: {szakterulet.Megnevezes}, Óradíj: {tanacsadoKereso.Oradij} Ft");
}
else
{
    Console.WriteLine("Ilyen néven nem található tanácsadó.");
}

// 7. feladat: Top 3 legtöbbet kereső tanácsadó
Console.WriteLine("7. feladat:");
var tanacsadoOsszeg = tanacsadok.Select(t => new
{
    Tanacsado = t,
    Osszeg = talalkozok.Where(tal => tal.TanacsadoId == t.TanacsadoId).Sum(tal => tal.Idotartam * t.Oradij)
})
.OrderByDescending(t => t.Osszeg)
.Take(3);

foreach (var item in tanacsadoOsszeg)
{
    Console.WriteLine($"Tanácsadó: {item.Tanacsado.Nev}, Összeg: {item.Osszeg} Ft");
}
        

static List<Ugyfel> BeolvasUgyfelek(string filePath)
{
    var lines = File.ReadAllLines(filePath).Skip(1);
    return lines.Select(line => line.Split(';')).Select(fields => new Ugyfel
    {
        UgyfelId = int.Parse(fields[0]),
        Nev = fields[1],
        Telefon = fields[2],
        Email = fields[3]
    }).ToList();
}

static List<Tanacsado> BeolvasTanacsadok(string filePath)
{
    var lines = File.ReadAllLines(filePath).Skip(1);
    return lines.Select(line => line.Split(';')).Select(fields => new Tanacsado
    {
        TanacsadoId = int.Parse(fields[0]),
        Nev = fields[1],
        SzakteruletId = int.Parse(fields[2]),
        Oradij = int.Parse(fields[3]),
        Telefon = fields[4],
        Email = fields[5]
    }).ToList();
}

static List<Talalkozo> BeolvasTalalkozok(string filePath)
{
    var lines = File.ReadAllLines(filePath).Skip(1);
    return lines.Select(line => line.Split(';')).Select(fields => new Talalkozo
    {
        TalalkozoId = int.Parse(fields[0]),
        TanacsadoId = int.Parse(fields[1]),
        UgyfelId = int.Parse(fields[2]),
        Datum = DateTime.ParseExact(fields[3], "yyyy.MM.dd", CultureInfo.InvariantCulture),
        Idopont = TimeSpan.Parse(fields[4]),
        Idotartam = int.Parse(fields[5])
    }).ToList();
}

static List<Szakterulet> BeolvasSzakteruletek(string filePath)
{
    var lines = File.ReadAllLines(filePath).Skip(1);
    return lines.Select(line => line.Split(';')).Select(fields => new Szakterulet
    {
        SzakteruletId = int.Parse(fields[0]),
        Megnevezes = fields[1]
    }).ToList();
}