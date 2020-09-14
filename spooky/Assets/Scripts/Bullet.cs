using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f;
    public float bulletSpeed = 10f;
    public float bulletLife = 5f;
    private float maxDistance;
    private GameObject triggeringEnemy;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= bulletLife)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Enemy>().health -= damage;
        }
    }
}
