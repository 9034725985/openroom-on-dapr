namespace OpenroomDapr.Shared
{
    public class AppState
    {
        public string? SelectedColor { get; private set; }

        public event Action? OnChange;

        public void SetTheme(string colour)
        {
            SelectedColor = colour;
            NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
