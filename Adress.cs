//Code Example
//Programming Using C#
//Farid Naisan
//Created: July 2012, translation into English of BoButiken


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeForSale
{
    public class Address
    {
        /// <summary>
        /// Fields
        /// </summary>
        private string street;
        private string zipCode;
        private string city;
 

        /// <summary>
        /// Default constructor. Empty all strings 
        /// </summary>
        public Address()
        {
            street = string.Empty;
            zipCode = string.Empty;
            city = string.Empty;
        }

        //copy consructor Exercise:  http://msdn.microsoft.com/en-us/library/ms173116(VS.80).aspx
        
        /// <summary>
        /// Copy constructor: Use this when you are copying
        /// from one object to another object of this class
        /// </summary>
        /// <param name="other"></param>
        public Address(Address other)
        {
            this.street = other.street;
            this.zipCode = other.zipCode;
            this.city = other.city;
        }

        #region properties
        public string Street
        {
            get { return street; }
            set { street = value; }
        }


        public string City
        {
            get { return city; }
            set 
            {
                if (!string.IsNullOrEmpty(value))
                    city = value; 
            }
        }

        public string Zip
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

       #endregion

        /// <summary>
        /// Formatting text into several lines
        /// </summary>
        /// <returns></returns>
        public string GetAddressLabel()
        { 
            string strOut = street + Environment.NewLine;
            strOut += zipCode + " " + city;
            return strOut;
 
        }

        /// <summary>
        /// Formatting text into one line
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strOut = string.Format("{0}, {1}, {2}", street, zipCode, city);
            return strOut;

        }
    }//class
}
