//Programming using .NET advanced course
//Code Example : Food.cs
//Farid Naisan Feb 2012

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodManagerApp.Foods;

namespace FoodManagerApp.Foods
{

    /// <summary>
    /// This class handles all about food, ingredience, etc.  It detects whether a 
    /// certain type of a food is suitable for a category of animals.
    /// This class is abstract and must be inherited
    /// </summary>
    /// <remarks></remarks>
    public abstract class Food : IFood
    {
        //name of a food item
        private string m_name;
        
        //List of ingredients
        private List<string> m_ingredients;
        
        //Category according to the enum FoodCategory
        private FoodCategory m_category;

        //default construktor
        public Food()
        { }

        //Constructor with two parameters
        public Food(string foodName, List<string> ingredientList)
        {
            m_name = foodName;
            if ((ingredientList != null))
            {
                m_ingredients = ingredientList;
            }
         }

        /// <summary>
        /// Name of the food.
        /// </summary>
        /// <value>Name of the food to be set.</value>
        /// <returns>Return the name of the food.</returns>
        /// <remarks></remarks>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        //Property related to m_ingredients
        public List<string> Ingredients
        {
            //To do: The get should send a copy of 
            //m_ingredients the ref
            get { return m_ingredients; }
            set { m_ingredients = value; }
        }

        //Property related to m_Category
        public FoodCategory Category
        {
            get { return m_category; }
            set { m_category = value; }
        }


        //Each food object must define whether the object is good for a eater type
        //implementation of the method in the Interfaces is delegated in turn to sub-classes
        public abstract bool IsGoodFor(EaterType eType);

        ///<summary>
        /// Check if the a certain ingredient is contained in the food object. This method simply 
        ///compares the names of the objects
        /// </summary>
        /// <param name="items">A string list defining the ingriedients to check.</param>
        /// <returns>True if the string matches any ingredient of the food item, 
        /// and false otherwise</returns>
        /// <remarks></remarks>
        public bool IngredientsContainsItem(params string[] items)
        {
            foreach (string ingredObj in m_ingredients)
            {
                foreach (string item in items)
                {
                    if (string.Compare(ingredObj.ToLower(), item.ToLower()) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// The function receives a string containing delimeters like ',', ';'.
        /// The string is then split into a number of sub-strings
        /// </summary>
        /// <param name="textToList">string to be broken into substrings</param>
        /// <param name="delimeters">a list of chars</param>
        /// <returns>A generic list containg the substrings.</returns>
        public List<string> GetIngredientsFromAStringList(string textToList, char[] delimeters)
        {
            List<string> stringList = new List<string>();
            string[] strItem = textToList.Split(delimeters);

            stringList.AddRange(strItem);
            return stringList;
        }

        /// <summary>
        /// Override ToString function - let it return a string made up of
        /// the name and ingredients
        /// </summary>
        /// <returns>The object in printable format</returns>
        public override string ToString()
        {
            StringBuilder textOut = new StringBuilder();
            textOut.Append(this.Name).Append(" (");

            foreach (string s in this.Ingredients)
            {
                textOut.Append(s).Append(", ");
            }

            textOut.Append(")");
            //remove the last comma 
            return textOut.ToString().Remove(textOut.ToString().LastIndexOf(","), 1);
        }

    }
}
