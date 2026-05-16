using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour,IAnimal
{
    public void Speak()
    {
        Debug.Log("¸Û¸Û");
    }
    // Start is called before the first frame update
    void Start()
    {
        Speak();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
