using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
  public class WeaponPickup : MonoBehaviour
  {
    [SerializeField] Weapon swordPrefab;
    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
        Debug.Log("eneter");
        other.GetComponent<Fighter>().EquipWeapon(swordPrefab);
        Destroy(gameObject);
      }
    }
  }
}
