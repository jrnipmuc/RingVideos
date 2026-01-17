using System;
using Event_Monitor.Entities;
using Event_Monitor.Data;

namespace Event_Monitor
{
    internal class AppContext
    {
        private static AppContext _current;
        public static AppContext Current
        {
            get => _current ??= new AppContext();
            set => _current = value;
        }


        private Connection _currentLogin;
        private string _rootDirectory;

        public string RootDirectory
        {
            get => _rootDirectory;
            set
            {
                if (_rootDirectory != value)
                {
                    _rootDirectory = value;
                    OnContextChanged();
                }
            }
        }
        public Connection CurrentLogin
        {
            get => _currentLogin;
            set
            {
                if (_currentLogin != value)
                {
                    _currentLogin = value;
                    OnContextChanged();
                }
            }
        }

        public static event EventHandler ContextChanged;

        private static void OnContextChanged()
        {
            ContextChanged?.Invoke(Current, EventArgs.Empty);
        }
        public static async Task LoadFromDatabaseAsync()
        {
            using var repo = new SettingRepository();
            var activeSetting = await repo.GetActiveSettingAsync();
            
            if (activeSetting != null)
            {
                Current.RootDirectory = activeSetting.RootDirectory;
                Current.CurrentLogin = activeSetting.Connection;
            }
        }

        public static async Task SaveToDatabaseAsync()
        {
            if (Current.CurrentLogin == null)
            {
                throw new InvalidOperationException("Cannot save settings without an active connection.");
            }

            using var repo = new SettingRepository();
            await repo.UpdateActiveSettingAsync(Current.RootDirectory, Current.CurrentLogin.Id);
        }
    }
}