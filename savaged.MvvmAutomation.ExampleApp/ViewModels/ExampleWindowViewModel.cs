namespace savaged.MvvmAutomation.ExampleApp.ViewModels
{
    class ExampleWindowViewModel : BaseViewModel
    {
        private string _feedback;

        public ExampleWindowViewModel()
        {
            Feedback = "Hello World!";
        }

        public string Feedback
        {
            get => _feedback;
            set => Set(ref _feedback, value);
        }
    }
}
