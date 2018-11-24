using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

	public enum ResourceType { mana, prebuildMana  } //Types of Resources
    public static int Mana; //mana variable

    public GameObject townhall; //to find the townhall

    [Header("Prefabs")]
    public GameObject manaAnimatedprefab;           //Prefab for mana  
    //public GameObject prebuiltManaAnimatedPrefab;   //Prefab for prebuilt mana

    public void CollectResource(ResourceType rscType, GameObject target)
     {
        ManaAnimatedPrefabScript m = manaAnimatedprefab.GetComponent<ManaAnimatedPrefabScript>();
        m.manaType = rscType;
        
        Vector3 enemyDeathPos = target.transform.position;
        GameObject manaSpawn = Instantiate(manaAnimatedprefab, enemyDeathPos, Quaternion.identity);
    }
}
