using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SecurityDriven.Core;

int numberOfWords = (args.Length > 0 && int.TryParse(args[0], out var parsed)) ? parsed : 6;
var wordList = LoadWordList();

var passphrase =
    string.Join(' ',
        Enumerable.Range(0, numberOfWords)
            .Select(_ => wordList[CryptoRandom.Shared.Next(wordList.Count)]));

Console.Out.WriteLine(passphrase);

static IReadOnlyList<string> LoadWordList() =>
    File
        .ReadLines("diceware-sv.txt")
        .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
        .Select(line => line.Split(' ')[1])
        .ToList();
