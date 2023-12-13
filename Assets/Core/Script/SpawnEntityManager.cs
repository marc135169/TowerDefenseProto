using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SpawnEntityManager : Singleton<SpawnEntityManager>
{
    [SerializeField] List<SpawnEntity> spawnedEntities = new();
    [SerializeField] int enemySpawnedCount = 0;

    public List<SpawnEntity> SpawnEntities => spawnedEntities;
    public int EnemySpawnedCount => enemySpawnedCount;

   public void AddInList(SpawnEntity entity)  //Ajoute a ma list un Spawned Enemy
    {
        if (!entity || Exist(entity)) return;
        spawnedEntities.Add(entity);
        enemySpawnedCount++;
        entity.name += $" <{spawnedEntities.IndexOf(entity)}> [MANAGED]";
    }

    public void RemoveInList(SpawnEntity entity)
    {
        if (!Exist(entity)) return;
        spawnedEntities.Remove(entity);
        enemySpawnedCount--;
    }

    public void RemoveInList(int index) 
    {
        if (!Exist(index)) return;
        spawnedEntities.RemoveAt(index);
        enemySpawnedCount--;
    }

    public void ClearList()
    {
        spawnedEntities.Clear();
        enemySpawnedCount = 0;
    }

    public bool Exist(SpawnEntity _toCheck)
    {
        return spawnedEntities.Contains(_toCheck);
    }

    public bool Exist(int _index)
    {
        if (_index >= spawnedEntities.Count || _index < 0) return false;
        return Exist(spawnedEntities[_index]);
    }
}
