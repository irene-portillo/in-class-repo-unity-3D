using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName="AbilitySO", menuName = "Abililities/PunchAbility")]
public class PunchAbility : AbilitySO
{
    [SerializeField] private float duration = 2;
    
    protected override bool Cast()
    {
        Player.Instance.SetAnimationTrigger(abilityAnimationTrigger);
        Player.Instance.InvokeAction(Finish, 2);
        return true;
    }

    private void Finish()
    {
        SetState(State.OnCooldown);
    }
}
