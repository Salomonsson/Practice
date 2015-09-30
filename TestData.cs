//Programming using .NET advanced course
//Code Example : TestData.cs
//Farid Naisan Feb 2012 

/// <summary>
/// This class is only for the purpose of testing some 
/// abstract methods.
/// </summary>
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodManagerApp.Foods;


namespace FoodManagerApp
{
    //Enum defining a number of test animals
    public enum Species
    {
        Bear,
        Cat,
        Panda,
        Dove,
        Pelican,
        Cow,
        Sheep,
        Lion
    }

    /// <summary>
    /// This struct contains simple data related to
    /// an animal.
    /// </summary>
    public struct TestData
    {
        private Species item;
        private EaterType eater;

        public TestData(Species item, EaterType eater)
        {
            this.item = item;
             this.eater = eater;
        }
        public EaterType Item
        { get { return eater; } }
    
    }

    /// <summary>
    /// A container class that mains a number of objects of TestaData.
    /// </summary>
    public static class TestDataManager
    {
        private static  List<TestData> testItems = new List<TestData>();

        //Set som test data in the registry
        public static  void AddTestData()
        {
            try
            {
                testItems.Add(new TestData(Species.Bear, EaterType.Omnivorous));  //All eater
                testItems.Add(new TestData(Species.Cat, EaterType.Omnivorous));   //All eater
                testItems.Add(new TestData(Species.Panda, EaterType.Herbivore));   //Plant eater
                testItems.Add(new TestData(Species.Cat, EaterType.Omnivorous));   //All eater
                testItems.Add(new TestData(Species.Dove, EaterType.Omnivorous));   //All eater
                testItems.Add(new TestData(Species.Cow, EaterType.Herbivore));   //All eater
                testItems.Add(new TestData(Species.Sheep, EaterType.Herbivore));
                testItems.Add(new TestData(Species.Lion, EaterType.Carnivora));
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }
        public static TestData GetItemAt(int index)
        {
            if (CheckIndex(index))
            {
                return testItems[index];
            }
            else
            {
                return new TestData();  //empty struct
            }
        }

        /// <summary>
        /// Check so index is with the allowed range of the colelction boundaries.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>True if index is valid, false otherwise</returns>
        /// <remarks>This function may be useful for client objects and therefore is
        /// declared public.</remarks>
        public static bool CheckIndex(int index)
        {
            return (testItems != null) & (index < testItems.Count) & (index >= 0);
        }

    }
}
