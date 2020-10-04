using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    public TMP_Text SoulOrbText;
    public TMP_Text SOCarriedText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onSOCarriedChange += OnSOCarriedChange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSOCarriedChange()
    {
        SOCarriedText.text = GameManager.Instance.playerC.soCarried.ToString();
        //Debug.Log("onSOCarriedChange sent from registered in UI should set value to "+ GameManager.Instance.playerC.soCarried.ToString());
    }



    private void OnDisable()
    {
        GameManager.Instance.onSOCarriedChange -= OnSOCarriedChange;
    }
}
