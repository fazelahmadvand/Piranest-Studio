using UnityEngine;
namespace Piranest
{
    public static class Extensions
    {
        public static void DestroyChildren(this Transform transform)
        {

            int count = transform.childCount;
            for (int i = count - 1; i > -1; i--)
            {
                Object.Destroy(transform.GetChild(i).gameObject);
            }


        }

    }
}
