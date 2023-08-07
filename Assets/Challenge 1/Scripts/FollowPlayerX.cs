using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public GameObject BackCameraPoint;
    public GameObject PilotCameraPoint;

    public float smoothness = 0.5f;

    private List<GameObject> cameraPoints = new List<GameObject>();
    private int currCameraView = -1;

    public GameObject plane;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //初始化相机位置
        cameraPoints.Add(BackCameraPoint);
        cameraPoints.Add(PilotCameraPoint);
        ChangeCameraType();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeCameraType();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, cameraPoints[currCameraView].transform.position, smoothness);
        transform.rotation = cameraPoints[currCameraView].transform.rotation;
    }

    void ChangeCameraType()
    {
        currCameraView++;
        currCameraView %= cameraPoints.Count;
    }
}
