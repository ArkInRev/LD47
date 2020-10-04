using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private GameManager gm;

    public int soCarried = 0;
    public float maxHealth = 100f;
    public float health { get; set; }


    public GameObject player;
    //public bool hasExitedStart = false;


    void Start()
    {
        gm = GameManager.Instance;
        GameManager.Instance.onResetPlayer += onResetPlayer;
        player = GameObject.FindWithTag("Player");
        health = maxHealth;
        //gm.SetPlayerGameObject(this.gameObject);
        //maxHealth = gm.GetPlayerHealth();
        //health = gm.GetPlayerHealth();
        //ResetSOCarried();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damageTaken)
    {
        if (damageTaken > 0) health = Mathf.Clamp(health - damageTaken, 0, maxHealth);
        if (health <= 0) Kill();
        //gm.PlayerHealthChange(); //game manager fires off event that player was damaged to listeners
    }

    public void Kill()
    {

        Debug.Log("The Player Was Killed, Not implemented");
        //ResetSOCarried();
        //gm.PlayerKilled();
    }

    public void ResetSOCarried()
    {
        // Put soul orbs into the phylactery. 
        // 


        soCarried = 0;
        gm.SOChange();   // game manager notifies listeners of Soul orb count change
    }

    public void ResetHealth()
    {
        // reset health. 
        // 
        health = maxHealth;
        //gm.PlayerHealthChange(); //game manager fires off event that player was damaged to listeners
    }

    public void PickupSO()
    {
        soCarried += 1;
        gm.SOChange(); // game manager notifies that a soul orb has been collected. 
    }

    private void onResetPlayer()
    {
       // Debug.Log("Reset Player from Player ControllerL player at: " + player.transform.position.ToString() + " and respawn at " + GameManager.Instance.playerRespawnLocation.transform.position.ToString());
        //player.transform.position.Set(gm.playerRespawnLocation.position.x, gm.playerRespawnLocation.position.y, gm.playerRespawnLocation.position.z);
        //player.transform.rotation.Set(gm.playerRespawnLocation.rotation.x, gm.playerRespawnLocation.rotation.y, gm.playerRespawnLocation.rotation.z, gm.playerRespawnLocation.rotation.w);
        ResetSOCarried();
        ResetHealth();

    }

    private void OnDisable()
    {
        GameManager.Instance.onResetPlayer -= onResetPlayer;
    }
}
