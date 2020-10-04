using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Skeleton : MonoBehaviour, IDamageable
{


    private GameManager gm;
    public GameObject player;
    public float shootFreq = 3f;


    private bool isAggro = false;
    public GameObject projectile;
    public Transform[] muzzleTip = new Transform[2];
    public float bulletForce = 7f;

    private float timeSinceLastShot = 0;

    public float rotSpeed = 1.25f;

    public float maxHealth;
    public float health { get; set; }

//    public Image healthBar;
    public GameObject deadSkeletonModel;
    public ParticleSystem explosionParticles;

    private int currentMuzzle = 0;

    public float eDropChance = .5f;
    public GameObject drop;

    public float aggroRange = 15f;

    public int minDrops = 0;
    public int maxDrops = 3;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        player = GameObject.FindWithTag("Player");
        health = maxHealth;
        timeSinceLastShot = Random.Range(0, 1);
        //healthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < aggroRange)
        {
            isAggro = true;
        } else
        {
            isAggro = false;
        }
    }

    private void FixedUpdate()
    {
        if (isAggro)
        {
            Vector3 targetPoint = player.transform.position;
            targetPoint.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
            timeSinceLastShot += Time.fixedDeltaTime;
            if (timeSinceLastShot > shootFreq)
            {
                Shoot(player.transform);
            }

        }
    }

    public void Shoot(Transform target)
    {

        GameObject bullet = Instantiate(projectile, muzzleTip[(int)currentMuzzle].position, muzzleTip[(int)currentMuzzle].rotation);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(muzzleTip[currentMuzzle].forward * bulletForce, ForceMode.Impulse);
        currentMuzzle += 1;
        currentMuzzle %= muzzleTip.Length;
        timeSinceLastShot = 0;
    }

    public void Kill()
    {
        //Explode
        //Debug.Log("Kill called and skeleton has this health listed: " + health.ToString());
        Instantiate(explosionParticles, this.transform.position, transform.rotation);
        Instantiate(deadSkeletonModel, this.transform.position, transform.rotation);
        DropLoot();
        Die();

    }

    public void Damage(float damageTaken)
    {
        //Debug.Log("Skeleton taking damage: " + damageTaken.ToString() + " out of " + health.ToString() + " remaining.");
        if (damageTaken > 0) health = Mathf.Clamp(health - damageTaken, 0, maxHealth);
        //healthBar.fillAmount = health / maxHealth;
        if (health <= 0) Kill();
    }

    public void Heal(float damageHealed)
    {
        //throw new System.NotImplementedException();
    }

    public void Die()
    {
        Destroy(gameObject);
        return;
    }

    public void DropLoot()
    {
        int lootDropped = 0;


        while(lootDropped<= maxDrops)
        {
            if (lootDropped <= minDrops) // ensure minimum drops
            {
                Instantiate(drop, transform.position, Quaternion.identity);
            }
            else //else randomize for each other drop
            {
                float lootRoll = Random.Range(0f, 1f);
                if (lootRoll <= eDropChance)
                {

                    Instantiate(drop, transform.position, Quaternion.identity);
                }
            }
            lootDropped++;
        }

        //Debug.Log("Loot roll was " + lootRoll + " looking for " + eDropChance);

    }
}