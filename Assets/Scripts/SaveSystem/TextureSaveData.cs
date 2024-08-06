using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Piranest
{
    [CreateAssetMenu(fileName = nameof(TextureSaveData), menuName = Utility.SCRIPTABLE_PATH + nameof(TextureSaveData))]
    public class TextureSaveData : SaveData<List<Map<string, byte[]>>>
    {

        protected override string KeyName()
        {
            return "TextureSaveData";
        }

        public bool HasTexture(string url)
        {
            return Data.FirstOrDefault(d => d.Key.Equals(url)) != null;
        }
        public void AddTexture(string url, Texture2D tex)
        {
            if (!Data.Exists(d => d.Key.Equals(url)))
            {
                var item = new Map<string, byte[]>(url, tex.EncodeToJPG());
                Data.Add(item);
            }
        }

        private byte[] GetValue(string url)
        {
            return Data.FirstOrDefault(d => d.Key.Equals(url)).Value;
        }

        public Texture2D GetSprite(string url)
        {
            if (!HasTexture(url))
            {
                Debug.LogError("Texture Not Found: " + url);
                return null;
            }

            var tex = new Texture2D(2, 2);
            tex.LoadRawTextureData(GetValue(url));
            return tex;
            Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
            //return sprite;
        }
    }


}
