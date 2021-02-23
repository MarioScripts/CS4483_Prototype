using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : EnemyController {
    [SerializeField] private float followCooldown;

    private GameObject player;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("followPlayer", followCooldown, followCooldown);
    }

    // Update is called once per frame
    void Update() {
    }

    public override void setShield(bool hasShield) {
        // Spider cannot have shield so do nothing
    }

    private void followPlayer() {
        // Only hop if player is within 1 track of spider and spider is in front of player
        if (Mathf.Abs(player.GetComponent<PlayerController>().currentTrack - trackNum) == 1 && player.transform.position.x < transform.position.x && !isDying) {
            // Correctly calculate y position based on sprite sizes
            float correctedPlayerYPos = player.transform.position.y - player.GetComponent<SpriteRenderer>().bounds.extents.y;
            transform.position = new Vector2(transform.position.x, correctedPlayerYPos + GetComponent<SpriteRenderer>().bounds.extents.y);
        }

    }
}