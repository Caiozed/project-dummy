using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArenaController : MonoBehaviour
{
    public List<GameObject> enemies;
    public Transform[] SpawnPoints;
    List<GameObject> enemiesActives;
    int enemyIndex;

    List<EnemyController> enemiesControllers;
    // Start is called before the first frame update
    void Start()
    {
       enemyIndex = 0;
       enemiesActives = new List<GameObject>();
       enemiesControllers = new List<EnemyController>();

       CreateEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesControllers.All(a=>a.IsDead)){
            CreateEnemy();
        }
    }

    public void CreateEnemy(){
        if(enemyIndex <= enemies.Count()){
        enemiesActives.Add(Instantiate(enemies[enemyIndex], SpawnPoints[Random.Range(0, SpawnPoints.Count())].position, Quaternion.identity));
        enemiesControllers =new List<EnemyController>(enemiesActives.Select(s=>s.GetComponent<EnemyController>()));
        enemyIndex++;
        }
    }
}
