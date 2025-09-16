// 代码生成时间: 2025-09-16 18:17:45
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace MathCalculationTool
{
    /// <summary>
    /// A Blazor component that provides a simple math calculation tool.
    /// </summary>
    public partial class MathCalculationTool
    {
        private double _number1;
        private double _number2;
        private double _result;
        private string _error;

        private readonly List<string> _operations = new List<string> {
            "Add",
            "Subtract",
            "Multiply",
            "Divide"
        };

        /// <summary>
        /// Gets or sets the first number for calculation.
        /// </summary>
        [Parameter]
        public double Number1
        {
            get => _number1;
            set
            {
                _number1 = value;
                _result = 0;
                _error = null;
            }
        }

        /// <summary>
        /// Gets or sets the second number for calculation.
        /// </summary>
        [Parameter]
        public double Number2
        {
            get => _number2;
            set
            {
                _number2 = value;
                _result = 0;
                _error = null;
            }
        }

        /// <summary>
        /// Performs the calculation based on the selected operation.
        /// </summary>
        /// <param name="operation">The operation to perform.</param>
        public async Task CalculateAsync(string operation)
        {
            if (string.IsNullOrEmpty(operation))
            {
                _error = "Please select an operation.";
                await InvokeAsync(StateHasChanged);
                return;
            }

            try
            {
                switch (operation)
                {
                    case "Add":
                        _result = _number1 + _number2;
                        break;
                    case "Subtract":
                        _result = _number1 - _number2;
                        break;
                    case "Multiply":
                        _result = _number1 * _number2;
                        break;
                    case "Divide":
                        if (_number2 == 0)
                        {
                            _error = "Division by zero is not allowed.";
                            await InvokeAsync(StateHasChanged);
                            return;
                        }
                        _result = _number1 / _number2;
                        break;
                    default:
                        _error = $"Unsupported operation: {operation}";
                        break;
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }

            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Renders the math calculation tool component.
        /// </summary>
        /// <returns>The rendered HTML content.</returns>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "math-calculation-tool");
            builder.OpenElement(2, "form");
            builder.AddAttribute(3, "onsubmit", EventCallback.Factory.Create(this, HandleCalculate));

            builder.OpenElement(4, "div");
            builder.AddAttribute(5, "class", "form-group");
            builder.AddContent(6, $"Number 1: <input type='number' bind='@_number1' />");
            builder.CloseElement();

            builder.OpenElement(7, "div");
            builder.AddAttribute(8, "class", "form-group");
            builder.AddContent(9, $"Number 2: <input type='number' bind='@_number2' />");
            builder.CloseElement();

            builder.OpenElement(10, "div");
            builder.AddAttribute(11, "class", "form-group");
            builder.AddContent(12, $"Operation: <select bind='@_selectedOperation'>@foreach (var operation in _operations){{<option value='{operation}'>{operation}</option>}}
            </select>");
            builder.CloseElement();

            builder.OpenElement(13, "div");
            builder.AddAttribute(14, "class", "form-group");
            builder.AddContent(15, $"Result: <div>{_result}</div>");
            builder.CloseElement();

            if (!string.IsNullOrEmpty(_error))
            {
                builder.OpenElement(16, "div");
                builder.AddAttribute(17, "class", "error");
                builder.AddContent(18, $"Error: {_error}");
                builder.CloseElement();
            }

            builder.OpenElement(19, "div");
            builder.AddAttribute(20, "class", "form-group");
            builder.AddContent(21, "<button type='submit'>Calculate</button>");
            builder.CloseElement();

            builder.CloseElement();
            builder.CloseElement();
        }

        private void HandleCalculate(UIMouseEventArgs e)
        {
            e.PreventDefault();
            CalculateAsync(_selectedOperation);
        }

        private string _selectedOperation;
    }
}
