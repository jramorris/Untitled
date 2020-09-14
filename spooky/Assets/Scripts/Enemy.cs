using UnityEngine;

public class Enemy : MonoBehaviour
{
    // variables
    public float health = 100f;
    public GameObject player;
    public float pointsToGive = 50f;
    public GameObject bullet;
    public GameObject bulletSpawnPoint;

    public float waitTime;
    private float currentTime;

    // methods
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }

        this.transform.LookAt(player.transform);

        if (currentTime >= waitTime)
        {
            Shoot();
            currentTime = 0;
        } else
        {
            currentTime += Time.deltaTime;
        }
    }

    public void Die()
    {
        Debug.Log(this.gameObject.name + "Died");
        Destroy(this.gameObject);
        player.GetComponent<Player>().points += pointsToGive;
    }

    public void Shoot()
    {
        Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
    }

}
