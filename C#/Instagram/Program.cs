using System.Security.Cryptography;
using Sodium;

string generateEncPassword(string password, string publicKey, string keyId, string version)
{
    var time = DateTime.UtcNow.ToTimestamp();
    var keyBytes = publicKey.HexToBytes();
    var key = new byte[32];
    new Random().NextBytes(key);
    var iv = new byte[12];
    var tag = new byte[16];
    var plainText = password.ToBytes();
    var cipherText = new byte[plainText.Length];

    using (var cipher = new AesGcm(key))
    {
        cipher.Encrypt(nonce: iv, plaintext: plainText, ciphertext: cipherText, tag: tag, associatedData: time.ToString().ToBytes());
    }

    var encryptedKey = SealedPublicKeyBox.Create(key, keyBytes);

    var bytesOfLen = BitConverter.GetBytes((short)encryptedKey.Length);
    var info = new byte[] { 1, byte.Parse(keyId) };
    var bytes = info.Concat(bytesOfLen).Concat(encryptedKey).Concat(tag).Concat(cipherText);

    return $"#PWD_INSTAGRAM_BROWSER:{version}:{time}:{bytes.ToBase64()}";
}

Console.Title = "Instagram Web Encryption";

Console.Write("Password : ");
string password = Console.ReadLine();
Console.Write("Key : ");
string key = Console.ReadLine();
Console.Write("ID : ");
string id = Console.ReadLine();
Console.Write("Version : ");
string version = Console.ReadLine();

Console.WriteLine();

if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(version))
    Console.WriteLine(generateEncPassword(password, key, id, version));
else
    Console.WriteLine("Error!");

Console.ReadKey();