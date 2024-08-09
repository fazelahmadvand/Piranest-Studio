using UnityEngine;

namespace Piranest
{
    public static class Utility
    {
        public const string SCRIPTABLE_PATH = "Data/";


        public static Sprite GetSprite(Texture2D texture)
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
            return sprite;
        }


        public static void OpenGoogleMap(float locationLat, float locationLong)
        {
            string url = $"https://www.google.com/maps/search/?api=1&query={locationLat},{locationLong}";
            Application.OpenURL(url);
        }

    }
}
