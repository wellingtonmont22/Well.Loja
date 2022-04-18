


using MySql.Data.MySqlClient;

namespace Well.Loja.Data
{
    public class DbSession : IAsyncDisposable
    {
        public MySqlConnection Connection { get; set; }

        public DbSession(IConfiguration configuration)
        {
            Connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
            Connection.Open();
        }

        public async ValueTask DisposeAsync()
        {
            await Connection.CloseAsync();
            await Connection.ClearAllPoolsAsync();
            await Connection.DisposeAsync();
        }
    }
}
