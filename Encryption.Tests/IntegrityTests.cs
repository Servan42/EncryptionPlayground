using Encryption;
using System.Text;

namespace Encryption.Tests
{
    public class IntegrityTests
    {
        public const string SOURCE_PATH = "source.txt";
        public const string ENCRYPTED_PATH = "encryped.txt";
        public const string DECRYPTED_PATH = "decrypted.txt";

        [SetUp]
        public void Setup()
        {
            File.WriteAllText(SOURCE_PATH, GenerateEveryAsciiString());
            File.WriteAllText(ENCRYPTED_PATH, "");
            File.WriteAllText(DECRYPTED_PATH, "");
        }

        [Test]
        public void Caesar_Integrity()
        {
            var ceasar = new Caesar(SOURCE_PATH, ENCRYPTED_PATH, DECRYPTED_PATH);
            ceasar.Encrypt(1);
            ceasar.Decrypt(1);
            EnsureIntegrity();
        }

        [Test]
        public void AES_Integrity()
        {
            var aes = new MyAES(SOURCE_PATH, ENCRYPTED_PATH, DECRYPTED_PATH);
            aes.Encrypt("pass");
            aes.Decrypt("pass");
            EnsureIntegrity();
        }

        [Test]
        public async Task AES_Integrity_Async()
        {
            var aes = new MyAES(SOURCE_PATH, ENCRYPTED_PATH, DECRYPTED_PATH);
            await aes.EncryptAsync("pass");
            await aes.DecryptAsync("pass");
            EnsureIntegrity();
        }

        #region Helpers
        private string GenerateEveryAsciiString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 256; i++)
            {
                sb.Append((char)i);
            }
            return sb.ToString();
        }

        private bool EnsureIntegrity()
        {
            var input = File.ReadAllBytes(SOURCE_PATH);
            var output = File.ReadAllBytes(DECRYPTED_PATH);
            if (input.Length != output.Length) return false;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != output[i])
                    return false;
            }
            return true;
        } 
        #endregion
    }
}