using DG.Tweening;

public class TileView : ABoardElementView
{
    public override void BreakAnimation(float time)
    {
        if (transform == null) return;
        transform.DOScale(0, time);
    }
}
