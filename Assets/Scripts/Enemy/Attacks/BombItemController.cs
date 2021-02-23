using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItemController : MonoBehaviour
{
    [SerializeField] private float damage = 100;
    [SerializeField] private CircleCollider2D explosionCollider;
    
    private bool beginExplosion = false;
    private BoxCollider2D collider;
    private Animator animator;

    void Start() {
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    public void destroyBomb() {
        Destroy(this.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy") && collider.IsTouching(other) && !beginExplosion) {
            // Start explosion
            beginExplosion = true;
            Vector2 explosionLocation = new Vector2(explosionCollider.transform.position.x + explosionCollider.offset.x,
                explosionCollider.transform.position.y + explosionCollider.offset.y);
            Collider2D[] colliders =
                Physics2D.OverlapCircleAll(explosionLocation, explosionCollider.radius);
            
            foreach (Collider2D collider in colliders) {
                if (collider.CompareTag("Enemy")) {
                    Debug.Log(collider.gameObject.name);
                    collider.GetComponent<EnemyController>().dealDamage(damage);
                }
            }
        }
    }
}
