//Programming using .NET advanced course
//Code Example : IFood.cs
//Farid Naisan May 2011

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodManagerApp.Foods;

namespace FoodManagerApp.Foods
{
    ///<summary>
    /// Interface defining some specific methods to be implemented by 
    /// all subclasses
    /// </summary>
    /// <remarks></remarks>
    public interface IFood
    {
        string Name { get; set; }
        FoodCategory Category { get; set; }

        List<string> Ingredients { get; set; }

        //Each food type must define whether a food is good for 
        //a eater type
        bool IsGoodFor(EaterType eType);
    }
}
