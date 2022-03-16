using RandomClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Models.Logic
{
    public class BusinessLogic
    {
        RandomGenerator randomClass = new RandomGenerator();

        /// <summary>
        /// METHOD USED TO RANDOMLY GENERATE A BUSINESS ID
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GenerateID(string name)
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
                    System.Diagnostics.Debug.WriteLine("GenerateID() ERROR MESSAGE: " + ex);
                }

                //output new ID
                id = sub + rnd.ToString();
                System.Diagnostics.Debug.WriteLine("BUS ID: " + id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GenerateID() ERROR MESSAGE: " + ex);
            }

            return id;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        /*--------------------------------------------------------------------------------------00oo END OF FILE oo00-------------------------------------------------------------------------------------------------*///

    }
}
