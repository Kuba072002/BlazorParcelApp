namespace BlazorParcelApp.Client
{
    public class AppState
    {
        public string Role = string.Empty;
        public event Action OnChange;
        public async Task Set(string role)
        {
            Role = role;
            OnChange?.Invoke();  
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
