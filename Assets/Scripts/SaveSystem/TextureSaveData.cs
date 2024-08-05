using System.Collections.Generic;
using UnityEngine;

namespace Piranest
{
    [CreateAssetMenu(fileName = nameof(TextureSaveData), menuName = Utility.SCRIPTABLE_PATH + nameof(TextureSaveData))]
    public class TextureSaveData : SaveData<Dictionary<int, byte[]>>
    {

        protected override string KeyName()
        {
            return "TextureSaveData";
        }

        public bool HasTexture(int id)
        {
            return Data.ContainsKey(id);
        }
        public void AddTexture(int id, Texture2D texture)
        {
            if (!Data.ContainsKey(id))
            {
                Data.Add(id, texture.EncodeToPNG());
            }
        }

        public Sprite GetSprite(int id)
        {
            if (!Data.ContainsKey(id))
            {
                Debug.LogError("Texture Not Found: " + id);
                return null;
            }
            var tex = new Texture2D(2, 2);
            tex.LoadRawTextureData(Data[id]);
            Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
            return sprite;
        }

    }


}
