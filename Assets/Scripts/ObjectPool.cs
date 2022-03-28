using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool SharedInstance;

    public List<GameObject> pooledObstacles;
    public GameObject obstacleToPool;
    public int obstaclePoolSize;

    public List<GameObject> pooledRewards;
    public GameObject rewardToPool;
    public int rewardPoolSize;

    public List<GameObject> pooledBigRewards;
    public GameObject bigRewardToPool;
    public int bigRewardPoolSize;

    public List<GameObject> pooledProjectiles;
    public GameObject projectileToPool;
    public int projectilePoolSize;

    void Awake() {
        SharedInstance = this;
    }

    void OnEnable()
    {
        pooledObstacles = new List<GameObject>();
        GameObject tempObstacle;
        for (int i = 0; i < obstaclePoolSize; i++) {
            tempObstacle = Instantiate(obstacleToPool);
            tempObstacle.SetActive(false);
            pooledObstacles.Add(tempObstacle);
        }

        pooledRewards = new List<GameObject>();
        GameObject tempReward;
        for (int i = 0; i < rewardPoolSize; i++) {
            tempReward = Instantiate(rewardToPool);
            tempReward.SetActive(false);
            pooledRewards.Add(tempReward);
        }

        pooledBigRewards = new List<GameObject>();
        GameObject tempBigReward;
        for (int i = 0; i < bigRewardPoolSize; i++) {
            tempBigReward = Instantiate(bigRewardToPool);
            tempBigReward.SetActive(false);
            pooledBigRewards.Add(tempBigReward);
        }

        pooledProjectiles = new List<GameObject>();
        GameObject tempProjectile;
        for (int i = 0; i < projectilePoolSize; i++) {
            tempProjectile = Instantiate(projectileToPool);
            tempProjectile.SetActive(false);
            pooledProjectiles.Add(tempProjectile);
        }
    }

    public GameObject GetPooledObstacle() {
        for (int i = 0; i < obstaclePoolSize; i++) {
            if (pooledObstacles[i].activeInHierarchy == false) {
                return pooledObstacles[i];
            }
        }
        return null;
    }

    public GameObject GetPooledReward() {
        for (int i = 0; i < rewardPoolSize; i++) {
            if (pooledRewards[i].activeInHierarchy == false) {
                return pooledRewards[i];
            }
        }
        return null;
    }

        public GameObject GetPooledBigReward() {
        for (int i = 0; i < bigRewardPoolSize; i++) {
            if (pooledBigRewards[i].activeInHierarchy == false) {
                return pooledBigRewards[i];
            }
        }
        return null;
    }


    public GameObject GetPooledProjectile() {
        for (int i = 0; i < projectilePoolSize; i++) {
            if (pooledProjectiles[i].activeInHierarchy == false) {
                return pooledProjectiles[i];
            }
        }
        return null;
    }
}
