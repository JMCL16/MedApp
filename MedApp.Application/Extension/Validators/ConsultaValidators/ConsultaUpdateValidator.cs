using FluentValidation;
using MedApp.Application.DTOs.Consultas;
using MedApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Application.Extension.Validators.ConsultaValidators
{
    public class ConsultaUpdateValidator : AbstractValidator<Consulta>
    {
        public ConsultaUpdateValidator() 
        {
            RuleFor(x => x.FechaConsulta).LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de consulta no puede ser una fecha futura.");
            RuleFor(x => x.Diagnostico).MaximumLength(500).WithMessage("El diagnóstico no debe exceder los 500 caracteres.");
            RuleFor(x => x.Tratamiento).MaximumLength(500).WithMessage("El tratamiento no debe exceder los 500 caracteres.");
        }
    }
}
