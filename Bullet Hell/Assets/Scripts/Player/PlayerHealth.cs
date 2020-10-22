using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingPlayerHealth = 100;
    public int currentPlayerHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathclip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMoviment playerMoviment;
    Shooting playerShooting;
    bool isDead;
    bool damaged;

    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMoviment = GetComponent<PlayerMoviment>();
        playerShooting = GetComponent<Shooting>();
        currentPlayerHealth = startingPlayerHealth;
        healthSlider.value = currentPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amout)
    {
        damaged = true;

        currentPlayerHealth -= amout;

        healthSlider.value = currentPlayerHealth;

        playerAudio.Play();

        if(currentPlayerHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        anim.SetTrigger("Die");

        playerAudio.clip = deathclip;
        playerAudio.Play();
        
        playerMoviment.enabled = false;
    }
}
