using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WpfUIAutomationProperties.Serialization
{
    internal class ReflectionActivatingPropertySetter<T> : IReflectionActivatingPropertySetter where T:new()
    {
        private readonly Dictionary<string, Action<T, object>> setValues;

        public ReflectionActivatingPropertySetter()
        {
            setValues = TypeProperties.Get(typeof(T)).ToDictionary(
                p => p.Name,
                p => ReflectionActivatingPropertySetter<T>.CreateSetter(p)
            );
        }
        public T ActivateAndSetProperties(Dictionary<string, object> propertyValues)
        {
            var instance = (T)Activator.CreateInstance(typeof(T));
            foreach (var kvp in propertyValues)
            {
                setValues[kvp.Key](instance, kvp.Value);
            }
            return instance;
        }

        object IReflectionActivatingPropertySetter.ActivateAndSetProperties(Dictionary<string, object> propertyValues)
        {
            return ActivateAndSetProperties(propertyValues);
        }

        private static Action<T, object> CreateSetter(PropertyInfo property)
        {
            var propertSetMethod = property.GetSetMethod();
            var parameterExpressionT = Expression.Parameter(typeof(T), "param");
            var parameterExpressionPropertyValue = Expression.Parameter(typeof(object), "propertyValue");
            var convertExpression = Expression.Convert(parameterExpressionPropertyValue, property.PropertyType);
            var setPropertyExpression = Expression.Call(parameterExpressionT, propertSetMethod, convertExpression);
            return Expression.Lambda<Action<T, object>>(
                setPropertyExpression,
                parameterExpressionT,
                parameterExpressionPropertyValue).Compile();
        }
    }
}
