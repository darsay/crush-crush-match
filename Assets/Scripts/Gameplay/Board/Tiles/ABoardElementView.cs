using UnityEngine;
using DG.Tweening;

public abstract class ABoardElementView : MonoBehaviour
{

    public abstract void BreakAnimation(float time);
    public void FallAnimation(Vector2 target, float time)
    {      
        var targetTranslated = new Vector2(target.x, -target.y);
        transform.DOMove(targetTranslated, time).SetEase(Ease.OutBounce);
    }   
}
