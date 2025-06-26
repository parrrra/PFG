using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;

namespace GymPlanner.Presentation.Helpers
{
    public static class ValidationHelper
    {
        public static void ValidateSingleField(object model, ValidationMessageStore store, FieldIdentifier field)
        {
            store.Clear(field);

            var property = model.GetType().GetProperty(field.FieldName);
            if (property is null) return;

            var value = property.GetValue(model);
            var context = new ValidationContext(model) { MemberName = field.FieldName };

            var results = new List<ValidationResult>();
            if (!Validator.TryValidateProperty(value, context, results))
            {
                foreach (var result in results)
                    store.Add(field, result.ErrorMessage!);
            }
        }

        public static bool ValidateProperties(object model, ValidationMessageStore store, EditContext context, IEnumerable<string> propertyNames)
        {
            bool valid = true;
            store.Clear();

            foreach (var name in propertyNames)
            {
                var field = new FieldIdentifier(model, name);
                var property = model.GetType().GetProperty(name);
                if (property is null) continue;

                var value = property.GetValue(model);
                var validationContext = new ValidationContext(model) { MemberName = name };
                var results = new List<ValidationResult>();

                if (!Validator.TryValidateProperty(value, validationContext, results))
                {
                    valid = false;
                    foreach (var result in results)
                        store.Add(field, result.ErrorMessage!);
                }
            }

            context.NotifyValidationStateChanged();
            return valid;
        }

        public static void ClearValidationMessagesForStep(object model, ValidationMessageStore store, EditContext context, IEnumerable<string> propertyNames)
        {
            foreach (var name in propertyNames)
            {
                store.Clear(new FieldIdentifier(model, name));
            }

            context.NotifyValidationStateChanged();
        }

        public static bool ValidateAll(object model, ValidationMessageStore store, EditContext context)
        {
            var results = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            bool isValid = Validator.TryValidateObject(model, validationContext, results, true);

            store.Clear();

            foreach (var result in results)
            {
                foreach (var member in result.MemberNames)
                {
                    var field = new FieldIdentifier(model, member);
                    store.Add(field, result.ErrorMessage!);
                }
            }

            context.NotifyValidationStateChanged();
            return isValid;
        }
    }
}
