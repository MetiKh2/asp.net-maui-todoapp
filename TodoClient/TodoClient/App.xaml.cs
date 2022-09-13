using TodoClient.Pages;

namespace TodoClient;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage =new NavigationPage(new TodosListPage());
	}
}
