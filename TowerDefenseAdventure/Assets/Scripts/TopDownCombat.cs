using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TopDownCombat : MonoBehaviour
{
    [NonSerialized] public Enemy target = null;
    private Player player;
    // Start is called before the first frame update
    public enum AttackSpells
    {
        Basic, Spell1, Spell2, Spell3
    }

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        foreach (var spell in player.GetPlayerInfos().possessedSpells.playerSpellData)
        {
            if(spell.coolDown <= 0f)
            {
                spell.coolDown = 0f;
                break;
            }
            spell.coolDown -= Time.deltaTime;
        }
    }
    public bool Attack(AttackSpells spell)
    {
        if (target == null)
            return false;

        bool isEnemyDead = false;
        PlayerSpells.PlayerSpellData spellData = player.GetPlayerInfos().possessedSpells.playerSpellData[(int)spell];
        float attackDamage = CalculateRawDamage(spellData);
        if(IsReady(spellData) && IsInRange(target.transform, spellData))
        {
            Debug.Log("ATTAQUE");
            isEnemyDead = target.ReceiveDamage(attackDamage);
            spellData.coolDown = spellData.baseCoolDown;
        }
        
        return isEnemyDead;
        /*switch (spell)
        {
            case AttackSpells.Basic:
                target.ReceiveDamage(attackDamage);
                break;
            case AttackSpells.Spell1:
                target.ReceiveDamage();
            case AttackSpells.Spell2:
            case AttackSpells.Spell3:

            default:
        }*/
    }

    private bool IsInRange(Transform transform, PlayerSpells.PlayerSpellData spellData)
    {
        Vector3 distVect = this.transform.position - transform.position;
        float dist = distVect.magnitude;

        if(dist <= spellData.baseRange )
            return true;

        return false;
    }

    public bool IsReady(PlayerSpells.PlayerSpellData spellData)
    {
        return spellData.coolDown == 0f;
    }

    private float CalculateRawDamage(PlayerSpells.PlayerSpellData spellData)
    {
        PlayerInfos.PlayerData playerData = player.GetPlayerInfos().playerData;
        float damage = spellData.baseDamage;
        damage *= spellData.level;
        damage *= playerData.level;

        return damage;
    }
    // Update is called once per frame
    
}
