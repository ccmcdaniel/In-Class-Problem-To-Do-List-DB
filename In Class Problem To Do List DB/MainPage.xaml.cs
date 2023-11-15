using SQLite;
using System.Collections.ObjectModel;

namespace In_Class_Problem_To_Do_List_DB
{
    [SQLite.Table("ToDoList")]
    public class ToDoItem
    {
        [PrimaryKey, AutoIncrement, SQLite.Column("_Id")]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateMade {  get; set; } = DateTime.Now;

        //In order to update the check box for indicating the task is
        //completed, you will need to setup a setter that calls the 
        //appropriate database command, as shown below.
        //You will need to do this for all updated properties.
        private bool isCompleted;
        public bool IsCompleted {  get
            {
                return isCompleted;
            }
            set
            {
                isCompleted = value;
                
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Database.SaveItemAsync(this);
                });
            }
        }
    }

    public partial class MainPage : ContentPage
    {
        public ObservableCollection<ToDoItem> Items { get; set; }
 
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                List<ToDoItem> imported_data = await Database.GetAllItemsAsync();
                Items = new ObservableCollection<ToDoItem>(imported_data);
                listToDo.ItemsSource = Items;
            });

            /*
            Items.Add(new ToDoItem
            {
                Id = 1,
                Text = "Make my Bed."
            });
            Items.Add(new ToDoItem
            {
                Id = 2,
                Text = "Work on group projects."
            });

            Items.Add(new ToDoItem
            {
                Id = 3,
                Text = "Get Groceries."
            });

            Items.Add(new ToDoItem
            {
                Id = 4,
                Text = "Practice Leet Code."
            });
            */

           
        }

        private async void AddNewItem(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Enter New Item",
                "Enter the text for your new item.");

            if(result !=  null)
            {
                ToDoItem item = new ToDoItem { Text = result };
                Items.Add(item);

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Database.SaveItemAsync(item);
                    await DisplayAlert("Your item was saved!", "Your item was saved!!", "Ok");
                });
            }
        }
    }
}