using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider playerSlider3D;
    Slider playerSlider2D;

    Stats stats;

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

        playerSlider2D = GetComponent<Slider>();
        

        playerSlider3D.maxValue = stats.maxHealth;
        playerSlider2D.maxValue = stats.maxHealth;
        stats.health = stats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        playerSlider2D.value = stats.health;
        playerSlider3D.value = playerSlider2D.value;
    }
}
