using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Allies : MonoBehaviour
{
    public string type;
    public float maxhp;
    private float hp;
    public GameObject prefab;
    public float attackTime;


    public event EventHandler<OnHpChangeEventArgs> OnHpChange;
    public event EventHandler<OnAttackTimmerEventArg> OnAttackTimmerEvent;
    public class OnHpChangeEventArgs : EventArgs { public float hpNormalized; }
    public class OnAttackTimmerEventArg : EventArgs { public float attackTime; }
    public void Start()
    {
        hp = maxhp;
    }
    public void FireHpChangeEvent(float newHp)
    {
        this.OnHpChange?.Invoke(this, new OnHpChangeEventArgs { hpNormalized = newHp / maxhp });
    }

    public void FireAttackTimmerEvent(float time)
    {
        this.OnAttackTimmerEvent?.Invoke(this, new OnAttackTimmerEventArg { attackTime = time/attackTime });
    }
    public void UpdateHp(float change)  //adds the change number to the current hp
    {
        this.hp += change;
        if(this.hp > maxhp)
            this.hp = maxhp;
        FireHpChangeEvent(hp);
    }

}

