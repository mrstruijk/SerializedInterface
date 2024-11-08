using System.Collections.Generic;
using SOSXR.SerializedInterface.Tools;
using UnityEngine;


namespace SOSXR.SerializedInterface.Demo
{
    public class ExampleReferencerVisibleInInspector : MonoBehaviour
    {
        public InterfaceReference<IExample> AddInTheInspector;
        [DisableEditing] public InterfaceReference<IExample> FindAsComponent;
        [DisableEditing] public List<InterfaceReferenceWrapper<IExample>> InterfaceReferences;


        private void Awake()
        {
            InterfaceReferences = Interface.FindAllAsList<IExample>();
        }


        private void Start()
        {
            FindAsComponent.UnderlyingValue = Interface.FindFirstAsComponent<IExample>();
            AddInTheInspector?.Value?.ExampleMethod(1);

            FindAsComponent?.Value?.ExampleMethod(2);

            foreach (var item in InterfaceReferences)
            {
                item.Reference?.Value?.ExampleMethod(5);
            }
        }
    }
}