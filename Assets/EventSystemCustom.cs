using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSystemCustom : MonoBehaviour
{
    public UnityEvent onShotDamage;
    public UnityEvent onRocketDamage;
    public UnityEvent onMineDamage;

    void Awake()
    {
        onShotDamage = new UnityEvent();
        onRocketDamage = new UnityEvent();
        onMineDamage = new UnityEvent();
    }
}
