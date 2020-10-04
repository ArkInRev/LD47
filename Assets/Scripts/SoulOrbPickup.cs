using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulOrbPickup : MonoBehaviour, IInteractable
{
    private GameManager gm;
    public CapsuleCollider cc;
    public PlayerController pc;
    public GameObject player;
    public ParticleSystem pickupEffect;

    public Transform initialParent;

    public GameObject spawnParentGO;
    GameObject goInstantiated;

    public int orbValue = 1;

    public void Interact()
    {
        //throw new System.NotImplementedException();

        pc.PickupSO(orbValue);
        Instantiate(pickupEffect, transform.position, Quaternion.identity);
        Destroy(initialParent.gameObject);




    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        player = GameObject.FindWithTag("Player");
        pc = player.GetComponent<PlayerController>();

        initialParent = gameObject.transform;

        spawnParentGO = GameObject.FindWithTag("MapGOTagger");
        goInstantiated = initialParent.gameObject;
        goInstantiated.transform.SetParent(spawnParentGO.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log("Collided with " + other.name);    
        if (other.CompareTag("Player"))
        {
            Interact();
        }
    }
}
