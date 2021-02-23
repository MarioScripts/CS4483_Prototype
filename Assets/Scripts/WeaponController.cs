using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    private bool canShoot = true;
    // Update is called once per frame
    void Update() {
        if (Input.GetButton("Fire1") && canShoot) {
            shoot();
        }
    }

    void shoot() {
        GameObject attackSpawned = Instantiate(GameState.attackItem.gameObject, firePoint.position, GameState.attackItem.gameObject.transform.rotation);
        attackSpawned.transform.localScale *= GameState.playerStats.attackSizeMultiplier;
        StartCoroutine(cooldownCoroutine());
    }

    IEnumerator cooldownCoroutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(GameState.playerStats.attackCooldown * GameState.playerStats.attackCooldownMultiplier);
        canShoot = true;
    }
}
