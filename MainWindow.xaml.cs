namespace TwoEleven;

public partial class MainWindow {
    public MainWindow() {
        Shared.NavigationService = NavigationService;
        
        InitializeComponent();
    }
}