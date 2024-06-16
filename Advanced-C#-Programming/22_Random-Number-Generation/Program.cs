using System.Security.Cryptography;
Console.WriteLine();

/* Random Number Generation */
/*
Random random = new(100); //Seed value is optional

for (int i = 0; i < 10; i++)
    Console.WriteLine(random.Next()); //Generates random number between 0 and int.MaxValue and each execution will generate same numbers because of seed value
*/


//Alternative way to generate random numbers => CryptoServiceProvider (System.Security.Cryptography) and RandomNumberGenerator (System.Security.Cryptography)

//CryptoServiceProvider
RNGCryptoServiceProvider rng = new();
byte[] randomBytes = new byte[4];
rng.GetBytes(randomBytes);
int randomNumber = BitConverter.ToInt32(randomBytes, 0);
Console.WriteLine(randomNumber);

//RandomNumberGenerator 
using var rng2 = RandomNumberGenerator.Create();
byte[] randomBytes2 = new byte[4];
rng.GetBytes(randomBytes2);
int randomNumber2 = BitConverter.ToInt32(randomBytes2, 0);
Console.WriteLine(randomNumber2);


