using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.Core
{
  public class Health : MonoBehaviour, ISaveable
  {
    [SerializeField] float health = 100f;
    bool isDead = false;

    public void TakeDamage(float damage)
    {
      health = Mathf.Max(health - damage, 0);
      if (health == 0)
      {
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

    public object CaptureState()
    {
      return health;
    }

    public void RestoreState(object state)
    {
      health = (float)state;
      if (health == 0)
      {
        Die();
      }
    }
  }
}
