using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] Projectile projectileToSpawn = null;
    [SerializeField] Turret turretRef = null;
    
    [SerializeField] SpawnEntity currentTarget = null;


    [SerializeField] float currentTime = 0;
    [SerializeField] float attackSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        SetCurrentTarget();
        CustomTimer();
    }

    void Init()
    {
        turretRef = GetComponent<Turret>();
    }

    void SetCurrentTarget()
    {
        currentTarget = turretRef.CurrentTarget;
    }

    void FireProjectile()
    {
        Vector3 _addOffset = new Vector3(0, 0.596f, 0.9f);        
        Vector3 _spawnPosition = transform.TransformPoint(_addOffset);
        Projectile _projectile = Instantiate(projectileToSpawn, _spawnPosition, Quaternion.identity);
        _projectile.SetCurrentTarget(currentTarget);

    }

    void CustomTimer()
    {
        if(currentTarget != null)
        {
            currentTime += Time.deltaTime;
            if (currentTime > attackSpeed)
            {
                currentTime = 0;
                FireProjectile();
            }
            
        }
        
    }


    private void OnDrawGizmos()
    {
        Vector3 _addOffset = new Vector3(0, 0.596f, 0.9f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.TransformPoint(_addOffset), 0.5f);
        Gizmos.color = Color.white;
    }
}
