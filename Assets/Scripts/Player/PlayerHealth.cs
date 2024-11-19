using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Damage Overlay")]
    public Image healthOverlay;
    public float duration;
    public float fadeSpeed;

    private float durationTimer;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthOverlay.color = new Color(healthOverlay.color.r, healthOverlay.color.g, healthOverlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if(healthOverlay.color.a > 0){
            if (health < 30)
                return;
            durationTimer += Time.deltaTime;
            if (durationTimer > duration){
                float tempAlpha = healthOverlay.color.a;
                tempAlpha -= fadeSpeed * Time.deltaTime;
                healthOverlay.color = new Color(healthOverlay.color.r, healthOverlay.color.g, healthOverlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateHealthUI(){
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float healthFraction = health / maxHealth;
        if(fillBack > healthFraction)
        {
            frontHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, healthFraction, percentComplete);

        }

        if (fillFront < healthFraction)
        {
            backHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, healthFraction, percentComplete);
            
        }
    }

    public void TakeDamage(float damage){
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        healthOverlay.color = new Color(healthOverlay.color.r, healthOverlay.color.g, healthOverlay.color.b, 1);
    }

    public void RestoreHealth(float healAmount){
        health += healAmount;
        lerpTimer = 0f;
    }
}    

