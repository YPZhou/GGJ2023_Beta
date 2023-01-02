using UnityEngine;

namespace GGJ2023.Beta
{
    public class BackgroundEntity : MonoBehaviour
    {
        public Color ColorA;
        public Color ColorB;

        private void Start()
        {
            ArtStatus.ColorA = ColorA;
            ArtStatus.ColorB = ColorB;
            gameObject.GetComponent<SpriteRenderer>().color = ArtStatus.ColorA;
        }
    }
}