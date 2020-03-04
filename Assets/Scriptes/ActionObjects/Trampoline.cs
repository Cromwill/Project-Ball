using UnityEngine;
using System.Collections;

public class Trampoline : ActionObject
{
    [SerializeField] private float _xPositionOffset;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(var contact in collision.contacts)
        {
            contact.rigidbody.velocity += (contact.point.x - _selfTransform.position.x) > 0 ? new Vector2(_xPositionOffset, 0) : new Vector2(_xPositionOffset * -1, 0);
        }
    }
}
