using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Sea.Web.Common
{
    public abstract class Validation
    {
        public virtual void Validate()
        {
            Type _t = GetType();

            List<PropertyInfo> _invalidValue = new List<PropertyInfo>();

            _t.GetProperties()
                .Where(a => a.GetCustomAttributes().Any(t => t is ValidationAttribute))
                .ToList()
                .ForEach(a =>
                {
                    var _validationAttribs = a.GetCustomAttributes()
                        .OfType<ValidationAttribute>();

                    foreach (var _validationAttrib in _validationAttribs)
                    {
                        if (!_isValid(_validationAttrib, a.GetValue(this)))
                        {
                            _invalidValue.Add(a);
                        }
                    }
                });

            if (_invalidValue.Any())
            {
                throw new Exception("Invalid Property");
            }
        }

        private bool _isValid(ValidationAttribute attrib, object value)
        {
            if (attrib is RequiredAttribute)
            {
                if (value == null)
                {
                    return false;
                }
            }
            return true;
        }

    }
}