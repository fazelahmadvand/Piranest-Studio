using DynamicPixels.GameService.Models;
using System;
using System.Threading.Tasks;
using UnityEngine;
namespace Piranest
{
    public abstract class BaseServiceData : ScriptableObject
    {
        public abstract Task Init(Action<DynamicPixelsException> OnFail);


    }
}
