public class PersonaRequestDto
{
    public string? Id { get; set; }
    public string PrimerNombre { get; set; }
    public string SegundoNombre { get; set; }
    public string PrimerApellido { get; set; }
    public string SegundoApellido { get; set; }
    public DateOnly FechaDeNacimiento { get; set; }
    public DateOnly FechaDeResidencia { get; set; }
    public string TipoDeSangre { get; set; }
    public int? RegionId { get; set; }
    public int? TipoPersonaId { get; set; }
    public string Genero { get; set; }
    public byte[] Foto { get; set; }
    public string Estado { get; set; }
}
