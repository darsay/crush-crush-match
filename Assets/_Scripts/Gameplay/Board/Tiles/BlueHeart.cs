using UnityEngine;

public class BlueHeart: ColorHeart
{
    public BlueHeart(Vector2Int position) : base(position)
    {
        color = EmojiColor.Blue;

        GraphicId = 4;
    }

    public override string ToString()
    {
        return "BHeart";
    }
}
