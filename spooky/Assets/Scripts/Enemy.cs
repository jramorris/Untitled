using UnityEngine;

public class Enemy : MonoBehaviour
{
    // variables
    public float health = 100f;
    public float torque;

    // RE player
    public GameObject player;
    public float pointsToGive = 50f;
    public float maxPlayerDistance = 5f;
    bool seen = false;

    // shooting
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

        // look at & shoot if close enough
        float playerDist = Vector3.Distance(player.transform.position, transform.position);
        if (playerDist <= maxPlayerDistance)
        {
            this.transform.GetChild(0).transform.LookAt(player.transform);
            if (currentTime >= waitTime)
            {
                Shoot();
                currentTime = 0;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
        player.GetComponent<Player>().points += pointsToGive;
    }

    public void Shoot()
    {
        Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
    }

}
