namespace APPFactusFacturacion.Services.Interfaces
{
    public interface IAESCrypto256
    {
        public string Encrypt(string plainText);
        public string Decrypt(string cipherText);
    }
}
