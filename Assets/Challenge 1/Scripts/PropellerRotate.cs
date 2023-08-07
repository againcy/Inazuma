using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerRotate : MonoBehaviour
{
    public FlyEngine flyEngineScript;
    private readonly Vector3 rotateDirection = new Vector3(0, 0, 1);
    private readonly float rotateSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateDirection * flyEngineScript.throttle *rotateSpeed);
    }
}
