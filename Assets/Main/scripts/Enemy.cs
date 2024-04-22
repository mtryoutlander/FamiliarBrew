using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string type;
    public float maxhp;
    private float hp;
    public ElementType resitance;
    public ElementType weakness;
    public GameObject prefab;
    public float attackDmg;
    public ElementType dmgEffect;
    public bool splashDmg;
    public float attackSpeed;
    
    private float timeFromLastAttack;

    public event EventHandler<OnHpChangeEventArgs> OnHpChange;
    public event EventHandler<OnAttackTimmerEventArg> OnAttackTimmerEvent;
    public void Start()
    {
        timeFromLastAttack = 0f;
        hp = maxhp;
    }
    public class OnHpChangeEventArgs : EventArgs
    {
        public float hpNormalized;
    }
    public class OnAttackTimmerEventArg : EventArgs { public float attackTime; }
    public void FireHpChangeEvent(float newHp)
    {
        this.OnHpChange?.Invoke(this, new OnHpChangeEventArgs { hpNormalized = newHp/maxhp});
    }
    public void FireAttackTimmerEvent(float time)
    {
        this.OnAttackTimmerEvent?.Invoke(this, new OnAttackTimmerEventArg { attackTime = time/attackSpeed });
    }
    public void UpdateLastAttackTime(float time)
    {
        timeFromLastAttack += time;
        FireAttackTimmerEvent(timeFromLastAttack);
    }
    public void Attack()
    {
        timeFromLastAttack = 0f;
        FireAttackTimmerEvent(timeFromLastAttack);
    }
    public bool CanAttack()
    {
        return timeFromLastAttack > attackSpeed;
    }
    public void UpdateHp(float change)  //adds the change number to the current hp
    {
        hp += change;
        if (hp > maxhp)
            hp = maxhp;
        FireHpChangeEvent(hp);
    }
    public bool IsEnemyDead()
    {
        return hp <= 0;
    }

}
