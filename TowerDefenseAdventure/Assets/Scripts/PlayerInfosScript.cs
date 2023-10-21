using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfos", menuName = "PlayerInfos", order = 0)]
public class PlayerInfosScript : ScriptableObject {
    public float walkSpeed = 7.5f;
    public float runSpeed = 11.5f;
    public float mana = 100f;
    public float health = 100f;

    public void Init(float walkSpeed, float runSpeed, float mana, float health)
   {
      this.walkSpeed = walkSpeed;
      this.runSpeed = runSpeed;
      this.mana = mana;
      this.health = health;
   }

   public static PlayerInfosScript CreateInstance(float walkSpeed, float runSpeed, float mana, float health)
   {
      var playerInfos = CreateInstance<PlayerInfosScript>();
      playerInfos.Init(walkSpeed, runSpeed, mana, health);
      return playerInfos;
   }

}
