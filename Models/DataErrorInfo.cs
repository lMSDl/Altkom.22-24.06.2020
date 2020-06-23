using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Internal;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class DataErrorInfo : IDataErrorInfo
    {
        private IValidator Validator => new AttributedValidatorFactory().GetValidator(GetType());
        public bool IsValid => Validator?.Validate(this).IsValid ?? true;

        private static string GetErrors(ValidationResult result)
        {
            if (result == null || !result.Errors.Any())
                return string.Empty;
            return string.Join(Environment.NewLine, result.Errors.Select(x => x.ErrorMessage).ToArray());
        }

        public string this[string propertyName]
        {
            get
            {
                if (Validator == null)
                    return string.Empty;

                var result = Validator.Validate(new ValidationContext(this, new PropertyChain(), new MemberNameValidatorSelector(new List<string> { propertyName })));
                return GetErrors(result);
            }
        }


        public string Error => Validator == null ? string.Empty : GetErrors(Validator.Validate(this));
    }
}
