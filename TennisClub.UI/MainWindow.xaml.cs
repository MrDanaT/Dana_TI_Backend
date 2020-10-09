using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
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

        private void GetRoles()
        {
            Task<HttpResponseMessage> roles = WebAPI.GetCall("roles");
            if (roles.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                RoleData.ItemsSource = roles.Result.Content.ReadAsAsync<List<Role>>().Result;
            }
        }

        private void GetRolesButton_Click(object sender, RoutedEventArgs e)
        {
            GetRoles();
        }
    }
}
