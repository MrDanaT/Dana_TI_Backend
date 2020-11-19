using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
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

        private List<RoleReadDTO> originalRoleList = new List<RoleReadDTO>();

        /*
         * CRUD
         */
        private void CreateRole(RoleReadDTO item)
        {
            RoleCreateDTO newRole = (RoleCreateDTO)item;
            //RoleCreateDTO newRole = new RoleCreateDTO
            //{
            //    Name = item.Name
            //};
            Task<HttpResponseMessage> response = WebAPI.PostCall("roles", newRole);

            if (response.Result.StatusCode == HttpStatusCode.Created)
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

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                itemsControl.ItemsSource = result.Result.Content.ReadAsAsync<List<RoleReadDTO>>().Result;
                originalRoleList = result.Result.Content.ReadAsAsync<List<RoleReadDTO>>().Result;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
            }
        }

        private void UpdateRole(RoleUpdateDTO item)
        {
            Task<HttpResponseMessage> result = WebAPI.PutCall("roles", item);
            DataGrid itemsControl = RoleData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                itemsControl.ItemsSource = result.Result.Content.ReadAsAsync<List<RoleReadDTO>>().Result;
                originalRoleList = result.Result.Content.ReadAsAsync<List<RoleReadDTO>>().Result;
                MessageBox.Show("Updated!");
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
            }
        }

        private void DeleteRole(RoleReadDTO item)
        {
            int id = item.Id;
            Task<HttpResponseMessage> response = WebAPI.DeleteCall($"roles/{id}");

            if (response.Result.StatusCode == HttpStatusCode.NoContent)
            {
                MessageBox.Show($"{item.Name} is verwijderd.");
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
        private void SyncLeaguesButton_Click(object sender, RoutedEventArgs e)
        {
            SynchroniseDatabase();
        }

        private void SynchroniseDatabase()
        {
            foreach (object item in RoleData.Items)
            {
                object roleItem = item;
                RoleReadDTO originalItem = originalRoleList.Find(x => x.Id == roleItem.Id);

                if (originalItem == null && roleItem == null)
                {
                    CreateRole((RoleReadDTO)roleItem);
                }
                else if (!((RoleReadDTO)roleItem).Name.Equals(originalItem.Name))
                {
                    UpdateRole((RoleUpdateDTO)roleItem);
                }
                else
                {
                    DeleteRole(originalItem);
                }
            }
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

            if (result.Result.StatusCode == HttpStatusCode.OK)
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
            Task<HttpResponseMessage> result = WebAPI.GetCall("genders");
            DataGrid itemsControl = GenderData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
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