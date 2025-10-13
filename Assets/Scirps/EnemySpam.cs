using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab;
    public float spawnTime;
    public Transform[] spawnPoints;
}

public class EnemySpam : MonoBehaviour
{
    [Header("Normal Enemies - Individual Spawn Times")]
    [SerializeField] private EnemySpawnData[] normalEnemyData;
    
    [Header("Special Enemies - Individual Spawn Times")]
    [SerializeField] private EnemySpawnData[] specialEnemyData;
    
    private void Start()
    {
        
        for (int i = 0; i < normalEnemyData.Length; i++)
        {
            StartCoroutine(spawnEnemyCoroutine(normalEnemyData[i], i));
        }
        
        
        for (int i = 0; i < specialEnemyData.Length; i++)
        {
            StartCoroutine(spawnEnemyCoroutine(specialEnemyData[i], i));
        }
    }
    
    private IEnumerator spawnEnemyCoroutine(EnemySpawnData enemyData, int index)
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyData.spawnTime);
            
            if (enemyData.enemyPrefab != null && enemyData.spawnPoints.Length > 0)
            {
               
                Transform spawnPoint = enemyData.spawnPoints[Random.Range(0, enemyData.spawnPoints.Length)];
                Instantiate(enemyData.enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }
}

