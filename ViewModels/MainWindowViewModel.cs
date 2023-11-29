using ReactiveUI;
using System;
using System.Reactive.Linq;
using ToDoList.DataModel;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ToDoListViewModel ToDoList { get; }

        /// <summary>
        /// Contiene el ViewModel que se mostrará en la ventana principal 
        /// </summary>
        private ViewModelBase _contentViewModel;

        /// <summary>
        /// Devuelve\establece el viewmodel que se mostrará en la ventana principal 
        /// </summary>
        public ViewModelBase ContentViewModel
        {
            get => _contentViewModel;
            private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
        }

        /// <summary>
        /// Al cargar: obtiene la lista ToDo actual y muestra la interface de lista.
        /// </summary>
        public MainWindowViewModel()
        {
            var service = new ToDoListService();
            ToDoList = new ToDoListViewModel(service.GetItems());
            _contentViewModel = ToDoList;
        }

        /// <summary>
        /// Muestra la interfaz "Agregar Lista" 
        /// </summary>
        public void AddItem()
        {
            AddItemViewModel addItemViewModel = new();

            Observable.Merge(
                addItemViewModel.OkCommand,
                addItemViewModel.CancelCommand.Select(_ => (ToDoItem?)null))
                .Take(1)
                .Subscribe(newItem =>
                {
                    if (newItem != null)
                    {
                        ToDoList.ListItems.Add(newItem);
                    }
                    ContentViewModel = ToDoList;
                });

            ContentViewModel = addItemViewModel;
        }
    }

}
