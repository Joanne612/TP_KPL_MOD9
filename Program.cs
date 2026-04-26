using System;
using System.IO;
using System.Text.Json;

public class CovidConfig
{
    public string satuan_suhu { get; set; }
    public int batas_hari_deman { get; set; }
    public string pesan_ditolak { get; set; }
    public string pesan_diterima { get; set; }

    public void UbahSatuan()
    {
        if (satuan_suhu.ToLower() == "celcius")
        {
            satuan_suhu = "fahrenheit";
        }
        else
        {
            satuan_suhu = "celcius";
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        string jsonString = File.ReadAllText("covid_config.json");
        CovidConfig config = JsonSerializer.Deserialize<CovidConfig>(jsonString);

        config.UbahSatuan();

        Console.Write($"Berapa suhu badan anda saat ini? Dalam nilai {config.satuan_suhu}: ");
        double suhu = Convert.ToDouble(Console.ReadLine());

        Console.Write("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam? ");
        int hari = Convert.ToInt32(Console.ReadLine());

        bool suhuValid = false;

        if (config.satuan_suhu.ToLower() == "celcius")
        {
            suhuValid = (suhu >= 36.5 && suhu <= 37.5);
        }
        else if (config.satuan_suhu.ToLower() == "fahrenheit")
        {
            suhuValid = (suhu >= 97.7 && suhu <= 99.5);
        }
     
        bool hariValid = (hari < config.batas_hari_deman);

        if (suhuValid && hariValid)
        {
            Console.WriteLine(config.pesan_diterima);
        }
        else
        {
            Console.WriteLine(config.pesan_ditolak);
        }
    }
}