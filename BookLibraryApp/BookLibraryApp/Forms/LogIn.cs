using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookLibraryApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        public void btnLogIn_Click(object sender, EventArgs e)
        {
            var username = tbUserName.Text;
            var password = tbPassword.Text;
            var databaseOperation = new DatabaseOperations();


             Task.Run(() => {
                try
                {
                    var user = databaseOperation.GetUserFromDatabase(username, password);

                    if (user == null)
                    {
                        MessageBox.Show("Username or password incorrect");
                    }
                    else
                    {
                         Action<bool> checkAdmin = determineTheCorrespondingMenu;
                         checkAdmin(user.IsAdmin);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            });

            void determineTheCorrespondingMenu(bool isAdmin)
            {
                Invoke((MethodInvoker)(() => {
                    if (isAdmin == true)
                    {
                        this.Hide();
                        Forms.AdminMenu formAdminMenu = new Forms.AdminMenu();
                        formAdminMenu.Show();

                    }
                    else
                    {
                        this.Hide();
                        Forms.UserMenu formUserMenu = new Forms.UserMenu();
                        formUserMenu.Show();
                    }
                }));
            }
        }

        public void btnSignUp_Click(object sender, EventArgs e)
        {
            this.Hide();
            Forms.SignUp formSignUp = new Forms.SignUp();
            formSignUp.Show();
        }
    }
}
