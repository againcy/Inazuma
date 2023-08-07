using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public FlyEngine flyEngine;
    public float forwardSpeed = 0.1f;
    public float pitchSpeed = 100;//俯仰速度
    public float rollSpeed = 50;//横滚速度
    public float yawSpeed = 0.5f;//偏航速度
    public float verticalInput;
    public float horizontalInput;

    [Range(-100, 100)]
    private float yawInput;//左右偏航输入
    

    // Start is called before the first frame update
    void Start()
    {
        flyEngine.ChangeThrottle(0);
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime;

        //俯仰
        verticalInput = Input.GetAxis("Vertical");
        transform.Rotate(Vector3.right * deltaTime * pitchSpeed * verticalInput);

        //横滚
        horizontalInput = Input.GetAxis("Horizontal");
        //transform.Rotate(Vector3.back * deltaTime * rollSpeed * horizontalInput);
        flyEngine.Roll(horizontalInput);

        //左右偏航
        ChangeYawStatus();
        transform.Rotate(Vector3.up * deltaTime * yawSpeed * yawInput);

        //向前速度
        ChangeThrottle();
        //transform.Translate(Vector3.forward * deltaTime * flyEngine.GetSpeed());
    }

    /// <summary>
    /// 根据Vertical输入更改油门
    /// </summary>
    private void ChangeThrottle()
    {
        if (Input.GetKey(KeyCode.J))
        {
            flyEngine.ChangeThrottle(1);
        }
        else if (Input.GetKey(KeyCode.K))
        {
            flyEngine.ChangeThrottle(-1);
        }
    }

    /// <summary>
    /// QE进行左右偏航
    /// </summary>
    private void ChangeYawStatus()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            yawInput += -1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            yawInput += 1;
        }
        else
        {
            yawInput += yawInput > 0 ? -1 : 1;
        }
    }

}
