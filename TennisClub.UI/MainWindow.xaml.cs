using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

            gameResultPlayerComboBoxFilter.ItemsSource = originalMemberList;
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

            if (result.Result.StatusCode == HttpStatusCode.NoContent)
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
                else if (!roleItem.Equals(originalItem))
                {
                    isSucceeded = UpdateRole(roleItem.Id, new RoleUpdateDTO { Name = roleItem.Name });
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

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<GenderReadDTO> tmp = result.Result.Content.ReadAsAsync<List<GenderReadDTO>>().Result;
                memberGender.ItemsSource = tmp.ToList();
                return true;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
                return false;
            }
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
            Task<HttpResponseMessage> result = WebAPI.GetCall($"gameresults?{GetGameResultFilters()}");
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

            if (result.Result.StatusCode == HttpStatusCode.NoContent)
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

        /*
         * Event Handlers
         */
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
                    isSucceeded = CreateGameResult(gameResultItem);
                }
                else if (!gameResultItem.Equals(originalItem))
                {
                    isSucceeded = UpdateGameResult(gameResultItem.Id, new GameResultUpdateDTO { ScoreOpponent = gameResultItem.ScoreOpponent, SetNr = gameResultItem.SetNr, ScoreTeamMember = gameResultItem.ScoreTeamMember });
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
            gameResultPlayerComboBoxFilter.SelectedItem = null;
            gameResultdatePickerFilter.SelectedDate = null;
            ReadGameResults();
        }

        private void SearchFilteredGameResults_Click(object sender, RoutedEventArgs e)
        {
            ReadGameResults();
        }

        private string GetGameResultFilters()
        {
            string memberIdUrl = $"memberId={gameResultPlayerComboBoxFilter.SelectedValue}";
            string selectedDateUrl = $"date={gameResultdatePickerFilter.SelectedDate.GetValueOrDefault().ToShortDateString()}";

            return $"{(gameResultPlayerComboBoxFilter.SelectedValue != null ? memberIdUrl : "")}&{(gameResultdatePickerFilter.SelectedDate != null ? selectedDateUrl : "")}";
        }

        #endregion

        #region Members

        private List<MemberReadDTO> originalMemberList;

        /*
         * CRUD
         */

        private bool ReadMembers()
        {
            Task<HttpResponseMessage> result = WebAPI.GetCall($"members/active?{GetMemberFilters()}");
            DataGrid itemsControl = MemberData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<MemberReadDTO> tmp = result.Result.Content.ReadAsAsync<List<MemberReadDTO>>().Result; ;
                itemsControl.ItemsSource = tmp;
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

        private string GetMemberFilters()
        {
            string federationNrUrl = $"federationNr={memberFilterFederationNr.Text}";
            string lastNameUrl = $"lastName={memberFilterLastName.Text}";
            string firstNameUrl = $"firstName={memberFilterFirstName.Text}";
            string locationUrl = $"location={memberFilterLocation.Text}";
            return $"{(string.IsNullOrEmpty(memberFilterFederationNr.Text) ? "" : federationNrUrl)}&{(string.IsNullOrEmpty(memberFilterLastName.Text) ? "" : lastNameUrl)}&{(string.IsNullOrEmpty(memberFilterFirstName.Text) ? "" : firstNameUrl)}&{(string.IsNullOrEmpty(memberFilterLocation.Text) ? "" : locationUrl)}";
        }

        /*
         * Event Handlers
         */

        private void SearchFilteredMembers_Click(object sender, RoutedEventArgs e)
        {
            ReadMembers();
        }


        private void MemberData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private void MemberData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            MemberReadDTO selectedItem = GetSelectedMember();
            if (!selectedItem.IsNull())
            {
                memberAddition.Text = selectedItem.Addition;
                memberAddress.Text = selectedItem.Address;
                memberBirthDate.SelectedDate = selectedItem.BirthDate;
                memberCity.Text = selectedItem.City;
                memberFirstName.Text = selectedItem.FirstName;
                memberGender.SelectedValue = selectedItem.GenderId;
                memberLastName.Text = selectedItem.LastName;
                memberNumber.Text = selectedItem.Number;
                memberPhoneNr.Text = selectedItem.PhoneNr;
                memberZipcode.Text = selectedItem.Zipcode;
                memberFederationNr.Text = selectedItem.FederationNr;
            }
        }

        private MemberReadDTO GetSelectedMember()
        {
            if (MemberData.SelectedItem.IsNull()) return null;

            return (MemberReadDTO)MemberData.SelectedItem;
        }

        private void SyncMembersButton_Click(object sender, RoutedEventArgs e)
        {
            SynchroniseMemberTable();
        }

        private void SynchroniseMemberTable()
        {
            bool isSucceeded = false;
            var datagrid = MemberData.ItemsSource.OfType<MemberReadDTO>().ToList();
            for (int i = 0; i < originalMemberList.Count; i++)
            {
                var originalItem = originalMemberList.ElementAt(i);
                MemberReadDTO memberItem = datagrid.Find(x => x.Id == originalItem.Id);

                if (originalItem == null && memberItem != null)
                {
                    isSucceeded = CreateMember(memberItem);
                }
                else if (originalItem != null && memberItem == null)
                {
                    isSucceeded = DeleteMember(originalItem.Id);
                }
                else if (!originalItem.Equals(memberItem))
                {
                    isSucceeded = UpdateMember(originalItem.Id, new MemberUpdateDTO { Addition = memberItem.Addition, Address = memberItem.Address, BirthDate = memberItem.BirthDate, City = memberItem.City, FederationNr = memberItem.FederationNr, FirstName = memberItem.FirstName, GenderId = memberItem.GenderId, LastName = memberItem.LastName, Number = memberItem.Number, PhoneNr = memberItem.PhoneNr, Zipcode = memberItem.Zipcode });
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
                ReadMembers();
            }
            else
            {
                MessageBox.Show("Er is een fout gebeurd bij het synchroniseren. Probeer dit opnieuw.");
            }
        }

        private bool DeleteMember(int id)
        {
            Task<HttpResponseMessage> response = WebAPI.DeleteCall($"members/{id}");

            if (response.Result.StatusCode == HttpStatusCode.NoContent)
            {
                Debug.WriteLine($"{id} is verwijderd.");
                return true;
            }
            else
            {
                Debug.WriteLine($"Er is iets foutgelopen.");
                return false;
            }
        }

        private bool UpdateMember(int id, MemberUpdateDTO memberUpdateDTO)
        {
            // throw new NotImplementedException();
            return true;
        }

        private bool CreateMember(MemberReadDTO memberItem)
        {
            MemberCreateDTO newMember = new MemberCreateDTO
            {
                Addition = memberItem.Addition,
                Address = memberItem.Address,
                BirthDate = memberItem.BirthDate,
                City = memberItem.City,
                FederationNr= memberItem.FederationNr,
                FirstName= memberItem.FirstName,
                GenderId = memberItem.GenderId,
                LastName = memberItem.LastName,
                Number = memberItem.Number,
                PhoneNr = memberItem.PhoneNr,
                Zipcode = memberItem.Zipcode
            };
            Task<HttpResponseMessage> response = WebAPI.PostCall("members", newMember);

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                Debug.WriteLine($"{newMember.FirstName + " " + newMember.LastName} is toegevoegd!");
                return true;
            }
            else
            {
                Debug.WriteLine($"Er is iets foutgelopen.");
                return false;
            }
        }

        private void GetMembersButton_Click(object sender, RoutedEventArgs e)
        {
            ReadMembers();
        }

        private void ClearMemberFilterButton_Click(object sender, RoutedEventArgs e)
        {
            memberFilterFederationNr.Text = "";
            memberFilterLastName.Text = "";
            memberFilterFirstName.Text = "";
            memberFilterLocation.Text = "";
            ReadMembers();
        }

        private void MemberGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MemberReadDTO member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.GenderName = memberGender.Text;
            member.GenderId = (int)memberGender.SelectedValue;
            MemberData.Items.Refresh();
        }

        private void MemberFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            MemberReadDTO member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.FirstName = memberFirstName.Text;
            MemberData.Items.Refresh();
        }

        private void MemberLastName_KeyDown(object sender, KeyEventArgs e)
        {
            MemberReadDTO member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.LastName = memberLastName.Text;
            MemberData.Items.Refresh();
        }

        private void MemberBirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MemberReadDTO member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }
            DateTime? selectedDate = memberBirthDate.SelectedDate;

            if (selectedDate.IsNull())
            {
                return;
            }

            member.BirthDate = selectedDate.Value;
            MemberData.Items.Refresh();
        }

        private void MemberAddress_KeyDown(object sender, KeyEventArgs e)
        {
            MemberReadDTO member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.Address = memberAddress.Text;
            MemberData.Items.Refresh();
        }

        private void MemberNumber_KeyDown(object sender, KeyEventArgs e)
        {
            MemberReadDTO member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.Number = memberNumber.Text;
            MemberData.Items.Refresh();
        }

        private void MemberAddition_KeyDown(object sender, KeyEventArgs e)
        {
            MemberReadDTO member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.Addition = memberAddition.Text;
            MemberData.Items.Refresh();
        }

        private void MemberZipcode_KeyDown(object sender, KeyEventArgs e)
        {
            MemberReadDTO member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.Zipcode = memberZipcode.Text;
            MemberData.Items.Refresh();
        }

        private void MemberCity_KeyDown(object sender, KeyEventArgs e)
        {
            MemberReadDTO member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.City = memberCity.Text;
            MemberData.Items.Refresh();
        }

        private void MemberPhoneNr_KeyDown(object sender, KeyEventArgs e)
        {
            MemberReadDTO member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.PhoneNr = memberPhoneNr.Text;
            MemberData.Items.Refresh();
        }

        private void MemberFederationNr_KeyDown(object sender, KeyEventArgs e)
        {
            MemberReadDTO member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.FederationNr = memberFederationNr.Text;
            MemberData.Items.Refresh();
        }

        #endregion

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            var newMember = new MemberReadDTO
            {
                Addition = memberAddition.Text,
                Address = memberAddress.Text,
                BirthDate = memberBirthDate.SelectedDate.Value,
                City = memberCity.Text,
                FederationNr = memberFederationNr.Text,
                FirstName = memberFirstName.Text,
                GenderId = (int)memberGender.SelectedValue,
                GenderName = memberGender.Text,
                LastName = memberLastName.Text,
                Number = memberNumber.Text,
                PhoneNr = memberPhoneNr.Text,
                Zipcode = memberZipcode.Text,
                Deleted = false,
            };
            MemberData.Items.Add(newMember);
        }
    }
}