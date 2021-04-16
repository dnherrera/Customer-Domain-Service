using System;

namespace CustomerAPI.Helpers
{
    /// <summary>
    /// Calculate Age Based on Birthdate
    /// </summary>
    public static class CalculateAge
    {
        /// <summary>
        /// Calculate Method
        /// </summary>
        /// <param name="theDateTime"></param>
        /// <returns></returns>
        public static int Calculate(DateTime? theDateTime)
        {
            int age = DateTime.Today.Year - theDateTime.Value.Year;

            if (theDateTime.Value.AddYears(age) > DateTime.Today)
                age--;

            return age;
        }
    }
}
