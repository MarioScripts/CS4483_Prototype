using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats {
    // Momentum
    public float momentumPool = 0f;
    public float momentumDecayRate = 2f;
    public float momentumIncreaseRate = 2f;
    public float momentumRate = 0.3f;
    public float momentumRateMultiplier = 1f;
    
    // Attack
    public float attackCooldown = 0.3f;
    public float attackDamageMultiplier = 1;
    public float attackSizeMultiplier = 1;
}