using System.Security.Cryptography;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s1 = "12345";
            string s2 = "12345";
            PasswordHasher hasher = new PasswordHasher();
            (string hash, string salt) = hasher.HashAndSalt(s1);
            bool result = hasher.Validate(hash, salt, s2);
            return;

        }

        protected const int saltLength = 16;
        protected const int hashLength = 32;
        protected const int iterationsAmount = 10000;
        /// <summary>
        /// генерация соли
        /// </summary>
        byte[] MyHashPassword(string password)
        {
            //генерация соли
            byte[] salt = RandomNumberGenerator.GetBytes(saltLength);

            //хеширование пароля
            byte[] hashedPassword = Rfc2898DeriveBytes.Pbkdf2(
                password, salt, iterationsAmount, HashAlgorithmName.SHA512, hashLength + saltLength);

            return hashedPassword;
        }

        /// <summary>
        /// Разделение массива хэш-соль пароля
        /// </summary>
        /// <returns>массив хэша, массив соли</returns>
        (byte[], byte[]) SplitHashAndSalt(byte[] encryptedData)
        {
            byte[] hash = new byte[hashLength];
            Array.Copy(encryptedData, 0, hash, 0, hashLength);
            byte[] salt = new byte[saltLength];
            Array.Copy(encryptedData, hashLength, salt, 0, saltLength);
            return (hash, salt);
        }

        string ByteHashToString(byte[] hash) => Convert.ToBase64String(hash);
        byte[] StringToByteHash(string hash) => Convert.FromBase64String(hash);
        bool CompareHash(byte[] hash1, byte[] hash2) => hash1.SequenceEqual(hash2);
    }
    public class PasswordHasher
    {
        const int saltSize = 16;
        const int keySize = 32;
        const int iterations = 10_000;
        static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;

        public string GenerateSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            return Convert.ToBase64String(salt);
        }

        public (string, string) HashAndSalt(string password)
        {
            //соль
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            //хэшируем пароль со сгенерированной солью
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, _hashAlgorithmName, keySize);
            return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
        }

        public bool Validate(string passwordHash, string salt, string inputPassword)
        {
            //преобразуем все строки в последовательность байтов
            byte[] saltByte = Convert.FromBase64String(salt);
            byte[] PasswordHashByte = Convert.FromBase64String(passwordHash);
            //шифруем полученный пароль
            byte[] inputPasswordHashByte = Rfc2898DeriveBytes.Pbkdf2(inputPassword, saltByte, iterations, _hashAlgorithmName, keySize);
            //сравниваем полученный пароль с хэшем существующего пароля
            return CryptographicOperations.FixedTimeEquals(inputPasswordHashByte, PasswordHashByte);
        }
    }
}
