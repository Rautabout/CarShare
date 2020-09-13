using CarShare.Properties;
using MySql.Data.MySqlClient;

namespace CarShare.DAL
{
    class DBConnection
    {
        //Pola odpowiadające za przygotowanie połaczenia
        private readonly MySqlConnectionStringBuilder connStringBuilder = new MySqlConnectionStringBuilder();
        private static DBConnection instance = null;
        public static DBConnection Instance => instance ?? (instance = new DBConnection());
        public MySqlConnection Connection => new MySqlConnection(connStringBuilder.ToString());

        //Konstruktor obiektu połaczenia pobierający dane z konfiguracji
        public DBConnection()
        {
            connStringBuilder.Server = Settings.Default.host;
            connStringBuilder.UserID = Settings.Default.user;
            connStringBuilder.Password = Settings.Default.password;
            connStringBuilder.Database = Settings.Default.database;
            connStringBuilder.Port = Settings.Default.port;
        }
    }
}
