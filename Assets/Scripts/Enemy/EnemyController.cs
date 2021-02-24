using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public EnemyProperties enemyProperties;

    protected bool isDying = false;

    protected Transform firePoint;
    protected Animator animator;
    protected float maxHealth;
    protected GameObject shieldSpawn;
    protected HealthBarController healthBar;
    protected bool hasShield;
    public int trackNum;

    private SpriteRenderer shieldSprite;
    private SpriteRenderer enemySprite;

    protected virtual void Start() {
        enemySprite = GetComponent<SpriteRenderer>();
        shieldSprite = enemyProperties.shield.GetComponent<SpriteRenderer>();
        firePoint = transform.GetChild(0);
        animator = GetComponent<Animator>();
        maxHealth = enemyProperties.health;
        healthBar = GetComponentInChildren<HealthBarController>();
        healthBar.setHealth(enemyProperties.health, maxHealth);
        InvokeRepeating("attack", UnityEngine.Random.Range(1, 3), enemyProperties.attackFreq);
    }

    protected virtual void Update()
    {
        if (hasShield && shieldSpawn == null)
        {
            float sizeDiff = enemySprite.bounds.extents.y - shieldSprite.bounds.extents.y;
            Vector2 newY = new Vector2(transform.position.x, transform.position.y - sizeDiff);
            
            shieldSpawn = Instantiate(enemyProperties.shield, newY, transform.rotation);
        } else if (!hasShield && shieldSpawn) {
            Destroy(shieldSpawn);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Track")) {
            trackNum = other.GetComponent<TrackController>().trackNum - 1;
        }
    }

    public virtual void setShield(bool hasShield) {
        this.hasShield = hasShield;
    }

    public void dealDamage(float damage) {
        if (!hasShield) {
            enemyProperties.health -= damage;
          healthBar.setHealth(enemyProperties.health, maxHealth);
          if (enemyProperties.health <= 0) {
              if (healthBar != null) {
                  Destroy(healthBar.gameObject);
              }
              animator.SetTrigger("Death");
              isDying = true;
          }  
        }
        
    }

    protected virtual void die() {
        GameState.score += enemyProperties.scoreGiven;
        Destroy(transform.gameObject);
    }

    protected virtual void attack() {
        StartCoroutine(attackCoroutine());
    }

    private IEnumerator attackCoroutine()
    {
        for (int i = 0; i < enemyProperties.attackTimes; i++) { 
            GameObject createdAttack = Instantiate(enemyProperties.attackPrefab, firePoint.position, enemyProperties.attackPrefab.transform.rotation);
            Rigidbody2D attackRigidbody = createdAttack.GetComponent<Rigidbody2D>();
            attackRigidbody.velocity = transform.right * -enemyProperties.attackSpeed;
  
            yield return new WaitForSeconds(0.2f);
        }
    }
}
