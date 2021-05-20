using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        if (health <= 1) transform.Find("Fill").gameObject.GetComponent<Image>().color = new Color32(185, 18, 27, 255);
        else if (health <= 3) transform.Find("Fill").gameObject.GetComponent<Image>().color = new Color32(217, 194, 36, 255);
        else transform.Find("Fill").gameObject.GetComponent<Image>().color = new Color32(87, 159, 26,255);
    }
}
