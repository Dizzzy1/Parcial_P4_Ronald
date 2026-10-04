using Dapper;
using Microsoft.Data.Sqlite;
using PrimerParcial1.Models;

namespace PrimerParcial1.Services;

public class NumbersService(IConfiguration configuration)
{
    private SqliteConnection CreateConnection()
    {
        var connectionString =
            configuration.GetConnectionString("DefaultConnection");

        return new SqliteConnection(connectionString);
    }

    public async Task InitializeAsync()
    {
        using var connection = CreateConnection();

        await connection.OpenAsync();

        const string sql = """
            CREATE TABLE IF NOT EXISTS NumberRecords
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Fecha TEXT NOT NULL,
                Numero INTEGER NOT NULL,
                Resultado INTEGER NOT NULL
            );
            """;

        await connection.ExecuteAsync(sql);
    }

    public async Task<int> SaveAsync(NumberRecordSet record)
    {
        using var connection = CreateConnection();

        const string sql = """
            INSERT INTO NumberRecords
                (Fecha, Numero, Resultado)
            VALUES
                (@Fecha, @Numero, @Resultado);

            SELECT last_insert_rowid();
            """;

        var parameters = new
        {
            Fecha = DateTime.Now,
            record.Numero,
            record.Resultado
        };

        return await connection.ExecuteScalarAsync<int>(sql, parameters);
    }

    public async Task UpdateAsync(int id, NumberRecordSet record)
    {
        using var connection = CreateConnection();

        const string sql = """
            UPDATE NumberRecords
            SET
                Fecha = @Fecha,
                Numero = @Numero,
                Resultado = @Resultado
            WHERE Id = @Id;
            """;

        var parameters = new
        {
            Id = id,
            Fecha = DateTime.Now,
            record.Numero,
            record.Resultado
        };

        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<NumberRecordGet?> GetByIdAsync(int id)
    {
        using var connection = CreateConnection();

        const string sql = """
        SELECT
            Id,
            Fecha,
            Numero,
            Resultado
        FROM NumberRecords
        WHERE Id = @Id;
        """;

        var result = await connection.QuerySingleOrDefaultAsync<dynamic>(
            sql,
            new { Id = id });

        if (result is null)
            return null;

        return new NumberRecordGet(
            (int)result.Id,
            DateTime.Parse(result.Fecha.ToString()!),
            (int)result.Numero,
            (int)result.Resultado
        );
    }

    public async Task<IEnumerable<NumberRecordGet>> GetListAsync()
    {
        using var connection = CreateConnection();

        const string sql = """
        SELECT
            Id,
            Fecha,
            Numero,
            Resultado
        FROM NumberRecords
        ORDER BY Id;
        """;

        var results = await connection.QueryAsync<dynamic>(sql);

        return results.Select(x => new NumberRecordGet(
            (int)x.Id,
            DateTime.Parse(x.Fecha.ToString()!),
            (int)x.Numero,
            (int)x.Resultado
        ));
    }
}