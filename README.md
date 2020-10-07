# DicewareCore
![Alt_text](https://i.ibb.co/9YQpSXP/49910157738-eed914f57f-o.jpg)

A .NET Standard library implementing the [Diceware](https://theworld.com/~reinhold/diceware.html) passphrase generation scheme. 

![Docker Cloud Build Status](https://img.shields.io/docker/cloud/build/nickpatsaris/dicewarecore)
![Nuget](https://img.shields.io/nuget/v/DicewareCore)

## Description
1. Languages supported: 
Basque, Catalan, Czech, Danish, English, Latin, Dutch, Esperando, Estonian, Finnish, French, German, Hungarian, Italian, Japanese, Chinese (Pinyin), Russian, Spanish, Swedish, Turkish.
2. By default rolls are made using .NET's [RNGCryptoServiceProvider](https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.rngcryptoserviceprovider?view=netcore-3.1), but any class inheriting [RandomNumberGenerator](https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator?view=netcore-3.1) can be used. 
3. All dictionaries are taken from the official Diceware page. 

## Example
```C#
using var dice = new Diceware();

var pass = dice.Create(wordno: 5, language: Language.English, separator: '-');

// pate-there-amok-mice-best
```
