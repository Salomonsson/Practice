using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodManagerApp.Foods
{
    public enum MeatTypes
    {
        Biff,
        Pork,
        Chicken,
        Deer,
        Other
    }


    public class Meat : Food
    {       
        //Constructor 
        public Meat()
        {
        }

        /// <summary>
        /// Determine if a certain eater type can eat this type of food
        /// </summary>
        /// <param name="eater">An EaterType enumeration element</param>
        /// <returns>True if this eater type can eat this food item, false otherwise</returns>
        public override bool IsGoodFor(EaterType eater)
        {
            return (eater == EaterType.Carnivora) || (eater == EaterType.Omnivorous);
        }


        public override string ToString()
        {
            return base.ToString();
        }

    }
}
