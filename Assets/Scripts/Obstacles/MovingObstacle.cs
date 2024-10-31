using System.Collections;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private float moveDistance;
    [SerializeField] private float speed;
    [SerializeField] private float delay;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + moveDirection.normalized * moveDistance;

        StartCoroutine(MoveObstacle());
    }

    private IEnumerator MoveObstacle()
    {
        while (true)
        {
            yield return StartCoroutine(MoveToPosition(targetPosition));
            yield return new WaitForSeconds(delay);
            yield return StartCoroutine(MoveToPosition(startPosition));
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPos)
    {
        float distance = Vector3.Distance(transform.position, targetPos);
        float duration = distance / speed;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(transform.position, targetPos, t);
            yield return null;
        }
    }
}