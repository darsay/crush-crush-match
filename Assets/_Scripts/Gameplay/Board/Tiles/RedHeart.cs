using UnityEngine;

public class RedHeart: ColorHeart
{
    public RedHeart(Vector2Int position) : base(position)
    {
        color = EmojiColor.Red;

        GraphicId = 2;
    }

    public override string ToString()
    {
        return "RHeart";
    }
}
