using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager _instance;
    public TextMeshProUGUI textHealth;
    public CanvasGroup healthImage;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        SetTextHealth();
    }
    public void SetTextHealth()
        {
        textHealth.text = "Health: " + Player._instance.health;
        //healthImage.alpha = 
        }
    }
