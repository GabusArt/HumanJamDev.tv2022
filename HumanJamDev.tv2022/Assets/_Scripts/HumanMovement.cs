using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HumanMovement : MonoBehaviour
{

    // fase 1 = Baby, fase 2 = child, fase 3 = adult, fase 4= old, fase 5= super old

    Transform center = null;

    [Header("Human Velocity")]
    [SerializeField] int f1VelocityMax = 20;
    [SerializeField] int f2VelocityMax = 50;
    [SerializeField] int f3VelocityMax = 30;
    [SerializeField] int f4VelocityMax = 15;
    [SerializeField] int f5VelocityMax = 8;
    [SerializeField] int velocityRange = 5;


    [Header("Human Lifetime")]
    [SerializeField] int timeRange = 6;
    [SerializeField] int timeToFase2min = 10;
    [SerializeField] int timeToFase3min = 10;
    [SerializeField] int timeToFase4min = 10;
    [SerializeField] int timeToFase5min = 10;
    public int humanFase = 1;
    bool alreadyDie = false;
    int ederlyActivator = 0;

    [Header("Human Reproduction")]
    [SerializeField] GameObject reproduction;

    [SerializeField] GameObject newBornParticles;


    [SerializeField] TextMeshPro humanName;


    [Header("Animations")]
    Animator myAnimator;


    float fase2Time;
    float fase3Time;
    float fase4Time;
    float fase5Time;
    float fase6Time;
    bool isTimerActive = true;
    public bool isMale = true;
    bool canHaveBabies = false;
    public float actualVelocity;




    Vector3 startPosition;
    SpriteRenderer mySprite;
    UICount uICount;
    AudioManager audioManager;
    NamePicker namePicker;



    float distance = -1f;
    public float timer = 0f; //recordar quitar public


    private void Awake()
    {
        startPosition = transform.position;
        mySprite = GetComponentInChildren<SpriteRenderer>();
        uICount = FindObjectOfType<UICount>();
        myAnimator = GetComponentInChildren<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        center = FindObjectOfType<Center>().GetComponent<Transform>();
        namePicker = GetComponent<NamePicker>();
    }

    void Start()
    {
        if (center == null) return;
        distance = Vector3.Distance(center.position, transform.position);
        GetRandomVelocities();
        GetRandomTimes();
        NewBornBehaviour();
        reproduction.SetActive(false);
        //texto1.text isMale = true ? uICount.AddMan() : uICount.AddWoman();
        if (isMale)
        {
            uICount.AddMan();
            humanName.text = namePicker.PickRandomMalesName();
            return;
        }
        uICount.AddWoman();
        humanName.text = namePicker.PickRandomFemaleName();


        //print("f2time: " + fase2Time + "f3time: " + fase3Time + "f4time: " + fase4Time + "f5time: " + fase5Time);

    }

    private void NewBornBehaviour()
    {
        Instantiate(newBornParticles, transform);
        audioManager.PlayNewBornSound();
    }

    private void OnEnable()
    {
        if (alreadyDie)
        {
            Restart();
        }

    }
    void Restart()
    {
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        GetRandomVelocities();
        GetRandomTimes();
        timer = 0;
        isTimerActive = true;
        humanFase = 1;
        canHaveBabies = false;
        reproduction.SetActive(false);
        if (isMale)
        {
            uICount.AddMan();
            return;
        }
        uICount.AddWoman();
    }
    private void OnDisable()
    {
        alreadyDie = true;

        if (ederlyActivator > 0)
        {
            uICount.ToggleEderly();
            ederlyActivator = 0;
        }

        if (isMale)
        {
            uICount.ToggleMan();
            return;
        }
        uICount.ToggleWoman();
    }

    void Update()
    {
        if (isTimerActive)
        {
            timer += Time.deltaTime;
        }


        if (center == null) return;
        if (timer >= fase2Time && timer < fase3Time)
        {
            humanFase = 2;
          

        }
        if (timer >= fase3Time && timer < fase4Time)
        {
            humanFase = 3;
            canHaveBabies = true;
            reproduction.SetActive(true);
           
        }
        if (timer >= fase4Time && timer < fase5Time)
        {
            humanFase = 4;
          
        }
        if (timer >= fase5Time)
        {
            humanFase = 5;
            canHaveBabies = false;
            reproduction.SetActive(false);
            if (ederlyActivator < 1)
            {
                uICount.AddEderly();
                ederlyActivator++;
            }
            
            isTimerActive = false;
        }

        if (myAnimator == null)
        {
            print("check animator");
        }
        myAnimator.SetInteger("Fase", humanFase);

        transform.RotateAround(center.position, new Vector3(0, 0, 1), VelocityBehaviour(humanFase) * Time.deltaTime);
        actualVelocity = VelocityBehaviour(humanFase);

    }



    private float VelocityBehaviour(int value)
    {
        switch (value)
        {
            case 5:
                return f5VelocityMax;

            case 4:
                return f4VelocityMax;

            case 3:
                return f3VelocityMax;
            case 2:
                return f2VelocityMax;
            case 1:
                return f1VelocityMax;
            default:
                return 0;
        }
    }

    private void GetRandomTimes()
    {
        fase2Time = Random.Range(timeToFase2min, timeToFase2min + timeRange);
        fase3Time = Random.Range(fase2Time + timeToFase3min, fase2Time + timeToFase3min + timeRange);
        fase4Time = Random.Range(fase3Time + timeToFase4min, fase3Time + timeToFase4min + timeRange);
        fase5Time = Random.Range(fase4Time + timeToFase5min, fase4Time + timeToFase5min + timeRange);
    }

    private void GetRandomVelocities()
    {
        f1VelocityMax = Random.Range(f1VelocityMax - velocityRange, f1VelocityMax+1);
        f2VelocityMax = Random.Range(f2VelocityMax - velocityRange, f2VelocityMax+1);
        f3VelocityMax = Random.Range(f3VelocityMax - velocityRange, f3VelocityMax+1);
        f4VelocityMax = Random.Range(f4VelocityMax - velocityRange, f4VelocityMax+1);
        f5VelocityMax = Random.Range(f5VelocityMax - velocityRange, f5VelocityMax+1);

    }

    public bool IsMale()
    {
        return isMale;
    }

    public int HumanFase()
    {
        return humanFase;
    }

    public bool CanHumanHaveBabies()
    {
        return canHaveBabies;
    }
}
