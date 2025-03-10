﻿using System.IO;
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
using Microsoft.Win32;

namespace LaboFiles.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    const string fileName = "personen.txt";
    const char sepChar = ',';
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

        firstNameTextBox.Clear();
        lastNameTextBox.Clear();
        firstNameTextBox.Focus();
    }

    private void saveFileButton_Click(object sender, RoutedEventArgs e)
    {
        try {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Alle bestanden|*.*|Tekstbestanden|*.txt|CSV bestanden|*.csv",
                FilterIndex = 2,
                Title = "Opslaan Personen Info",
                OverwritePrompt = true,
                AddExtension = true,
                FileName = fileName,
                InitialDirectory = Environment.CurrentDirectory
            };
            if (sfd.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    for (int i = 0; i < firstNameListBox.Items.Count; i++)
                    {
                        sw.WriteLine($"{firstNameListBox.Items[i]}{sepChar}{lastNameListBox.Items[i]}");
                    }
                }
                MessageBox.Show($"Personen data opgeslagen naar {sfd.FileName}.");
            }
        }
        catch (IOException)
        {
            MessageBox.Show("fout bij schrijven naar bestand");
        }
        catch (Exception)
        {
            MessageBox.Show("Onverwachte fout");
        }
    }

    private void readFileButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Alle bestanden|*.*|Tekstbestanden|*.txt|CSV bestanden|*.csv",
                FilterIndex = 2,
                Title = "Open Personen Info",
                InitialDirectory = Environment.CurrentDirectory
            };
            if (ofd.ShowDialog() == true)
            {
                firstNameListBox.Items.Clear();
                lastNameListBox.Items.Clear();
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] contents = sr.ReadLine().Split(sepChar);
                        firstNameListBox.Items.Add(contents[0]);
                        lastNameListBox.Items.Add(contents[1]);
                    }
                }
            }
        }
        catch (IOException)
        {
            MessageBox.Show("fout bij lezen van bestand");
        }
        catch (Exception)
        {
            MessageBox.Show("Onverwachte fout");
        }
    }
}