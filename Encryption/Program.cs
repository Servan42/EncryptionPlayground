// See https://aka.ms/new-console-template for more information
using Encryption;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // TODO https://code-maze.com/csharp-string-encryption-decryption/

        var sw = Stopwatch.StartNew();

        //var caesar = new Caesar("input.txt", "cyph.txt", "decyph.txt");
        //caesar.Encrypt(-10);
        //caesar.Decrypt(-10);

        var aes = new MyAES("input.txt", "cyph.txt", "decyph.txt");
        await aes.EncryptAsync("pass");
        await aes.DecryptAsync("pass");

        sw.Stop();
        Console.WriteLine(sw.Elapsed);
        WriteSize("input.txt");
        WriteSize("cyph.txt");
        Console.WriteLine(EnsureIntegrity("input.txt", "decyph.txt"));
    }

    private static bool EnsureIntegrity(string inputFile, string outputFile)
    {
        var inputHash = SHA256.HashData(File.ReadAllBytes(inputFile));
        var outputHash = SHA256.HashData(File.ReadAllBytes(outputFile));
        if(inputHash.Length != outputHash.Length) return false;
        for (int i = 0; i < inputHash.Length; i++)
        {
            if (inputHash[i] != outputHash[i])
                return false;
        }
        return true;
    }

    private static void WriteSize(string path)
    {
        long byteCount = new FileInfo(path).Length;

        if (byteCount >= 1024 * 1024)
        {
            double mbCount = (double)byteCount / (1024 * 1024);
            Console.WriteLine($"Size: {mbCount:F2} MB ({path})");
        }
        else if (byteCount >= 1024)
        {
            double kbCount = (double)byteCount / 1024;
            Console.WriteLine($"Size: {kbCount:F2} KB ({path})");
        }
        else
        {
            Console.WriteLine($"Size: {byteCount} bytes ({path})");
        }
    }
}
