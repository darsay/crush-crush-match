using UnityEngine;

public class GreenHeart: ColorHeart
{
    public GreenHeart(Vector2Int position) : base(position)
    {
        color = EmojiColor.Green;

        GraphicId = 3;
    }

    public override string ToString()
    {
        return "GHeart";
    }
}
