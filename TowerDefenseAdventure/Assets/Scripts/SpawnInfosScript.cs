using UnityEngine;

[CreateAssetMenu(fileName = "SpawnInfos", menuName = "SpawnInfos", order = 0)]
public class SpawnInfosScript : ScriptableObject
{
    public EnemyInfos enemyInfos;
    public int enemyNumber;

    public void Init(EnemyInfos enemyInfos, int enemyNumber)
   {
      this.enemyInfos = enemyInfos;
      this.enemyNumber = enemyNumber;
   }

   public static SpawnInfosScript CreateInstance(EnemyInfos enemyInfos, int enemyNumber)
   {
      var spawnInfos = ScriptableObject.CreateInstance<SpawnInfosScript>();
      spawnInfos.Init(enemyInfos, enemyNumber);
      return spawnInfos;
   }
}
