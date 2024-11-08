using System;
using UnityEngine;
using Object = UnityEngine.Object;


namespace SOSXR.SerializedInterface
{
    [Serializable]
    public class InterfaceReference<TInterface, TObject> where TObject : Object where TInterface : class
    {
        [SerializeField] [HideInInspector] private TObject underlyingValue;


        public InterfaceReference()
        {
        }


        public InterfaceReference(TObject target)
        {
            underlyingValue = target;
        }


        public InterfaceReference(TInterface @interface)
        {
            underlyingValue = @interface as TObject;
        }


        public TInterface Value
        {
            get => underlyingValue switch
                   {
                       null => null,
                       TInterface @interface => @interface,
                       _ => throw new InvalidOperationException($"{underlyingValue} needs to implement interface {nameof(TInterface)}.")
                   };
            set => underlyingValue = value switch
                                     {
                                         null => null,
                                         TObject newValue => newValue,
                                         _ => throw new ArgumentException($"{value} needs to be of type {typeof(TObject)}.", string.Empty)
                                     };
        }

        public TObject UnderlyingValue
        {
            get => underlyingValue;
            set => underlyingValue = value;
        }


        public static implicit operator TInterface(InterfaceReference<TInterface, TObject> obj)
        {
            return obj.Value;
        }


        // Method to find the first object implementing TInterface
        public static InterfaceReference<TInterface, TObject> FindObject()
        {
            var foundObject = Interface.FindByType<TInterface>(true);

            return foundObject != null ? new InterfaceReference<TInterface, TObject>(foundObject as TObject) : null;
        }


        // Method to find all objects implementing TInterface
        public static InterfaceReference<TInterface, TObject>[] FindObjects()
        {
            var foundObjects = Interface.FindByType<TInterface>();

            if (foundObjects == null)
            {
                return null;
            }

            var resultArray = new InterfaceReference<TInterface, TObject>[foundObjects.Count];

            for (var i = 0; i < foundObjects.Count; i++)
            {
                resultArray[i] = new InterfaceReference<TInterface, TObject>(foundObjects[i] as TObject);
            }

            return resultArray;
        }
    }


    [Serializable]
    public class InterfaceReference<TInterface> : InterfaceReference<TInterface, Object> where TInterface : class
    {
    }


    [Serializable]
    public class InterfaceReferenceWrapper<T> where T : class
    {
        public InterfaceReference<T> Reference;
    }
}