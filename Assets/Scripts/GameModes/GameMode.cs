using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest
{
    public abstract class GameMode : MonoBehaviour
    {
        public abstract void Play();
        public virtual bool CanHide() => false;
    }
}
