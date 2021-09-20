using System;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPaginacaoDevMedia.Models
{
    [Table("Aulas")]
	public class Aula
    {
        [Required(ErrorMessage ="O ttitulo da aula deve ser preenchido.")]
        [MaxLength(50, ErrorMessage ="Otitulo da aula deve ter até 50 caracteres.")]
        [MinLength(50, ErrorMessage ="O titulo da aula deve ter pelo menos 10 caracteres.")]
        public string Titulo { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage ="A ordem da aula deve ser maior que zero.")]
        public int Ordem { get; set; }


        [JsonIgnore]
        [ForeignKey("Cursos")]
        public int IdCurso { get; set; }

        [JsonIgnore]
        public virtual Curso Curso { get; set; }
    }
}