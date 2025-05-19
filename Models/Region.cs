using System;
using System.Collections.Generic;

namespace Backend_Analisis.Models;

public partial class Region
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
