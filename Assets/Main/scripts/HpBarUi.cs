using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUi : MonoBehaviour
{
    private enum Mode
    {
        Enemy,
        Allie,
    }
    [SerializeField] private Mode mode;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Allies allie;

    [SerializeField] private Image hpBar;



    // Start is called before the first frame update
    void Start()
    {
        switch (mode)
        {
            case Mode.Enemy:
                enemy.OnHpChange += UpdateEnemyHpBar;
                hpBar.fillAmount = enemy.maxhp;
                break;
                case Mode.Allie:
                allie.OnHpChange += UpdateAllieHpBar;
                hpBar.fillAmount = allie.maxhp;
                break;
        }
        
        
    }

    private void UpdateEnemyHpBar(object sender, Enemy.OnHpChangeEventArgs e)
    {
        hpBar.fillAmount = e.hpNormalized;
    }
    private void UpdateAllieHpBar(object sender, Allies.OnHpChangeEventArgs e)
    {
        hpBar.fillAmount = e.hpNormalized;
    }
}
