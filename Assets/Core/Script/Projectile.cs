using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] SpawnEntity currentTarget = null;
    [SerializeField] Turret turretRef = null;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float minDistAllowed = 0.2f;
    [SerializeField] int damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (currentTarget == null)Destroy(gameObject);
    }

    public void SetCurrentTarget(SpawnEntity _target)
    {
        if (_target == null) return;
        currentTarget = _target;
    }

    public void SetTurretRef(Turret _target)
    {
        turretRef = _target;
    }

    void Move()
    {
        if(currentTarget != null)
        {
            Vector3 _offset = currentTarget.GetComponent<SphereCollider>().center;
            Vector3 _target = currentTarget.transform.position + _offset;

           
            transform.position = Vector3.MoveTowards(transform.position, _target, moveSpeed * Time.deltaTime);
            if(Vector3.Distance(_target, transform.position) < minDistAllowed)
            {
                if(currentTarget != null)
                {
                    currentTarget.UpdateHealthPoint(-damage);              

                    Destroy(gameObject);
                }
                Destroy(gameObject, 3);
            }
        }

    }
}
