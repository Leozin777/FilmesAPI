using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Required(ErrorMessage = "O título do filme é obrigatório")]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O genêro do filme é obrigatório")]
        [MaxLength(50, ErrorMessage = "O gênero pode ter no máximo 50 caracteres")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "O diretor é obrigatório")]
        public string Diretor { get; set; }

        [Required]
        [Range(70, 600, ErrorMessage = "A duração deve ser de 70 a 600 minutos")]
        public int Duracao { get; set; }

    }
}
