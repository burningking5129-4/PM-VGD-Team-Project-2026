using UnityEngine;

public class Spear : WeaponScript 
{
    
    private void Start()
    {
        attackBox = GameObject.Find("spearAttackBox");
    }

}
