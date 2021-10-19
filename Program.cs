using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SecurityDriven.Core;

int numberOfWords = TryReadIntArg(args, 0) ?? 6;
int numberOfPhrases = TryReadIntArg(args, 1) ?? 1;
var wordList = LoadWordList();

foreach (var phraseNum in Enumerable.Range(1, numberOfPhrases))
{
    var passphrase =
        string.Join(' ',
            Enumerable.Range(0, numberOfWords)
                .Select(_ => wordList[CryptoRandom.Shared.Next(wordList.Count)]));

    Console.Out.WriteLine(passphrase);
}

static IReadOnlyList<string> LoadWordList() =>
    File
        .ReadLines("diceware-sv.txt")
        .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
        .Select(line => line.Split(' ')[1])
        .ToList();

static int? TryReadIntArg(string[] args, int index) =>
    args.Length > index &&
    int.TryParse(args[index], out var parsed)
    ? parsed
    : null;