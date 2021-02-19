using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyProperties
{ 
    public float health;
    public GameObject attackPrefab;
    public int attackSpeed;
    public int attackFreq;
    public int attackTimes;
    public GameObject shield;
    public int scoreGiven;
}
