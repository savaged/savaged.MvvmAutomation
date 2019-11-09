namespace savaged.ExampleApp.ViewModels
{
    public class DialogViewModel : BaseViewModel
    {
        private bool? _dialogResult;

        public bool? DialogResult
        {
            get => _dialogResult;
            set => Set(ref _dialogResult, value);
        }
    }
}
