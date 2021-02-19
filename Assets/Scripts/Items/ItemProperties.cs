using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemProperties {
    public string name;
    public string description;
    public int id;
    public ItemType type;
    public enum ItemType {
        Passive,
        Active,
        Attack
    };
}