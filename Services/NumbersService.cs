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

    public async Task<int> SaveAsync(NumberRecord record)
    {
        using var connection = CreateConnection();

        const string sql = """
            INSERT INTO NumberRecords
                (Fecha, Numero, Resultado)
            VALUES
                (@Fecha, @Numero, @Resultado);

            SELECT last_insert_rowid();
            """;

        return await connection.ExecuteScalarAsync<int>(sql, record);
    }

    public async Task UpdateAsync(NumberRecord record)
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

        await connection.ExecuteAsync(sql, record);
    }

    public async Task<NumberRecord?> GetByIdAsync(int id)
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

        return await connection.QueryFirstOrDefaultAsync<NumberRecord>(
            sql,
            new { Id = id });
    }

    public async Task<IEnumerable<NumberRecord>> GetListAsync()
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

        return await connection.QueryAsync<NumberRecord>(sql);
    }
}