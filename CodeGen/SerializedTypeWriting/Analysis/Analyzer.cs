using ItemStatusAutomationPeerGeneration;
using System.Diagnostics;

namespace CodeGen
{
    public static class Analyzer {
        public static void TestSerialization()
        {
            List<object> totest = new()
            {
            };
            totest.ForEach(t =>
            {
                var r = InstanceSerializer.Serialize(t);
            });
        }

        public static void DependencyPropertiesOfType(params Type[] types)
        {
            var allTypeDps = FrameworkElementTypeProvider.Types().Select(type => new TypeDps(type)).ToList();
            var allTypesWithDpsOfType = allTypeDps.Select(typeDps =>
            {
                return new { typeDps.Type.Name, Dps = typeDps.DependencyProperties.Where(dp => types.Contains(dp.PropertyType)).ToList() };
            }).Where(a => a.Dps.Any());

            foreach(var typeWithDps in allTypesWithDpsOfType)
            {
                Debug.WriteLine(typeWithDps.Name);
                foreach(var dp in typeWithDps.Dps)
                {
                    Debug.WriteLine($"{dp.Name} - {dp.OwnerType.Name} - {dp.PropertyType.Name}");
                }
                Debug.WriteLine("");
            }
        }

        private static IEnumerable<Type> GetIncludedTypes()
        {
            var allTypeDps = FrameworkElementTypeProvider.Types().Select(type => new TypeDps(type)).ToList();
            return allTypeDps.SelectMany(
                typeDps => typeDps.DependencyProperties.Where(
                    dp => !SerializationRestrictions.IgnoredDependencyProperties.Contains(dp)
                )
                .Select(dp => dp.PropertyType))
                .Distinct().Where(t => !SerializationRestrictions.IgnoreTypes.Any(et => et.Type == t));
        }

        public static bool ShouldProceed()
        {
            return !GetUncategorizedTypes().Any();
        }

        private static IEnumerable<Type> GetUncategorizedTypes()
        {
            return GetIncludedTypes().Where(t => IsNotSerializable(t) && IsNullableNotSerializable(t));
        }

        public static void WriteIncludedTypes()
        {
            File.WriteAllLines(@"C:\Users\tonyh\Downloads\IncludedTypes.txt", GetIncludedTypes().Select(t => t.FullName!));
        }

        public static void WriteUncategorizedTypes()
        {
            File.WriteAllLines(@"C:\Users\tonyh\Downloads\UncategorizedTypes.txt", GetUncategorizedTypes().Select(t => t.FullName!));
        }

        private static bool IsNotSerializable(Type t, bool includeConditional = true)
        {
            return !(t.IsEnum || t.IsPrimitive || IsSerializable(t, includeConditional));
        }

        private static bool IsSerializable(Type t,bool includeConditional)
        {
            return SerializationRestrictions.SafeTypes.Contains(t) || (includeConditional && SerializationRestrictions.ConditionallySafeTypes.Any(et => et.Type == t));
        }

        private static bool IsNullableNotSerializable(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var nullableType = type.GetGenericArguments()[0];
                return IsNotSerializable(nullableType);
            }
            return true;
        }

        /*
        
            Content - ContentControl
            DataContext - FrameworkElement
            Tag - FrameworkElement
            ToolTip - ToolTipService
            PrintTicket - FixedPage
            CommandParameter - ButtonBase
            SelectionBoxItem - ComboBox
            SelectedItem - Selector
            SelectedValue - Selector
            CurrentItem - DataGrid
            Item - DataGridRow
            Header - DataGridRow
            Header - HeaderedContentControl
            ColumnHeaderToolTip - GridView
            Icon - MenuItem
            SelectedContent - TabControl
            SelectedItem - TreeView
            SelectedValue - TreeView
        */
        public static void ObjectTypeDps()
        {
            var allTypeDps = FrameworkElementTypeProvider.Types().Select(type => new TypeDps(type)).ToList();
            var dps = allTypeDps.SelectMany(typeDps => typeDps.DependencyProperties.Where(dp => dp.PropertyType == typeof(object))).Distinct();
            foreach(var dp in dps)
            {
                Debug.WriteLine($"{dp.Name} - {dp.OwnerType.Name}");
            }
        }

        

        public static void Analyze()
        {
            var types = GetIncludedTypes();
            var typesThatCannotConstruct = new List<Type>();
            var serializationResults = new List<InstanceSerializationResult>();
            var typesNullInstantiated = new List<Type>();
            foreach (var type in types)
            {
                try
                {
                    var instance = Activator.CreateInstance(type);
                    if (instance != null)
                    {
                        serializationResults.Add(InstanceSerializer.Serialize(instance));
                    }
                    else
                    {
                        typesNullInstantiated.Add(type);
                    }


                }
                catch
                {
                    typesThatCannotConstruct.Add(type);
                }
            }
            var cannotConstructGrouping = typesThatCannotConstruct.GroupBy(t => t.IsAbstract);
            foreach (var g in cannotConstructGrouping)
            {
                var msg = g.Key ? "Abstract" : "Cannot construct"; 
                Debug.WriteLine(msg);
                foreach (var t in g)
                {
                    Debug.WriteLine(t.FullName);
                }
            }


            var numNullInstantiate = typesNullInstantiated.Count;//3 all Nullable one is {Name = "Nullable`1" FullName = "System.Nullable`1[[System.ComponentModel.ListSortDirection
            // Object to JObject
            var diffTypes = serializationResults.Where(r => r.ResultType == InstanceSerializationResultType.DifferentSerializedType).First();

            // may be a way to do loops 
            // FlowDocument "Self referencing loop detected for property 'Parent' with type 'System.Windows.Documents.FlowDocument'. Path 'ContentStart'."
            var serExc = serializationResults.Where(r => r.ResultType == InstanceSerializationResultType.SerializeException).First();

            Debug.WriteLine("DeserializationException");
            foreach (var deserExc in serializationResults.Where(r => r.ResultType == InstanceSerializationResultType.DeserializeException))
            {
                Debug.WriteLine(deserExc.Type.FullName);
                //Debug.WriteLine(deserExc.ErrorMessage);
                /*
                    Unable to cast object of type 'Newtonsoft.Json.Linq.JObject' to type 'System.Runtime.Remoting.Messaging.LogicalCallContext'.
                    Error setting value to 'DataType' on 'System.Windows.DataTemplate'.
                    Error setting value to 'SrgsMarkup' on 'System.Windows.Input.InputScope'.
                    Unable to cast object of type 'Newtonsoft.Json.Linq.JObject' to type 'System.Runtime.Remoting.Messaging.LogicalCallContext'.
                    Unable to cast object of type 'Newtonsoft.Json.Linq.JObject' to type 'System.Runtime.Remoting.Messaging.LogicalCallContext'.
                    Unable to cast object of type 'Newtonsoft.Json.Linq.JObject' to type 'System.Runtime.Remoting.Messaging.LogicalCallContext'.
                    Error setting value to 'Template' on 'System.Windows.Controls.ItemsPanelTemplate'.
                    Unable to cast object of type 'Newtonsoft.Json.Linq.JObject' to type 'System.Runtime.Remoting.Messaging.LogicalCallContext'.
                    Unable to cast object of type 'Newtonsoft.Json.Linq.JObject' to type 'System.Runtime.Remoting.Messaging.LogicalCallContext'.
                */
            }
            var resultsGrouped = serializationResults.GroupBy(r => r.ResultType);
            foreach (var g in resultsGrouped)
            {
                Debug.WriteLine($"{g.Key} - {g.Count()}");
            }
            /*
                Success - 75 - write these out and use as check to see if ok
                -----------------------------------------  All others should be able to have as type string
                DifferentSerializedType - 1
                DeserializeException - 11
                SerializeException - 1
            */
        }

    }
}
