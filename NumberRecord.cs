namespace PrimerParcial1.Models;

public class NumberRecord
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public int Numero { get; set; }
    public int Resultado { get; set; }

    public NumberRecord()
    {
    }

    public NumberRecord(
        int id,
        DateTime fecha,
        int numero,
        int resultado)
    {
        Id = id;
        Fecha = fecha;
        Numero = numero;
        Resultado = resultado;
    }
}