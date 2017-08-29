using System;
using System.Security.Cryptography;
using PicnicAuth.Interfaces.Cryptography.Encryption;

namespace PicnicAuth.Implementations.Cryptography.Encryption
{
    public class Decryptor : RijndaelBasedEncryption, IDecryptor
    {
        private readonly IRijndaelManagedCreator rijndaelManagedCreator;
        private readonly IKeyDerivation keyDerivation;
        private readonly ICryptoTransformApplier cryptoTransformApplier;

        public Decryptor(IRijndaelManagedCreator rijndaelManagedCreator, 
            IKeyDerivation keyDerivation, ICryptoTransformApplier cryptoTransformApplier)
        {
            this.rijndaelManagedCreator = rijndaelManagedCreator;
            this.keyDerivation = keyDerivation;
            this.cryptoTransformApplier = cryptoTransformApplier;
        }

        public byte[] Decrypt(byte[] bytesToDecrypt, byte[] passPhrase, byte[] salt, byte[] iv)
        {
            if (bytesToDecrypt == null || passPhrase == null || salt == null || iv == null)
                throw new ArgumentNullException();

            RijndaelManaged rijndael = null;
            ICryptoTransform rijndaelEncryptor = null;
            try
            {
                byte[] keyBytes = keyDerivation.GetDerivedBytes(passPhrase, salt, DerivationIterations, KeySizeInBytes);

                rijndael = rijndaelManagedCreator.CreateRijndaelManaged();
                rijndaelEncryptor = rijndael.CreateDecryptor(keyBytes, iv);

                byte[] decryptedBytes = cryptoTransformApplier
                    .ApplyCryptoTransform(bytesToDecrypt, rijndaelEncryptor, EncryptorStreamMode);

                return decryptedBytes;
            }
            finally
            {
                rijndael?.Dispose();
                rijndaelEncryptor?.Dispose();
            }
        }
    }
}