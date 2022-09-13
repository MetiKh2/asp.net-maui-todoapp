using TodoClient.Models;
using TodoClient.Services;

namespace TodoClient.Pages;

public partial class ManageTodoPage : ContentPage
{
    private RestDataServices _restDataServices = new RestDataServices();
    public Todo Todo { get; set; }
    public ManageTodoPage(Todo todo)
    {
        InitializeComponent();
        Todo = todo;
        TitleCell.Text = Todo.Title;
        DescriptionCell.Text = Todo.Description;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        Todo.Description = DescriptionCell.Text;
        Todo.Title = TitleCell.Text;
        if (Todo.Id != null && Todo.Id > 0)
        {
            //edit
            await _restDataServices.UpdateTodo(Todo);
            await Navigation.PopAsync();
        }
        else
        {
            //create
            await _restDataServices.CreateTodo(Todo);
            await Navigation.PopAsync();

        }
    }

    private async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        await _restDataServices.DeleteTodo(Todo.Id);
        await Navigation.PopAsync();
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}