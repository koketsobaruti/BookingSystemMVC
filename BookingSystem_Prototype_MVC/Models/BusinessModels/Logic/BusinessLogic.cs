using PasswordClassLibrary;
using RandomClassLibrary;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Models.BusinessModels.Logic
{
    public class BusinessLogic
    {
        //class library references
        RandomGenerator randomClass = new RandomGenerator();
        PasswordManager passwordManager = new PasswordManager();

        /// <summary>
        /// METHOD USED TO RANDOMLY GENERATE A BUSINESS ID
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GenerateBusinessID(string name)
        {
            //intitalize the value of the ID
            var id = "";
            var rnd = 0;
            try
            {
                //get the first three letters
                var sub = name.Substring(0, 2);

                //try generate a random 5 digit number
                try
                {
                    rnd = randomClass.RandomNumberGenerator(10000, 99999);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("GenerateBusinessID() ERROR MESSAGE: " + ex);
                }

                //output new ID
                id = sub + rnd.ToString();
                System.Diagnostics.Debug.WriteLine("BUS ID: " + id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GenerateBusinessID() ERROR MESSAGE: " + ex);
            }

            return id;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// METHOD USED TO GENERATE A HASHED PASSWORD
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string GenerateHash(string password)
        {
            var hashed = "";
            try
            {
                hashed = HashPassword(password);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GenerateHash() ERROR MESSAGE: " + ex);
            }

            return hashed;
        }

        public string HashPassword(string password)
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        /*--------------------------------------------------------------------------------------00oo END OF FILE oo00-------------------------------------------------------------------------------------------------*///

    }
}
