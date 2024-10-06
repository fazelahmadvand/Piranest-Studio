using Piranest.UI;
namespace Piranest
{
    public class RacingGameMode : GameMode
    {
        public override void Play()
        {
            PopUpManager.Instance.Show("Coming Soon...", () =>
            {

            });
        }
    }
}
