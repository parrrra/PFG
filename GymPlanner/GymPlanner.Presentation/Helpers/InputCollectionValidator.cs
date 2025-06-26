using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;

namespace GymPlanner.Presentation.Helpers;

public partial class InputCollectionValidator : ComponentBase
{
    private ValidationMessageStore _messageStore = default!;
    private EditContext? _subscribedEditContext;

    [CascadingParameter]
    private EditContext CurrentEditContext { get; set; } = default!;

    [Parameter]
    public ICollection<object> Items { get; set; } = new List<object>();

    protected override void OnInitialized()
    {
        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException($"{GetType()} requires a cascading parameter of type {nameof(EditContext)}.");
        }

        _messageStore = new ValidationMessageStore(CurrentEditContext);

        CurrentEditContext.OnValidationRequested += HandleValidationRequested;
        _subscribedEditContext = CurrentEditContext;
    }

    private void HandleValidationRequested(object? sender, ValidationRequestedEventArgs e)
    {
        foreach (var item in Items)
        {
            ClearMessagesForItem(item);
            ValidateItem(item);
        }

        CurrentEditContext.NotifyValidationStateChanged();
    }

    private void ValidateItem(object item)
    {
        var validationContext = new ValidationContext(item);
        var validationResults = new List<ValidationResult>();

        Validator.TryValidateObject(item, validationContext, validationResults, true);

        foreach (var result in validationResults)
        {
            if (string.IsNullOrEmpty(result.ErrorMessage)) continue;

            if (result.MemberNames.Any())
            {
                foreach (var memberName in result.MemberNames)
                {
                    var fieldIdentifier = new FieldIdentifier(item, memberName);
                    if (!_messageStore[fieldIdentifier].Contains(result.ErrorMessage))
                    {
                        _messageStore.Add(fieldIdentifier, result.ErrorMessage);
                    }
                }
            }
            else
            {
                var fieldIdentifier = new FieldIdentifier(item, string.Empty);
                if (!_messageStore[fieldIdentifier].Contains(result.ErrorMessage))
                {
                    _messageStore.Add(fieldIdentifier, result.ErrorMessage);
                }
            }
        }
    }

    public void ClearMessagesForItem(object item)
    {
        var properties = item.GetType().GetProperties();
        foreach (var property in properties)
        {
            var fieldIdentifier = new FieldIdentifier(item, property.Name);
            _messageStore.Clear(fieldIdentifier);
        }
        _messageStore.Clear(new FieldIdentifier(item, string.Empty));
    }
}
