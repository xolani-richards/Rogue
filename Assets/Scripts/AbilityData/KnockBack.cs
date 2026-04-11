using UnityEngine;
using ROGUE.Characters;

[CreateAssetMenu(fileName = "KnockBack", menuName = "ROGUE/Effects/KnockBack")]
public class KnockBack : AbilityEffect
{
    public override void Execute(GameObject caster, GameObject target, float force)
    {
        Rigidbody rb = target.GetComponentInParent<Rigidbody>();
        if (rb == null) return;
        Vector3 dir = ((target.transform.position - caster.transform.position) + Vector3.up).normalized;
        rb.AddForce(dir * force, ForceMode.Impulse);
        Character character = target.GetComponentInParent<Character>();

        if (character != null) DisableMovement(character, force/(rb.mass*10f));
    }
}