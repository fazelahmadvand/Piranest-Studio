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
                Data.Add(id, texture.EncodeToJPG());
            }
        }

        public Sprite GetSprite(int id)
        {
            if (!Data.ContainsKey(id)) return null;
            var texture = new Texture2D(id, id);
            texture.LoadRawTextureData(Data[id]);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f, 0);
            return sprite;
        }

    }


}
