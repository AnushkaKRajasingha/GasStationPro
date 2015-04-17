using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace GasStationPro.PresentationLayer
{
    public partial class frmCustomer : Form
    {
        public frmCustomer()
        {
            InitializeComponent();
        }
        SqlConnection con;

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            //Calling the DBConnection class and making the database connection
            cboCusCode.Focus();
         //   DBConnection obj = new DBConnection();
          //  con = new SqlConnection(obj.getConstr());

            try
            {

               // con.Open();
                //RefreshData();
               

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Petro Gas (Pvt)Ltd");
                return;

            }
        }
        //Data Refresh------------------------------------------------------------
                private void RefreshData()
                {
            //Data adapter with select command 
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("sp_GetAllCustomer", con);

            DataTable table = new DataTable();
            da.Fill(table);
            dgCus.DataSource = table;
            
                    
        }

                private void cmdClear_Click(object sender, EventArgs e)
                {
                    this.clearData();
                }
        // clear Data-------------------------------------------------------------
                private void clearData()
                {
                    cboCusCode.Text = "";
                    txtCusname.Text = "";
                    txtAdd1.Text = "";
                    txtAdd2.Text = "";
                    txtAdd3.Text = "";
                    txtCusTel.Text = "";
                    txtLimit.Text = "";
                    chkBlocked.Checked = false;


                }

                private void cmdAdd_Click(object sender, EventArgs e)
                {
                    // check for null field---------------------------------------
                    if (string.IsNullOrEmpty(cboCusCode.Text))
                    {
                        MessageBox.Show("Enter customer code");
                        cboCusCode.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtCusname.Text))
                    {
                        MessageBox.Show("Enter customer Name");
                        txtCusname.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtAdd1.Text))
                    {
                        MessageBox.Show("Enter customer Address");
                        txtAdd1.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtLimit.Text))
                    {
                        MessageBox.Show("Enter the Limit");
                        txtLimit.Focus();
                        return;
                    }
                }
                
                
                    //Check customer is all ready is Exist one------------------------------
                public int isExist(string CUS_Code)
                {
                    if (isExist(cboCusCode.Text) > 0)
                        {
                            MessageBox.Show("The Customer Allready Exist.");
                              //return;
                         }
                    //SqlDataReader red = new SqlDataReader();
                    SqlCommand Com = new SqlCommand("sp_GetCustomerByID", con);
                    Com.CommandType = CommandType.StoredProcedure;
                    Com.Parameters.Add("@CUS_Code", SqlDbType.NVarChar);
                    Com.Parameters["@CUS_Code"].Value = cboCusCode.Text;
                    SqlDataReader rd = Com.ExecuteReader();
                    int counter = 0;
                    while (rd.Read())
                    {
                        counter++;
                    }
                    rd.Close();
                    return counter;
                  
                   
             //insert  values to databas
            SqlCommand insetComm = new SqlCommand();
            insetComm.Connection = con;
            insetComm.CommandText = "sp_CustomerAdd";
            insetComm.CommandType= CommandType.StoredProcedure;
            
            insetComm.Parameters.Add("@CUS_Code" ,SqlDbType.NVarChar );
  	        insetComm.Parameters.Add("@CUS_Name",SqlDbType.NVarChar );
  	        insetComm.Parameters.Add("@CUS_ADD1",SqlDbType.NVarChar);
  	        insetComm.Parameters.Add("@CUS_ADD2",SqlDbType.NVarChar );
  	        insetComm.Parameters.Add("@CUS_ADD3",SqlDbType.NVarChar );
  	        insetComm.Parameters.Add("@CUS_Tel",SqlDbType.NVarChar );
  	        insetComm.Parameters.Add("@CUS_Limit"	,SqlDbType.NVarChar );
  	        insetComm.Parameters.Add("@CUS_Blocked",SqlDbType.NVarChar );

            insetComm.Parameters["@CUS_Code"].Value = cboCusCode.Text ;
            insetComm.Parameters["@CUS_Name"].Value = txtCusname.Text ;
            insetComm.Parameters["@CUS_ADD1"].Value = txtAdd1.Text ;
            insetComm.Parameters["@CUS_ADD2"].Value = txtAdd2.Text;
            insetComm.Parameters["@CUS_ADD3"].Value = txtAdd3.Text;
            insetComm.Parameters["@CUS_Tel"].Value = txtCusTel.Text ;
            insetComm.Parameters["@CUS_Limit"].Value = txtLimit.Text ;
            if (chkBlocked.Checked == true)
            {
                insetComm.Parameters["@CUS_Blocked"].Value = "Yes";
            }
            else 
            {
                insetComm.Parameters["@CUS_Blocked"].Value = "No";
            }
            con.Open();
            int ans = insetComm.ExecuteNonQuery();
            con.Close();
            if (ans > 0)
                {
                MessageBox.Show("Customer added to db");
                RefreshData();
                }
            
              }

                private void cmdExit_Click(object sender, EventArgs e)
                {
                    MessageBox.Show("DO you want to exit","Petrogas Company",MessageBoxButtons.YesNo);
                    if (this.DialogResult == DialogResult.Yes)
                    {
                        this.Close();
                    }
                    
                }
         }
        
   }

                   

                        

    
    

