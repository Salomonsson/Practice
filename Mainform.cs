using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HomeForSale.Commercial;
using HomeForSale.Home;

namespace HomeForSale
{
    public partial class MainForm : Form
    {   
        /// <summary>
        /// MinForm is using ("have one") object of type EstateManager
        /// </summary>
        private EstateManager estateMngr = null;  //ref variable declared

        /// <summary>
        /// Default constructor - a good place for initializations
        /// </summary>
        public MainForm()
        {
            //Visual Studio initializations
            InitializeComponent();

            //My initializations
            InitializeGUI();
            
            //Create the manager
            estateMngr = new EstateManager();
        }

        internal EstateManager EstateManager
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

   
        /// <summary>
        /// Prepare the form before display
        /// Initiate input controls with default values
        /// Remove design values from output controls (label1 ex.)
        /// </summary>
        private void InitializeGUI()
        {
            //Clear output controls
            txtCity.Text = String.Empty;
            txtStreet.Text = String.Empty;
            txtZipCode.Text = String.Empty;
            
            // Fill the estate combobox with values from enum
            cmbTyp.Items.AddRange(Enum.GetNames(typeof(EstateType)));
            //Make this readonly
            cmbTyp.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTyp.SelectedIndex = (int)EstateType.Apartment; //Choose one type as default

            rbtnHyres.Checked = true;

            // LandSize input only applies to Villa type
            txtLandSize.Enabled = false;
            lblLandSize.Enabled = false;
        }

        /// <summary>
        /// Get adddress items from the Address groupbox (textboxes)
        /// and save the values in a local instance of address.
        /// This method is called by ReadInput.
        /// </summary>
        /// <returns></returns>
        private Address ReadAddress()
        {
            //Create a local address instance
            Address adr = new Address();

            //Get the text from the textboxes
            adr.Street = txtStreet.Text;
            adr.City = txtCity.Text;
            adr.Zip = txtZipCode.Text;
            return adr;  //return the object (containing address data)
        }

        /// <summary>
        /// Hämata data från GUI, fyll i ett lokalt object av Fastighet
        /// för att senare skickas till fastighetMngr
        /// </summary>
        /// <param name="fastighet"></param>
        /// <returns></returns>
        private bool ReadInput(out Estate estate)
        {
            //Create a local estate instance for filling in input
            estate = new Estate(); 

            //Call a method that returns a complete address object 
            Address adr = ReadAddress();

             //If no error message, everything ok for now
            //Set the address of the local estate 
            estate.PostAddress = adr;

            //Then set legal type
            if (rbtnHyres.Checked)
              estate.LegalStatus = LegalType.Rental;
            else if   (rbtnInsats.Checked)
              estate.LegalStatus = LegalType.Tenement;
            else
                estate.LegalStatus = LegalType.Ownership;

            //Process price
            bool prisOK = false;
            //Read and test price input in separate method
            estate.Price = ReadPris(out prisOK);
            
            //SAme for number of rooms
            bool antalRumOK = false;
            estate.NbrRooms = ReadAntalRum(out antalRumOK);
            
            //return true or false depending on user input. 
            //If both price and nr of rooms ok, return true
            return prisOK && antalRumOK;
        }

        /// <summary>
        /// Try to convert the text in the price textbox to a valid float
        /// </summary>
        /// <param name="success">Out parameter, true if a conversion is possible
        /// false otherwise.</param>
        /// <returns>Price as decimal type or 0 if not possible to convert</returns>
        private decimal ReadPris(out bool success)
        {
            decimal pris = 0.0m;

            success = decimal.TryParse(txtPris.Text, out pris);

            if (!success)
                MessageBox.Show("The entered price is not valid!");
                
            return pris;
        }
        /// <summary>
        /// Try to convert the text in the rooms textbox to a valid integer
        /// </summary>
        /// <param name="success"> Out parameter, true if a conversion is possible
        /// false otherwise.</param>
        /// <returns></returns>
        private int ReadAntalRum(out bool success)
        {
            int antalRum = 0;
            success = int.TryParse(txtRooms.Text, out antalRum); //true if ok
            if (!success)
                MessageBox.Show("Number of rooms is not valid!");
            return antalRum;
        }

        /// <summary>
        /// Try to convert the landSize input to a valid number
        /// </summary>
        /// <param name="success">true if OK</param>
        /// <returns></returns>
        private int ReadLandSize(out bool success)
        {
          int landSurface = 0;
          success = int.TryParse(txtLandSize.Text, out landSurface); //true if ok
          if (!success)
            MessageBox.Show("Land size is not valid!");
          return landSurface;
        }

        /// <summary>
        /// Try to convert the floor input to a valid number
        /// </summary>
        /// <param name="success"></param>
        /// <returns></returns>
        private int ReadFloor(out bool success)
        {
          int floor = 0;
          success = int.TryParse(txtFloor.Text, out floor); //true if ok
          if (!success)
            MessageBox.Show("Floor is not valid!");
          return floor;
        }

        //När användaren trycker på knappen Lägg till
        //Läs all indata från GUI i ett lokalt objekt av fastighet
        //Gör indata kontroll
        //Spara objektet i fastighetMngr
        private void btnAdd_Click(object sender, EventArgs e)
        {        
            Estate estate;  //holder for input - create in ReadInput

            //Send this object to ReadInput
            //for filling in values (input)
            //out tells ReadInput that the variable 
            //is data out. All changes to this object
            //comes back here.
            //ReadInput creates all data used for the common fields of all estates.
            //The special for each are filled in later in the switch case.
            bool ok = ReadInput(out estate);

            if (ok)  //If all common data (in variable estate) is OK
            {
              // Now create a correct estate according to the type combo
              switch ((EstateType)cmbTyp.SelectedIndex)
              {
                case EstateType.WareHouse:
                  {
                    // Use a copy constructor to set a warehouse with common data
                    WareHouse  wHouse = new WareHouse(estate);
                    // If more data in GUI to fill in for this estate, do it here
                    //Then send it to the manager for adding to the list
                    estateMngr.Add(wHouse);
                    break;
                  }
                case EstateType.Villa:
                  {
                    // Same procedure all different types of estates
                    Villa vHouse = new Villa(estate);
                    // But here we need to add landSize size
                    bool landSizeOk = false;
                    vHouse.LandSize = ReadLandSize(out landSizeOk);
                    if (!landSizeOk)
                      return;
                    estateMngr.Add(vHouse);
                    break;
                  }
                case EstateType.Store:
                  {
                    Store store = new Store(estate);
                    estateMngr.Add(store);
                    break;
                  }
                case EstateType.RowHouse:
                  {
                    RowHouse rHouse = new RowHouse(estate);
                    // Here we need to add landSize size
                    bool gardenok = false;
                    rHouse.LandSize = ReadLandSize(out gardenok);
                    if (!gardenok)
                      return;
                    estateMngr.Add(rHouse);
                    break;
                  }
                case EstateType.Apartment:
                  {
                    Apartment apart = new Apartment(estate);
                    // We need to add floor
                    bool floorok = false;
                    apart.Floor = ReadFloor(out floorok);
                    if (!floorok)
                      return;
                    estateMngr.Add(apart);
                    break;
                  }
              }
               
              //Then Update the GUI
              UpdateResults();
            }

        }

        /// <summary>
        /// Reset result list and fill in with new values
        /// </summary>
        private void UpdateResults()
        {
            lstResults.Items.Clear();  //Erase current list
            //Get one elemnet at a time from manager, and call its 
            //ToString method for info - send to listbox
            for (int index = 0; index < estateMngr.ElementCount; index++)
            {
                //Q: Vhy not use new in the line below?
                Estate estate = estateMngr.GetElementAtPosition(index);
                // We can get an estate since here we don't need to separate
                // the different estates,ej, we are only interested in the toString method.
                lstResults.Items.Add(estate.ToString());
            }
        }

        /// <summary>
        /// Input for variables landSize and floor only enabled win certain cases
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
          if (cmbTyp.SelectedIndex == (int)EstateType.Villa ||
            cmbTyp.SelectedIndex == (int)EstateType.RowHouse)
          {
            txtLandSize.Enabled = true;
            lblLandSize.Enabled = true;
          }
          else
          {
            txtLandSize.Enabled = false;
            lblLandSize.Enabled = false;
          }
          if (cmbTyp.SelectedIndex == (int)EstateType.Apartment)
          {
            txtFloor.Enabled = true;
            lblFloor.Enabled = true;
          }
          else
          {
            txtFloor.Enabled = false;
            lblFloor.Enabled = false;
          }
        }

      
    }
}
