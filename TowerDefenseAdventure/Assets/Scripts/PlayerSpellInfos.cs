using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;


[CreateAssetMenu(fileName = "PlayerSpells", menuName = "Player/PlayerSpells", order = 0)]
public class PlayerSpells : ScriptableObject {
    public List<PlayerSpellData> playerSpellData;
    [Serializable] public class PlayerSpellData
    {
        public string spellImage;
        public float baseDamage = 0f;
        public float baseRange = 0f;
        public SpellEffect[] baseSpellEffects;
        public int level = 0;
        public float baseCoolDown = 0f;
        public float coolDown = 0f;

        PlayerSpellData(PlayerSpellData data)
        {
            spellImage = data.spellImage;
            baseDamage = data.baseDamage;
            baseRange = data.baseRange;
            baseSpellEffects = data.baseSpellEffects;
            level = data.level;
            baseCoolDown = data.coolDown;
        }
        PlayerSpellData(string spellImage, float baseDamage, float baseRange, SpellEffect[] spellEffects, int level, float baseCoolDown)
        {
            this.spellImage = spellImage;
            this.baseDamage = baseDamage;
            this.baseRange = baseRange;
            this.baseSpellEffects = spellEffects;
            this.level = level;
            this.baseCoolDown = baseCoolDown;
        }
    }

    public static PlayerSpells CreateInstance(PlayerSpellData[] data)
    {
        PlayerSpells PlayerSpells = CreateInstance<PlayerSpells>();
        PlayerSpells.Init(data);
        return PlayerSpells;
    }

    private void Init(PlayerSpellData[] data)
    {
        playerSpellData = data.ToList();
    }
}

public enum SpellEffect
{
    Ignite, Poison, Slow, Stun, KnockBack
}

