using DG.Tweening;

public class BombView : ABoardElementView
{
    public override void BreakAnimation(float time)
    {
        if (transform == null) return;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(transform.localScale * 2, 2 * time / 3));
        sequence.Append(transform.DOScale(0, time / 3));

        sequence.Play();
    }
}
