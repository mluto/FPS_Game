using UnityEngine;

public class DestructibleLamp : Destructible
{
    [SerializeField] private Light lampLight;

    protected override void AfterKill()
    {
        lampLight.enabled =false;
    }
}
