using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] float distanceToCover;
    [SerializeField] float speed;
    [SerializeField] float RotAngleX;
    [SerializeField] float RotAngleZ;

    private Vector3 startingPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        //startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 v = startingPosition;
        //v.z += distanceToCover * Mathf.Sin(Time.time * speed);
        //transform.position = v;
        float rX = Mathf.SmoothStep(0,RotAngleX,Mathf.PingPong(Time.time * speed,1));
        float rZ = Mathf.SmoothStep(0,RotAngleZ,Mathf.PingPong(Time.time * speed,1));
        transform.rotation = Quaternion.Euler(rX,0,rZ);
    }
}
