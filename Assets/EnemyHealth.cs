using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider enemySlider3D;

    Stats stats;

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Stats>();
        

        enemySlider3D.maxValue = stats.maxHealth;
        stats.health = stats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        enemySlider3D.value = stats.health;
    }
}
