﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSystemCustom : MonoBehaviour
{
    public UnityEvent onShotDamage;
    public UnityEvent onRocketDamage;
    public UnityEvent onMineDamage;
    public UnityEvent PlayerOneCycleEnter;
    public UnityEvent PlayerTwoCycleEnter;

    void Awake()
    {
        onShotDamage = new UnityEvent();
        onRocketDamage = new UnityEvent();
        onMineDamage = new UnityEvent();
        PlayerOneCycleEnter = new UnityEvent();
        PlayerTwoCycleEnter = new UnityEvent();
    }
}
