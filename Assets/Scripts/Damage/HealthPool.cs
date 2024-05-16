using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthPool : Resettable
{
    public event Action<HealthPool, float> OnHPChange;

    [SerializeField]
    private float _startingHP;

    private float _maxHP;
    public float MaxHP => _maxHP;

    private float _hp;
    public float HP
    {
        get 
        { 
            return _hp; 
        }
        private set 
        {
            float prevHP = _hp;
            _hp = value;
            OnHPChange?.Invoke(this, prevHP);
        }
    }

    private void OnEnable()
    {
        _maxHP = _startingHP;
        HP = _startingHP;
    }

    public float TakeDamage(float amt)
    {
        if(HP - amt <= 0f)
        {
            float damageApplied = amt - HP;
            HP = 0f;
            return damageApplied;
        }
        else
        {
            HP -= amt;
            return amt;
        }
    }

    public float Heal(float amt)
    {
        if(HP + amt >= _maxHP)
        {
            float healthHealed = _maxHP - HP;
            HP = _maxHP;
            return healthHealed;
        }
        else
        {
            HP += amt;
            return amt;
        }
    }

    public override void ResetModel()
    {
        HP = _maxHP;
    }
}
