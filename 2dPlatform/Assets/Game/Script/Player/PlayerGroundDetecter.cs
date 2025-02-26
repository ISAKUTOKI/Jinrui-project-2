using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerGroundDetecter : MonoBehaviour
{
    public bool isGrounded;
    public List<GameObject> toIgnores;
    List<GameObject> _currentGrounds = new List<GameObject>();

    public float radius;

    List<Collider2D> _cols;

    private void Awake()
    {
        _cols = new List<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (PlayerBehaviour.instance.health.isDead)
            return;
        var cols = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var col in cols)
        {
            if (toIgnores.Contains(col.gameObject))
                continue;

            if (!_cols.Contains(col))
            {
                OnEnter(col);
                _cols.Add(col);
            }
        }
        for (var i = _cols.Count - 1; i >= 0; i--)
        {
            var col = _cols[i];
            if (col == null)
            {
                _cols.Remove(col);
                continue;
            }

            if (!cols.Contains(col))
            {
                OnExit(col);
                _cols.Remove(col);
            }
        }
    }
    private void OnEnter(Collider2D col)
    {
        if (col.tag == "Kill")
        {
            PlayerBehaviour.instance.health.Die(true);
            return;
        }

        //var colEnemy = col.gameObject.GetComponent<EnemyBehaviour>();
        //if (colEnemy != null
        //    && colEnemy.headKickSlay != null
        //    && colEnemy.headKickSlay.CheckHit(transform.position))
        //{
        //    colEnemy.TakeDamage(colEnemy.headKickSlay.damage);
        //}

        if (col.isTrigger)
            return;
        //Debug.Log(collision.contacts.Length);
        if (!_currentGrounds.Contains(col.gameObject))
            _currentGrounds.Add(col.gameObject);
        isGrounded = _currentGrounds.Count > 0;
        PlayerBehaviour.instance.jump.OnGrounded();
    }

    private void OnExit(Collider2D col)
    {
        //Debug.Log("OnCollisionExit2D " + collision.gameObject);
        if (_currentGrounds.Contains(col.gameObject))
            _currentGrounds.Remove(col.gameObject);
        isGrounded = _currentGrounds.Count > 0;
    }
}
