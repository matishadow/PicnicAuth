using PicnicAuth.Interfaces.Cryptography.Randomness;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.ServiceInterfaces.Dependencies;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class SecretGenerator : ISecretGenerator, IRequestDependency
    {
        private const int PreferredNumberOfBytes = 10;
        private readonly ISecureRandomNumberGenerator randomNumberGenerator;
        private readonly IBase32Encoder base32Encoder;

        public SecretGenerator(ISecureRandomNumberGenerator randomNumberGenerator, IBase32Encoder base32Encoder)
        {
            this.randomNumberGenerator = randomNumberGenerator;
            this.base32Encoder = base32Encoder;
        }

        public string GenerateSecret()
        {
            byte[] randomBytes = randomNumberGenerator.GenerateRandomBytes(PreferredNumberOfBytes);
            return base32Encoder.Encode(randomBytes);
        }
    }
}