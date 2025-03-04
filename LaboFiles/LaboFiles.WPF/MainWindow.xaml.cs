using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LaboFiles.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    const string fileName = "personen.txt";
    public MainWindow()
    {
        InitializeComponent();
    }

    private void addButton_Click(object sender, RoutedEventArgs e)
    {
        bool firstNameIsEmpty = string.IsNullOrWhiteSpace(firstNameTextBox.Text);
        bool lastNameIsEmpty = string.IsNullOrWhiteSpace(lastNameTextBox.Text);
        if (firstNameIsEmpty || lastNameIsEmpty)
        {
            MessageBox.Show("voornaam of achternaam is leeg.");
            return;
        }
        firstNameListBox.Items.Add(firstNameTextBox.Text);
        lastNameListBox.Items.Add(lastNameTextBox.Text);
    }

    private void saveFileButton_Click(object sender, RoutedEventArgs e)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            for (int i = 0; i < firstNameListBox.Items.Count; i++)
            {
                sw.WriteLine($"{firstNameListBox.Items[i]},{lastNameListBox.Items[i]}");
            }
        }
    }
}