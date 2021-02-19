using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// https://www.youtube.com/watch?v=3UO-1suMbNc
public class LoopBackground : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    [SerializeField] private GameObject[] endLevels;
    [SerializeField] private float choke;

    private Camera mainCamera;

    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds =
            mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in levels)
        {
            LoadChildObjects(obj);
        }

        foreach (GameObject obj in endLevels)
        {
            LoadChildObjects(obj);
            obj.SetActive(false);
        }
    }

    void LoadChildObjects(GameObject obj)
    {
        float objectWidth = obj.GetComponent<TilemapRenderer>().bounds.size.x - choke;
        int childsNeeded = (int) Mathf.Ceil(screenBounds.x * 2 / objectWidth);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;

        }
        Destroy(clone);
        Destroy(obj.GetComponent<TilemapRenderer>());
    }

    void repositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<TilemapRenderer>().bounds.extents.x - choke;
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2,
                    lastChild.transform.position.y, lastChild.transform.position.z);
            } else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2,
                    firstChild.transform.position.y, firstChild.transform.position.z);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Finish")) {
            GameState.stopCamera = true;
        }
    }

    private void LateUpdate()
    {
        if (!GameState.inEndPosition) {
            foreach (GameObject obj in levels)
            {
                repositionChildObjects(obj);
            }
        } else {
            foreach (GameObject obj in levels) {
                repositionChildObjects(obj);
            }
            StartCoroutine(endSceneCoroutine());
        }
    }

    private IEnumerator endSceneCoroutine() {
        yield return new WaitForSeconds(2);
        foreach (GameObject obj in endLevels) {
            obj.SetActive(true);
            repositionChildObjects(obj);
        }
    }
}
