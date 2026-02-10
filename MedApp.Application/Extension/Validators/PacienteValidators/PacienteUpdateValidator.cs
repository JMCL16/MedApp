using FluentValidation;
using MedApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Application.Extension.Validators.PacienteValidators
{
    public class PacienteUpdateValidator : AbstractValidator<Paciente>
    {
        public PacienteUpdateValidator()
        {
            RuleFor(x => x.OperacionesPrevias).MaximumLength(500).WithMessage("Las operaciones previas no deben exceder los 500 caracteres.");
            RuleFor(x => x.AntecedentesFamiliares).MaximumLength(500).WithMessage("Los antecedentes familiares no deben exceder los 500 caracteres.");
            RuleFor(x => x.Direccion).MaximumLength(100).WithMessage("La dirección no debe exceder los 100 caracteres.");
            RuleFor(x => x.Ocupacion).MaximumLength(50).WithMessage("La ocupación no debe exceder los 50 caracteres.");
        }
    }
}
