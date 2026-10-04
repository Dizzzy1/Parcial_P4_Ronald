namespace PrimerParcial1.Models;
public record NumberRecordGet(
    int Id, 
    DateTime Fecha, 
    int Numero, 
    int Resultado
 );
public record NumberRecordSet(
    int Numero, 
    int Resultado
 );