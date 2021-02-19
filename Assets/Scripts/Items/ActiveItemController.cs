using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemController : ItemController {
    [SerializeField] protected bool recurringUse;
    [SerializeField] protected float cooldown;
    [SerializeField] protected int numberOfUses;

    protected bool canUse = true;
    public virtual void use() {
        // Empty for base class
    }
}