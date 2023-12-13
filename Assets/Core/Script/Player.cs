using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] int healthPoint = 20;
    [SerializeField] TextMeshPro uiHealthPoint = null;

    public int HealthPoint 
    { 
        get
        { 
            return healthPoint; 
        } 
        set
        {
            healthPoint = value;
            uiHealthPoint.text = $"Life : {healthPoint}";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthPoint(int _damage) 
    { 
        
        HealthPoint += _damage;
        
        Debug.Log($"{HealthPoint}");
        if (HealthPoint <= 0)
        {
            HealthPoint = 0;
            
            Debug.Log("QuitGame");
            EditorApplication.isPlaying = false;
        }
    }
}
