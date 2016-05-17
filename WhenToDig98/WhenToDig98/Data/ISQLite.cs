
using SQLite;

namespace WhenToDig98.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
