using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace SOSXR.SerializedInterface.Demo
{
    public class ExampleManualReferencerVisibleInInspector : MonoBehaviour
    {
        public InterfaceReference<IExample> AddInTheInspector;
        public List<InterfaceReferenceWrapper<IExample>> InterfaceReferences = new();


        private void Start()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            AddInTheInspector?.Value?.ExampleMethod(1);

            foreach (var item in InterfaceReferences)
            {
                item.Reference?.Value?.ExampleMethod(5);
            }

            stopwatch.Stop();

            Debug.Log("Time taken: " + stopwatch.ElapsedMilliseconds + " ms");
        }
    }
}