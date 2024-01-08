using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [SerializeField] float RotAngleZ;
    [SerializeField] float delay;
    
    /* Update is called once per frame
    void Update()
    {
        float rZ = Mathf.SmoothStep(0,RotAngleZ,Mathf.PingPong(Time.time,delay));
		transform.rotation = Quaternion.Euler(0f, 0f, RotAngleZ * Mathf.Sin(Time.time * delay));
    }*/

    void Start(){
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(3);
        while (true)
        {
            yield return StartCoroutine(RotateMe1());
            yield return waitTime;
            yield return StartCoroutine(RotateMe2());
            yield return waitTime;
        }
    }

    IEnumerator RotateMe1()
    {
    float startRotation = transform.eulerAngles.z;
    float endRotation = startRotation + RotAngleZ;
    float t = 0.0f;

        while ( t  < delay )
        {
            t += Time.deltaTime;

            float zRotation = Mathf.Lerp(startRotation, endRotation, t / delay) % 360.0f;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 
            zRotation);

            yield return null;
        }
    }

    IEnumerator RotateMe2()
    {
    float startRotation = transform.eulerAngles.z;
    float endRotation = startRotation - RotAngleZ;
    float t = 0.0f;

        while ( t  < delay )
        {
            t += Time.deltaTime;

            float zRotation = Mathf.Lerp(startRotation, endRotation, t / delay) % 360.0f;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 
            zRotation);

            yield return null;
        }
    }
}