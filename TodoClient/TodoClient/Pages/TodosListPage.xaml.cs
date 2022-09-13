using TodoClient.Models;
using TodoClient.Services;

namespace TodoClient.Pages;

public partial class TodosListPage : ContentPage
{
    //private RestDataServices _dataServices = new RestDataServices();
    public TodosListPage()
    {
        InitializeComponent();
    }
    protected async override void OnAppearing()
    {
        var _dataServices = new RestDataServices();
        ActivityIndicator.IsRunning = true;
        var todos = await _dataServices.GetAllTodos();
        ActivityIndicator.IsRunning = false;
        //TodosCollectionView.ItemsSource = todos.Select(t =>
        //{
        //   t.Description= t.Description.Length > 120? t.Title.Substring(0, 120) + " ..." : t.Description;
        //    return t;
        //}).ToList();
        TodosCollectionView.ItemsSource = todos;
    }
    private void TodosCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var todo = e.CurrentSelection.FirstOrDefault() as Todo;
        Navigation.PushAsync(new ManageTodoPage(todo));
    }
    private void AddTodoBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ManageTodoPage(new Todo()));
    }

     
}