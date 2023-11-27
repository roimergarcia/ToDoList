using ReactiveUI;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ToDoListViewModel ToDoList { get; }

        private ViewModelBase _contentViewModel;

        public MainWindowViewModel()
        {
            var service = new ToDoListService();
            ToDoList = new ToDoListViewModel(service.GetItems());
            _contentViewModel = ToDoList;
        }
        public ViewModelBase ContentViewModel
        {
            get => _contentViewModel;
            private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
        }

        public void AddItem()
        {
            ContentViewModel = new AddItemViewModel();
        }

    }
}
