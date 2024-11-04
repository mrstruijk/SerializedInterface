using System.Collections.Generic;
using UnityEngine;


public class ExampleReferencer : MonoBehaviour
{
    public InterfaceReference<IExample> ReferenceOne;
    public InterfaceReference<IExample> ReferenceTwo;
    public IExample ReferenceThree;
    public IList<IExample> ReferenceFour;


    private void Awake()
    {
        ReferenceTwo.UnderlyingValue = Interface.FindFirstAsComponent<IExample>();

        ReferenceThree = Interface.FindFirstByType<IExample>();

        ReferenceFour = Interface.FindByType<IExample>();
    }


    private void Start()
    {
        ReferenceOne?.Value?.ExampleMethod(1);

        ReferenceTwo?.Value?.ExampleMethod(2);

        ReferenceThree?.ExampleMethod(3);
        
        foreach (var example in ReferenceFour)
        {
            example.ExampleMethod(4);
        }
    }
}