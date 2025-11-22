using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float speed = 5.0f, maxHealth = 15.0f, power = 10.0f;
    public string[] abilityList, speedList, dropList;
    public float[] dropChance;
    public int[] dropNum;
}
