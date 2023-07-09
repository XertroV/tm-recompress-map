// See https://aka.ms/new-console-template for more information
using GBX.NET;
using GBX.NET.Engines.Game;
using GBX.NET.Engines.GameData;
using GBX.NET.LZO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("error: pass map file");
            // AwaitEnter();
            return;
        }

        // assuming .map.gbx
        var filenameNoExt = args[0].Remove(args[0].Length - 8);

        GBX.NET.Lzo.SetLzo(typeof(GBX.NET.LZO.MiniLZO));

        CGameCtnChallenge map;
        try {
            map = GameBox.ParseNode<CGameCtnChallenge>(args[0]);
        } catch (Exception e) {
            Console.WriteLine($"Error: could not parse map:\n{e.ToString()}");
            throw e;
        }

        Dictionary<string, byte[]> items = new Dictionary<string, byte[]>();

        foreach (var kp in map.EmbeddedData) {
            var key = kp.Key;
            var item = kp.Value;

            // var hdr = GameBox.ParseHeader(new MemoryStream(item));
            // var node = GameBox.ParseNode(new MemoryStream(item));
            // hdr.RawBody
            // var box = GameBox.Parse(new MemoryStream(item));
            var outStream = new MemoryStream();
            GameBox.Decompress(new MemoryStream(item), outStream);

            // box.ChangeBodyCompression(GameBoxCompression.Uncompressed);
            // box.Save(outStream);
            items[key] = outStream.GetBuffer();
        }

        foreach (var kp in items) {
            var origLen = map.EmbeddedData[kp.Key].Length;
            map.EmbeddedData[kp.Key] = kp.Value;
            var newLen = map.EmbeddedData[kp.Key].Length;
            Console.WriteLine($"Updated item: {kp.Key}; orig len: {origLen} B; new len: {newLen} B");
        }

        var origName = map.MapName;
        string outFile;

        map.MapName += "_Recompressed";
        outFile = filenameNoExt + "_Recompressed.Map.Gbx";
        map.Save(outFile);
        Console.WriteLine($"Done, map saved to {outFile}");

        // map.MapName = origName + "_Uncompressed";
        // outFile = filenameNoExt + "_Uncompressed.Map.Gbx";
        // var mapStream = new MemoryStream();
        // map.Save(mapStream);
        // mapStream.Seek(0, SeekOrigin.Begin);
        // var outFileStream = new FileStream(outFile, FileMode.OpenOrCreate, FileAccess.Write);
        // GameBox.Decompress(mapStream, outFileStream);
        // Console.WriteLine($"Done, map saved to {outFile}");
    }
}
