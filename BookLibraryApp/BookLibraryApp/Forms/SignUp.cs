using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookLibraryApp.Forms
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            var username = tbUsername.Text;
            var password = tbPassword.Text;
            var confirmPassword = tbConfirmPassword.Text;
            var databaseOperation = new DatabaseOperations();

            Task.Run(() => {
                try
                {
                    var usernameAlreadyExists = databaseOperation.usernameAlreadyInDatabase(username);

                    if(usernameAlreadyExists == true)
                    {
                        MessageBox.Show("The username already exists");
                    }
                    else
                    {
                        if(password == confirmPassword)
                        {
                            var isAdmin = false;
                            databaseOperation.createUser(username, password, isAdmin);
                            Invoke((MethodInvoker)(() =>
                            {
                                this.Hide();
                                Forms.UserMenu formUserMenu = new Forms.UserMenu();
                                formUserMenu.Show();
                            }));
                        }
                        else
                        {
                            MessageBox.Show("The specified passwords must be identical");
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            });
        }
    }
}
