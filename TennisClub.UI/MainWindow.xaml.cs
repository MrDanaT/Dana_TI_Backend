using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TennisClub.Common.GameResult;
using TennisClub.Common.Gender;
using TennisClub.Common.League;
using TennisClub.Common.Member;
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

            Setup();
        }

        private void Setup()
        {
            originalGameResultList = new List<GameResultReadDTO>();
            originalMemberList = new List<MemberReadDTO>();
            originalRoleList = new List<RoleReadDTO>();

            ReadRoles();
            ReadMembers();
            ReadGenders();
            ReadLeagues();
            ReadGameResults();

            gameResultPlayerComboBox.ItemsSource = originalMemberList;
        }

        #region Roles

        private List<RoleReadDTO> originalRoleList;

        /*
         * CRUD
         */
        private bool CreateRole(RoleReadDTO item)
        {
            RoleCreateDTO newRole = new RoleCreateDTO
            {
                Name = item.Name
            };
            Task<HttpResponseMessage> response = WebAPI.PostCall("roles", newRole);

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                Debug.WriteLine($"{newRole.Name} is toegevoegd!");
                return true;
            }
            else
            {
                Debug.WriteLine($"Er is iets foutgelopen.");
                return false;
            }
        }

        private bool ReadRoles()
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

                return true;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
                return false;
            }
        }

        private bool UpdateRole(int id, RoleUpdateDTO item)
        {
            Task<HttpResponseMessage> result = WebAPI.PutCall($"roles/{id}", item);

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                Debug.WriteLine("Updated!");
                return true;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
                return false;
            }
        }

        private bool DeleteRole(RoleReadDTO item)
        {
            int id = item.Id;
            Task<HttpResponseMessage> response = WebAPI.DeleteCall($"roles/{id}");

            if (response.Result.StatusCode == HttpStatusCode.NoContent)
            {
                Debug.WriteLine($"{item.Name} is verwijderd.");
                return true;
            }
            else
            {
                Debug.WriteLine($"Er is iets foutgelopen.");
                return false;
            }
        }

        private void SynchroniseRoleTable()
        {
            bool isSucceeded = false;

            for (int i = 0; i < RoleData.Items.Count - 1; i++)
            {
                object item = RoleData.Items[i];
                RoleReadDTO roleItem = (RoleReadDTO)item;
                RoleReadDTO originalItem = originalRoleList.Find(x => x.Id == roleItem.Id);

                if (originalItem == null && roleItem != null)
                {
                    isSucceeded = CreateRole(roleItem);
                }
                else if (!roleItem.Name.Equals(originalItem.Name))
                {
                    isSucceeded = UpdateRole(roleItem.Id, new RoleUpdateDTO { Name = roleItem.Name });
                }
                //else if (originalItem != null && roleItem == null)
                //{
                //    DeleteRole(originalItem);
                //}
                else
                {
                    isSucceeded = true;
                }

                if (!isSucceeded)
                {
                    break;
                }
            }

            if (isSucceeded)
            {
                MessageBox.Show("De tabel is succesvol gesynchroniseerd met de database!");
                ReadRoles();
            }
            else
            {
                MessageBox.Show("Er is een fout gebeurd bij het synchroniseren. Probeer dit opnieuw.");
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
            SynchroniseRoleTable();
        }


        #endregion

        #region Leagues

        /*
         * CRUD
         */
        private bool ReadLeagues()
        {
            Task<HttpResponseMessage> result = WebAPI.GetCall("leagues");
            DataGrid itemsControl = LeagueData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                itemsControl.ItemsSource = result.Result.Content.ReadAsAsync<List<LeagueReadDTO>>().Result;
                return true;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
                return false;
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
        private bool ReadGenders()
        {
            Task<HttpResponseMessage> result = WebAPI.GetCall("genders");
            DataGrid itemsControl = GenderData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                itemsControl.ItemsSource = result.Result.Content.ReadAsAsync<List<GenderReadDTO>>().Result;
                return true;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
                return false;
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

        #region GameResults

        /*
         * CRUD
         */

        private List<GameResultReadDTO> originalGameResultList;

        private bool CreateGameResult(GameResultReadDTO item)
        {
            GameResultCreateDTO newGameResult = new GameResultCreateDTO
            {
                GameId = item.GameId,
                ScoreOpponent = item.ScoreOpponent,
                ScoreTeamMember = item.ScoreTeamMember,
                SetNr = item.SetNr
            };
            Task<HttpResponseMessage> response = WebAPI.PostCall("gameresults", newGameResult);

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                Debug.WriteLine($"{newGameResult.GameId} is toegevoegd!");
                return true;
            }
            else
            {
                Debug.WriteLine($"Er is iets foutgelopen.");
                return false;
            }
        }


        private bool ReadGameResults()
        {
            Task<HttpResponseMessage> result = WebAPI.GetCall("gameresults");
            DataGrid itemsControl = GameResultData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<GameResultReadDTO> tmp = result.Result.Content.ReadAsAsync<List<GameResultReadDTO>>().Result; ;
                itemsControl.ItemsSource = tmp;
                List<GameResultReadDTO> tmp2 = new List<GameResultReadDTO>(tmp.Count);
                tmp.ForEach((item) =>
                {
                    tmp2.Add(new GameResultReadDTO
                    {
                        Id = item.Id,
                        GameId = item.GameId,
                        ScoreOpponent = item.ScoreOpponent,
                        ScoreTeamMember = item.ScoreTeamMember,
                        SetNr = item.SetNr
                    });
                });
                originalGameResultList = tmp2;
                return true;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
                return false;
            }
        }

        private bool UpdateGameResult(int id, GameResultUpdateDTO item)
        {
            Task<HttpResponseMessage> result = WebAPI.PutCall($"gameresults/{id}", item);

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                Debug.WriteLine("Updated!");
                return true;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
                return false;
            }
        }
        private void SynchroniseGameResultTable()
        {
            bool isSucceeded = false;

            for (int i = 0; i < GameResultData.Items.Count - 1; i++)
            {
                object item = GameResultData.Items[i];
                GameResultReadDTO gameResultItem = (GameResultReadDTO)item;
                GameResultReadDTO originalItem = originalGameResultList.Find(x => x.Id == gameResultItem.Id);

                if (originalItem == null && gameResultItem != null)
                {
                    isSucceeded =  CreateGameResult(gameResultItem);
                }
                else if (!gameResultItem.ScoreOpponent.Equals(originalItem.ScoreOpponent) || !gameResultItem.ScoreTeamMember.Equals(originalItem.ScoreTeamMember) || !gameResultItem.SetNr.Equals(originalItem.SetNr))
                {
                    isSucceeded= UpdateGameResult(gameResultItem.Id, new GameResultUpdateDTO { ScoreOpponent = gameResultItem.ScoreOpponent, SetNr = gameResultItem.SetNr, ScoreTeamMember = gameResultItem.ScoreTeamMember });
                }
                else
                {
                    isSucceeded = true;
                }

                if (!isSucceeded)
                {
                    break;
                }
            }

            if (isSucceeded)
            {
                MessageBox.Show("De tabel is succesvol gesynchroniseerd met de database!");
                ReadGameResults();
            }
            else
            {
                MessageBox.Show("Er is een fout gebeurd bij het synchroniseren. Probeer dit opnieuw.");
            }
        }

        private void GetGameResultsButton_Click(object sender, RoutedEventArgs e)
        {
            ReadGameResults();
        }

        private void SyncGameResulsButton_Click(object sender, RoutedEventArgs e)
        {
            SynchroniseGameResultTable();
        }

        private void ClearGameResultsFilterButton_Click(object sender, RoutedEventArgs e)
        {
            gameResultPlayerComboBox.SelectedIndex = 0;
            GameResultData.ItemsSource = originalGameResultList;
        }
        private void SearchFilteredGameResults_Click(object sender, RoutedEventArgs e)
        {
            Task<HttpResponseMessage> result = WebAPI.GetCall($"gameresults/bymemberid/{gameResultPlayerComboBox.SelectedValue}");
            DataGrid itemsControl = GameResultData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<GameResultReadDTO> tmp = result.Result.Content.ReadAsAsync<List<GameResultReadDTO>>().Result; ;
                itemsControl.ItemsSource = tmp;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
            }
        }

        #endregion

        #region Members

        private List<MemberReadDTO> originalMemberList;

        private bool ReadMembers()
        {
            Task<HttpResponseMessage> result = WebAPI.GetCall("members");
            // DataGrid itemsControl = GameResultData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<MemberReadDTO> tmp = result.Result.Content.ReadAsAsync<List<MemberReadDTO>>().Result; ;
                // itemsControl.ItemsSource = tmp;
                List<MemberReadDTO> tmp2 = new List<MemberReadDTO>(tmp.Count);
                tmp.ForEach((item) =>
                {
                    tmp2.Add(new MemberReadDTO
                    {
                        Id = item.Id,
                        Addition = item.Addition,
                        Address = item.Address,
                        BirthDate = item.BirthDate,
                        City = item.City,
                        Deleted = item.Deleted,
                        FederationNr = item.FederationNr,
                        FirstName = item.FirstName,
                        GenderId = item.GenderId,
                        LastName = item.LastName,
                        Number = item.Number,
                        PhoneNr = item.PhoneNr,
                        Zipcode = item.Zipcode
                    });
                });
                originalMemberList = tmp2;
                return true;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
                return false;
            }
        }

        #endregion

     
    }
}