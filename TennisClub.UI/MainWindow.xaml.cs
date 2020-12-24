using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TennisClub.Common.Game;
using TennisClub.Common.GameResult;
using TennisClub.Common.Gender;
using TennisClub.Common.League;
using TennisClub.Common.Member;
using TennisClub.Common.MemberRole;
using TennisClub.Common.Role;

namespace TennisClub.UI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
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
            ReadAllMembers();
            ReadActiveMembers();
            ReadAllActiveSpelerMembers();
            ReadGenders();
            ReadLeagues();
            ReadGameResults();
            ReadMemberRoles();
            ReadGames();
        }

        #region Genders

        /*
         * CRUD
         */
        private bool ReadGenders()
        {
            var result = WebAPI.GetCall("genders");

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                var tmp = result.Result.Content.ReadAsAsync<List<GenderReadDTO>>().Result;
                MemberGender.ItemsSource = tmp.ToList();
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        #endregion

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #region Roles

        private List<RoleReadDTO> originalRoleList;

        /*
         * CRUD
         */
        private bool CreateRole(RoleReadDTO item)
        {
            var newRole = new RoleCreateDTO
            {
                Name = item.Name
            };
            var response = WebAPI.PostCall("roles", newRole);

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                Debug.WriteLine($"{newRole.Name} is toegevoegd!");
                return true;
            }

            Debug.WriteLine("Er is iets foutgelopen.");
            return false;
        }

        private bool ReadRoles()
        {
            var result = WebAPI.GetCall("roles");
            var itemsControl = RoleData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                var tmp = result.Result.Content.ReadAsAsync<List<RoleReadDTO>>().Result;
                ;
                itemsControl.ItemsSource = tmp;
                var tmp2 = new List<RoleReadDTO>(tmp.Count);
                tmp.ForEach(item => { tmp2.Add(new RoleReadDTO {Id = item.Id, Name = item.Name}); });
                originalRoleList = tmp2;
                MemberRoleRolesFilter.ItemsSource = tmp2;
                MemberRoleRole.ItemsSource = tmp2;
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        private bool UpdateRole(int id, RoleUpdateDTO item)
        {
            var result = WebAPI.PutCall($"roles/{id}", item);

            if (result.Result.StatusCode == HttpStatusCode.NoContent)
            {
                Debug.WriteLine("Updated!");
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
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

        /*
         * Methods
         */

        private void SynchroniseRoleTable()
        {
            var isSucceeded = false;

            for (var i = 0; i < RoleData.Items.Count - 1; i++)
            {
                var item = RoleData.Items[i];
                var roleItem = (RoleReadDTO) item;
                var originalItem = originalRoleList.Find(x => x.Id == roleItem.Id);

                if (originalItem.IsNull() && !roleItem.IsNull())
                    isSucceeded = CreateRole(roleItem);
                else if (!roleItem.Equals(originalItem))
                    isSucceeded = UpdateRole(roleItem.Id, new RoleUpdateDTO {Name = roleItem.Name});
                else
                    isSucceeded = true;

                if (!isSucceeded) break;
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

        #endregion

        #region Leagues

        /*
         * CRUD
         */
        private bool ReadLeagues()
        {
            var result = WebAPI.GetCall("leagues");
            var itemsControl = LeagueData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                var tmp = result.Result.Content.ReadAsAsync<List<LeagueReadDTO>>().Result;
                itemsControl.ItemsSource = tmp;
                var tmp2 = new List<LeagueReadDTO>(tmp.Count);
                tmp.ForEach(item =>
                {
                    tmp2.Add(new LeagueReadDTO
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                });
                GameLeague.ItemsSource = tmp2;
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        /*
         * Event Handlers
         */
        private void GetLeaguesButton_Click(object sender, RoutedEventArgs e)
        {
            ReadLeagues();
        }

        #endregion

        #region GameResults

        /*
         * CRUD
         */

        private List<GameResultReadDTO> originalGameResultList;

        private bool CreateGameResult(GameResultReadDTO item)
        {
            var newGameResult = new GameResultCreateDTO
            {
                GameId = item.GameId,
                ScoreOpponent = item.ScoreOpponent,
                ScoreTeamMember = item.ScoreTeamMember,
                SetNr = item.SetNr
            };
            var response = WebAPI.PostCall("gameresults", newGameResult);

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                Debug.WriteLine($"{newGameResult.GameId} is toegevoegd!");
                return true;
            }

            Debug.WriteLine("Er is iets foutgelopen.");
            return false;
        }

        private bool ReadGameResults()
        {
            var result = WebAPI.GetCall($"gameresults?{GetGameResultFilters()}");
            var itemsControl = GameResultData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                var tmp = result.Result.Content.ReadAsAsync<List<GameResultReadDTO>>().Result;
                ;
                itemsControl.ItemsSource = tmp;
                var tmp2 = new List<GameResultReadDTO>(tmp.Count);
                tmp.ForEach(item =>
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

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        private bool UpdateGameResult(int id, GameResultUpdateDTO item)
        {
            var result = WebAPI.PutCall($"gameresults/{id}", item);

            if (result.Result.StatusCode == HttpStatusCode.NoContent)
            {
                Debug.WriteLine("Updated!");
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        /*
         * Event Handlers
         */

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
            GameResultPlayerComboBoxFilter.SelectedItem = null;
            GameResultdatePickerFilter.SelectedDate = null;
            ReadGameResults();
        }

        private void SearchFilteredGameResults_Click(object sender, RoutedEventArgs e)
        {
            ReadGameResults();
        }

        /*
         * Methods
         */

        private void SynchroniseGameResultTable()
        {
            var isSucceeded = false;

            for (var i = 0; i < GameResultData.Items.Count - 1; i++)
            {
                var item = GameResultData.Items[i];
                var gameResultItem = (GameResultReadDTO) item;
                var originalItem = originalGameResultList.Find(x => x.Id == gameResultItem.Id);

                if (originalItem.IsNull() && !gameResultItem.IsNull())
                    isSucceeded = CreateGameResult(gameResultItem);
                else if (!gameResultItem.Equals(originalItem))
                    isSucceeded = UpdateGameResult(gameResultItem.Id,
                        new GameResultUpdateDTO
                        {
                            ScoreOpponent = gameResultItem.ScoreOpponent, SetNr = gameResultItem.SetNr,
                            ScoreTeamMember = gameResultItem.ScoreTeamMember
                        });
                else
                    isSucceeded = true;

                if (!isSucceeded) break;
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

        private string GetGameResultFilters()
        {
            var memberIdUrl = $"memberId={GameResultPlayerComboBoxFilter.SelectedValue}";
            var selectedDateUrl =
                $"date={GameResultdatePickerFilter.SelectedDate.GetValueOrDefault().ToShortDateString()}";

            return
                $"{(!GameResultPlayerComboBoxFilter.SelectedValue.IsNull() ? memberIdUrl : "")}&{(!GameResultdatePickerFilter.SelectedDate.IsNull() ? selectedDateUrl : "")}";
        }

        #endregion

        #region Members

        private List<MemberReadDTO> originalMemberList;

        /*
         * CRUD
         */
        private bool CreateMember(MemberReadDTO memberItem)
        {
            var newMember = new MemberCreateDTO
            {
                Addition = memberItem.Addition,
                Address = memberItem.Address,
                BirthDate = memberItem.BirthDate,
                City = memberItem.City,
                FederationNr = memberItem.FederationNr,
                FirstName = memberItem.FirstName,
                GenderId = memberItem.GenderId,
                LastName = memberItem.LastName,
                Number = memberItem.Number,
                PhoneNr = memberItem.PhoneNr,
                Zipcode = memberItem.Zipcode
            };
            var response = WebAPI.PostCall("members", newMember);

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                Debug.WriteLine($"{newMember.FirstName + " " + newMember.LastName} is toegevoegd!");
                return true;
            }

            Debug.WriteLine("Er is iets foutgelopen.");
            return false;
        }

        private void ReadAllMembers()
        {
            var result = WebAPI.GetCall("members");

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                var tmp = result.Result.Content.ReadAsAsync<List<MemberReadDTO>>().Result;
                MemberRoleMember.ItemsSource = tmp;
                MemberRoleMemberFilter.ItemsSource = tmp;
                GameMemberFilter.ItemsSource = tmp;
                GameResultPlayerComboBoxFilter.ItemsSource = tmp;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
            }
        }

        private void ReadAllActiveSpelerMembers()
        {
            var result = WebAPI.GetCall("members/active/speler");

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                var tmp = result.Result.Content.ReadAsAsync<List<MemberReadDTO>>().Result;
                GameMember.ItemsSource = tmp;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
            }
        }

        private bool ReadActiveMembers()
        {
            var result = WebAPI.GetCall($"members/active?{GetMemberFilters()}");
            var itemsControl = MemberData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                var tmp = result.Result.Content.ReadAsAsync<List<MemberReadDTO>>().Result;
                ;
                itemsControl.ItemsSource = tmp;
                var tmp2 = new List<MemberReadDTO>(tmp.Count);
                tmp.ForEach(item =>
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
                MemberRoleMemberFilter.ItemsSource = tmp2;
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        private bool UpdateMember(int id, MemberUpdateDTO memberUpdateDTO)
        {
            var result = WebAPI.PutCall($"members/{id}", memberUpdateDTO);

            if (result.Result.StatusCode == HttpStatusCode.NoContent)
            {
                Debug.WriteLine("Updated!");
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        private bool DeleteMember(int id)
        {
            var response = WebAPI.DeleteCall($"members/{id}");

            if (response.Result.StatusCode == HttpStatusCode.NoContent)
            {
                Debug.WriteLine($"{id} is verwijderd.");
                return true;
            }

            Debug.WriteLine("Er is iets foutgelopen.");
            return false;
        }

        /*
         * Event Handlers
         */
        private void SearchFilteredMembers_Click(object sender, RoutedEventArgs e)
        {
            ReadActiveMembers();
        }

        private void MemberData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private void MemberData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var selectedItem = GetSelectedMember();
            if (!selectedItem.IsNull())
            {
                MemberAddition.Text = selectedItem.Addition;
                MemberAddress.Text = selectedItem.Address;
                MemberBirthDate.SelectedDate = selectedItem.BirthDate;
                MemberCity.Text = selectedItem.City;
                MemberFirstName.Text = selectedItem.FirstName;
                MemberGender.SelectedValue = selectedItem.GenderId;
                MemberLastName.Text = selectedItem.LastName;
                MemberNumber.Text = selectedItem.Number;
                MemberPhoneNr.Text = selectedItem.PhoneNr;
                MemberZipcode.Text = selectedItem.Zipcode;
                MemberFederationNr.Text = selectedItem.FederationNr;
            }
        }

        private void SyncMembersButton_Click(object sender, RoutedEventArgs e)
        {
            SynchroniseMemberTable();
        }

        private void GetMembersButton_Click(object sender, RoutedEventArgs e)
        {
            ReadActiveMembers();
        }

        private void ClearMemberFilterButton_Click(object sender, RoutedEventArgs e)
        {
            MemberFilterFederationNr.Text = "";
            MemberFilterLastName.Text = "";
            MemberFilterFirstName.Text = "";
            MemberFilterLocation.Text = "";
            ReadActiveMembers();
        }

        private void MemberGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var member = GetSelectedMember();

            if (member.IsNull()) return;

            var gender = (GenderReadDTO) MemberGender.SelectedItem;
            member.GenderName = gender.Name;
            member.GenderId = gender.Id;
            MemberData.Items.Refresh();
        }

        /*
         * Methods
         */
        private void SynchroniseMemberTable()
        {
            var isSucceeded = false;
            var memberData = MemberData.ItemsSource.OfType<MemberReadDTO>().ToList();

            for (var i = 0; i < originalMemberList.Count; i++)
            {
                var originalItem = originalMemberList.ElementAt(i);
                ;
                var memberItem = memberData.Find(x => x.Id == originalItem.Id);

                if (!originalItem.IsNull() && memberItem.IsNull())
                    isSucceeded = DeleteMember(originalItem.Id);
                else if (originalItem.IsNull() && !memberItem.IsNull())
                    isSucceeded = CreateMember(memberItem);
                else if (!originalItem.Equals(memberItem))
                    isSucceeded = UpdateMember(originalItem.Id,
                        new MemberUpdateDTO
                        {
                            Addition = memberItem.Addition, Address = memberItem.Address,
                            BirthDate = memberItem.BirthDate, City = memberItem.City,
                            FederationNr = memberItem.FederationNr, FirstName = memberItem.FirstName,
                            GenderId = memberItem.GenderId, LastName = memberItem.LastName, Number = memberItem.Number,
                            PhoneNr = memberItem.PhoneNr, Zipcode = memberItem.Zipcode
                        });
                else
                    isSucceeded = true;

                if (!isSucceeded) break;
            }

            if (isSucceeded)
            {
                MessageBox.Show("De tabel is succesvol gesynchroniseerd met de database!");
                ReadActiveMembers();
            }
            else
            {
                MessageBox.Show("Er is een fout gebeurd bij het synchroniseren. Probeer dit opnieuw.");
            }
        }

        private string GetMemberFilters()
        {
            var federationNrUrl = $"federationNr={MemberFilterFederationNr.Text}";
            var lastNameUrl = $"lastName={MemberFilterLastName.Text}";
            var firstNameUrl = $"firstName={MemberFilterFirstName.Text}";
            var locationUrl = $"location={MemberFilterLocation.Text}";
            return
                $"{(string.IsNullOrEmpty(MemberFilterFederationNr.Text) ? "" : federationNrUrl)}&{(string.IsNullOrEmpty(MemberFilterLastName.Text) ? "" : lastNameUrl)}&{(string.IsNullOrEmpty(MemberFilterFirstName.Text) ? "" : firstNameUrl)}&{(string.IsNullOrEmpty(MemberFilterLocation.Text) ? "" : locationUrl)}";
        }

        private MemberReadDTO GetSelectedMember()
        {
            if (MemberData.SelectedItem.IsNull()) return null;

            return (MemberReadDTO) MemberData.SelectedItem;
        }

        private void MemberFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var member = GetSelectedMember();

            if (member.IsNull()) return;

            member.FirstName = MemberFirstName.Text;
            MemberData.Items.Refresh();
        }

        private void MemberLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var member = GetSelectedMember();

            if (member.IsNull()) return;

            member.LastName = MemberLastName.Text;
            MemberData.Items.Refresh();
        }

        private void MemberAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            var member = GetSelectedMember();

            if (member.IsNull()) return;

            member.Address = MemberAddress.Text;
            MemberData.Items.Refresh();
        }

        private void MemberNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            var member = GetSelectedMember();

            if (member.IsNull()) return;

            member.Number = MemberNumber.Text;
            MemberData.Items.Refresh();
        }

        private void MemberAddition_TextChanged(object sender, TextChangedEventArgs e)
        {
            var member = GetSelectedMember();

            if (member.IsNull()) return;

            member.Addition = MemberAddition.Text;
            MemberData.Items.Refresh();
        }

        private void MemberZipcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            var member = GetSelectedMember();

            if (member.IsNull()) return;

            member.Zipcode = MemberZipcode.Text;
            MemberData.Items.Refresh();
        }

        private void MemberCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            var member = GetSelectedMember();

            if (member.IsNull()) return;

            member.City = MemberCity.Text;
            MemberData.Items.Refresh();
        }

        private void MemberPhoneNr_TextChanged(object sender, TextChangedEventArgs e)
        {
            var member = GetSelectedMember();

            if (member.IsNull()) return;

            member.PhoneNr = MemberPhoneNr.Text;
            MemberData.Items.Refresh();
        }

        private void MemberFederationNr_TextChanged(object sender, TextChangedEventArgs e)
        {
            var member = GetSelectedMember();

            if (member.IsNull()) return;

            member.FederationNr = MemberFederationNr.Text;
            MemberData.Items.Refresh();
        }

        private void MemberBirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var member = GetSelectedMember();

            if (member.IsNull()) return;
            var selectedDate = MemberBirthDate.SelectedDate;

            if (selectedDate.IsNull()) return;

            member.BirthDate = selectedDate.Value;
            MemberData.Items.Refresh();
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            var birthDate = MemberBirthDate.SelectedDate;

            if (!birthDate.IsNull())
            {
                var gender = (GenderReadDTO) MemberGender.SelectedItem;
                var newMember = new MemberReadDTO
                {
                    Addition = MemberAddition.Text,
                    Address = MemberAddress.Text,
                    BirthDate = birthDate.Value,
                    City = MemberCity.Text,
                    FederationNr = MemberFederationNr.Text,
                    FirstName = MemberFirstName.Text,
                    GenderId = gender.Id,
                    GenderName = gender.Name,
                    LastName = MemberLastName.Text,
                    Number = MemberNumber.Text,
                    PhoneNr = MemberPhoneNr.Text,
                    Zipcode = MemberZipcode.Text,
                    Deleted = false
                };

                var newList = MemberData.ItemsSource.OfType<MemberReadDTO>().ToList();
                newList.Add(newMember);
                MemberData.ItemsSource = newList;

                MemberData.Items.Refresh();
            }
        }

        #endregion

        #region MemberRoles

        private List<MemberRoleReadDTO> originalMemberRoleList;

        /*
         * CRUD
         */
        private bool CreateMemberRole(MemberRoleReadDTO memberRoleItem)
        {
            var newMemberRole = new MemberRoleCreateDTO
            {
                MemberId = memberRoleItem.MemberId,
                RoleId = memberRoleItem.RoleId,
                StartDate = memberRoleItem.StartDate
            };
            var response = WebAPI.PostCall("memberroles", newMemberRole);

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                Debug.WriteLine($"({newMemberRole.MemberId}-{newMemberRole.RoleId}) is toegevoegd!");
                return true;
            }

            Debug.WriteLine("Er is iets foutgelopen.");
            return false;
        }

        private bool ReadMemberRoles()
        {
            var result = WebAPI.GetCall($"memberroles{GetMemberRoleFilters()}");
            var itemsControl = MemberRoleData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                var tmp = result.Result.Content.ReadAsAsync<List<MemberRoleReadDTO>>().Result;
                itemsControl.ItemsSource = tmp;
                var tmp2 = new List<MemberRoleReadDTO>(tmp.Count);
                tmp.ForEach(item =>
                {
                    tmp2.Add(new MemberRoleReadDTO
                    {
                        Id = item.Id,
                        EndDate = item.EndDate,
                        MemberFullName = item.MemberFullName,
                        MemberId = item.MemberId,
                        RoleId = item.RoleId,
                        RoleName = item.RoleName,
                        StartDate = item.StartDate
                    });
                });
                originalMemberRoleList = tmp2;
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        private bool UpdateMemberRole(int id, MemberRoleUpdateDTO memberRoleUpdateDTO)
        {
            var result = WebAPI.PutCall($"memberroles/{id}", memberRoleUpdateDTO);

            if (result.Result.StatusCode == HttpStatusCode.NoContent)
            {
                Debug.WriteLine("Updated!");
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        /*
         * Event Handlers
         */
        private void ClearMemberRoleFilterButton_Click(object sender, RoutedEventArgs e)
        {
            MemberRoleMemberFilter.SelectedItem = null;
            MemberRoleRolesFilter.UnselectAll();
            ReadMemberRoles();
        }

        private void GetMemberRolesButton_Click(object sender, RoutedEventArgs e)
        {
            ReadMemberRoles();
        }

        private void SyncMemberRolesButton_Click(object sender, RoutedEventArgs e)
        {
            SynchroniseMemberRoleTable();
        }

        private void AddMemberRoleButton_Click(object sender, RoutedEventArgs e)
        {
            var startDate = MemberRoleStartDate.SelectedDate;
            var endDate = MemberRoleEndDate.SelectedDate;
            var member = (MemberReadDTO) MemberRoleMember.SelectedItem;
            var role = (RoleReadDTO) MemberRoleRole.SelectedItem;

            if (!startDate.IsNull() && !member.IsNull() && !role.IsNull())
            {
                var newMemberRole = new MemberRoleReadDTO
                {
                    Id = 0,
                    StartDate = startDate.Value,
                    MemberId = member.Id,
                    RoleId = role.Id,
                    MemberFullName = member.FullName,
                    RoleName = role.Name
                };

                if (!endDate.IsNull())
                    newMemberRole.EndDate = endDate.Value;

                var newList = MemberRoleData.ItemsSource.OfType<MemberRoleReadDTO>().ToList();
                newList.Add(newMemberRole);
                MemberRoleData.ItemsSource = newList;

                MemberRoleData.Items.Refresh();
            }
        }

        private void FilterMemberRolesButton_Click(object sender, RoutedEventArgs e)
        {
            ReadMemberRoles();
        }

        private void MemberRoleMemberFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MemberRoleRolesFilter.UnselectAll();
        }

        private void MemberRoleRolesFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MemberRoleMemberFilter.SelectedItem = null;
        }

        private void MemberRoleMember_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var memberRole = GetSelectedMemberRole();

            if (memberRole.IsNull()) return;

            if (MemberRoleMember.SelectedItem.IsNull()) return;

            var member = (MemberReadDTO) MemberRoleMember.SelectedItem;
            memberRole.MemberId = member.Id;
            memberRole.MemberFullName = member.FullName;

            MemberRoleData.Items.Refresh();
        }

        private void MemberRoleRole_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var memberRole = GetSelectedMemberRole();

            if (memberRole.IsNull()) return;

            if (MemberRoleRole.SelectedItem.IsNull()) return;

            var role = (RoleReadDTO) MemberRoleRole.SelectedItem;
            memberRole.RoleId = role.Id;
            memberRole.RoleName = role.Name;
            MemberRoleData.Items.Refresh();
        }

        private void MemberRoleStartDate_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            var memberRole = GetSelectedMemberRole();

            if (memberRole.IsNull()) return;

            if (!MemberRoleStartDate.SelectedDate.HasValue) return;

            memberRole.StartDate = MemberRoleStartDate.SelectedDate.Value;
            MemberRoleData.Items.Refresh();
        }

        private void MemberRoleEndDate_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            var memberRole = GetSelectedMemberRole();

            if (memberRole.IsNull()) return;

            if (!MemberRoleEndDate.SelectedDate.HasValue) return;

            memberRole.EndDate = MemberRoleEndDate.SelectedDate.Value;
            MemberRoleData.Items.Refresh();
        }

        private void MemberRoleData_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var memberRole = GetSelectedMemberRole();

            if (memberRole.IsNull()) return;

            MemberRoleMember.SelectedValue = memberRole.MemberId;
            MemberRoleRole.SelectedValue = memberRole.RoleId;
            MemberRoleStartDate.SelectedDate = memberRole.StartDate;
            MemberRoleEndDate.SelectedDate = memberRole.EndDate;

            var isNewMember = originalMemberRoleList.Contains(memberRole);

            SetMemberRoleBoxEnable(isNewMember);
        }

        private void ClearMemberRoleSelectionButton_OnClick(object sender, RoutedEventArgs e)
        {
            MemberRoleData.UnselectAll();

            MemberRoleMember.SelectedValue = null;
            MemberRoleRole.SelectedValue = null;
            MemberRoleStartDate.SelectedDate = null;
            MemberRoleEndDate.SelectedDate = null;

            SetMemberRoleBoxEnable(true);
        }

        /*
         * Methods
         */
        private void SynchroniseMemberRoleTable()
        {
            var isSucceeded = false;

            for (var i = 0; i < MemberRoleData.Items.Count; i++)
            {
                var item = MemberRoleData.Items[i];
                var memberRoleItem = (MemberRoleReadDTO) item;
                var originalItem = originalMemberRoleList.Find(x => x.Id == memberRoleItem.Id);

                if (!originalItem.IsNull() && memberRoleItem.IsNull())
                {
                    //
                }
                else if (originalItem.IsNull() && !memberRoleItem.IsNull())
                {
                    isSucceeded = CreateMemberRole(memberRoleItem);
                }
                else if (originalItem.MemberId == memberRoleItem.MemberId &&
                         originalItem.RoleId == memberRoleItem.RoleId &&
                         originalItem.StartDate.Equals(memberRoleItem.StartDate) &&
                         !originalItem.EndDate.Equals(memberRoleItem.EndDate))
                {
                    isSucceeded = UpdateMemberRole(originalItem.Id, new MemberRoleUpdateDTO
                    {
                        EndDate = memberRoleItem.EndDate
                    });
                }
                else
                {
                    isSucceeded = true;
                }

                if (!isSucceeded) break;
            }

            if (isSucceeded)
            {
                MessageBox.Show("De tabel is succesvol gesynchroniseerd met de database!");
                ReadActiveMembers();
            }
            else
            {
                MessageBox.Show("Er is een fout gebeurd bij het synchroniseren. Probeer dit opnieuw.");
            }
        }

        private void SetMemberRoleBoxEnable(bool isEnabled)
        {
            MemberRoleRole.IsEnabled = isEnabled;
            MemberRoleMember.IsEnabled = isEnabled;
            MemberRoleStartDate.IsEnabled = isEnabled;
        }

        private string GetMemberRoleFilters()
        {
            var result = "";

            if (!MemberRoleMemberFilter.SelectedItem.IsNull())
            {
                result += $"/bymemberid/{MemberRoleMemberFilter.SelectedValue}";
            }
            else if (MemberRoleRolesFilter.SelectedItems.Count > 0)
            {
                result += "/byroleids/";
                foreach (var item in MemberRoleRolesFilter.SelectedItems)
                {
                    var role = (RoleReadDTO) item;
                    if (!role.IsNull())
                        result += role.Id + ",";
                }
            }

            return result;
        }

        private MemberRoleReadDTO GetSelectedMemberRole()
        {
            if (MemberRoleData.SelectedItem.IsNull()) return null;

            var memberRole = (MemberRoleReadDTO) MemberRoleData.SelectedItem;

            return memberRole;
        }

        #endregion

        #region Games

        private List<GameReadDTO> originalGameList;

        /*
         * CRUD
         */
        private bool CreateGame(GameReadDTO gameReadDto)
        {
            var newGame = new GameCreateDTO
            {
                Date = gameReadDto.Date,
                GameNumber = gameReadDto.GameNumber,
                LeagueId = gameReadDto.LeagueId,
                MemberId = gameReadDto.MemberId
            };
            var response = WebAPI.PostCall("games", newGame);

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                Debug.WriteLine($"({newGame.GameNumber}) is toegevoegd!");
                return true;
            }

            Debug.WriteLine("Er is iets foutgelopen.");
            return false;
        }

        private bool ReadGames()
        {
            var result = WebAPI.GetCall($"games{GetGamesFilter()}");
            var itemsControl = GameData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                var tmp = result.Result.Content.ReadAsAsync<List<GameReadDTO>>().Result;
                itemsControl.ItemsSource = tmp;
                var tmp2 = new List<GameReadDTO>(tmp.Count);
                tmp.ForEach(item =>
                {
                    tmp2.Add(new GameReadDTO
                    {
                        Id = item.Id, MemberId = item.MemberId, LeagueId = item.LeagueId,
                        MemberFullName = item.MemberFullName, LeagueName = item.LeagueName, Date = item.Date,
                        GameNumber = item.GameNumber
                    });
                });
                originalGameList = tmp2;
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        private bool UpdateGame(int id, GameUpdateDTO gameUpdateDto)
        {
            var result = WebAPI.PutCall($"games/{id}", gameUpdateDto);

            if (result.Result.StatusCode == HttpStatusCode.NoContent)
            {
                Debug.WriteLine("Updated!");
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        private bool DeleteGame(int id)
        {
            var response = WebAPI.DeleteCall($"games/{id}");

            if (response.Result.StatusCode == HttpStatusCode.NoContent)
            {
                Debug.WriteLine($"{id} is verwijderd.");
                return true;
            }

            Debug.WriteLine("Er is iets foutgelopen.");
            return false;
        }

        /*
         * Event Handlers
         */


        private void GameData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var game = GetSelectedGame();

            if (game.IsNull()) return;

            GameDate.SelectedDate = game.Date;
            GameMember.SelectedValue = game.MemberId;
            GameLeague.SelectedValue = game.LeagueId;
            GameNumber.Text = game.GameNumber;
        }

        private void GameLeague_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var game = GetSelectedGame();

            if (game.IsNull()) return;

            if (GameMember.SelectedItem.IsNull()) return;

            LeagueReadDTO member = (LeagueReadDTO) GameLeague.SelectedItem;
            game.LeagueName = member.Name;
            game.LeagueId = member.Id;
            GameData.Items.Refresh();
        }

        private void GameMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var game = GetSelectedGame();

            if (game.IsNull()) return;

            if (GameMember.SelectedItem.IsNull()) return;

            MemberReadDTO member = (MemberReadDTO) GameMember.SelectedItem;
            game.MemberFullName = member.FullName;
            game.MemberId = member.Id;
            GameData.Items.Refresh();
        }

        private void GameDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var game = GetSelectedGame();

            if (game.IsNull()) return;

            if (!GameDate.SelectedDate.HasValue) return;

            game.Date = GameDate.SelectedDate.Value;
            GameData.Items.Refresh();
        }

        private void ClearGameFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            GameMemberFilter.SelectedItem = null;
            ReadGames();
        }

        private void FilterGamesButton_OnClick(object sender, RoutedEventArgs e)
        {
            ReadGames();
        }

        private void GetGamesButton_OnClick(object sender, RoutedEventArgs e)
        {
            ReadGames();
        }

        private void ClearGameSelectionButton_OnClick(object sender, RoutedEventArgs e)
        {
            GameData.UnselectAll();

            GameLeague.SelectedValue = null;
            GameMember.SelectedValue = null;
            GameNumber.Text = "";
            GameDate.SelectedDate = null;

            SetMemberRoleBoxEnable(true);
        }

        private void AddGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            var date = GameDate.SelectedDate;

            if (!date.IsNull())
            {
                var newGame = new GameReadDTO
                {
                    Id = 0,
                    GameNumber = GameNumber.Text,
                    Date = date.Value
                };

                if (!GameMember.SelectedItem.IsNull() && !GameMember.SelectedValue.IsNull())
                {
                    var member = (MemberReadDTO) GameMember.SelectedItem;
                    newGame.MemberFullName = member.FullName;
                    newGame.MemberId = member.Id;
                }

                if (!GameLeague.SelectedItem.IsNull() && !GameLeague.SelectedValue.IsNull())
                {
                    var league = (LeagueReadDTO) GameLeague.SelectedItem;
                    newGame.LeagueName = league.Name;
                    newGame.LeagueId = league.Id;
                }

                var newList = GameData.ItemsSource.OfType<GameReadDTO>().ToList();
                newList.Add(newGame);
                GameData.ItemsSource = newList;

                GameData.Items.Refresh();
            }
        }

        private void SyncGamesButton_OnClick(object sender, RoutedEventArgs e)
        {
            SynchroniseGameTable();
        }

        /*
         * Methods
         */
        private void SynchroniseGameTable()
        {
            var isSucceeded = false;
            var gameData = GameData.ItemsSource.OfType<GameReadDTO>().ToList();

            for (var i = 0; i < originalGameList.Count; i++)
            {
                var originalItem = originalGameList.ElementAt(i);
                ;
                var gameItem = gameData.Find(x => x.Id == originalItem.Id);

                if (!originalItem.IsNull() && gameItem.IsNull())
                    isSucceeded = DeleteGame(originalItem.Id);
                else if (originalItem.IsNull() && !gameItem.IsNull())
                    isSucceeded = CreateGame(gameItem);
                else if (!originalItem.Equals(gameItem))
                    isSucceeded = UpdateGame(originalItem.Id,
                        new GameUpdateDTO
                        {
                            Date = gameItem.Date, GameNumber = gameItem.GameNumber, LeagueId = gameItem.LeagueId,
                            MemberId = gameItem.MemberId
                        });
                else
                    isSucceeded = true;

                if (!isSucceeded) break;
            }

            if (isSucceeded)
            {
                MessageBox.Show("De tabel is succesvol gesynchroniseerd met de database!");
                ReadActiveMembers();
            }
            else
            {
                MessageBox.Show("Er is een fout gebeurd bij het synchroniseren. Probeer dit opnieuw.");
            }
        }

        private string GetGamesFilter()
        {
            var result = "";

            if (!GameMemberFilter.SelectedItem.IsNull())
                result += "/bymemberid/" + ((MemberReadDTO) GameMemberFilter.SelectedItem).Id;

            return result;
        }

        private GameReadDTO GetSelectedGame()
        {
            if (GameData.SelectedItem.IsNull()) return null;

            var game = (GameReadDTO) GameData.SelectedItem;

            return game;
        }

        private void GameNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            var game = GetSelectedGame();

            if (game.IsNull()) return;

            if (GameMember.SelectedItem.IsNull()) return;

            game.GameNumber = GameNumber.Text;
            GameData.Items.Refresh();
        }

        #endregion
    }
}