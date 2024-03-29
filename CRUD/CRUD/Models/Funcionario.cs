﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ECore.WebAPI.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string RG { get; set; }
        public string? Foto { get; set; }

        [ForeignKey("Departamento")]
        public int DepartamentoId { get; set; }
        public Departamento? Departamento { get; set; }

    }
}
