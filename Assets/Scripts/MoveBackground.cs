using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (!GameState.stopCamera) {
            transform.position = new Vector2(transform.position.x - scrollSpeed, transform.position.y);
        }
    }
}
