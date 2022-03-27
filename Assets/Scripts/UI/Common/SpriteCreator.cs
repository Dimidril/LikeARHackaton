using UnityEngine;

namespace Utils
{
    public static class SpriteCreator
    {
        public static Sprite Create(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.one * 0.5f);
        }
    }
}