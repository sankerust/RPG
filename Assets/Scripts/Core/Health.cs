using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
  public class Health : MonoBehaviour
  {
    [SerializeField] float health = 100f;
    bool isDead = false;

    public void TakeDamage(float damage)
    {
      health = Mathf.Max(health - damage, 0);
      if (health == 0) {
        Die();
      }
    }

    public bool IsDead() {
      return isDead;
    }

    private void Die()
    {
      if (isDead) return;
      isDead = true;
      GetComponent<Animator>().SetTrigger("die");
      GetComponent<ActionScheduler>().CancelCurrentAction();
    }
  }
}
