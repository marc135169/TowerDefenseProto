using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] List<SpawnEntity> enemies = new();
    [SerializeField] bool canRotate = false;
    [SerializeField] AttackComponent atkComp = null;
    [SerializeField] SpawnEntity currentTarget = null;

    public SpawnEntity CurrentTarget => currentTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
        if(canRotate)RotateToFirst();
    }


    void Init()
    {
        atkComp = GetComponent<AttackComponent>();
    }



    // target the first enemy in List "enemies" and rotateTo
    public void RotateToFirst()
    {
        if (currentTarget == null || enemies.Count <= 0) return;
        /*Vector3 _addVector = new Vector3(0, 0.595f, 0);
        Vector3 _targetDirection = (currentTarget.transform.position + _addVector) - transform.position;*/
        Vector3 _offset = currentTarget.GetComponent<SphereCollider>().center;
        Vector3 _targetDirection = (currentTarget.transform.position + _offset) - transform.position;

        Quaternion _targetRotation = Quaternion.LookRotation(_targetDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, Time.deltaTime * 200);
       
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter");
        canRotate = true;
        SpawnEntity _entity = other.GetComponent<SpawnEntity>();
        if (_entity == null) return;
        enemies.Add(_entity);
    }

    private void OnTriggerExit(Collider other)
    {
        SpawnEntity _entity = other.GetComponent<SpawnEntity>();
        if (_entity != null && enemies.Contains(_entity))
        {
            enemies.Remove(_entity);
        }
    }

    public void RemoveFromList(SpawnEntity _target)
    {
        enemies.Remove(_target);
    }


    public void UpdateTarget()
    {
        if (enemies.Count <= 0) return;
        if (enemies.Count > 0 && currentTarget == null)
        {
            if (enemies[0] == null) RemoveFromList(enemies[0]);
            if (enemies.Count <= 0) return;
            currentTarget = enemies[0];
        }
        else if (enemies.Count > 0 && currentTarget != null && !enemies.Contains(currentTarget))
        {
            currentTarget = enemies[0];
        }
    }


}
