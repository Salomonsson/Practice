using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeForSale.Home
{
  public class Apartment : Home
  {
    // Define new variables for all apartments
    // The ones defined in Estate are valid here also through properties
    // Also the ones defined in Home
    private int floor;  // Besides the address, there can be a floor variable

    // Together with a property
    public int Floor
    {
      get { return floor; }
      set
      {
        if (value > 0)
          floor = value;
      }
    }

    /// <summary>
    /// Default constructor, uses the Estate constructor
    /// and also sets the type of estate
    /// </summary>
    public Apartment()
      : base()
    {
      RealEstateType = EstateType.Apartment;
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="other"></param>
    public Apartment(Apartment other)
    {
      this.LegalStatus = other.LegalStatus;
      this.Price = other.Price;
      this.NbrRooms = other.NbrRooms;
      this.PostAddress = new Address(other.PostAddress);
      this.Id = other.Id;
      this.floor = other.floor;
      // Then copy the special data for this estate
    }

    // <summary>
    /// Copy the common data from a estate object
    /// </summary>
    /// <param name="other"></param>
    public Apartment(Estate other)
    {
      RealEstateType = EstateType.Apartment;
      this.LegalStatus = other.LegalStatus;
      this.Price = other.Price;
      this.NbrRooms = other.NbrRooms;
      this.PostAddress = new Address(other.PostAddress);
    }
  
  
    /// <summary>
    /// </summary>
    /// <returns>A formated string about the object.</returns>
    public override string ToString()
    {
       return String.Format(" {0} {1}", base.ToString(), 
                                   floor.ToString().ToUpper());
    }
  }
}
