namespace Piranest
{
    public class ArGameMode : GameMode
    {
        public override void Play()
        {
            SceneLoader.LoadScene(SceneName.ARMultipleObjects_Puzzles);
        }
    }
}
