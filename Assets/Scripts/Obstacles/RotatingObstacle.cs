using System.Collections;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [SerializeField] private float rotAngleZ;
    [SerializeField] private float delay;

    private Quaternion startRotation;
    private Quaternion endRotation;

    private void Start()
    {
        StartCoroutine(RotateObstacle());
    }

    private IEnumerator RotateObstacle()
    {
        while (true)
        {
            yield return StartCoroutine(RotateToAngle(rotAngleZ));
            yield return new WaitForSeconds(delay);
            yield return StartCoroutine(RotateToAngle(-rotAngleZ));
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator RotateToAngle(float angle)
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 
            transform.eulerAngles.z + angle);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / delay;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
    }
}