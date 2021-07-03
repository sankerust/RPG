using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{

  public class Fighter : MonoBehaviour, IAction
  {
    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] Transform rightHandTransform= null;
    [SerializeField] Transform leftHandTransform = null;
    [SerializeField] Weapon defaultWeapon = null;
    
    Health target;
    Weapon currentWeapon = null;
    float timeSinceLastAttack = Mathf.Infinity;

    private void Start()
    {
      EquipWeapon(defaultWeapon);
    }

    private void Update()
    {
      timeSinceLastAttack += Time.deltaTime;
      if (target == null) return;
      if (target.IsDead()) return;
      if (!GetIsInRange())
      {
        GetComponent<Mover>().MoveTo(target.transform.position);
      }
      else
      {
        GetComponent<Mover>().Cancel();
        AttackBehaviour();
      }
    }

    public void EquipWeapon(Weapon weapon)
    {
      currentWeapon = weapon;
      Animator animator = GetComponent<Animator>();
      weapon.Spawn(rightHandTransform, leftHandTransform, animator);
    }

    private void AttackBehaviour()
    {
      transform.LookAt(target.transform);
      // This will trigger the Hit() event.
      if (timeSinceLastAttack > timeBetweenAttacks)
      {
        TriggerAttack();
        timeSinceLastAttack = 0;
      }
    }

    private void TriggerAttack()
    {
      GetComponent<Animator>().ResetTrigger("stopAttack");
      GetComponent<Animator>().SetTrigger("attack");
    }

    // Animation event
    void Hit()
    {
      if (target == null) { 
        return;
        }
      target.TakeDamage(currentWeapon.GetWeaponDamage());
    }

    private bool GetIsInRange()
    {
      return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
    }

    public bool CanAttack(GameObject combatTarget) {
      if (combatTarget == null) {
        return false;
      }
      Health targetToTest = combatTarget.GetComponent<Health>();
      return targetToTest != null && !targetToTest.IsDead();
    }

    public void Attack(GameObject combatTarget) {
    GetComponent<ActionScheduler>().StartAction(this);
    target = combatTarget.GetComponent<Health>();
  }

  public void Cancel()
    {
      StopAttack();
      target = null;
      GetComponent<Mover>().Cancel();
    }

    private void StopAttack()
    {
      GetComponent<Animator>().ResetTrigger("attack");
      GetComponent<Animator>().SetTrigger("stopAttack");
    }
  }
}
