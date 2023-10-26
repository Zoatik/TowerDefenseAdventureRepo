using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "PlayerInfos", menuName = "PlayerInfos", order = 0), Serializable]
public class PlayerInfos : ScriptableObject {
   public PlayerData playerData;
   public PlayerSpells possessedSpells;
   [Serializable]
   public class PlayerData
   {
      public float walkSpeed = 7.5f;
      public float runSpeed = 11.5f;
      public float mana = 100f;
      public float health = 100f;
      public int level = 1;

      public PlayerData(float walkSpeed, float runSpeed, float mana, float health, int level)
      {
         this.walkSpeed = walkSpeed;
         this.runSpeed = runSpeed;
         this.mana = mana;
         this.health = health;
         this.level = level;
      }
   }

   public void Init(float walkSpeed, float runSpeed, float mana, float health, int level)
   {
      playerData = new PlayerData(walkSpeed, runSpeed, mana, health, level);
   }

   public static PlayerInfos CreateInstance(float walkSpeed, float runSpeed, float mana, float health, int level)
   {
      var playerInfos = CreateInstance<PlayerInfos>();
      playerInfos.Init(walkSpeed, runSpeed, mana, health, level);
      return playerInfos;
   }
   public static PlayerInfos CreateInstance(PlayerData playerData)
   {
      var playerInfos = CreateInstance<PlayerInfos>();
      playerInfos.Init(playerData.walkSpeed, playerData.runSpeed, playerData.mana, playerData.health, playerData.level);
      return playerInfos;
   }

}
