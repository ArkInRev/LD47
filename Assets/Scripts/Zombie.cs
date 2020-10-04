using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class Zombie : MonoBehaviour, IDamageable
{
    private GameManager gm;
    public GameObject player;

    private bool isAggro = false;
    public float bulletForce = 7f;
    public bool isFireGhost = false;
    private float timeSinceLastShot = 0;

    public float rotSpeed = 1.25f;

    public float maxHealth;
    public float health { get; set; }

    //    public Image healthBar;
    public GameObject deadZombieModel;
    public ParticleSystem explosionParticles;
    public GameObject ghostFire;

    public float eDropChance = .5f;
    public GameObject drop;

    public float aggroRange = 15f;

    public int minDrops = 0;
    public int maxDrops = 3;


    public float lookRadius = 20f;
    NavMeshAgent agent;
    public float shootFreq = 3f;

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        player = GameObject.FindWithTag("Player");
        health = maxHealth;
        //healthBar.fillAmount = 1;
        timeSinceLastShot = Random.Range(0, 1);
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < aggroRange)
        {
            isAggro = true;
        }
        else
        {
            isAggro = false;
        }
    }

    private void FixedUpdate()
    {
        if (isAggro)
        {
            agent.SetDestination(player.transform.position);
            if (isFireGhost)
            {
                if (timeSinceLastShot > shootFreq)
                {
                    DropFire();
                }
            }


        } else
        {
            agent.SetDestination(transform.position);
        }
    }

    public void DropFire()
    {
        Instantiate(ghostFire, this.transform.position, transform.rotation);
    }

    public void Kill()
    {
        //Explode

        Instantiate(explosionParticles, this.transform.position, transform.rotation);
        Instantiate(deadZombieModel, this.transform.position, transform.rotation);
        DropLoot();
        Die();

    }

    public void Damage(float damageTaken)
    {
        if (damageTaken > 0) health = Mathf.Clamp(health - damageTaken, 0, maxHealth);
        //healthBar.fillAmount = health / maxHealth;
        // gm.LarvaHealthChange();
        if (health <= 0) Kill();
    }

    public void Heal(float damageHealed)
    {
        //turrets are not healing
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


        while (lootDropped <= maxDrops)
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
    }
}