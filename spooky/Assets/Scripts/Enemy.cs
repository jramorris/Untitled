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
    public float attackDistance = 1f;
    public bool playerSeen = false;
    
    // shooting
    public GameObject bullet;
    public GameObject bulletSpawnPoint;
    public Animator zombieAnim;
    public float waitTime;
    private float currentTime;

    // walking
    public float walkTime = 2f;
    public float currentWalkTime = 0f;

    // methods
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        zombieAnim = GetComponentInChildren<Animator>();
        zombieAnim.SetBool("playerSeen", false);
    }

    private void Update()
    {
        Vector3 newPos = this.transform.position + this.transform.GetChild(0).transform.position;
        if (health <= 0)
        {
            Die();
        }

        bool seen = zombieAnim.GetBool("playerSeen");
        if (!seen) {
            Walk();
        }


        // look at & shoot if close enough
        float playerDist = Vector3.Distance(player.transform.position, transform.position);
        if (playerDist <= maxPlayerDistance)
        {
            zombieAnim.SetBool("playerSeen", true);
            Vector3 relativePos = player.transform.position - transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
            //this.transform.GetChild(0).transform.LookAt(player.transform);
            Debug.Log("dist: " + playerDist);

            if (playerDist <= attackDistance)
            {
                Debug.Log("Attack!");
                zombieAnim.SetBool("canAttack", true);
            } else
            {
                zombieAnim.SetBool("canAttack", false);
            }
        } else
        {
            zombieAnim.SetBool("playerSeen", false);
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

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.collider.tag;
        if (tag != "Ground" && tag != "Player")
        {
            NewWalkDirection();
        }
        
    }

    private void NewWalkDirection()
    {
        float dir = Random.Range(1, 4);
        this.transform.Rotate(0, dir * 90, 0);
    }

    private void Walk()
    {
        currentWalkTime += Time.deltaTime;
        if (currentWalkTime >= walkTime)
        {
            zombieAnim.SetBool("idling", true);
            currentWalkTime = -1f;
        }
    }
}
