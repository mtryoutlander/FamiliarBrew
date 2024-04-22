using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private Allies[] allies;
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private float enemySpawnTimmer;
    [SerializeField] private Canvas combatCanvas;
    [SerializeField] private Image winImage;
    [SerializeField] private Vector3 spawnPoint; // where the enemy spawns
    [SerializeField] private Vector3 attackPoint; // where the enemy walks to after spawing
    [SerializeField] private float walkSpeed;
    [SerializeField] private float enemySize; // size of enemy image along the x-axis
    private struct enemyGameObjects
    {
        public Enemy enemy;
        public GameObject visural;
        
    }

    private float currentTime;
    private int enemyCount;
    private bool spawnEnemy;
    private List<GameObject> aliveEnemy = new List<GameObject>();

    

    private void Start()
    {
        currentTime = 0;
        enemyCount = 0;
        spawnEnemy = true;
    }
    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        MoveEnemy();
        EnemyAttack();
        if (currentTime >= enemySpawnTimmer && spawnEnemy)
        {
            currentTime = 0;
            SpawnEnemy();
            
        }
        if(enemyCount >= enemies.Length)
        {
            spawnEnemy = false;
        }
        if (!spawnEnemy && aliveEnemy.Count == 0)
        {
            winImage.enabled = true;
        }
        
    }

    private void SpawnEnemy()
    {
        GameObject temp = Instantiate(enemies[enemyCount].prefab);
        aliveEnemy.Add(temp);
        temp.transform.position = spawnPoint;
        temp.transform.SetParent(combatCanvas.transform);
        enemyCount++;
    }

    private void MoveEnemy()
    {
        for(int i = 0; i < aliveEnemy.Count ;i++ )
        {
            Vector3 finalPostion = aliveEnemy[i].transform.position - new Vector3(walkSpeed, 0, 0);
            if (finalPostion.x <= attackPoint.x + enemySize*i) 
                aliveEnemy[i].transform.position = new Vector3(attackPoint.x + enemySize * i, attackPoint.y, attackPoint.z);
            else
                aliveEnemy[i].transform.position = finalPostion;
        }
    }

    private void EnemyAttack()
    {
        for(int i = 0; i < aliveEnemy.Count ;i++ )
        {
            if (aliveEnemy[i].transform.position.x != attackPoint.x + (i * enemySize))
            {
                continue;
            }
            Enemy e = aliveEnemy[i].GetComponent<Enemy>();
            e.UpdateLastAttackTime(Time.deltaTime);
            if (e.CanAttack())
            {
                e.Attack();
                if (e.splashDmg)
                {
                    foreach (Allies allie in allies)
                    {
                        allie.UpdateHp(-e.attackDmg);
                    }
                }
                else
                {
                    allies.First().UpdateHp(-e.attackDmg);
                }
            }

        }
    }

    private void HurtEnemy(ElementType dmgType, int splashRange, int dmg)
    {
        
        for(int i = 0;  i>=splashRange; i++)
        {
            Enemy enemy = HurtEnemy(dmgType, dmg, aliveEnemy[i].GetComponent<Enemy>());
            if (enemy.IsEnemyDead())
                Destroy(aliveEnemy[i]);
                aliveEnemy.RemoveAt(i);

        }
    }
    private Enemy HurtEnemy(ElementType dmgType, int dmg, Enemy enemy)
    {
        if (enemy.resitance == dmgType)
        {
            enemy.UpdateHp(-(dmg / 2));
        }
        else if (enemy.weakness == dmgType)
        {
            enemy.UpdateHp(-(dmg * 2));
        }
        else
        {
            enemy.UpdateHp(-dmg);
        }
        return enemy;

    }

}
