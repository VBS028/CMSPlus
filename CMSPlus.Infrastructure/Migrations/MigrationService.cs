using System.Configuration;
using CMSPlus.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using EvolveDb;

namespace CMSPlus.Domain.Services;

public class MigrationService:IMigrationService
{
    private readonly Evolve _evolve;
    public MigrationService(string connectionString)
    {
        var cnx = new SqlConnection(connectionString);
        var location = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Migrations");
        _evolve = new Evolve(cnx)
        {
            Locations = new[]{location},
            IsEraseDisabled = true,
        };
    }

    public void Migrate()
    {
        try
        {
            _evolve.Migrate();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to run migration", ex);
        }
    }
}