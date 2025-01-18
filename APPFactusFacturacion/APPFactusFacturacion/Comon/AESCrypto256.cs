using System.Security.Cryptography;
using System.Text;

namespace APPFactusFacturacion.Comon
{
    public class AESCrypto256
    {
        private readonly string Key; // Leer clave exactos para AES-256)

        public AESCrypto256()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

            Key = configuration["KeyValues:Key256"];
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return null;

            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 128;
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return null;

            try
            {
                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);

                using (Aes aes = Aes.Create())
                {
                    aes.KeySize = 128;
                    aes.Key = Encoding.UTF8.GetBytes(Key);
                    aes.IV = iv;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (FormatException)
            {
                throw new FormatException("The cipherText is not in a valid Base64 format.");
            }
            catch (CryptographicException)
            {
                throw new CryptographicException("Decryption failed. Please check the encryption key and cipherText.");
            }
        }
    }
}
