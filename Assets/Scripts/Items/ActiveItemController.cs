using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemController : ItemController {
    [SerializeField] protected bool recurringUse;
    [SerializeField] protected float cooldown;
    [SerializeField] protected int numberOfUses;

    protected int availableUses;
    public bool canUse = true;

    protected override void Start() {
        resetUse();
        base.Start();
    }

    public virtual void use() {
        // Empty for base class
    }

    public virtual void resetUse() {
        availableUses = numberOfUses;
        canUse = true;
    }
}