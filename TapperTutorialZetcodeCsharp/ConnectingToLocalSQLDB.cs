using System.Configuration;
using System.Data.SqlClient;

namespace TapperTutorialZetcodeCsharp
{
    public class ConnectingToLocalSQLDB
    {
        
        string cs = ConfigurationManager.ConnectionStrings["DapperConnection"].ConnectionString;
        using IDbConnection con = new SqlConnection(cs);

    }
