using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RadialTrigger : MonoBehaviour
{
    public Transform _otherObject;
    [Range(0f,4f)]
    public float _radius = 1f;
    [Range(0f, 1f)]
    public float _pricesAmount = .5f;
    [Range(0f, 2f)]
    public int _functionSelection = 1;
    

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        switch(_functionSelection)
        {
            case 0:
                PlayerInCircleOrNot();
                break;
            case 1:
                PlayerToLookEnemyButDifferentApproach();
                break;
            case 2:
                PlayerToLookEnemy();
                break;
        }       
        
    }
#endif
    public void PlayerToLookEnemy()
    {
        Vector2 _originPos = transform.position;
        Vector2 _otherObjectPos = _otherObject.position;

        Vector2 _otherObjectForward = _otherObject.right.normalized;
        Vector2 _otherObjectDir = (_originPos - _otherObjectPos).normalized;

        Debug.Log(Vector2.Dot(_otherObjectDir, _otherObjectForward));

        Gizmos.color = Vector2.Dot(_otherObjectDir, _otherObjectForward) > _pricesAmount ? Color.green : Color.red;

        Gizmos.DrawLine(_otherObjectPos, _otherObjectPos + _otherObjectForward * 2);

        Gizmos.color = Color.white;

        Gizmos.DrawLine(_otherObjectPos, _otherObjectPos + _otherObjectDir);

    }

    public void PlayerToLookEnemyButDifferentApproach()
    {
        Vector2 _origin = transform.position;
        Vector2 _player = _otherObject.position;

        Vector2 _playerDir = _player.normalized;
        Vector2 _originDir = _origin.normalized;

        float _dotProduct = Vector2.Dot(_originDir, _playerDir);

        Color _color = new Color(0, Mathf.Clamp(_dotProduct, 0, 1), 0, 1);

        Handles.color = _color;

        Handles.DrawWireDisc(_origin, Vector3.forward, _radius);
    }

    public void PlayerInCircleOrNot()
    {
        Vector2 _origin = transform.position;
        Vector2 _originOfOther = _otherObject.position;

        float _dist = Vector2.Distance(_origin, _originOfOther);

        Handles.color = _dist < _radius ? Color.green : Color.red;

        Handles.DrawWireDisc(_origin, Vector3.forward, _radius);
    }
}
