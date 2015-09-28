using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FoodManagerApp.Foods;


namespace FoodManagerApp
{
    public partial class MainForm : Form
    {
        //Use an instace of the FoodManager to handle 
        //the food list (food registry)
        private FoodManager m_foodManager = new FoodManager(); 

        public MainForm()
        {
            InitializeComponent();
            
            //My initializations
            InitializeGUI();
            UpdateGUI();
        }

        /// <summary>
        /// Prepare the GUI with initilizations
        /// </summary>
        private void InitializeGUI()
        { 
            cmbCategory.DataSource = Enum.GetValues(typeof(FoodCategory));

            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            //Set up the tooltip text for conrols
            string ingredientTips = "The ingrediensta are to be separated either by a comma ";
            ingredientTips += "or a semi colon.  An ingredient can consist of several words.";
            toolTip1.SetToolTip(this.txtIngriedients, ingredientTips);
            toolTip1.SetToolTip(this.lstSpecies, "Select also an item from the animal list.");
            
            //Test of abstract methods
            TestDataManager.AddTestData();
            lstSpecies.DataSource = Enum.GetValues(typeof (Species));
            lstSpecies.SelectedIndex = 0;
            btnChange.Enabled = false;
            btnDelete.Enabled = false;
        }

        /// <summary>
        /// Everytime the model changes, update the data on GUI
        /// </summary>
        private void UpdateGUI()
        {
            txtIngriedients.Text = string.Empty;
            txtName.Text = string.Empty;

            //Refresh the animal listbox
            lstRegistryItems.Items.Clear();
            lstRegistryItems.Items.AddRange(m_foodManager.GetFoodListStringArray());
        }

        /// <summary>
        /// Testing animal foods require that different an item is selected on the 
        /// animal listbox and the species listbox.
        /// </summary>
        /// <returns>true if items are selecte, false otherwise.</returns>
        private bool ValidateListBoxSelections()
        {
            return (lstRegistryItems.SelectedIndex > -1) && (lstSpecies.SelectedIndex > -1);
        }

        /// <summary>
        /// To test some abstract methods in the FoodManager classes, 
        /// some test data is put in the program. This method checks
        /// whether a test animal type (ex sheep) likes a food.
        /// Each animal is classed as an eater type.  See the
        /// TestData class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckFood_Click_1(object sender, EventArgs e)
        {
            if (!ValidateListBoxSelections())
                return;

            TestData animal = TestDataManager.GetItemAt(lstSpecies.SelectedIndex);
            Food foodItem = m_foodManager.GetFoodItemAt(lstRegistryItems.SelectedIndex);

            string animalReaction = "Ush! Put it away!  This makes me lose my appetite!";

            if (foodItem.IsGoodFor(animal.Item))
               animalReaction ="Yummy Yummy!!";

            MessageBox.Show(animalReaction);
        } 
     
        /// <summary>
        /// Add a new animal item in the registry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
                AddFoodItem();
            else
                MessageBox.Show("Fill the name, ingredients and check the eater types and try again.", "Invalid input!");
        }

        /// <summary>
        /// Validate input.  A name for the food and a list av ingredients are 
        /// to bu supplied by the user. The ingrediensta are to be separated 
        /// either by a comma or a semi colon.  An ingredient can consist of several words.
        /// </summary>
        /// <returns></returns>
        private bool ValidateInput()
        {
            bool bok = (!string.IsNullOrEmpty(txtIngriedients.Text) & 
                (!string.IsNullOrEmpty(txtName.Text)));
            return bok;
        }

        /// <summary>
        /// Read the data from the GUI, set up an object and save the object in
        /// the animal registry.
        /// </summary>
        private void AddFoodItem()
        {
            string name = txtIngriedients.Text;
            string ingredients = txtName.Text;
            
            //Find out which category the 
            FoodCategory foodType = (FoodCategory)cmbCategory.SelectedIndex;
            Food foodItem = null; 

            switch (foodType)
            {
                case FoodCategory.Meat:
                    foodItem = new Meat();//Late binding                
                    break; 

                case FoodCategory.Vegetable:
                    foodItem = new Vegeterian();//Late binding                
                    break; 

                case FoodCategory.Mixed:
                    foodItem = new MixedFood();//Late binding    
                    break; 
            }

            char[] delimeters = { ',', ';' }; //',' and/or ';' separated

            //Set other info into the object
            if (foodItem != null)
            {
                foodItem.Name = String.IsNullOrEmpty(txtName.Text) ? "No name" : txtName.Text;
                foodItem.Ingredients = foodItem.GetIngredientsFromAStringList(txtIngriedients.Text, delimeters);

                //Save the object into the registry
                m_foodManager.Add(foodItem);
                UpdateGUI();
            }

        }

        private void btnChange_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

       
      }
}
