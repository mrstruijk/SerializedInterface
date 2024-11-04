using UnityEngine;


public class ExampleReferencer : MonoBehaviour
{
    public InterfaceReference<IExample> ReferenceOne;
    public InterfaceReference<IExample> ReferenceTwo;
    public IExample ReferenceThree;


    private void Awake()
    {
        ReferenceThree = InterfaceHelper.FindObject<IExample>();
        ReferenceTwo = InterfaceHelper.FindObject<InterfaceReference<IExample>>();
    }


    private void Start()
    {
        Debug.Log("Reference two is null: " + (ReferenceTwo == null));
        Debug.Log("Reference three is null: " + (ReferenceThree == null));

        ReferenceOne.Value.ExampleMethod(1);

        if (ReferenceTwo != null)
        {
            ReferenceTwo.Value.ExampleMethod(2);
        }

        if (ReferenceThree != null)
        {
            ReferenceThree.ExampleMethod(3);
        }
    }
}