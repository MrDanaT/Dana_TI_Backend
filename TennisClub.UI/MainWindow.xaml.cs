using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TennisClub.Common;
using TennisClub.Common.Game;
using TennisClub.Common.GameResult;
using TennisClub.Common.Gender;
using TennisClub.Common.League;
using TennisClub.Common.Member;
using TennisClub.Common.MemberFine;
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
            createMemberlist = new List<MemberReadDTO>();
            createGameList = new List<GameReadDTO>();
            originalMemberFineList = new List<MemberFineReadDTO>();

            ReadRoles();
            ReadAllMembers();
            ReadActiveMembers();
            ReadAllActiveSpelerMembers();
            ReadGenders();
            ReadLeagues();
            ReadGameResults();
            ReadMemberRoles();
            ReadGames();
            ReadMemberFines();
        }

        #region Genders

        /*
         * CRUD
         */
        private bool ReadGenders()
        {
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.GetCall("genders");

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<GenderReadDTO>? tmp = result.Result.Content.ReadAsAsync<List<GenderReadDTO>>().Result;
                MemberGender.ItemsSource = tmp.ToList();
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        #endregion

        #region Leagues

        /*
         * CRUD
         */
        private bool ReadLeagues()
        {
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.GetCall("leagues");

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<LeagueReadDTO>? tmp = result.Result.Content.ReadAsAsync<List<LeagueReadDTO>>().Result;
                GameLeague.ItemsSource = tmp;
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        #endregion

        private void FloatNumberValidation_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex? regex = new Regex(@"^\d*\.?\d?$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex? regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #region MemberFine

        private void ClearMemberFineSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            MemberFineData.UnselectAll();

            MemberFineFineNumber.Text = "";
            MemberFineAmount.Text = "";
            MemberFineMember.SelectedValue = null;
            MemberFineHandoutDate.SelectedDate = null;
            MemberFinePaymentDate.SelectedDate = null;

            SetMemberFineBoxEnable(true);
        }

        private void SetMemberFineBoxEnable(bool isEnabled)
        {
            MemberFineAmount.IsEnabled = isEnabled;
            MemberFineFineNumber.IsEnabled = isEnabled;
            MemberFineMember.IsEnabled = isEnabled;
            MemberFineHandoutDate.IsEnabled = isEnabled;

            MemberFineReadDTO? memberFine = GetSelectedMemberFine();

            if (memberFine.IsNull())
            {
                return;
            }

            MemberFinePaymentDate.IsEnabled =
                memberFine.PaymentDate.IsNull() || memberFine.PaymentDate.Date.Equals(new DateTime());
        }

        private void AddMemberFineButton_Click(object sender, RoutedEventArgs e)
        {
            string? fineNumber = MemberFineFineNumber.Text;
            string? amount = MemberFineAmount.Text;
            object? member = MemberFineMember.SelectedItem;
            DateTime? handoutDate = MemberFineHandoutDate.SelectedDate;
            DateTime? paymentDate = MemberFinePaymentDate.SelectedDate;

            if (!fineNumber.Equals("") && !member.IsNull() && !handoutDate.IsNull())
            {
                MemberFineReadDTO? newMemberFine = new MemberFineReadDTO
                {
                    Id = 0,
                    FineNumber = int.Parse(fineNumber),
                    Amount = decimal.Parse(amount),
                    HandoutDate = handoutDate.Value
                };

                if (paymentDate.IsNull())
                {
                    newMemberFine.PaymentDate = new DateTime();
                }
                else
                {
                    newMemberFine.PaymentDate = paymentDate.Value;
                }

                MemberReadDTO? actualMember = (MemberReadDTO)member;
                if (!actualMember.IsNull())
                {
                    newMemberFine.MemberFullName = actualMember.FullName;
                    newMemberFine.MemberId = actualMember.Id;
                }

                List<MemberFineReadDTO>? newList = MemberFineData.ItemsSource.OfType<MemberFineReadDTO>().ToList();
                newList.Add(newMemberFine);
                MemberFineData.ItemsSource = newList;

                MemberFineData.Items.Refresh();
            }
        }

        private void MemberFineData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MemberFineReadDTO? memberFine = GetSelectedMemberFine();

            if (memberFine.IsNull())
            {
                return;
            }

            MemberFineFineNumber.Text = memberFine.FineNumber.ToString();
            MemberFineAmount.Text = memberFine.Amount.ToString();
            MemberFineMember.SelectedValue = memberFine.MemberId;
            MemberFineHandoutDate.SelectedDate = memberFine.HandoutDate;
            MemberFinePaymentDate.SelectedDate = memberFine.PaymentDate;

            bool isNewMemberFine = originalMemberFineList.Contains(memberFine);

            SetMemberFineBoxEnable(isNewMemberFine);
        }

        private MemberFineReadDTO GetSelectedMemberFine()
        {
            if (MemberFineData.SelectedItem.IsNull())
            {
                return null;
            }

            return (MemberFineReadDTO)MemberFineData.SelectedItem;
        }

        private void MemberFinePaymentDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MemberFineReadDTO? memberFine = GetSelectedMemberFine();

            if (memberFine.IsNull())
            {
                return;
            }

            DateTime? selectedDate = MemberFinePaymentDate.SelectedDate;

            if (selectedDate.IsNull())
            {
                return;
            }

            memberFine.PaymentDate = selectedDate.Value;
            MemberFineData.Items.Refresh();
        }

        #endregion

        #region Roles

        private List<RoleReadDTO> originalRoleList;

        /*
         * CRUD
         */
        private bool CreateRole(RoleReadDTO item)
        {
            RoleCreateDTO? newRole = new RoleCreateDTO
            {
                Name = item.Name
            };
            System.Threading.Tasks.Task<HttpResponseMessage>? response = WebAPI.PostCall("roles", newRole);

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
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.GetCall("roles");
            DataGrid? itemsControl = RoleData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<RoleReadDTO>? tmp = result.Result.Content.ReadAsAsync<List<RoleReadDTO>>().Result;
                ;
                itemsControl.ItemsSource = tmp;
                List<RoleReadDTO>? tmp2 = new List<RoleReadDTO>(tmp.Count);
                tmp.ForEach(item => { tmp2.Add(new RoleReadDTO { Id = item.Id, Name = item.Name }); });
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
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.PutCall($"roles/{id}", item);

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
        private void ClearRoleSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            RoleData.UnselectAll();

            RoleName.Text = "";
        }

        private void GetRolesButton_Click(object sender, RoutedEventArgs e)
        {
            ReadRoles();
        }

        private void SyncLeaguesButton_Click(object sender, RoutedEventArgs e)
        {
            SynchroniseRoleTable();
        }

        private void RoleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            RoleReadDTO? role = GetSelectedRole();

            if (role.IsNull())
            {
                return;
            }

            role.Name = RoleName.Text;
            RoleData.Items.Refresh();
        }

        private void RoleData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoleReadDTO? selectedItem = GetSelectedRole();
            if (!selectedItem.IsNull())
            {
                RoleName.Text = selectedItem.Name;
            }
        }

        private void AddRoleButton_Click(object sender, RoutedEventArgs e)
        {
            string? name = RoleName.Text;

            if (!name.Equals(""))
            {
                RoleReadDTO? newRole = new RoleReadDTO
                {
                    Id = 0,
                    Name = name
                };

                List<RoleReadDTO>? newList = RoleData.ItemsSource.OfType<RoleReadDTO>().ToList();
                newList.Add(newRole);
                RoleData.ItemsSource = newList;

                RoleData.Items.Refresh();
            }
        }

        /*
         * Methods
         */
        private RoleReadDTO GetSelectedRole()
        {
            if (RoleData.SelectedItem.IsNull())
            {
                return null;
            }

            return (RoleReadDTO)RoleData.SelectedItem;
        }

        private void SynchroniseRoleTable()
        {
            bool isSucceeded = false;
            ItemCollection? items = RoleData.Items;
            for (int i = 0; i < RoleData.Items.Count; i++)
            {
                object? item = RoleData.Items[i];
                RoleReadDTO? roleItem = (RoleReadDTO)item;
                RoleReadDTO? originalItem = originalRoleList.Find(x => x.Id == roleItem.Id);
                if (originalItem.IsNull() && !roleItem.IsNull())
                {
                    isSucceeded = CreateRole(roleItem);
                }
                else if (!roleItem.Name.Equals(originalItem.Name))
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

        #endregion

        #region GameResults

        /*
         * CRUD
         */

        private List<GameResultReadDTO> originalGameResultList;

        private bool CreateGameResult(GameResultReadDTO item)
        {
            GameResultCreateDTO? newGameResult = new GameResultCreateDTO
            {
                GameId = item.GameId,
                ScoreOpponent = item.ScoreOpponent,
                ScoreTeamMember = item.ScoreTeamMember,
                SetNr = item.SetNr
            };
            System.Threading.Tasks.Task<HttpResponseMessage>? response = WebAPI.PostCall("gameresults", newGameResult);

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
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.GetCall($"gameresults?{GetGameResultFilters()}");
            DataGrid? itemsControl = GameResultData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<GameResultReadDTO>? tmp = result.Result.Content.ReadAsAsync<List<GameResultReadDTO>>().Result;
                ;
                itemsControl.ItemsSource = tmp;
                List<GameResultReadDTO>? tmp2 = new List<GameResultReadDTO>(tmp.Count);
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
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.PutCall($"gameresults/{id}", item);

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

        private void AddGameResultButton_Click(object sender, RoutedEventArgs e)
        {
            string? gameId = GameResultGameId.Text;
            string? setNr = GameResultSetNr.Text;
            string? scoreTeamMeber = GameResultScoreTeamMember.Text;
            string? scoreOpponent = GameResultscoreOpponent.Text;

            if (!gameId.Equals("") && !setNr.Equals("") && !scoreTeamMeber.Equals("") && !scoreOpponent.Equals(""))
            {
                GameResultReadDTO? newGameResult = new GameResultReadDTO
                {
                    Id = 0,
                    GameId = int.Parse(gameId),
                    ScoreOpponent = byte.Parse(scoreOpponent),
                    ScoreTeamMember = byte.Parse(scoreTeamMeber),
                    SetNr = byte.Parse(setNr)
                };

                List<GameResultReadDTO>? newList = GameResultData.ItemsSource.OfType<GameResultReadDTO>().ToList();
                
                if (newList.Exists(x => x.GameId == int.Parse(gameId) && x.SetNr == int.Parse(setNr)))
                {
                    MessageBox.Show("De combinatie van de game id en set nr zijn niet uniek!");
                    return;
                }

                newList.Add(newGameResult);
                GameResultData.ItemsSource = newList;

                GameResultData.Items.Refresh();
            }
        }

        private void GameResultGameId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GameResultReadDTO? gameResult = GetSelectedGameResult();

            if (gameResult.IsNull())
            {
                return;
            }

            gameResult.GameId = int.Parse(GameResultGameId.SelectedValue.IsNull() ? "0" : GameResultGameId.SelectedValue.ToString());
            GameResultData.Items.Refresh();
        }

        private void GameResultSetNr_TextChanged(object sender, TextChangedEventArgs e)
        {
            GameResultReadDTO? gameResult = GetSelectedGameResult();

            if (gameResult.IsNull())
            {
                return;
            }

            gameResult.SetNr = byte.Parse(GameResultSetNr.Text == "" ? "0" : GameResultSetNr.Text);
            GameResultData.Items.Refresh();
        }

        private void GameResultScoreTeamMember_TextChanged(object sender, TextChangedEventArgs e)
        {
            GameResultReadDTO? gameResult = GetSelectedGameResult();

            if (gameResult.IsNull())
            {
                return;
            }

            gameResult.ScoreTeamMember =
                byte.Parse(GameResultScoreTeamMember.Text == "" ? "0" : GameResultScoreTeamMember.Text);
            GameResultData.Items.Refresh();
        }

        private void GameResultscoreOpponent_TextChanged(object sender, TextChangedEventArgs e)
        {
            GameResultReadDTO? gameResult = GetSelectedGameResult();

            if (gameResult.IsNull())
            {
                return;
            }

            gameResult.ScoreOpponent =
                byte.Parse(GameResultscoreOpponent.Text == "" ? "0" : GameResultscoreOpponent.Text);
            GameResultData.Items.Refresh();
        }

        private void GameResultData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GameResultReadDTO? selectedItem = GetSelectedGameResult();
            if (!selectedItem.IsNull())
            {
                GameResultGameId.Text = selectedItem.GameId.ToString();
                GameResultSetNr.Text = selectedItem.SetNr.ToString();
                GameResultScoreTeamMember.Text = selectedItem.ScoreTeamMember.ToString();
                GameResultscoreOpponent.Text = selectedItem.ScoreOpponent.ToString();
            }
        }

        private void ClearGameResultSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            GameResultData.UnselectAll();

            GameResultGameId.Text = "";
            GameResultSetNr.Text = "";
            GameResultscoreOpponent.Text = "";
            GameResultScoreTeamMember.Text = "";
        }

        /*
         * Methods
         */
        private GameResultReadDTO GetSelectedGameResult()
        {
            if (GameResultData.SelectedItem.IsNull())
            {
                return null;
            }

            return (GameResultReadDTO)GameResultData.SelectedItem;
        }

        private void SynchroniseGameResultTable()
        {
            bool isSucceeded = false;

            for (int i = 0; i < GameResultData.Items.Count; i++)
            {
                object? item = GameResultData.Items[i];
                GameResultReadDTO? gameResultItem = (GameResultReadDTO)item;
                GameResultReadDTO? originalItem = originalGameResultList.Find(x => x.Id == gameResultItem.Id);

                if (originalItem.IsNull() && !gameResultItem.IsNull())
                {
                    isSucceeded = CreateGameResult(gameResultItem);
                }
                else if (gameResultItem.SetNr != originalItem.SetNr ||
                         gameResultItem.ScoreOpponent != originalItem.ScoreOpponent ||
                         gameResultItem.ScoreTeamMember != originalItem.ScoreTeamMember)
                {
                    isSucceeded = UpdateGameResult(gameResultItem.Id,
                        new GameResultUpdateDTO
                        {
                            ScoreOpponent = gameResultItem.ScoreOpponent,
                            SetNr = gameResultItem.SetNr,
                            ScoreTeamMember = gameResultItem.ScoreTeamMember
                        });
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

        private string GetGameResultFilters()
        {
            string? memberIdUrl = $"memberId={GameResultPlayerComboBoxFilter.SelectedValue}";
            string? selectedDateUrl =
                $"date={GameResultdatePickerFilter.SelectedDate.GetValueOrDefault().ToShortDateString()}";

            return
                $"{(!GameResultPlayerComboBoxFilter.SelectedValue.IsNull() ? memberIdUrl : "")}&{(!GameResultdatePickerFilter.SelectedDate.IsNull() ? selectedDateUrl : "")}";
        }

        #endregion

        #region Members

        private List<MemberReadDTO> originalMemberList;
        private List<MemberReadDTO> createMemberlist;

        /*
         * CRUD
         */
        private bool CreateMember(MemberReadDTO memberItem)
        {
            MemberCreateDTO? newMember = new MemberCreateDTO
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
            System.Threading.Tasks.Task<HttpResponseMessage>? response = WebAPI.PostCall("members", newMember);

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
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.GetCall("members");

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<MemberReadDTO>? tmp = result.Result.Content.ReadAsAsync<List<MemberReadDTO>>().Result;
                MemberRoleMember.ItemsSource = tmp;
                MemberRoleMemberFilter.ItemsSource = tmp;
                GameMemberFilter.ItemsSource = tmp;
                GameResultPlayerComboBoxFilter.ItemsSource = tmp;
                MemberFineMemberFilter.ItemsSource = tmp;
                MemberFineMember.ItemsSource = tmp;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
            }
        }

        private void ReadAllActiveSpelerMembers()
        {
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.GetCall("members/active/speler");

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<MemberReadDTO>? tmp = result.Result.Content.ReadAsAsync<List<MemberReadDTO>>().Result;
                GameMember.ItemsSource = tmp;
            }
            else
            {
                Debug.WriteLine("Niet gelukt!");
            }
        }

        private bool ReadActiveMembers()
        {
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.GetCall($"members/active?{GetMemberFilters()}");
            DataGrid? itemsControl = MemberData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<MemberReadDTO>? tmp = result.Result.Content.ReadAsAsync<List<MemberReadDTO>>().Result;
                itemsControl.ItemsSource = tmp;
                List<MemberReadDTO>? tmp2 = new List<MemberReadDTO>(tmp.Count);
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
                createMemberlist.Clear();
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        private bool UpdateMember(int id, MemberUpdateDTO memberUpdateDTO)
        {
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.PutCall($"members/{id}", memberUpdateDTO);

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
            System.Threading.Tasks.Task<HttpResponseMessage>? response = WebAPI.DeleteCall($"members/{id}");

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

        private void ClearMemberSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            MemberData.UnselectAll();

            MemberFederationNr.Text = "";
            MemberFirstName.Text = "";
            MemberLastName.Text = "";
            MemberBirthDate.SelectedDate = null;
            MemberGender.SelectedItem = null;
            MemberAddress.Text = "";
            MemberNumber.Text = "";
            MemberAddition.Text = "";
            MemberZipcode.Text = "";
            MemberCity.Text = "";
            MemberPhoneNr.Text = "";
        }

        private void SearchFilteredMembers_Click(object sender, RoutedEventArgs e)
        {
            ReadActiveMembers();
        }

        private void MemberData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MemberReadDTO? selectedItem = GetSelectedMember();
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
            MemberReadDTO? member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            GenderReadDTO? gender = (GenderReadDTO)MemberGender.SelectedItem;
            if (gender.IsNull())
            {
                return;
            }

            member.GenderName = gender.Name;
            member.GenderId = gender.Id;
            MemberData.Items.Refresh();
        }

        /*
         * Methods
         */
        private void SynchroniseMemberTable()
        {
            bool isSucceeded = false;
            List<MemberReadDTO>? memberData = MemberData.ItemsSource.OfType<MemberReadDTO>().ToList();

            for (int i = 0; i < originalMemberList.Count; i++)
            {
                MemberReadDTO? originalItem = originalMemberList.ElementAt(i);
                MemberReadDTO? memberItem = memberData.Find(x => x.Id == originalItem.Id);

                if (!originalItem.IsNull() && memberItem.IsNull())
                {
                    isSucceeded = DeleteMember(originalItem.Id);
                }
                else if (originalItem.IsNull() && !memberItem.IsNull())
                {
                    continue;
                }
                else if (!originalItem.Equals(memberItem))
                {
                    isSucceeded = UpdateMember(originalItem.Id,
                        new MemberUpdateDTO
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
                        });
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

            if (isSucceeded || originalMemberList.Count == 0)
            {
                foreach (MemberReadDTO? x in createMemberlist)
                {
                    isSucceeded = CreateMember(x);

                    if (!isSucceeded)
                    {
                        break;
                    }
                }
            }

            if (isSucceeded)
            {
                createMemberlist.Clear();
                MessageBox.Show("De tabel is succesvol gesynchroniseerd met de database!");
                ReadActiveMembers();
                ReadAllActiveSpelerMembers();
                ReadAllMembers();
            }
            else
            {
                MessageBox.Show("Er is een fout gebeurd bij het synchroniseren. Probeer dit opnieuw.");
            }
        }

        private string GetMemberFilters()
        {
            string? federationNrUrl = $"federationNr={MemberFilterFederationNr.Text}";
            string? lastNameUrl = $"lastName={MemberFilterLastName.Text}";
            string? firstNameUrl = $"firstName={MemberFilterFirstName.Text}";
            string? locationUrl = $"location={MemberFilterLocation.Text}";
            return
                $"{(string.IsNullOrEmpty(MemberFilterFederationNr.Text) ? "" : federationNrUrl)}&{(string.IsNullOrEmpty(MemberFilterLastName.Text) ? "" : lastNameUrl)}&{(string.IsNullOrEmpty(MemberFilterFirstName.Text) ? "" : firstNameUrl)}&{(string.IsNullOrEmpty(MemberFilterLocation.Text) ? "" : locationUrl)}";
        }

        private MemberReadDTO GetSelectedMember()
        {
            if (MemberData.SelectedItem.IsNull())
            {
                return null;
            }

            return (MemberReadDTO)MemberData.SelectedItem;
        }

        private void MemberFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            MemberReadDTO? member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.FirstName = MemberFirstName.Text;
            MemberData.Items.Refresh();
        }

        private void MemberLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            MemberReadDTO? member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.LastName = MemberLastName.Text;
            MemberData.Items.Refresh();
        }

        private void MemberAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            MemberReadDTO? member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.Address = MemberAddress.Text;
            MemberData.Items.Refresh();
        }

        private void MemberNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            MemberReadDTO? member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.Number = MemberNumber.Text;
            MemberData.Items.Refresh();
        }

        private void MemberAddition_TextChanged(object sender, TextChangedEventArgs e)
        {
            MemberReadDTO? member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.Addition = MemberAddition.Text;
            MemberData.Items.Refresh();
        }

        private void MemberZipcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            MemberReadDTO? member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.Zipcode = MemberZipcode.Text;
            MemberData.Items.Refresh();
        }

        private void MemberCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            MemberReadDTO? member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.City = MemberCity.Text;
            MemberData.Items.Refresh();
        }

        private void MemberPhoneNr_TextChanged(object sender, TextChangedEventArgs e)
        {
            MemberReadDTO? member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.PhoneNr = MemberPhoneNr.Text;
            MemberData.Items.Refresh();
        }

        private void MemberFederationNr_TextChanged(object sender, TextChangedEventArgs e)
        {
            MemberReadDTO? member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            member.FederationNr = MemberFederationNr.Text;
            MemberData.Items.Refresh();
        }

        private void MemberBirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MemberReadDTO? member = GetSelectedMember();

            if (member.IsNull())
            {
                return;
            }

            DateTime? selectedDate = MemberBirthDate.SelectedDate;

            if (selectedDate.IsNull())
            {
                return;
            }

            member.BirthDate = selectedDate.Value;
            MemberData.Items.Refresh();
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? birthDate = MemberBirthDate.SelectedDate;

            if (!birthDate.IsNull())
            {
                GenderReadDTO? gender = (GenderReadDTO)MemberGender.SelectedItem;
                MemberReadDTO? newMember = new MemberReadDTO
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

                List<MemberReadDTO>? newList = MemberData.ItemsSource.OfType<MemberReadDTO>().ToList();
                newList.Add(newMember);
                createMemberlist.Add(newMember);
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
            MemberRoleCreateDTO? newMemberRole = new MemberRoleCreateDTO
            {
                MemberId = memberRoleItem.MemberId,
                RoleId = memberRoleItem.RoleId,
                StartDate = memberRoleItem.StartDate
            };
            System.Threading.Tasks.Task<HttpResponseMessage>? response = WebAPI.PostCall("memberroles", newMemberRole);

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
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.GetCall($"memberroles{GetMemberRoleFilters()}");
            DataGrid? itemsControl = MemberRoleData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<MemberRoleReadDTO>? tmp = result.Result.Content.ReadAsAsync<List<MemberRoleReadDTO>>().Result;
                itemsControl.ItemsSource = tmp;
                List<MemberRoleReadDTO>? tmp2 = new List<MemberRoleReadDTO>(tmp.Count);
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
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.PutCall($"memberroles/{id}", memberRoleUpdateDTO);

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
            DateTime? startDate = MemberRoleStartDate.SelectedDate;
            DateTime? endDate = MemberRoleEndDate.SelectedDate;
            MemberReadDTO? member = (MemberReadDTO)MemberRoleMember.SelectedItem;
            RoleReadDTO? role = (RoleReadDTO)MemberRoleRole.SelectedItem;

            if (!startDate.IsNull() && !member.IsNull() && !role.IsNull())
            {
                MemberRoleReadDTO? newMemberRole = new MemberRoleReadDTO
                {
                    Id = 0,
                    StartDate = startDate.Value,
                    MemberId = member.Id,
                    RoleId = role.Id,
                    MemberFullName = member.FullName,
                    RoleName = role.Name
                };

                if (!endDate.IsNull())
                {
                    newMemberRole.EndDate = endDate.Value;
                }

                List<MemberRoleReadDTO>? newList = MemberRoleData.ItemsSource.OfType<MemberRoleReadDTO>().ToList();
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
            MemberRoleReadDTO? memberRole = GetSelectedMemberRole();

            if (memberRole.IsNull())
            {
                return;
            }

            if (MemberRoleMember.SelectedItem.IsNull())
            {
                return;
            }

            MemberReadDTO? member = (MemberReadDTO)MemberRoleMember.SelectedItem;
            if (member.IsNull())
            {
                return;
            }

            memberRole.MemberId = member.Id;
            memberRole.MemberFullName = member.FullName;

            MemberRoleData.Items.Refresh();
        }

        private void MemberRoleRole_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MemberRoleReadDTO? memberRole = GetSelectedMemberRole();

            if (memberRole.IsNull())
            {
                return;
            }

            if (MemberRoleRole.SelectedItem.IsNull())
            {
                return;
            }

            RoleReadDTO? role = (RoleReadDTO)MemberRoleRole.SelectedItem;
            if (role.IsNull())
            {
                return;
            }

            memberRole.RoleId = role.Id;
            memberRole.RoleName = role.Name;
            MemberRoleData.Items.Refresh();
        }

        private void MemberRoleStartDate_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            MemberRoleReadDTO? memberRole = GetSelectedMemberRole();

            if (memberRole.IsNull())
            {
                return;
            }

            if (!MemberRoleStartDate.SelectedDate.HasValue)
            {
                return;
            }

            memberRole.StartDate = MemberRoleStartDate.SelectedDate.Value;
            MemberRoleData.Items.Refresh();
        }

        private void MemberRoleEndDate_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            MemberRoleReadDTO? memberRole = GetSelectedMemberRole();

            if (memberRole.IsNull())
            {
                return;
            }

            if (!MemberRoleEndDate.SelectedDate.HasValue)
            {
                return;
            }

            memberRole.EndDate = MemberRoleEndDate.SelectedDate.Value;
            MemberRoleData.Items.Refresh();
        }

        private void MemberRoleData_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MemberRoleReadDTO? memberRole = GetSelectedMemberRole();

            if (memberRole.IsNull())
            {
                return;
            }

            MemberRoleMember.SelectedValue = memberRole.MemberId;
            MemberRoleRole.SelectedValue = memberRole.RoleId;
            MemberRoleStartDate.SelectedDate = memberRole.StartDate;
            MemberRoleEndDate.SelectedDate = memberRole.EndDate;

            bool isNewMember = originalMemberRoleList.Contains(memberRole);

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
            bool isSucceeded = false;

            for (int i = 0; i < MemberRoleData.Items.Count; i++)
            {
                object? item = MemberRoleData.Items[i];
                MemberRoleReadDTO? memberRoleItem = (MemberRoleReadDTO)item;
                MemberRoleReadDTO? originalItem = originalMemberRoleList.Find(x => x.Id == memberRoleItem.Id);

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

                if (!isSucceeded)
                {
                    break;
                }
            }

            if (isSucceeded)
            {
                MessageBox.Show("De tabel is succesvol gesynchroniseerd met de database!");
                ReadActiveMembers();
                ReadMemberRoles();
                ReadAllMembers();
                ReadAllActiveSpelerMembers();
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
            string? result = "";

            if (!MemberRoleMemberFilter.SelectedItem.IsNull())
            {
                result += $"/bymemberid/{MemberRoleMemberFilter.SelectedValue}";
            }
            else if (MemberRoleRolesFilter.SelectedItems.Count > 0)
            {
                result += "/byroleids/";
                foreach (object? item in MemberRoleRolesFilter.SelectedItems)
                {
                    RoleReadDTO? role = (RoleReadDTO)item;
                    if (!role.IsNull())
                    {
                        result += role.Id + ",";
                    }
                }
            }

            return result;
        }

        private MemberRoleReadDTO GetSelectedMemberRole()
        {
            if (MemberRoleData.SelectedItem.IsNull())
            {
                return null;
            }

            MemberRoleReadDTO? memberRole = (MemberRoleReadDTO)MemberRoleData.SelectedItem;

            return memberRole;
        }

        #endregion

        #region Games

        private List<GameReadDTO> originalGameList;
        private List<GameReadDTO> createGameList;

        /*
         * CRUD
         */
        private bool CreateGame(GameReadDTO gameReadDto)
        {
            GameCreateDTO? newGame = new GameCreateDTO
            {
                Date = gameReadDto.Date,
                GameNumber = gameReadDto.GameNumber,
                LeagueId = gameReadDto.LeagueId,
                MemberId = gameReadDto.MemberId
            };
            System.Threading.Tasks.Task<HttpResponseMessage>? response = WebAPI.PostCall("games", newGame);

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
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.GetCall($"games{GetGamesFilter()}");
            DataGrid? itemsControl = GameData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<GameReadDTO>? tmp = result.Result.Content.ReadAsAsync<List<GameReadDTO>>().Result;
                itemsControl.ItemsSource = tmp;
                List<GameReadDTO>? tmp2 = new List<GameReadDTO>(tmp.Count);
                tmp.ForEach(item =>
                {
                    tmp2.Add(new GameReadDTO
                    {
                        Id = item.Id,
                        MemberId = item.MemberId,
                        LeagueId = item.LeagueId,
                        MemberFullName = item.MemberFullName,
                        LeagueName = item.LeagueName,
                        Date = item.Date,
                        GameNumber = item.GameNumber
                    });
                });
                originalGameList = tmp2;
                GameResultGameId.ItemsSource = tmp2;
                createGameList.Clear();
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        private bool UpdateGame(int id, GameUpdateDTO gameUpdateDto)
        {
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.PutCall($"games/{id}", gameUpdateDto);

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
            System.Threading.Tasks.Task<HttpResponseMessage>? response = WebAPI.DeleteCall($"games/{id}");

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
            GameReadDTO? game = GetSelectedGame();

            if (game.IsNull())
            {
                return;
            }

            GameDate.SelectedDate = game.Date;
            GameMember.SelectedValue = game.MemberId;
            GameLeague.SelectedValue = game.LeagueId;
            GameNumber.Text = game.GameNumber;
        }

        private void GameLeague_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GameReadDTO? game = GetSelectedGame();

            if (game.IsNull())
            {
                return;
            }

            if (GameMember.SelectedItem.IsNull())
            {
                return;
            }

            LeagueReadDTO member = (LeagueReadDTO)GameLeague.SelectedItem;
            if (member.IsNull())
            {
                return;
            }

            game.LeagueName = member.Name;
            game.LeagueId = member.Id;
            GameData.Items.Refresh();
        }

        private void GameMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GameReadDTO? game = GetSelectedGame();

            if (game.IsNull())
            {
                return;
            }

            if (GameMember.SelectedItem.IsNull())
            {
                return;
            }

            MemberReadDTO member = (MemberReadDTO)GameMember.SelectedItem;
            if (member.IsNull())
            {
                return;
            }

            game.MemberFullName = member.FullName;
            game.MemberId = member.Id;
            GameData.Items.Refresh();
        }

        private void GameDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            GameReadDTO? game = GetSelectedGame();

            if (game.IsNull())
            {
                return;
            }

            if (!GameDate.SelectedDate.HasValue)
            {
                return;
            }

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
            DateTime? date = GameDate.SelectedDate;

            if (!date.IsNull())
            {
                GameReadDTO? newGame = new GameReadDTO
                {
                    Id = 0,
                    GameNumber = GameNumber.Text,
                    Date = date.Value
                };

                if (!GameMember.SelectedItem.IsNull() && !GameMember.SelectedValue.IsNull())
                {
                    MemberReadDTO? member = (MemberReadDTO)GameMember.SelectedItem;
                    newGame.MemberFullName = member.FullName;
                    newGame.MemberId = member.Id;
                }

                if (!GameLeague.SelectedItem.IsNull() && !GameLeague.SelectedValue.IsNull())
                {
                    LeagueReadDTO? league = (LeagueReadDTO)GameLeague.SelectedItem;
                    newGame.LeagueName = league.Name;
                    newGame.LeagueId = league.Id;
                }

                List<GameReadDTO>? newList = GameData.ItemsSource.OfType<GameReadDTO>().ToList();
                newList.Add(newGame);
                createGameList.Add(newGame);
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
            bool isSucceeded = false;
            List<GameReadDTO>? gameData = GameData.ItemsSource.OfType<GameReadDTO>().ToList();

            for (int i = 0; i < originalGameList.Count; i++)
            {
                GameReadDTO? originalItem = originalGameList.ElementAt(i);
                ;
                GameReadDTO? gameItem = gameData.Find(x => x.Id == originalItem.Id);

                if (!originalItem.IsNull() && gameItem.IsNull())
                {
                    isSucceeded = DeleteGame(originalItem.Id);
                }
                else if (originalItem.IsNull() && !gameItem.IsNull())
                {
                    continue;
                }
                else if (!originalItem.Equals(gameItem))
                {
                    isSucceeded = UpdateGame(originalItem.Id,
                        new GameUpdateDTO
                        {
                            Date = gameItem.Date,
                            GameNumber = gameItem.GameNumber,
                            LeagueId = gameItem.LeagueId,
                            MemberId = gameItem.MemberId
                        });
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

            if (isSucceeded || originalGameList.Count == 0)
            {
                foreach (GameReadDTO? x in createGameList)
                {
                    isSucceeded = CreateGame(x);

                    if (!isSucceeded)
                    {
                        break;
                    }
                }
            }

            if (isSucceeded)
            {
                createGameList.Clear();
                MessageBox.Show("De tabel is succesvol gesynchroniseerd met de database!");
                ReadActiveMembers();
                ReadGames();
                ReadGameResults();
            }
            else
            {
                MessageBox.Show("Er is een fout gebeurd bij het synchroniseren. Probeer dit opnieuw.");
            }
        }

        private string GetGamesFilter()
        {
            string? result = "";

            if (!GameMemberFilter.SelectedItem.IsNull())
            {
                result += "/bymemberid/" + ((MemberReadDTO)GameMemberFilter.SelectedItem).Id;
            }

            return result;
        }

        private GameReadDTO GetSelectedGame()
        {
            if (GameData.SelectedItem.IsNull())
            {
                return null;
            }

            GameReadDTO? game = (GameReadDTO)GameData.SelectedItem;

            return game;
        }

        private void GameNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            GameReadDTO? game = GetSelectedGame();

            if (game.IsNull())
            {
                return;
            }

            if (GameMember.SelectedItem.IsNull())
            {
                return;
            }

            game.GameNumber = GameNumber.Text;
            GameData.Items.Refresh();
        }

        #endregion

        #region MemberFines

        private List<MemberFineReadDTO> originalMemberFineList;

        /*
         * CRUD
         */
        private bool CreateMemberFine(MemberFineReadDTO item)
        {
            MemberFineCreateDTO? newMemberFine = new MemberFineCreateDTO
            {
                Amount = item.Amount,
                FineNumber = item.FineNumber,
                HandoutDate = item.HandoutDate,
                MemberId = item.MemberId,
                PaymentDate = item.PaymentDate
            };
            System.Threading.Tasks.Task<HttpResponseMessage>? response = WebAPI.PostCall("memberfines", newMemberFine);

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                Debug.WriteLine($"{newMemberFine.FineNumber} is toegevoegd!");
                return true;
            }

            Debug.WriteLine("Er is iets foutgelopen.");
            return false;
        }

        private bool ReadMemberFines()
        {
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.GetCall($"memberfines{GetMemberFineFilters()}");
            DataGrid? itemsControl = MemberFineData;

            if (result.Result.StatusCode == HttpStatusCode.OK)
            {
                List<MemberFineReadDTO>? tmp = result.Result.Content.ReadAsAsync<List<MemberFineReadDTO>>().Result;

                if (!MemberFineHandoutDateFilter.SelectedDate.IsNull())
                {
                    DateTime date = MemberFineHandoutDateFilter.SelectedDate.Value.Date;
                    tmp = tmp.Where(x => x.HandoutDate.Date.Equals(date)).ToList();
                }

                if (!MemberFinePaymentDateFilter.SelectedDate.IsNull())
                {
                    DateTime date = MemberFinePaymentDateFilter.SelectedDate.Value.Date;
                    tmp = tmp.Where(x => x.PaymentDate.Equals(date)).ToList();
                }

                itemsControl.ItemsSource = tmp;
                List<MemberFineReadDTO>? tmp2 = new List<MemberFineReadDTO>(tmp.Count);
                tmp.ForEach(item =>
                {
                    tmp2.Add(new MemberFineReadDTO
                    {
                        Id = item.Id,
                        Amount = item.Amount,
                        FineNumber = item.FineNumber,
                        HandoutDate = item.HandoutDate,
                        MemberId = item.MemberId,
                        PaymentDate = item.PaymentDate
                    });
                });
                originalMemberFineList = tmp2;
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        private bool UpdateMemberFine(int id, MemberFineUpdateDTO item)
        {
            System.Threading.Tasks.Task<HttpResponseMessage>? result = WebAPI.PutCall($"memberfines/{id}", item);

            if (result.Result.StatusCode == HttpStatusCode.NoContent)
            {
                Debug.WriteLine("Updated!");
                return true;
            }

            Debug.WriteLine("Niet gelukt!");
            return false;
        }

        /*
         * Event handlers
         */

        private void GetMemberFinesButton_Click(object sender, RoutedEventArgs e)
        {
            ReadMemberFines();
        }

        private void SyncMemberFinesButton_Click(object sender, RoutedEventArgs e)
        {
            SynchroniseMemberFineTable();
        }

        private void ClearMemberFineFilterButton_Click(object sender, RoutedEventArgs e)
        {
            MemberFineHandoutDateFilter.SelectedDate = null;
            MemberFinePaymentDateFilter.SelectedDate = null;
            MemberFineMemberFilter.SelectedItem = null;
            ReadMemberFines();
        }

        private void FilterMemberFinesButton_Click(object sender, RoutedEventArgs e)
        {
            ReadMemberFines();
        }

        /*
         * Methods
         */

        private void SynchroniseMemberFineTable()
        {
            bool isSucceeded = false;

            for (int i = 0; i < MemberFineData.Items.Count; i++)
            {
                object? item = MemberFineData.Items[i];
                MemberFineReadDTO? memberFineItem = (MemberFineReadDTO)item;
                MemberFineReadDTO? originalItem = originalMemberFineList.Find(x => x.Id == memberFineItem.Id);

                if (originalItem.IsNull() && !memberFineItem.IsNull())
                {
                    isSucceeded = CreateMemberFine(memberFineItem);
                }
                else if (!memberFineItem.PaymentDate.Equals(originalItem.PaymentDate))
                {
                    isSucceeded = UpdateMemberFine(memberFineItem.Id,
                        new MemberFineUpdateDTO
                        {
                            PaymentDate = memberFineItem.PaymentDate
                        });
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
                ReadMemberFines();
            }
            else
            {
                MessageBox.Show("Er is een fout gebeurd bij het synchroniseren. Probeer dit opnieuw.");
            }
        }

        private string GetMemberFineFilters()
        {
            string? result = "";

            if (!MemberFineMemberFilter.SelectedValue.IsNull())
            {
                result += "/bymemberid/" + MemberFineMemberFilter.SelectedValue;
            }

            return result;
        }

        #endregion

    }
}