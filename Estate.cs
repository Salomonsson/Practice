using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace HomeForSale
{
    public class Estate
    {
#region Fields
        /// <summary>
        /// Fields
        /// </summary>
        private EstateType estateType;
        private LegalType legalType;
        private decimal price;
        private int rooms;
        private Address address;   //"has " relation
        private int id;
 #endregion

        /// <summary>
        /// Default construktor - a good place for creating 
        /// all the fields in the class
        /// </summary>
        public Estate()
        {
            address = new Address();
            price = 0.0m;  //m is short for "money". 
            rooms = 0;
            legalType = LegalType.Rental;
            id = 0;
        }

        /// <summary>
        /// Copy constructor. Is used when an estate shall
        /// be cpoied from another estate. Provides for deep copy
        /// </summary>
        /// <param name="other">The estate to copy</param>
        public Estate(Estate other)
        {
            this.estateType = other.estateType;
            this.price = other.price;
            this.rooms = other.rooms;
            this.legalType = other.legalType;
            this.id = other.id;
            //NOTE! The copy constructor of the address class is called
            //Direct copy like obj1 = obj2 does NOT provide a deep copy 
            this.address = new Address(other.address);   //"Has a"
        }

#region properties
        //properties tied to the instans variables

        public int Id
        {
          get { return id; }
          set 
          { 
            if (value > 0) 
              id = value; 
          }
        }

        public EstateType RealEstateType
        {
          get { return estateType; }
          set { estateType = value; }
        }

        public LegalType LegalStatus
        {
            get { return legalType; }
            set { legalType = value; }
        }
        //continue rest of properties
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public int NbrRooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        public Address PostAddress
        {
            //the return of address is dangerous, send a copy instead
            get { return address; }  
            set 
            { 
                //value is a reference to an object of Address
                //The object must be created!
                //Debug.Assert forces the execution to stop at this line
                //when (value != null) is false. This line
                //is  not part of the program in release mode.
                Debug.Assert(value != null, "Kill the programmer!");

                if (value != null)  //value must not be null
                  address = value; 
            } 
        }

#endregion

        public Address Address
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public EstateType EstateType
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public LegalType LegalType
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        //Methods
        
        /// <summary>
        ///Format a string with values from this estate.
        ///Note that data for the address object is fetched from the
        ///address-object belonging to this estate.
        /// </summary>
        /// <returns>The formatted string.</returns>
        public override string ToString()
        {
            string strLegalStatus = Enum.GetName(typeof(LegalType), legalType);
            string strEstateType = Enum.GetName(typeof(EstateType), estateType);

            //Vhat is {0, -12}, {3, 6} eller {4} ?
            string strOut = String.Format(" {0, -12} {1,-12} {2, 12}, {3, 6} {4}",
                strEstateType, strLegalStatus, price, rooms, address.ToString());

            strOut = strOut.ToUpper();
            return strOut;
        }
    }
}
