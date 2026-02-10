using FluentValidation;
using MedApp.Domain.Models;


namespace MedApp.Application.Extension.Validators.ConsultaValidators
{
    public class ConsultaValidator : AbstractValidator<Consulta>
    {
        public ConsultaValidator()
        {
            RuleFor(x => x.FechaConsulta).NotEmpty().WithMessage("La fecha de consulta es obligatoria.").LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de consulta no puede ser una fecha futura.");
            RuleFor(x => x.Diagnostico).NotEmpty().WithMessage("El diagnóstico es obligatorio.").MaximumLength(500).WithMessage("El diagnóstico no debe exceder los 500 caracteres.");
            RuleFor(x => x.Tratamiento).NotEmpty().WithMessage("El tratamiento es obligatorio.").MaximumLength(500).WithMessage("El tratamiento no debe exceder los 500 caracteres.");
        }

    }
}
