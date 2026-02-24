using FluentValidation;
using MedApp.Application.DTOs.Paciente;
using MedApp.Domain.Models;


namespace MedApp.Application.Extension.Validators.PacienteValidators
{
    public class PacienteValidator : AbstractValidator<PacienteDTO>
    {
        public PacienteValidator()
        {
            RuleFor (x => x.Cedula)
                .NotEmpty().WithMessage("La cédula es obligatoria.")
                .Matches(@"^\d{11}$").WithMessage("La cédula debe tener 11 dígitos numéricos.");
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio.").MaximumLength(50).WithMessage("El nombre no debe exceder los 50 caracteres.");
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("El apellido es obligatorio.").MaximumLength(50).WithMessage("El apellido no debe exceder los 50 caracteres.");
            RuleFor(x => x.FechaNacimiento).NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.").LessThan(DateTime.Now).WithMessage("La fecha de nacimiento debe ser una fecha pasada.");
            RuleFor(x => x.Edad).NotEmpty().WithMessage("La edad es obligatoria.").Must(ValidarEdad).WithMessage("La edad debe ser un número mayor a 0");
            RuleFor(x => x.Genero).NotEmpty().WithMessage("El género es obligatorio.").MaximumLength(20).WithMessage("El género no debe exceder los 20 caracteres.");
            RuleFor(x => x.Nacionalidad).NotEmpty().WithMessage("La nacionalidad es obligatoria.").MaximumLength(50).WithMessage("La nacionalidad no debe exceder los 50 caracteres.");
            RuleFor(x => x.Direccion).NotEmpty().WithMessage("La dirección es obligatoria.").MaximumLength(100).WithMessage("La dirección no debe exceder los 100 caracteres.");
            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .Matches(@"^\d{10}$").WithMessage("El teléfono debe tener 10 dígitos numéricos.");
            RuleFor(x => x.OperacionesPrevias).MaximumLength(500).WithMessage("Las operaciones previas no deben exceder los 500 caracteres.");
            RuleFor(x => x.AntecedentesFamiliares).MaximumLength(500).WithMessage("Los antecedentes familiares no deben exceder los 500 caracteres.");

        }

        private bool ValidarEdad(string edad)
        {
            if (int.TryParse(edad, out int edadInt))
            {
                return edadInt > 0;
            }
            return false;
        }
    }
}
