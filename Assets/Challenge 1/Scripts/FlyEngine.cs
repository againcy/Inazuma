using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEngine : MonoBehaviour
{
    [Range(0, 1000)]
    public float throttle = 0;
    private readonly float maxThrottle = 1000;
    private readonly float minThrottle = 0;
    public float throttleCoeff = 1.0f;
    public Vector3 thrust;//�����ṩ������

    //����=1/2 * �����ܶ� * ����ϵ�� * ������� * v^2
    public int liftCoeffLeft = 2;//1/2 * �����ܶ� * ����ϵ��
    public int liftCoeffRight = 2;//���� ��������ϵ��
    private readonly int liftCoeffMax = 3;
    private readonly int liftCoeffMin = 1;
    private readonly int liftCoeffNormal = 2;
    public float liftWingArea = 100.0f;
    public float mass = 200.0f;//����kg
    public float leftLift;
    public float rightLift;
    
    private float resisCoeff;//����ϵ�� ����������ٶȺ���������� �������������
    
    public float speed;
    public float maxSpeed = 0.2f;
    
    public PlayerControllerX player;

    // Start is called before the first frame update
    void Start()
    {
        resisCoeff = maxThrottle * throttleCoeff / (maxSpeed * maxSpeed);//����������ٶȺ���������� �������������
    }

    // Update is called once per frame
    void Update()
    {
        //�����ٶȺ��ƶ�����
        var time = Time.deltaTime;
        var acc = (thrust.z - resisCoeff * speed * speed) / mass;//F=ma, F=����-����
        var dist = speed + time * time * acc / 2;//S=V0+0.5*at^2
        speed += acc * time;//v=v0+at;
        transform.Translate(Vector3.forward * dist);

        CalculateSpeed();
    }

    /// <summary>
    /// ����
    /// </summary>
    public void ChangeThrottle(int dir)
    {
        throttle += dir;
        if (throttle > maxThrottle) throttle = maxThrottle;
        if (throttle < minThrottle) throttle = minThrottle;
        thrust = new Vector3(0, 0, throttle * throttleCoeff);
    }

    /// <summary>
    /// ���
    /// </summary>
    /// <param name="dir">+1 roll left; -1 roll right</param>
    public void Roll(float dir)
    {
        if (dir < 0)
        {
            liftCoeffLeft = liftCoeffMin;
            liftCoeffRight = liftCoeffMax;
        }
        else if (dir > 0)
        {
            liftCoeffLeft = liftCoeffMax;
            liftCoeffRight = liftCoeffMin;
        }
        else
        {
            liftCoeffLeft = liftCoeffNormal;
            liftCoeffRight = liftCoeffNormal;
        }
    }

    private void CalculateSpeed()
    {
        //var lift = liftCoeff * liftWingArea * speed * speed;
        var time = Time.deltaTime;
        var speed2 = speed * speed;
        leftLift = liftCoeffLeft * liftWingArea * speed2;
        rightLift = liftCoeffRight * liftWingArea * speed2;
        transform.Rotate(Vector3.back * time * (leftLift-rightLift) * 50);
    }

    
}
