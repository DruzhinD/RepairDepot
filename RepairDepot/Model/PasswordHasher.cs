using System.Security.Cryptography;

namespace RepairDepot.Model
{
    /// <summary>
    /// Класс, работающий с хешами паролей
    /// </summary>
    public static class PasswordHasher
    {
        const int saltSize = 16;
        const int keySize = 32;
        const int iterations = 10000;
        static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        const char saltDelimeter = ';';

        /// <summary>
        /// Хеширование пароля
        /// </summary>
        /// <returns>Последовательность, состоящая из соли и хешированного пароля, разделенного ';'</returns>
        public static string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, _hashAlgorithmName, keySize);
            return string.Join(saltDelimeter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        /// <summary>
        /// Сравнение хеша существующего пароля и введенного пароля
        /// </summary>
        public static bool Validate(string passwordHash, string password)
        {
            string[] pwdElements = passwordHash.Split(saltDelimeter);
            byte[] salt = Convert.FromBase64String(pwdElements[0]);
            byte[] hash = Convert.FromBase64String(pwdElements[1]);
            byte[] hashInput = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, _hashAlgorithmName, keySize);
            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}
