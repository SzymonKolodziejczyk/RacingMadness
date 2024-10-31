using UnityEngine;

public class InfiniteMovementObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 rotateAxis;
    [SerializeField] private float rotateSpeed;

    private void Update()
    {
        MoveObstacle();
        RotateObstacle();
    }

    private void MoveObstacle()
    {
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }

    private void RotateObstacle()
    {
        Quaternion rotation = Quaternion.Euler(rotateAxis.normalized * rotateSpeed * Time.deltaTime);
        transform.rotation *= rotation;
    }
}