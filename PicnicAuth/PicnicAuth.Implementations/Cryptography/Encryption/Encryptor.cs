using System;
using System.Security.Cryptography;
using PicnicAuth.Interfaces.Cryptography.Encryption;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Implementations.Cryptography.Encryption
{
    public class Encryptor : RijndaelBasedEncryption, IEncryptor, IRequestDependency
    {
        private readonly IRijndaelManagedCreator rijndaelManagedCreator;
        private readonly IKeyDerivation keyDerivation;
        private readonly ICryptoTransformApplier cryptoTransformApplier;

        public Encryptor(IRijndaelManagedCreator rijndaelManagedCreator, 
            IKeyDerivation keyDerivation, ICryptoTransformApplier cryptoTransformApplier)
        {
            this.rijndaelManagedCreator = rijndaelManagedCreator;
            this.keyDerivation = keyDerivation;
            this.cryptoTransformApplier = cryptoTransformApplier;
        }

        public byte[] Encrypt(byte[] bytesToEncrypt, byte[] passPhrase, byte[] salt, byte[] iv)
        {
            if (bytesToEncrypt == null || passPhrase == null || salt == null || iv == null)
                throw new ArgumentNullException();

            RijndaelManaged rijndael = null;
            ICryptoTransform rijndaelEncryptor = null;
            try
            {
                byte[] keyBytes = keyDerivation.GetDerivedBytes(passPhrase, salt, DerivationIterations, KeySizeInBytes);

                rijndael = rijndaelManagedCreator.CreateRijndaelManaged();
                rijndaelEncryptor = rijndael.CreateEncryptor(keyBytes, iv);

                byte[] encryptedBytes = cryptoTransformApplier
                    .ApplyCryptoTransform(bytesToEncrypt, rijndaelEncryptor, EncryptorStreamMode);

                return encryptedBytes;
            }
            finally
            {
                rijndael?.Dispose();
                rijndaelEncryptor?.Dispose();
            }
        }
    }
}