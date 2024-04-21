namespace Encryption
{
    internal class Caesar(string sourcePath, string encryptedPath, string decryptedPath)
    {
        public void Encrypt(int offset)
        {
            using var reader = new StreamReader(sourcePath);
            using var writter = new StreamWriter(encryptedPath);

            while (!reader.EndOfStream)
            {
                char c = (char)reader.Read();
                c = (char)(c + offset);
                writter.Write(c);
            }
        }

        public void Decrypt(int offset)
        {
            using var reader = new StreamReader(encryptedPath);
            using var writter = new StreamWriter(decryptedPath);

            while (!reader.EndOfStream)
            {
                char c = (char)reader.Read();
                c = (char)(c - offset);
                writter.Write(c);
            }
        }
    }
}
