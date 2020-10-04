using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VignetteManager : MonoBehaviour
{
    public GameObject[] basicVig; //Common/unpurchased vignettes
    public GameObject[] purchasedVig; //Purchased Vignettes
    public Transform[] vignetteLocations; // locations to instantiate the vignettes
    public float[] rotations = { 0.0f, 90f, 180f, 270f }; // rotation of instantiated vignette

    public GameObject vignetteParent; //game object to contain all of the spawned locations


    private List<Transform> listLocs;
    private List<Transform> shufflingLocs;
    private List<Transform> shuffledLocs;

    private GameObject goInstantiated;

    void Start()
    {

        listLocs = new List<Transform>();
        shufflingLocs = new List<Transform>();
        shuffledLocs = new List<Transform>();

        for (int i = 0; i < vignetteLocations.Length; i++)
        {
            listLocs.Add(vignetteLocations[i]);
            shufflingLocs.Add(vignetteLocations[i]);
        }

        DestroyAllInstantiatedVignettes(vignetteParent);
        InstantiateVignettes();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyAllInstantiatedVignettes(GameObject parent)
    {
        if (parent.transform.childCount > 0)
        {
            foreach (Transform child in parent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

        }
    }

    private void InstantiateVignettes()
    {
        List<Transform> shuffled = new List<Transform>();


        if (purchasedVig.Length > 0)
        {

        }

        while (shufflingLocs.Any())
        {
            Transform thisVignette = randomLocationTransform();
            goInstantiated = Instantiate(randomPrefab(basicVig), thisVignette.position, GetSpawnRotation(randomPrefabRotation()));
            goInstantiated.transform.SetParent(vignetteParent.transform);

        }

    }

    Transform randomLocationTransform()
    {
        Transform selectedTransform;

        int index = Random.Range((int)0, (int)shufflingLocs.Count);
        selectedTransform = shufflingLocs[index];
        shufflingLocs.RemoveAt(index);
        shuffledLocs.Add(selectedTransform);
        return selectedTransform;
    }

    float randomPrefabRotation()
    {
        float randomRot;
        int randIndex = Random.Range((int)0, rotations.Length);
        randomRot = rotations[randIndex];

        return randomRot;
    }

    GameObject randomPrefab(GameObject[] goArray)
    {
        GameObject randomGO;
        int randIndex = Random.Range((int)0, goArray.Length);
        randomGO = goArray[randIndex];

        return randomGO;
    }

    Quaternion GetSpawnRotation(float rotationAngle)
    {
        Quaternion rotationQ = Quaternion.identity;
        rotationQ.eulerAngles = new Vector3(0, rotationAngle, 0);


        return rotationQ;
    }
}
