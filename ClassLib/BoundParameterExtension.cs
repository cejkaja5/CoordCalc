using System;
using System.Windows.Markup;
using System.Windows.Data;

namespace CoordCalc
{
    public class BoundParameterExtension : MarkupExtension
    {
        public BindingBase Binding { get; set; }

        public BoundParameterExtension() { }

        public BoundParameterExtension(BindingBase binding)
        {
            Binding = binding;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // Retrieve the service to obtain the correct context for the binding
            var bindingExpression = Binding?.ProvideValue(serviceProvider);

            if (bindingExpression is BindingExpression bindingExpr)
            {
                // Evaluate the binding and return the value
                return bindingExpr.ParentBinding.ConverterParameter;
            }

            return null; // Return null if the binding cannot be evaluated correctly
        }
    }
}