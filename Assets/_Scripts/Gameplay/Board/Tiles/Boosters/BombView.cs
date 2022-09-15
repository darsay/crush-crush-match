using DG.Tweening;
using UnityEngine;

public class BombView : ABoardElementView
{
    [SerializeField] protected float targetScale;

    [SerializeField] GameObject particles;

    public override void BreakAnimation(float time)
    {
        if (transform == null) return;

        Sequence sequence = DOTween.Sequence();

        particles.transform.parent = null;

        sequence.Append(transform.DOScale(transform.localScale * targetScale, 2 * time / 3).OnComplete(() => particles.SetActive(true)));
        sequence.Append(transform.DOScale(0, time / 3));

        sequence.Play().OnComplete(() => {         
            Destroy(this.gameObject);
        });


    }

    public override void TouchAnimation(float time)
    {
        
    }
}
