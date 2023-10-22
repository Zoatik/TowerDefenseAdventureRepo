using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfos", menuName = "PlayerInfos", order = 0), Serializable]
public class PlayerInfos : ScriptableObject {
   public PlayerData playerData;
   [Serializable]
   public class PlayerData
   {
      public float walkSpeed = 7.5f;
      public float runSpeed = 11.5f;
      public float mana = 100f;
      public float health = 100f;

      public PlayerData(float walkSpeed, float runSpeed, float mana, float health)
      {
         this.walkSpeed = walkSpeed;
         this.runSpeed = runSpeed;
         this.mana = mana;
         this.health = health;
      }
   }

   public void Init(float walkSpeed, float runSpeed, float mana, float health)
   {
      playerData = new PlayerData(walkSpeed, runSpeed, mana, health);
   }

   public static PlayerInfos CreateInstance(float walkSpeed, float runSpeed, float mana, float health)
   {
      var playerInfos = CreateInstance<PlayerInfos>();
      playerInfos.Init(walkSpeed, runSpeed, mana, health);
      return playerInfos;
   }
   public static PlayerInfos CreateInstance(PlayerData playerData)
   {
      var playerInfos = CreateInstance<PlayerInfos>();
      playerInfos.Init(playerData.walkSpeed, playerData.runSpeed, playerData.mana, playerData.health);
      return playerInfos;
   }

}
