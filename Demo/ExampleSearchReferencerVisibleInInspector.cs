using System.Collections.Generic;
using System.Diagnostics;
using SOSXR.SerializedInterface.Tools;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace SOSXR.SerializedInterface.Demo
{
    public class ExampleSearchReferencerVisibleInInspector : MonoBehaviour
    {
        [DisableEditing] public InterfaceReference<IExample> FindAsComponent;
        [DisableEditing] public List<InterfaceReferenceWrapper<IExample>> InterfaceReferences;


        private void OnEnable()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            FindReferences();

            Debug.Log("Time taken: " + stopwatch.ElapsedMilliseconds + " ms" + " to find references");

            stopwatch.Restart();

            DoMethods();

            Debug.Log("Time taken: " + stopwatch.ElapsedMilliseconds + " ms" + " to execute methods");

            stopwatch.Stop();
        }


        private void FindReferences()
        {
            FindAsComponent.UnderlyingValue = Interface.FindFirstAsComponent<IExample>();
            InterfaceReferences = Interface.FindAllAsList<IExample>();
        }


        private void DoMethods()
        {
            FindAsComponent?.Value?.ExampleMethod(2);

            foreach (var item in InterfaceReferences)
            {
                item.Reference?.Value?.ExampleMethod(5);
            }
        }
    }
}