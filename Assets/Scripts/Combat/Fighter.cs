using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{

  public class Fighter : MonoBehaviour, IAction
  {
    [SerializeField] float weaponRange = 2f;
    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] float weaponDamage = 5;

    Health target;
    float timeSinceLastAttack = Mathf.Infinity;

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
      target.TakeDamage(weaponDamage);
    }

    private bool GetIsInRange()
    {
      return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
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
