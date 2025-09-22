namespace Lista_Validação_de_dados.DTOs
{
    using System.ComponentModel.DataAnnotations;
    using Lista_Validação_de_dados.Validation;
    public class AlunoCreateDto
    {
        [Required(ErrorMessage = "Nome Obrigatorio")]
        [StringLength(255, MinimumLength = 3)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Ra E obrigatorio")]
        [RegularExpression(@"^\d{4,10}$", ErrorMessage = "RA inválido")]
        public string Ra { get; set; }
        [Required(ErrorMessage = "Email E obrigatorio")]
        [EmailAddress(ErrorMessage = "Email em formato invalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Cpf E obrigatorio")]
        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "CPF inválido")]
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
    }
    public class AlunoUpdateDto : AlunoCreateDto
    {
        [Required(ErrorMessage = "Nome E obrigatorio")]
        [StringLength(255, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email E obrigatorio")]
        [EmailAddress(ErrorMessage = "Email em formato invalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Cpf E obrigatorio")]
        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "CPF inválido")]
        public string Cpf { get; set; }
        public bool Ativo { get; set; }

    }
}

