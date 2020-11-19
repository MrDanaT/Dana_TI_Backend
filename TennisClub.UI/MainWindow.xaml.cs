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

        private List<RoleReadDTO> originalRoleList;

        /*
         * CRUD
         */
        private void CreateRole(RoleReadDTO item)
        {
            RoleCreateDTO newRole = new RoleCreateDTO
            {
                Name = item.Name
            };
            Task<HttpResponseMessage> response = WebAPI.PostCall("roles", newRole);

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                Debug.WriteLine($"{newRole.Name} is toegevoegd!");
            }
            else
            {
                Debug.WriteLine($"Er is iets foutgelopen.");
            }
        }

        private void ReadRoles()
        {
            Task<HttpResponseMessage> result = WebAPI.GetCall("roles");
            DataGrid itemsControl = RoleData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<RoleReadDTO> tmp = result.Result.Content.ReadAsAsync<List<RoleReadDTO>>().Result; ;
                itemsControl.ItemsSource = tmp;
                List<RoleReadDTO> tmp2 = new List<RoleReadDTO>(tmp.Count);
                tmp.ForEach((item) =>
                {
                    tmp2.Add(new RoleReadDTO { Id = item.Id, Name = item.Name });
                });
                originalRoleList = tmp2;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
            }
        }

        private void UpdateRole(int id, RoleUpdateDTO item)
        {
            Task<HttpResponseMessage> result = WebAPI.PutCall($"roles/{id}", item);
            DataGrid itemsControl = RoleData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                itemsControl.ItemsSource = result.Result.Content.ReadAsAsync<List<RoleReadDTO>>().Result;
                originalRoleList = result.Result.Content.ReadAsAsync<List<RoleReadDTO>>().Result;
                Debug.WriteLine("Updated!");
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
                Debug.WriteLine($"{item.Name} is verwijderd.");
                ReadRoles();
            }
            else
            {
                Debug.WriteLine($"Er is iets foutgelopen.");
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
            for (int i = 0; i < RoleData.Items.Count - 1; i++)
            {
                object item = RoleData.Items[i];
                RoleReadDTO roleItem = (RoleReadDTO)item;
                RoleReadDTO originalItem = originalRoleList.Find(x => x.Id == roleItem.Id);

                if (originalItem == null && roleItem != null)
                {
                    CreateRole(roleItem);
                }
                else if (!roleItem.Name.Equals(originalItem.Name))
                {
                    UpdateRole(roleItem.Id, new RoleUpdateDTO { Name = roleItem.Name });
                }
                else if (originalItem != null && roleItem == null)
                {
                    DeleteRole(originalItem);
                }
                else
                {
                    // Nothing
                }
            }

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