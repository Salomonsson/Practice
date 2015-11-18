using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
 

namespace SerializationExProjCS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            //Add any initialization after the InitializeComponent() call.
            lblMessage.Text = String.Empty;
            timer1.Enabled = false;

        }

        private string fileName= Application.StartupPath + "\\TestPerson.dat";  //file at Application directory
        private string xmlFileName= Application.StartupPath + "\\TestPerson.xml";  //(file for testing xml

        //To test, run the example, serialize and close the application

        private void btnSerialize_Click(object sender, EventArgs e)
        {
            Person pers = new Person(txtFirstName.Text, txtLastName.Text);

            string strMessage = string.Format("{0} is saved on disk at {1}.{2}{2}{2}You can now exit the application, restart and deserialize!", 
                pers.FullName, fileName, Environment.NewLine);

            if (!BinaryFileSerializer.Serialize(pers, fileName))
            {
                strMessage = string.Format("{0} could not be saved on disk!", fileName);
            }
            lblMessage.Text = strMessage;
            timer1.Enabled = true;

        }

        //To test, start the application and click the "Deserialize" button directly.
        private void btnDeserialize_Click(object sender, EventArgs e)
        {
	        Person pers = BinaryFileSerializer.Deserialize<Person>(fileName);

	        string strMessage = string.Format("File {0}{1} not found.{1}Deserialization did not succeed!", fileName, Environment.NewLine);
	        if ((pers != null)) 
            {
		        strMessage = string.Format("{0} is deserialized into an object of {1} successfully! ", 
                    pers.FullName, pers.GetType().ToString());
                txtFirstName.Text = pers.FirstName.ToUpper();
                txtLastName.Text = pers.LastName.ToUpper();
	        }

	    lblMessage.Text = strMessage;
	    timer1.Enabled = true;

     }


    //byteArray is used to serialize an object of a person.
    //Remember that this array is saved in memory and if you exit the application,
    //it will not possible to serialize it.
    byte[] byteArray;

    private void btnSerializeArray_Click(object sender, EventArgs e)
    {
        Person pers = new Person(txtFirstName.Text, txtLastName.Text);

        string strMessage = string.Format("File {0} not found.  Deserialization did not succeed!", fileName);

        //Break down pers into bytes - Serialize
        byteArray = BinaryArraySerializer.Serialize(pers);

        if (byteArray != null)
        {
            strMessage = String.Format("{0}, serialized to a series of bytes.   Size = {1}.{2}{3}", 
                pers.ToString(), byteArray.Length, Environment.NewLine, 
                "You can now exit application, restart and directly deseialize array!");
        }

        lblMessage.Text = strMessage;
        timer1.Enabled = true;
    }

    private void btnDeserializeArray_Click(object sender, EventArgs e)
    {
        //Put together the byteArray to a person object
      
        string strMessage = string.Format("No byte array in memory. Deserialization cannot continue!");

        if (byteArray != null)
        {
            Person pers = BinaryArraySerializer.Deserialize<Person>(byteArray);

            if (pers != null)
            {
                strMessage = string.Format("{0},{2} From an array of 0:s and 1:s back as a Person.{2}Size of array = {1}.{2}{2} Teleportation works now!", 
                    pers.ToString(), byteArray.Length, Environment.NewLine);

                txtFirstName.Text = pers.FirstName.ToUpper();
                txtLastName.Text = pers.LastName.ToUpper();
            }
        }

        lblMessage.Text = strMessage;
        timer1.Enabled = true;
    }

    /// <summary>
    /// Sreialize an object of Person as an xml-file-  the file will be at the 
    /// Application Directory (...\bin)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>    
    /// 
    private void btnXMLSerialize_Click(object sender, EventArgs e)
    {
        Person pers = new Person(txtFirstName.Text, txtLastName.Text);
        string strMessage = string.Format("{0} is saved on disk at {1}.{2}{2}{2}You can now exit the application!", pers.FullName, fileName, Environment.NewLine);

        if (!XMLSerialization.SerializeToFile<Person>(xmlFileName, pers)) 
        {
	        strMessage = string.Format("{0} could not be saved on disk!", fileName);
        }

        lblMessage.Text = strMessage;
        timer1.Enabled = true;
        }

        /// <summary>
        /// Read the xmlFileName (data file) and save the data into an object of a person
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void btnXMLDeserialize_Click(object sender, EventArgs e)
        {
	        string strMessage = string.Format("File corrupt or not found. Deserialization cannot continue!");

	        Person pers = XMLSerialization.DeserializeFromFile<Person>(xmlFileName);

	        if (pers != null) 
            {
		        strMessage = string.Format("{0},{2} From the xml-file: {2}{1} back as a Person.",
                    pers.ToString(), xmlFileName, Environment.NewLine);
                txtFirstName.Text = pers.FirstName.ToUpper();
                txtLastName.Text = pers.LastName.ToUpper();

	        }

	        lblMessage.Text = strMessage;
	        timer1.Enabled = true;
        }
    }   
  
}
