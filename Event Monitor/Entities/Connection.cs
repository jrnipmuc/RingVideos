using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace Event_Monitor.Entities
{
    [Table("Connections")]
    public class Connection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(500)]
        public string Password { get; set; }
        
        [MaxLength(1000)]
        public string RefreshToken { get; set; }

        [MaxLength(100)]
        public string EncryptionIV { get; set; }
        
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// When the current refresh token was obtained
        /// </summary>
        public DateTime? TokenObtainedDate { get; set; }
        
        /// <summary>
        /// When the current refresh token will expire
        /// </summary>
        public DateTime? TokenExpirationDate { get; set; }
        
        /// <summary>
        /// Last time this connection was successfully used to authenticate
        /// </summary>
        public DateTime? LastAuthenticatedDate { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string ClearTextPassword { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string ClearTextRefreshToken { get; set; }

        [NotMapped]
        public string DisplayName { get; set; }

        /// <summary>
        /// Returns true if the connection has a valid, non-expired refresh token
        /// </summary>
        [NotMapped]
        public bool HasValidToken => !string.IsNullOrWhiteSpace(RefreshToken) && 
                                      TokenExpirationDate.HasValue && 
                                      TokenExpirationDate.Value > DateTime.Now;

        /// <summary>
        /// Returns a user-friendly status message about the token
        /// </summary>
        [NotMapped]
        public string TokenStatus
        {
            get
            {
                if (string.IsNullOrWhiteSpace(RefreshToken))
                    return "No Token";
                
                if (!TokenExpirationDate.HasValue)
                    return "Token (Unknown Expiry)";
                
                if (TokenExpirationDate.Value < DateTime.Now)
                    return "Token Expired";
                
                var timeRemaining = TokenExpirationDate.Value - DateTime.Now;
                if (timeRemaining.TotalDays > 1)
                    return $"Valid ({(int)timeRemaining.TotalDays} days)";
                else if (timeRemaining.TotalHours > 1)
                    return $"Valid ({(int)timeRemaining.TotalHours} hours)";
                else
                    return $"Valid ({(int)timeRemaining.TotalMinutes} minutes)";
            }
        }

        private string Key => Environment.MachineName + Environment.UserName + "453nfawehfaypg94#$#@%34wghvoawe[cwe45a3wtg";
        
        private const string Salt = "$2a$04$qdxi1jNcjqWBlsviWGilx.Xxw0oMm0gZYx8ZsLq5ntsy5s4GFq3kq";

        public Connection Encrypt()
        {
            try
            {
                using (Aes myAes = Aes.Create())
                {
#pragma warning disable SYSLIB0041 // Type or member is obsolete
                    var derived = new Rfc2898DeriveBytes(Encoding.ASCII.GetBytes(Key), Encoding.ASCII.GetBytes(Salt), 100);
#pragma warning restore SYSLIB0041 // Type or member is obsolete
                    myAes.GenerateIV();
                    myAes.Key = derived.GetBytes(32);
                    EncryptionIV = Convert.ToBase64String(myAes.IV);

                    ICryptoTransform encryptor = myAes.CreateEncryptor(myAes.Key, myAes.IV);

                    if (!string.IsNullOrWhiteSpace(ClearTextPassword))
                    {
                        var clearTextBytes = Encoding.ASCII.GetBytes(ClearTextPassword);
                        using (MemoryStream msEncrypt = new MemoryStream())
                        {
                            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            {
                                csEncrypt.Write(clearTextBytes, 0, clearTextBytes.Length);
                                csEncrypt.Close();
                                var encrBytes = msEncrypt.ToArray();
                                Password = Convert.ToBase64String(encrBytes);
                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ClearTextRefreshToken))
                    {
                        var clearTextBytes = Encoding.ASCII.GetBytes(ClearTextRefreshToken);
                        using (MemoryStream msEncrypt = new MemoryStream())
                        {
                            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            {
                                csEncrypt.Write(clearTextBytes, 0, clearTextBytes.Length);
                                csEncrypt.Close();
                                var encrBytes = msEncrypt.ToArray();
                                RefreshToken = Convert.ToBase64String(encrBytes);
                            }
                        }
                    }
                }
                return this;
            }
            catch (Exception)
            {
                return this;
            }
        }

        public Connection Decrypt()
        {
            try
            {
                using (Aes myAes = Aes.Create())
                {
                    if (string.IsNullOrWhiteSpace(EncryptionIV))
                    {
                        myAes.GenerateIV();
                        EncryptionIV = Convert.ToBase64String(myAes.IV);
                    }
                    else
                    {
                        myAes.IV = Convert.FromBase64String(EncryptionIV);
                    }

#pragma warning disable SYSLIB0041 // Type or member is obsolete
                    var derived = new Rfc2898DeriveBytes(Encoding.ASCII.GetBytes(Key), Encoding.ASCII.GetBytes(Salt), 100);
#pragma warning restore SYSLIB0041 // Type or member is obsolete
                    myAes.Key = derived.GetBytes(32);

                    ICryptoTransform decryptor = myAes.CreateDecryptor(myAes.Key, myAes.IV);

                    if (!string.IsNullOrWhiteSpace(Password))
                    {
                        var passwordBytes = Convert.FromBase64String(Password);
                        using (MemoryStream msDecrypt = new MemoryStream())
                        {
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                            {
                                csDecrypt.Write(passwordBytes, 0, passwordBytes.Length);
                                csDecrypt.Close();
                                var clearBytes = msDecrypt.ToArray();
                                ClearTextPassword = Encoding.UTF8.GetString(clearBytes);
                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(RefreshToken))
                    {
                        var refreshBytes = Convert.FromBase64String(RefreshToken);
                        using (MemoryStream msDecrypt = new MemoryStream())
                        {
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                            {
                                csDecrypt.Write(refreshBytes, 0, refreshBytes.Length);
                                csDecrypt.Close();
                                var clearBytes = msDecrypt.ToArray();
                                ClearTextRefreshToken = Encoding.UTF8.GetString(clearBytes);
                            }
                        }
                    }
                }
                return this;
            }
            catch (Exception)
            {
                ClearTextPassword = Password;
                return this;
            }
        }
    }
}