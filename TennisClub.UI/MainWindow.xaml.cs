using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TennisClub.Common.Gender;
using TennisClub.Common.League;
using TennisClub.Common.Role;

namespace TennisClub.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Roles

        /*
         * CRUD
         */
        private void CreateRole()
        {
            RoleReadDTO selectedItem = (RoleReadDTO)RoleData.SelectedItem;
            RoleCreateDTO newRole = new RoleCreateDTO
            {
                Name = selectedItem.Name
            };
            Task<HttpResponseMessage> response = WebAPI.PostCall("roles", newRole);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                MessageBox.Show($"{newRole.Name} is toegevoegd!");
            }
            else
            {
                MessageBox.Show($"Er is iets foutgelopen.");
            }
        }

        private void ReadRoles()
        {
            Task<HttpResponseMessage> result = WebAPI.GetCall("roles");
            DataGrid itemsControl = RoleData;

            if (result.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                itemsControl.ItemsSource = result.Result.Content.ReadAsAsync<List<RoleReadDTO>>().Result;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
            }
        }

        private void UpdateRole()
        {
            MessageBox.Show("Updated!");
        }

        private void DeleteRole()
        {
            RoleReadDTO selectedItem = (RoleReadDTO)RoleData.SelectedItem;
            byte id = selectedItem.Id;
            Task<HttpResponseMessage> response = WebAPI.DeleteCall($"roles/{id}");

            if (response.Result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                MessageBox.Show($"{selectedItem.Name} is verwijderd.");
                ReadRoles();
            }
            else
            {
                MessageBox.Show($"Er is iets foutgelopen.");
            }
        }

        /*
         * Event Handlers
         */
        private void GetRolesButton_Click(object sender, RoutedEventArgs e)
        {
            ReadRoles();
        }

        #endregion

        #region Leagues

        /*
         * CRUD
         */
        private void ReadLeagues()
        {
            Task<HttpResponseMessage> result = WebAPI.GetCall("leagues");
            DataGrid itemsControl = LeagueData;

            if (result.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                itemsControl.ItemsSource = result.Result.Content.ReadAsAsync<List<LeagueReadDTO>>().Result;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
            }
        }

        /*
         * Event Handlers
         */
        private void GetLeaguesButton_Click(object sender, RoutedEventArgs e)
        {
            ReadLeagues();
        }

        #endregion

        #region Genders
        /*
         * CRUD
         */
        private void ReadGenders()
        {
            Task<HttpResponseMessage> result = WebAPI.GetCall("leagues");
            DataGrid itemsControl = GenderData;

            if (result.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                itemsControl.ItemsSource = result.Result.Content.ReadAsAsync<List<GenderReadDTO>>().Result;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
            }
        }

        /*
         * Event Handlers
         */
        private void GetGendersButton_Click(object sender, RoutedEventArgs e)
        {
            ReadGenders();
        }

        #endregion
    }
}