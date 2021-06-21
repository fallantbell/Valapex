using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform start, end;

    public GameObject enemy;

    public int maxEnemy = 10;

    public float waitSecondToSpawn = 2f;

    float sx, sz, ex, ez;

    int enemyCount = 0;

    float timer = 0.0f;

    private void Start()
    {
        sx = start.position.x <= end.position.x ? start.position.x : end.position.x;
        sz = start.position.z <= end.position.z ? start.position.z : end.position.z;
        ex = start.position.x >= end.position.x ? start.position.x : end.position.x;
        ez = start.position.z >= end.position.z ? start.position.z : end.position.z;
        enemy.GetComponent<Enemy>().destroyEvent += onDestoryEnemy;
        StartCoroutine(spawnEnemy());
    }

    private void Update()
    {

    }

    IEnumerator spawnEnemy()
    {
        while(true)
        {   if (enemyCount < maxEnemy)
            {
                    enemyCount++;
                    Vector3 spawnLocation = new Vector3(Random.Range(sx, ex), transform.position.y, Random.Range(sz, ez));
                    GameObject ienemy = Instantiate(enemy, spawnLocation, enemy.transform.rotation);
                    ienemy.GetComponent<Enemy>().destroyEvent += onDestoryEnemy;
                    ienemy.transform.parent = this.transform;
            }

            yield return new WaitForSeconds(waitSecondToSpawn);
        }
    }

    private void onDestoryEnemy()
    {
        enemyCount--;
    }
}
