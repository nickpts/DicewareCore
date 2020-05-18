# DicewareCore
![Alt_text](https://live.staticflickr.com/65535/49910157738_8c8643d0cb_q_d.jpg)

A .NET Standard library implementing the [Diceware](https://theworld.com/~reinhold/diceware.html) passphrase generation scheme. 

![Docker Cloud Build Status](https://img.shields.io/docker/cloud/build/nickpatsaris/diceware.svg)
![Nuget](https://img.shields.io/nuget/v/Diceware.svg)

## Description
1. Languages supported: 
Basque, Catalan, Czech, Danish, English, Latin, Dutch, Esperando, Estonian, Finnish, French, German, Hungarian, Italian, Japanese, Chinese (Pinyin), Russian, Spanish, Swedish, Turkish.
2. By default rolls are made using .NET's RNGCryptoServiceProvider
3. All dictionaries are taken from the official Diceware page. 

## Example
```C#
using var dice = new Diceware();

var pass = dice.Create(wordno: 5, language: Language.English, separator: '-');

// pate-there-amok-mice-best
```