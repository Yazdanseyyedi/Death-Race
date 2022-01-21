using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public EventSystemCustom eventSystem;

    private void Start()
    {
        eventSystem.onRocketDamage.AddListener(UpdateHealth_Rocket);
        eventSystem.onRocketDamage.AddListener(UpdateHealth_Shot);
        eventSystem.onRocketDamage.AddListener(UpdateHealth_Mine);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    private void UpdateHealth_Mine()
    {
        SetHealth(FindObjectOfType<PlayerController>().currentHealth);
    }

    private void UpdateHealth_Shot()
    {
        SetHealth(FindObjectOfType<PlayerController>().currentHealth);
    }
    private void UpdateHealth_Rocket()
    {
        SetHealth(FindObjectOfType<PlayerController>().currentHealth);
    }
}
