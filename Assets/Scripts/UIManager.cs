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
    public TMP_Text SOTotalText;
    public TMP_Text ShopSOTotalText;

    public CanvasGroup InGameUI;
    public CanvasGroup ShopUI;

    public CanvasGroup transitionPanel;

    public CanvasGroup[] allUIPanels;

    public float timeToFade = 2f;


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
        GameManager.Instance.onSOTotalChange += OnSOTotalChange;
        GameManager.Instance.onShopEnable += OnShopEnable;
        GameManager.Instance.onGameStart += OnGameStart;
 //       GameManager.Instance.onNextLevel += OnNextLevel;
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

    private void OnSOTotalChange()
    {
        SOTotalText.text = GameManager.Instance.totalSO.ToString();
        ShopSOTotalText.text = GameManager.Instance.totalSO.ToString();
    }

    private void OnShopEnable()
    {
        Debug.Log("UI Manager's OnShopEnable was called");
        Cursor.lockState = CursorLockMode.None;
        changeUIPanel(ShopUI);
    }

    private void OnGameStart()
    {
        changeUIPanel(InGameUI);
    }

 //   private void OnNextLevel()
 //   {
 //       changeUIPanel(InGameUI);
 //   }

    private void OnDisable()
    {
        GameManager.Instance.onSOCarriedChange -= OnSOCarriedChange;
        GameManager.Instance.onShopEnable -= OnShopEnable;
    }

    private void changeUIPanel(CanvasGroup cg)
    {
        //Debug.Log("trying to change the UI panel to active for the " + cg.name);
        transitionPanel.alpha = 1;
        foreach(CanvasGroup thisgroup in allUIPanels)
        {
            thisgroup.alpha = 0;
        }
        cg.alpha = 1;
        //StartCoroutine(Fade(cg, timeToFade, 1, 0));
        transitionPanel.alpha = 0;
    }

    IEnumerator Fade(CanvasGroup cg, float duration, float startAlpha, float endAlpha)
    {
        float currentFade;
        float t = 0f;
        while (t < timeToFade)
        {
            yield return null;
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / timeToFade);
            currentFade = Mathf.Lerp(startAlpha, endAlpha, blend);
            cg.alpha = currentFade;
            
        }
        
    }
}
