using UnityEngine;

public class PurpleHeart: ColorHeart
{
    public PurpleHeart(Vector2Int position) : base(position)
    {
        color = EmojiColor.Purple;

        GraphicId = 6;
    }

    public override string ToString()
    {
        return "PHeart";
    }
}
