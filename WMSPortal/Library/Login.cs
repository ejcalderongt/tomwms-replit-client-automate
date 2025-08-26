using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace WMSPortal.Library
{
    public class ContrasenaValidate : Attribute, IModelValidator
    {
        public string ErrorMessage { get; set; }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            return new List<ModelValidationResult>
            {
                new ModelValidationResult("", ErrorMessage)
            };
        }


    }
}
