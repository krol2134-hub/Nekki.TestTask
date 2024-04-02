﻿using UnityEngine;

namespace Enemies
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private Enemy[] prefabs;
        
        public Enemy CreateEnemy(EnemyType type, Vector3 position)
        {
            foreach (var prefab in prefabs)
            {
                var isTargetType = prefab.Type == type;
                if (!isTargetType) 
                    continue;
                
                var enemy = Instantiate(prefab, position, Quaternion.identity);
                return enemy;
            }

            return null;
        }
    }
}