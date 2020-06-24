using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Encryption
{
    public class Encryptor
    {
        private byte[] _salt;
        private AesManaged _algorithm = new AesManaged();

        public Encryptor(string salt)
        {
            _salt = Encoding.Unicode.GetBytes(salt);
        }

        public byte[] Encrypt(string stringToEncrypt, string password)
        {
            return Encrypt(Encoding.Unicode.GetBytes(stringToEncrypt), password);
        }


        public byte[] Encrypt(byte[] byteToEncrypt, string password)
        {
            var passwordHash = GenerateHash(password);
            var key = GenerateKey(passwordHash);
            var iv = GenerateIV(passwordHash);

            var encryptor = _algorithm.CreateEncryptor(key, iv);
            return Transform(encryptor, byteToEncrypt);
        }

        public byte[] Decrypt(byte[] byteToDecrypt, string password)
        {
            var passwordHash = GenerateHash(password);
            var key = GenerateKey(passwordHash);
            var iv = GenerateIV(passwordHash);

            var decryptor = _algorithm.CreateDecryptor(key, iv);
            return Transform(decryptor, byteToDecrypt);
        }

        private byte[] Transform(ICryptoTransform encryptor, byte[] byteToEncrypt)
        {
            using (var buffer = new MemoryStream())
            using (var cryptoStream = new CryptoStream(buffer, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(byteToEncrypt, 0, byteToEncrypt.Length);
                cryptoStream.FlushFinalBlock();

                return buffer.ToArray();
            }
        }

        private byte[] GenerateIV(Rfc2898DeriveBytes passwordHash)
        {
            return passwordHash.GetBytes(_algorithm.BlockSize / 8);
        }

        private byte[] GenerateKey(Rfc2898DeriveBytes passwordHash)
        {
            return passwordHash.GetBytes(_algorithm.KeySize / 8);
        }

        private Rfc2898DeriveBytes GenerateHash(string password)
        {
            return new Rfc2898DeriveBytes(password, _salt);
        }
    }
}
