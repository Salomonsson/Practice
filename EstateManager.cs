using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using HomeForSale.Commercial;
using HomeForSale.Home;

namespace HomeForSale
{
    class EstateManager
    {   //0. Declare a collection
        //lists = collections 
        //You can use  either ArrayList or List - the latter
        //is preferred. Both are handled in the same way
        //
        //An arraylist variable is declared as: 
        //private ArrayList estates; 


        //----------------------------------------------------------------------
        //Here we are using the List object 
        private List<Estate> estateList;   //declaration, not yet created

        //Konstruktor - skapa objekten som ingår som variabler 
        /// <summary>
        /// Default constructor - create the estate list
        /// </summary>
        public EstateManager()
        {
            //1.  Create the list object
            // estates = new ArrayList();  IF we are using an arrayList
            estateList = new List<Estate>();
        }

        /// <summary>
        /// Add a new estate object to the list
        /// </summary>
        /// <param name="estObj"></param>
        public void Add(Estate estObj)
        {
            if (estObj != null)  //Important - the object must be created (in calling method)
                estateList.Add(estObj);

          
        }

        /// <summary>
        /// Get one element from list
        /// 
        /// Two ways:
        /// 1. Return the element directly (the reference returned)
        /// 
        /// return estatList[index];  - Dangerous as any later change will also
        /// affect my list
        /// 2. return a (deep copy) element
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Estate GetElementAtPosition(int index)
        {
            //We choose to return a copy, why do we need type casting when copying?
            if (IsIndexValid(index))
            {
              if (estateList[index] is WareHouse)
                return new WareHouse((WareHouse)estateList[index]);
              if (estateList[index] is Villa)
                return new Villa((Villa)estateList[index]);
              if (estateList[index] is Store)
                return new Store((Store)estateList[index]);
              if (estateList[index] is RowHouse)
                return new RowHouse((RowHouse)estateList[index]);
              if (estateList[index] is Apartment)
                return new Apartment((Apartment)estateList[index]);
              return null;
            }
            else
                return null;
        }

        /// <summary>
        /// A list shall not be able to be indexed out of bounds.
        /// This method can be used from different places to ensure 
        /// correct indexing.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsIndexValid(int index)
        {
            return ((index >= 0) && (index < estateList.Count)); 
        }

        /// <summary>
        /// Read only property to get nr of elemnts in list
        /// </summary>
        public int ElementCount
        {
            get { return estateList.Count; }
        }

        public Estate Estate
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        } 
    }
}

