using System.Collections.Generic;
using UnityEngine;


namespace SOSXR.SerializedInterface.Demo
{
    public class ExampleReferencerInvisibleInInspector : MonoBehaviour
    {
        public IExample NotVisibleInInspector;
        public IList<IExample> ListNotVisibleInInspector;


        private void Awake()
        {
            NotVisibleInInspector = Interface.FindFirstByType<IExample>();
            ListNotVisibleInInspector = Interface.FindByType<IExample>();
        }


        private void Start()
        {
            NotVisibleInInspector?.ExampleMethod(3);

            foreach (var item in ListNotVisibleInInspector)
            {
                item.ExampleMethod(4);
            }
        }
    }
}