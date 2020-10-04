using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityOrganizer : MonoBehaviour
{
    public Transform initialParent;
    public GameObject spawnParentGO;
    GameObject goInstantiated;

    // Start is called before the first frame update
    void Start()
    {
        initialParent = gameObject.transform;

        spawnParentGO = GameObject.FindWithTag("MapGOTagger");
        goInstantiated = initialParent.gameObject;
        goInstantiated.transform.SetParent(spawnParentGO.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
