using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Encryption
{
    class AsymmetricEncryptor
    {
        private X509Certificate2 GetCertificate(string certName)
        {
            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                foreach (var cert in store.Certificates)
                {
                    if (cert.SubjectName.Name == certName)
                        return cert;
                }
                return null;
            }
        }

        public byte[] Encrypt(byte[] bytesToEncrypt, string certName)
        {
            var cert = GetCertificate(certName);

            var provider = cert.PublicKey.Key;
            using (var algorithm = new AesManaged())
            using (var outStream = new MemoryStream())
            using (var encryptor = algorithm.CreateEncryptor())
            {
                var keyFormatter = new RSAPKCS1KeyExchangeFormatter(provider);
                var encryptedKey = keyFormatter.CreateKeyExchange(algorithm.Key, algorithm.GetType());

                var keyLength = BitConverter.GetBytes(encryptedKey.Length);
                var ivLength = BitConverter.GetBytes(algorithm.IV.Length);

                outStream.Write(keyLength, 0, 4);
                outStream.Write(ivLength, 0, 4);
                outStream.Write(encryptedKey, 0, encryptedKey.Length);
                outStream.Write(algorithm.IV, 0, algorithm.IV.Length);

                using (var encrypt = new CryptoStream(outStream, encryptor, CryptoStreamMode.Write))
                {
                    encrypt.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
                    encrypt.FlushFinalBlock();

                    return outStream.ToArray();
                }
            }

        }

        public byte[] Decrypt(byte[] bytesToDecrypt, string certName)
        {
            var cert = GetCertificate(certName);

            var provider = (RSACryptoServiceProvider)cert.PrivateKey;
            using (var algorithm = new AesManaged())
            using (var inStream = new MemoryStream(bytesToDecrypt))
            {
                var keyLength = new byte[4];
                var ivLength = new byte[4];
                inStream.Seek(0, SeekOrigin.Begin);
                inStream.Read(keyLength, 0, keyLength.Length);
                inStream.Read(ivLength, 0, ivLength.Length);

                var keyLengthInt = BitConverter.ToInt32(keyLength, 0);
                var ivLengthInt = BitConverter.ToInt32(ivLength, 0);

                var dataStartPosition = keyLengthInt + ivLengthInt + keyLength.Length + ivLength.Length;
                var dataLength = (int)inStream.Length - dataStartPosition;

                var key = new byte[keyLengthInt];
                var iv = new byte[ivLengthInt];
                var data = new byte[dataLength];

                inStream.Read(key, 0, keyLengthInt);
                inStream.Read(iv, 0, ivLengthInt);
                inStream.Read(data, 0, dataLength);

                var decryptedKey = provider.Decrypt(key, false);

                using (var outStream = new MemoryStream())
                using (var decryptor = algorithm.CreateDecryptor(decryptedKey, iv))
                using (var decrypt = new CryptoStream(outStream, decryptor, CryptoStreamMode.Write))
                {
                    decrypt.Write(data, 0, data.Length);
                    decrypt.FlushFinalBlock();

                    return outStream.ToArray();
                }
            }

        }
    }
}
