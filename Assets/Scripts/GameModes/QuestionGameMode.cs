using Piranest.UI.Menu;
using UnityEngine;

namespace Piranest
{
    public class QuestionGameMode : GameMode
    {
        [SerializeField] private GameView gameView;
        public override void Play()
        {
            gameView.Show();
        }


        public override bool CanHide()
        {
            return true;
        }

    }
}
