using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public Transform target1; // First target
    public Transform target2; // Second target
    public float moveSpeed = 5f; // Speed of movement
    public float rotationSpeed = 5f; // Speed of rotation
    public float switchDistance = 1f; // Distance to switch to the other target

    private Transform currentTarget; // The current target

    void Start()
    {
        // Set the first target as the initial target
        if (target1 != null)
        {
            currentTarget = target1;
        }
    }

    void Update()
    {
        if (currentTarget == null) return;

        // Rotate towards the current target
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move towards the current target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        // Switch target if close enough
        if (Vector3.Distance(transform.position, currentTarget.position) < switchDistance)
        {
            SwitchTarget();
        }
    }

    void SwitchTarget()
    {
        if (currentTarget == target1 && target2 != null)
        {
            currentTarget = target2;
        }
        else if (currentTarget == target2 && target1 != null)
        {
            currentTarget = target1;
        }
    }
}
