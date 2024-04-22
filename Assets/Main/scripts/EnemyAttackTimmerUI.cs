using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttackTimmerUI : MonoBehaviour
{
    private enum Mode
    {
        Enemy,
        Allie,
    }
    private enum Visable
    {
        enable,
        disable,
    }
    [SerializeField] private Visable vision; 
    [SerializeField] private Mode mode;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Allies allie;

    [SerializeField] private Image attackTimer;

    // Start is called before the first frame update
    public void Start()
    {
        switch (vision)
        {
            case Visable.enable:
                attackTimer.enabled = true;
                break;
            case Visable.disable:
                attackTimer.enabled = false;
                break;
        }
        switch (mode)
        {
            case Mode.Enemy:
                enemy.OnAttackTimmerEvent += UpdateEnemyTimmer;
                attackTimer.fillAmount = 0;
                break;
            case Mode.Allie:
                allie.OnAttackTimmerEvent += UpdateAllieTimmer;
                attackTimer.fillAmount = 0;
                break;
        }


    }

    private void UpdateAllieTimmer(object sender, Allies.OnAttackTimmerEventArg e)
    {
        attackTimer.fillAmount = e.attackTime;
    }

    private void UpdateEnemyTimmer(object sender, Enemy.OnAttackTimmerEventArg e)
    {
        attackTimer.fillAmount = e.attackTime;
    }
}
