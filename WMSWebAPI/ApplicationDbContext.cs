using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WMSWebAPI.Models;
public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
    public SqlConnection GetSqlConnection()
    {
        return (SqlConnection)Database.GetDbConnection();
    }
    public required DbSet<TransaccionProcesada> TransaccionesProcesadas { get; set; }

}