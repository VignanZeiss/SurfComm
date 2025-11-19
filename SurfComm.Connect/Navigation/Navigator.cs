using SurfComm.Connect;
using System.Windows.Controls;

public static class Navigator
{
    public static void Navigate(UserControl view, object viewModel)
    {
        if (App.Nav == null)
            return;

        view.DataContext = viewModel;

        App.Nav.Content = view;
    }
}
