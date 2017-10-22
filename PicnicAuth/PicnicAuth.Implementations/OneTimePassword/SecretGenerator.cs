using PicnicAuth.Interfaces.Cryptography.Randomness;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class SecretGenerator : ISecretGenerator, IRequestDependency
    {
        private const int PreferredNumberOfBytes = 10;
        private readonly ISecureRandomNumberGenerator randomNumberGenerator;

        public SecretGenerator(ISecureRandomNumberGenerator randomNumberGenerator)
        {
            this.randomNumberGenerator = randomNumberGenerator;
        }

        public byte[] GenerateSecret()
        {
            byte[] randomBytes = randomNumberGenerator.GenerateRandomBytes(PreferredNumberOfBytes);
            return randomBytes;
        }
    }
}