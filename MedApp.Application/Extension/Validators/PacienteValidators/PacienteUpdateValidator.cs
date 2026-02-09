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
        }
    }
}
