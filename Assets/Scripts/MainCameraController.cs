using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] float rotationSpeed =1.0f;
    [SerializeField] float upperLimit = 80;
    [SerializeField] float lowerLimit = -40;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentEuler = transform.eulerAngles;
        //Debug.Log(currentEuler);
        if (Input.GetKey(KeyCode.UpArrow)) 
        {
            currentEuler += new Vector3(rotationSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow)) 
        {
            currentEuler += new Vector3(-rotationSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            currentEuler += new Vector3(0, rotationSpeed, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            currentEuler += new Vector3(0, -rotationSpeed, 0);
        }
        //Debug.Log(currentEuler);
        if (currentEuler.x >= upperLimit && currentEuler.x <= 180)
            currentEuler.x = upperLimit;
        if (currentEuler.x <= 360+lowerLimit && currentEuler.x >= 180)
            currentEuler.x = lowerLimit;
        transform.eulerAngles = currentEuler;
    }
}
