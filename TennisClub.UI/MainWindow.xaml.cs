using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TennisClub.Common.League;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

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
        private void CreateRole(object sender, AddingNewItemEventArgs e)
        {
            DataGrid itemsControl = RoleData;

            RoleCreateDTO newRole = new RoleCreateDTO { Name = "lol" };
            Task<HttpResponseMessage> response = WebAPI.PostCall("roles", newRole);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                System.Console.WriteLine("Gelukt");
                itemsControl.ItemsSource = response.Result.Content.ReadAsAsync<List<object>>().Result;
            }
            else
            {
                System.Console.WriteLine("Niet gelukt!");
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
                System.Console.WriteLine("Niet gelukt!");
            }
        }

        private void UpdateRole()
        {

        }

        private void DeleteRole()
        {

        }

        /*
         * Event Handlers
         */
        private void GetRolesButton_Click(object sender, RoutedEventArgs e)
        {
            ReadRoles();
        }
        private void RoleData_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            DeleteRole();
        }

        private void RoleData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            UpdateRole();
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
                System.Console.WriteLine("Niet gelukt!");
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
                itemsControl.ItemsSource = result.Result.Content.ReadAsAsync<List<object>>().Result;
            }
            else
            {
                System.Console.WriteLine("Niet gelukt!");
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