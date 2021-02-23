using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour {
    public static int score;
    public static float time;
    public static int level;
    public static bool levelComplete;
    public static bool inEndPosition;
    public static bool stopCamera;
    public static bool levelEnd;
    public static BoxCollider2D[] tracks;
    
    // Items
    public static List<ItemController> items;
    public static List<ItemController> passiveItems;
    public static ActiveItemController activeItem;
    public static AttackItemController attackItem;
    
    // Player stats
    public static PlayerStats playerStats;
    public static PlayerController player;

    [SerializeField] private AttackItemController defaultAttackItem;
    [SerializeField] private ActiveItemController defaultActiveItem;

    private static bool initialLoad = false;
    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<PlayerController>();
        initializeTracksForLevel();
        if (activeItem != null) {
            activeItem.resetUse();
        }
        // addItem(defaultAttackItem);
        
        levelComplete = false;
        inEndPosition = false;
        stopCamera = false;
        levelEnd = false;
    }

    // Update is called once per frame
    void Update() {
        if (!levelComplete) {
            time += Time.deltaTime;

            EnemySpawnerController[] enemyspawners = FindObjectsOfType<EnemySpawnerController>();
            if (enemyspawners == null || enemyspawners.Length == 0) {
                levelComplete = true;
                player.attemptEndPosition();
            }
        }
    }
    
    void Awake() {
        if (!initialLoad) {
            initialLoad = true;
            items = new List<ItemController>();
            items.Add(defaultAttackItem);
            passiveItems = new List<ItemController>();
            playerStats = new PlayerStats();
            attackItem = defaultAttackItem;
            activeItem = defaultActiveItem;
        }
        
        DontDestroyOnLoad(this);
    }

    public static void addItem(ItemController item) {
        bool itemAlreadyExists = items.Contains(item);
        if (!itemAlreadyExists) {
            items.Add(item);
            if (item.type == ItemController.ItemType.Active) {
                activeItem = (ActiveItemController) item;
            } else if (item.type == ItemController.ItemType.Attack) {
                attackItem = (AttackItemController) item;
            }
            else {
                passiveItems.Add(item);
            }
        }
    }
    
    private void initializeTracksForLevel() {
        GameObject[] trackObjects = GameObject.FindGameObjectsWithTag("Track");
        tracks = new BoxCollider2D[trackObjects.Length];
        foreach (GameObject track in trackObjects) {
            tracks[track.GetComponent<TrackController>().trackNum - 1] = track.GetComponent<BoxCollider2D>();
        }
    }
}