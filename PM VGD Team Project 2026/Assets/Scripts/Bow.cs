using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Bow : WeaponScript
{
    private void Update()
    {
        if (arrows < maxArrows && !loadingArrows)
        {
            loadingArrows = true;
            StartCoroutine("arrowRegen");
        }
    }
    IEnumerator arrowRegen(int v)
    {
        yield return new WaitForSeconds(arrowRegenRate);
        
        arrows++;

        if(arrows < maxArrows)
        {
            StartCoroutine("arrowRegen");
        } 
        else
        {
            loadingArrows = false;
        }
    }
}
