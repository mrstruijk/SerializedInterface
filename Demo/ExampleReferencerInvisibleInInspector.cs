using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace SOSXR.SerializedInterface.Demo
{
    public class ExampleReferencerInvisibleInInspector : MonoBehaviour
    {
        public IExample NotVisibleInInspector;
        public IList<IExample> ListNotVisibleInInspector;


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
            NotVisibleInInspector = Interface.FindFirstByType<IExample>();
            ListNotVisibleInInspector = Interface.FindByType<IExample>();
        }


        private void DoMethods()
        {
            NotVisibleInInspector?.ExampleMethod(3);

            foreach (var item in ListNotVisibleInInspector)
            {
                item.ExampleMethod(4);
            }
        }
    }
}