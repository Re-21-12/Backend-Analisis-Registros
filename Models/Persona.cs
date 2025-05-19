using System;
using System.Collections.Generic;

namespace Backend_Analisis.Models;

public partial class Persona
{
    public int Id { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string? SegundoNombre { get; set; }

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public DateOnly FechaDeNacimiento { get; set; }

    public string? TipoDeSangre { get; set; }

    public int? RegionId { get; set; }

    public string? Genero { get; set; }

    public byte[]? Foto { get; set; }

    public virtual Region? Region { get; set; }
}
