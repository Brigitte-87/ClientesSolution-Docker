using System.ComponentModel.DataAnnotations;

namespace ClientesBlazor.Models
{
    public class Cliente : IValidatableObject
    {
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El CUI es obligatorio")]
        [RegularExpression(@"^[0-9]{13}$",
            ErrorMessage = "El CUI debe contener exactamente 13 números")]
        public string CUI { get; set; } = string.Empty;

        [Required(ErrorMessage = "El NIT es obligatorio")]
        [RegularExpression(@"^[0-9]+(?:-[0-9])?$",
            ErrorMessage = "El NIT debe contener números y, opcionalmente, un guion seguido del dígito verificador")]
        public string NIT { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los nombres son obligatorios")]
        [RegularExpression(
            @"^[\p{L}][\p{L}\p{M}]*(?:[ '-][\p{L}][\p{L}\p{M}]*)*$",
            ErrorMessage = "Los nombres solo pueden contener letras, espacios, guiones o apóstrofos")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [RegularExpression(
            @"^[\p{L}][\p{L}\p{M}]*(?:[ '-][\p{L}][\p{L}\p{M}]*)*$",
            ErrorMessage = "Los apellidos solo pueden contener letras, espacios, guiones o apóstrofos")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200,
            ErrorMessage = "La dirección no puede exceder 200 caracteres")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^[0-9]{8}$",
            ErrorMessage = "El teléfono debe contener exactamente 8 números")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; } =
            DateTime.Today.AddYears(-18);

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            var hoy = DateTime.Today;

            if (FechaNacimiento == default)
            {
                yield return new ValidationResult(
                    "La fecha de nacimiento debe ser válida",
                    new[] { nameof(FechaNacimiento) }
                );

                yield break;
            }

            if (FechaNacimiento.Date > hoy)
            {
                yield return new ValidationResult(
                    "La fecha de nacimiento no puede ser futura",
                    new[] { nameof(FechaNacimiento) }
                );

                yield break;
            }

            var edad = hoy.Year - FechaNacimiento.Year;

            if (FechaNacimiento.Date > hoy.AddYears(-edad))
            {
                edad--;
            }

            if (edad < 18)
            {
                yield return new ValidationResult(
                    "El cliente debe tener al menos 18 años",
                    new[] { nameof(FechaNacimiento) }
                );
            }
        }
    }
}