using savaged.ExampleApp.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;

namespace savaged.ExampleApp
{
    public partial class ExampleDialog : Window
    {
        private DialogViewModel _dialogViewModel;

        public ExampleDialog()
        {
            InitializeComponent();
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            if (DataContext is DialogViewModel dialogViewModel)
            {
                _dialogViewModel = dialogViewModel;
                _dialogViewModel.PropertyChanged += OnDialogViewModelPropertyChanged;
            }
        }

        private void OnDialogViewModelPropertyChanged(
            object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DialogViewModel.DialogResult))
            {
                DialogResult = _dialogViewModel.DialogResult;
            }
        }

    }
}
