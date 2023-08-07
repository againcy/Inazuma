using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float currZ;

    private readonly float speedPerThrottle = 0.2f;
    private readonly float turnSpeed = 25.0f;
    private float horizontalInput;
    private float throttle;
    private readonly float maxThrottle = 100;
    private readonly float minThrottle = -50;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        var deltaTime = Time.deltaTime;
        currZ = transform.position.z;// debug only
        //前进
        ChangeThrottle();
        transform.Translate(Vector3.forward * deltaTime * speedPerThrottle * throttle);
        //转向
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * deltaTime * turnSpeed * horizontalInput);
    }

    /// <summary>
    /// 根据Vertical输入更改油门
    /// </summary>
    private void ChangeThrottle()
    {
        var verticalInput = Input.GetAxis("Vertical");
        throttle += verticalInput;
        if (throttle >= maxThrottle) throttle = maxThrottle;
        if (throttle < minThrottle) throttle = minThrottle;
    }

}
