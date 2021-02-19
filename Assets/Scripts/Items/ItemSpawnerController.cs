using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class ItemSpawnerController : MonoBehaviour {
    [SerializeField] private List<ItemController> items;
    [SerializeField] private int initialChance = 1;
    [SerializeField] private int chanceDropoff = 2;
    
    private List<ItemController> availableItems;
    private List<ItemController> spawnedItems;
    private bool itemsAreShown = false;

    // Start is called before the first frame update
    void Start() {
        availableItems = generateAvailableItemsList();
        spawnedItems = new List<ItemController>();

        int i = 0;
        foreach (BoxCollider2D track in GameState.tracks) {
            if (availableItems.Count == 0) break;
            
            int generatedChance;
            if (i == 0) {
                generatedChance = UnityEngine.Random.Range(1, initialChance);
            } else {
                generatedChance = UnityEngine.Random.Range(1, initialChance * chanceDropoff + (i + 1));
            }
            
            if (generatedChance == 1) {
                // Spawn random item on track
                int randomItemIndex = UnityEngine.Random.Range(0, availableItems.Count);
                ItemController itemToSpawn = availableItems[randomItemIndex];
                availableItems.RemoveAt(randomItemIndex);
                float yTrackPos = (track.size.y / 2) + track.transform.position.y;
                transform.position = new Vector2(GameState.player.transform.position.x + 2, yTrackPos);
                
                ItemController spawnedItem = Instantiate(itemToSpawn, new Vector2(transform.position.x, yTrackPos), transform.rotation);
                spawnedItem.gameObject.SetActive(false);
                spawnedItems.Add(spawnedItem);
                spawnedItem.trackNum = track.GetComponent<TrackController>().trackNum - 1;
            }

            i++;
        }
    }

    // Update is called once per frame
    void Update() {
        if (GameState.stopCamera && !itemsAreShown) {
            foreach (ItemController item in spawnedItems) {
                item.gameObject.SetActive(true);
            }

            itemsAreShown = true;
        }
    }

    private List<ItemController> generateAvailableItemsList() {
        List<ItemController> availableItems = new List<ItemController>();
        foreach (ItemController item in items) {
            if (!GameState.items.Contains(item)) {
                availableItems.Add(item);
            }
        }

        return availableItems;
    }
}