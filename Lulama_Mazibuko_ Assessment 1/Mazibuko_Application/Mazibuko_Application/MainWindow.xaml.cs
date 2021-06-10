using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Mazibuko_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }

        //SQL CONNECTION STRING
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Code Art\Documents\LulamaDB.mdf;Integrated Security=True;Connect Timeout=30");
        //Varibales
        SqlCommand cmd;
        DataTable dt;
        SqlDataReader sdr;


        // Validate Empty Fields
        public bool TextFieldValidation()
        {
            if (txt_name.Text == string.Empty)
            {
                MessageBox.Show("Firstname required","Field Validation Notification",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
            if (txt_surname.Text == string.Empty)
            {
                MessageBox.Show("Surname required", "Field Validation Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (combo_gender.Text == string.Empty)
            {
                MessageBox.Show("Gender required", "Field Validation Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (combo_degree.Text == string.Empty)
            {
                MessageBox.Show("Qualification required", "Field Validation Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (year_done.Text == string.Empty)
            {
                MessageBox.Show("Qualification Year Completed required", "Field Validation Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check for Field Validatoion Then to Data to the DataBase
                if (TextFieldValidation())
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO Graduates_Table VALUES('"+txt_name.Text +"','"+ txt_surname.Text+"','" + combo_gender.Text + "','" + combo_degree.Text + "','" + year_done.Text + "')",con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(txt_surname.Text+" " + txt_name.Text+ " " + "Data Saved Sucessfully", "Save Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    con.Close();
                    ClearFields();
                    LoadGrid();

                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               con.Close();
            }
        }

        //Clear all the fields
        public void ClearFields()
        {
            txt_name.Clear();
            year_done.Text = "";
            txt_surname.Clear();
            combo_degree.SelectedIndex = -1;
            combo_gender.SelectedIndex = -1;
        }
        private void reset_btn_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                con.Open();
                cmd = new SqlCommand("UPDATE Graduates_Table set Lastname='" + txt_surname.Text + "', Firstname='" + txt_name.Text + "', Qualification='" + combo_degree.Text + "'WHERE ID ='" + txt_Id.Text + "' ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Graduate" +" "+ txt_name.Text +" "+ "Record has been Updated", "Update Notification", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Updated" + " " + ex.Message);
            }
            finally
            {
                con.Close();
                LoadGrid();
                txt_Id.Clear();
                ClearFields();
            }

        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
 
            try
            {
                con.Open();
                cmd = new SqlCommand("DELETE FROM Graduates_Table Where ID = '" + txt_Id.Text + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Graduate" + " " + txt_name.Text + " " + "Record has been Deleted", "Delete Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                LoadGrid();
                txt_Id.Clear();
                ClearFields();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Deleted" + " " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        //SHOW DATA TO THE DATAGRID
        public void LoadGrid()
        {
            con.Open();
            cmd = new SqlCommand("Select * from Graduates_Table",con);
            dt = new DataTable();
            sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            datagrid.ItemsSource = dt.DefaultView;
            con.Close();
            
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void datagrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }
}
