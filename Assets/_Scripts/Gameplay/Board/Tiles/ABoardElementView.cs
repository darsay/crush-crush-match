using UnityEngine;
using DG.Tweening;

public abstract class ABoardElementView : MonoBehaviour
{
    public Vector2Int Position;

    public void AppearAnimation(Vector2Int position)
    {
        Position = position;
    }

    public abstract void BreakAnimation(float time);

    public abstract void TouchAnimation(float time);
    public void MoveAnimation(Vector2 target, float time)
    {      
        var targetTranslated = new Vector2(target.x, -target.y);
        transform.DOMove(targetTranslated, time);
    }   

    public void DestroyTile()
    {
        Destroy(this.gameObject);
    }
}
