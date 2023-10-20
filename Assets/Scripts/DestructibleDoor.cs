using UnityEngine;

public class DestructibleDoor : Destructible
{
    [SerializeField] private Transform door;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] [Tooltip("Door movement speed")] private float speed = 1.0f;

    private bool startMoov;

    private void Update()
    {
        if (startMoov)
        {
            if (door.transform.position == targetPosition)
            {
                startMoov = false;
                return;
            }
            else
            {
                door.position = Vector3.Lerp(door.position, targetPosition, speed * Time.deltaTime);
            }
        }
    }

    protected override void AfterKill()
    {
        startMoov = true;
    }
}
