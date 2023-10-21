using UnityEngine;

public class Base : MonoBehaviour
{
    public Transform playerSpawnPoint;
    private float _health = 100f;
    private float _nonSpawnRange = 40f;

    public void SetHealth(float health)
    {
        _health = health;
    }
    public void SetNonSpawnRange(float nonSpawnRange)
    {
        _nonSpawnRange = nonSpawnRange;
    }
    public float GetNonSpawnRange()
    {
        return _nonSpawnRange;
    }
    public float GetHealth()
    {
        return _health;
    }

}
