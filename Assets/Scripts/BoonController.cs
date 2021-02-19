using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonController : MonoBehaviour {
    [SerializeField] private float speed;
    void Update()
    {
        if (GameState.levelComplete) {
            Destroy(transform.gameObject);
        }
        else {
            transform.position = new Vector2(transform.position.x - speed, transform.position.y);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerController>().increaseMomentum(50);
            Destroy(gameObject);
        }
    }
}
