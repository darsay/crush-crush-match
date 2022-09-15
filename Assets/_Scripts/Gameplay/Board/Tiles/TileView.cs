using DG.Tweening;
using UnityEngine;


public class TileView : ABoardElementView
{
    [SerializeField] GameObject particles;
    public override void BreakAnimation(float time)
    {
        particles.transform.parent = null;
        particles.SetActive(true);

        transform.DOScale(0, time).OnComplete(()=> {            
            Destroy(this.gameObject);
            });
    }


    public override void TouchAnimation(float time)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOPunchRotation(Vector3.forward * 10, time, 10, 1));
        seq.Append(transform.DORotate(Vector3.zero, time / 2));

        seq.Play();
    }
}
