using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


namespace SOSXR.SerializedInterface
{
    public static class Interface
    {
        static Interface()
        {
            InitInterfaceToComponentMapping();
        }


        private static Dictionary<Type, List<Type>> _interfaceToComponentMapping;
        private static Type[] _allTypes;


        private static void InitInterfaceToComponentMapping()
        {
            _interfaceToComponentMapping = new Dictionary<Type, List<Type>>();
            _allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).ToArray();

            foreach (var curInterface in _allTypes.Where(t => t.IsInterface && !IsSystemInterface(t.ToString())))
            {
                var componentsList = GetTypesInheritedFromInterface(curInterface)
                                     .Where(t => typeof(Component).IsAssignableFrom(t) && !t.IsInterface)
                                     .Distinct().ToList();

                if (componentsList.Count > 0)
                {
                    _interfaceToComponentMapping[curInterface] = componentsList;
                }
            }
        }


        private static bool IsSystemInterface(string typeName)
        {
            var exclusions = new[]
            {
                "unity", "system.", "mono.", "icsharpcode.", "nsubstitute", "nunit.", "microsoft.", "boo.",
                "serializ", "json", "log.", "logging", "test", "editor", "debug"
            };

            return exclusions.Any(exclusion => typeName.Contains(exclusion, StringComparison.OrdinalIgnoreCase));
        }


        private static IList<Type> GetTypesInheritedFromInterface(Type type)
        {
            return _allTypes.Where(curType => type.IsAssignableFrom(curType) && typeof(Object).IsAssignableFrom(curType)).ToList();
        }


        private static IList<T> FindComponents<T>(bool firstOnly) where T : class
        {
            if (!_interfaceToComponentMapping.TryGetValue(typeof(T), out var types) || types.Count == 0)
            {
                Debug.LogError("No descendants found for type " + typeof(T));

                return Array.Empty<T>();
            }

            var resList = new List<T>();

            foreach (var curType in types)
            {
                var objects = firstOnly
                    ? new[] {Object.FindFirstObjectByType(curType)}
                    : Object.FindObjectsByType(curType, FindObjectsSortMode.None);

                var orderedObjects = objects
                                     .OfType<Component>() // Ensure we only work with Components
                                     .OrderBy(obj => obj.transform.GetSiblingIndex())
                                     .Cast<T>() // Cast the ordered list to the correct type
                                     .ToList();

                resList.AddRange(orderedObjects);
            }

            return resList;
        }


        public static IList<T> FindByType<T>(bool firstOnly = false) where T : class
        {
            return FindComponents<T>(firstOnly);
        }


        public static T FindFirstByType<T>() where T : class
        {
            return FindByType<T>(true).FirstOrDefault();
        }


        public static IList<T> GetAsComponents<T>(this Component component, bool firstOnly = false) where T : class
        {
            if (!_interfaceToComponentMapping.TryGetValue(typeof(T), out var types) || types.Count == 0)
            {
                Debug.LogError("No descendants found for type " + typeof(T));

                return Array.Empty<T>();
            }

            var resList = new List<T>();

            foreach (var curType in types)
            {
                var components = firstOnly
                    ? new[] {component.GetComponent(curType)}
                    : component.GetComponents(curType);

                var orderedComponents = components
                                        .OfType<Component>() // Ensure we're only working with Components
                                        .OrderBy(comp => comp.transform.GetSiblingIndex())
                                        .Cast<T>() // Cast the ordered list to the correct type
                                        .ToList();

                resList.AddRange(orderedComponents);
            }

            return resList;
        }


        public static T GetFirstAsComponent<T>(this Component component) where T : class
        {
            return GetAsComponents<T>(component, true).FirstOrDefault();
        }


        public static IList<Component> FindAsComponents<T>() where T : class
        {
            if (!_interfaceToComponentMapping.TryGetValue(typeof(T), out var componentTypes))
            {
                Debug.LogError($"No components implementing {typeof(T).Name} found.");

                return Array.Empty<Component>();
            }

            var components = new List<Component>();

            foreach (var componentType in componentTypes)
            {
                var foundComponents = Object.FindObjectsByType(componentType, FindObjectsSortMode.None);
                components.AddRange(foundComponents.OfType<Component>());
            }

            return components
                   .OrderBy(comp => comp.transform.GetSiblingIndex())
                   .ToList();
        }


        public static Component FindFirstAsComponent<T>() where T : class
        {
            return FindAsComponents<T>().FirstOrDefault();
        }


        public static List<InterfaceReferenceWrapper<T>> FindAllAsList<T>() where T : class
        {
            var targetList = new List<InterfaceReferenceWrapper<T>>();

            var references = FindByType<T>();

            foreach (var reference in references)
            {
                if (reference is Object validReference)
                {
                    targetList.Add(new InterfaceReferenceWrapper<T>
                    {
                        Reference = new InterfaceReference<T> {UnderlyingValue = validReference}
                    });
                }
            }

            return targetList;
        }
    }
}