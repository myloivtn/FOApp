namespace CQApp.Views;

public partial class NotesPage : ContentPage
{
    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
    public NotesPage()
	{
		InitializeComponent();
	}
    void OnSaveButtonClicked(object sender, EventArgs e)
    {
        File.WriteAllText(_fileName, editor.Text);
    }

    void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (File.Exists(_fileName))
        {
            File.Delete(_fileName);
        }
        editor.Text = string.Empty;
    }
}