using UnityEngine;

public class YellowHeart: ColorHeart
{
    public YellowHeart(Vector2Int position) : base(position)
    {
        color = EmojiColor.Yellow;

        GraphicId = 5;
    }

    public override string ToString()
    {
        return "YHeart";
    }
}
