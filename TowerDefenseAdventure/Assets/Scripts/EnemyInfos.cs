using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfos", menuName = "EnemyInfos", order = 0)]
public class EnemyInfos : ScriptableObject {
    public new  String name = "";
    public float health = 0;
    public float armor = 0;
    public float damage = 0;
    public float range = 0;
    public float attackSpeed = 0;
    public float moveSpeed = 0;
}

