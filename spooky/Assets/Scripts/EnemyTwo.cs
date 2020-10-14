using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        transform.parent.position += anim.deltaPosition;
    }
}
