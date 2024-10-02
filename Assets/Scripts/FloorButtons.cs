using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButtons : MonoBehaviour
{
    internal object material;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision cube)
    {
        if (cube.gameObject.CompareTag("PushtoButton"))
        {
            gameObject.GetComponent<FloorButtons>().material = Color.green;
        }
    }
    
}
