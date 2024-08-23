using UnityEngine;

namespace Piranest.UI.Menu
{
    public class GameView : View
    {
        [SerializeField] private HeaderView headerView;
        [SerializeField] private GameInfoView gameInfo;
        [SerializeField] private GameChapterView chapterView;

        [SerializeField] private PageHandlerView pageHandler;
        [SerializeField] private GameData gameData;

        public override void InitView()
        {
            base.InitView();
            gameInfo.InitView();
            chapterView.InitView();

            var gm = GameManager.Instance;
            gm.OnGameChange += OnGameChange;
            gm.OnChapterChange += OnChapterChange;
            gm.OnQuestionChange += OnQuestionChange;
        }

        private void OnDestroy()
        {

            var gm = GameManager.Instance;
            if (gm == null) return;
            gm.OnGameChange -= OnGameChange;
            gm.OnChapterChange -= OnChapterChange;
            gm.OnQuestionChange -= OnQuestionChange;
        }



        private void OnGameChange(Model.Game game)
        {
            gameInfo.UpdateInfo(game, () =>
            {
                headerView.HandleBackButton(false);
                headerView.UpdatePage(game.Name);
                GameManager.Instance.LoadChapter();

            });
        }

        private void OnChapterChange(Model.GameChapter chapter)
        {
            gameInfo.Hide();
            chapterView.UpdateChapter(chapter, () =>
            {
                GameManager.Instance.LoadQuestion();
            });
        }

        private void OnQuestionChange(Model.GameChapterQuestion question)
        {
            chapterView.Hide();
            pageHandler.Hide();

        }

        public override void Show()
        {
            headerView.UpdatePage("Game");

            base.Show();
        }






    }
}
