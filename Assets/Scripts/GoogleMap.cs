using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Piranest
{
    public class GoogleMap : MonoBehaviour
    {
        [SerializeField] private RawImage rawImg;
        [SerializeField] private Button btn;
        [SerializeField] private double lat; // Changed to double
        [SerializeField] private double lon; // Changed to double
        [SerializeField] private int zoom = 12;

        private int mapWidth = 640;
        private int mapHeight = 640;
        private Rect rect;



        private void Start()
        {
            StartCoroutine(GetGoogleMap());
            rect = rawImg.rectTransform.rect;
            mapWidth = (int)Math.Round(rect.width);
            mapHeight = (int)Math.Round(rect.height);
        }

        public void ShowMap(float lat, float lon)
        {
            this.lat = lat;
            this.lon = lon;
            btn.SetEvent(() => Utility.OpenGoogleMap(lat, lon));

            rect = rawImg.rectTransform.rect;
            mapWidth = (int)Math.Round(rect.width);
            mapHeight = (int)Math.Round(rect.height);
            StartCoroutine(GetGoogleMap());
        }

        public void SetZoom(int zoom)
        {
            this.zoom = zoom;
        }

        private IEnumerator GetGoogleMap()
        {
            string url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
                  "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight +
                  "&scale=" + "low" + "&maptype=" + "roadmap" +
                  "&markers=color:red%7C" + lat + "," + lon +
                  "&key=" + Utility.GOOGLE_MAP_API_KEY;

            yield return API.API.DownloadTexture(url, (tex) =>
            {
                rawImg.texture = tex;
            }, () =>
            {
                //TODO on fail
                Debug.Log("Load Texture Fail=>" + url);

            });
        }
    }
}
