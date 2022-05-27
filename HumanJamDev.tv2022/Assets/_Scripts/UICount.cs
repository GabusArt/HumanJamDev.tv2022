using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICount : MonoBehaviour

{
    [Header("Texts Prefabs")]
    [SerializeField] TextMeshProUGUI totalHumans;
    [SerializeField] TextMeshProUGUI totalMales;
    [SerializeField] TextMeshProUGUI totalFemales;
    [SerializeField] TextMeshProUGUI totalEderly;


    [Header("Events Prefabs")]
    [SerializeField] GameObject newBornEvent;

    [Header("CanvasGroup")]
    [SerializeField] float alphaRange = 0.3f;




    float females = 0;
    float males = 0;
    float ederly = 0;

    CanvasGroup canvasGroup;
    Spawner spawner;
    PlayerController player;
    GameManager gameManager;

    


    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        canvasGroup = GetComponent<CanvasGroup>();
        player = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>(); 
    }



    void Start()
    {
        UpgradeUI();
    }

    void Update()
    {
        UpgradeUI();
        if (player.CanSeeBlueScreenCanvas())
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, alphaRange * Time.deltaTime);
        }
        else
        {
            canvasGroup.alpha = 0;
        }
    }


    private void UpgradeUI()
    {
        totalHumans.text = "Total Humans: " + string.Format("{0:0}/{1:0}", spawner.totalHumansCount() , gameManager.MaxHumans() + "  Millions");
        totalMales.text = "= " + string.Format("{0:0}", males);
        totalFemales.text = "= " + string.Format("{0:0}", females );
        totalEderly.text = "= " + string.Format("{0:0}", ederly );
    }


    public void AddWoman()
    {
        females++;
    }
    public void AddMan()
    {
        males++;
    }
    public void AddEderly()
    {
        ederly++;
        if (ederly > gameManager.MaXElderly())
        {
            Debug.Log("To Many elderly, you Lost");
            StartCoroutine(gameManager.FinishingGame(spawner.totalHumansCount()));
        }
    }

    public void ToggleWoman()
    {
        females--;
    }
    public void ToggleMan()
    {
        males--;
    }
    public void ToggleEderly()
    {
        ederly--;
    }

    public void ActivateNewBornEvent()
    {
        newBornEvent.SetActive(true);

    }
}
