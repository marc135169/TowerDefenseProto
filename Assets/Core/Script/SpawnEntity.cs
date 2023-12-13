using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpawnEntity : MonoBehaviour
{
    
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] float rotationSpeed = 150;
    [SerializeField] float currentTime = 0;
    [SerializeField] float maxTime = 5;
    [SerializeField] List<Waypoint> waypoints = new();
    [SerializeField] int currentIndex = 0;
    [SerializeField] Player playerRef = null;
    [SerializeField] Turret turretRef = null;
    [SerializeField] int healthPoint = 5;
    [SerializeField] List<Turret> turrets = new();
    [SerializeField] bool isDead = false;
    
    public int HealthPoint => healthPoint;
    public bool IsDead => isDead;

    // Start is called before the first frame update
    void Start()
    {
        Init();        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Init()
    {
        

        List<Waypoint> waypointObjects = FindObjectsByType<Waypoint>(FindObjectsSortMode.None).ToList();      
       
        waypoints = waypointObjects.OrderBy(waypointObject => waypointObject.name).ToList();

        playerRef = FindAnyObjectByType<Player>();
        turretRef = FindAnyObjectByType<Turret>();
        SpawnEntityManager.Instance.AddInList(this);

    }

    void Move()
    {
        if(currentIndex == waypoints.Count)
        {
            Debug.Log("Destroy");
            if(playerRef != null)
            {
                playerRef.UpdateHealthPoint(-1);
            }

            Destroy(gameObject);
        }
        if (currentIndex < 0 || currentIndex >= waypoints.Count) return;
        Vector3 _targetPosition = waypoints[currentIndex].transform.position;
        float _distance = Vector3.Distance(transform.position, _targetPosition);

        if (_distance > 0.2f)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);

            Vector3 direction = (_targetPosition - transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            currentIndex++;
        }

    }

    public void UpdateHealthPoint(int _hp)
    {
        healthPoint += _hp;
        Debug.Log($"{healthPoint}");
        if (healthPoint <= 0)
        {
            healthPoint = 0;
            isDead = true;
            DestroySpawnEntity();
            turretRef.RemoveFromList(this);
            SpawnEntityManager.Instance.RemoveInList(this);
        }
    }
    

    void DestroySpawnEntity()
    {
        if (!isDead) return;
       Destroy(gameObject);        
    }
}
