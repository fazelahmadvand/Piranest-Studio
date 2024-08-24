using UnityEngine;

namespace Piranest.UI.Menu
{
    public class GameView : View
    {
        [SerializeField] private HeaderView headerView;
        [SerializeField] private GameInfoView gameInfo;
        [SerializeField] private GameChapterView chapterView;
        [SerializeField] private QuestionInfoView questionView;

        [SerializeField] private PageHandlerView pageHandler;
        [SerializeField] private GameData gameData;

        public override void InitView()
        {
            base.InitView();
            gameInfo.InitView();
            chapterView.InitView();
            questionView.InitView();

            var gm = GameManager.Instance;
            gm.OnGameStateChange += OnGameStateChanged;
        }

        private void OnDestroy()
        {

            var gm = GameManager.Instance;
            if (gm == null) return;
            gm.OnGameStateChange -= OnGameStateChanged;

        }

        private void OnGameStateChanged(GameState state)
        {
            gameInfo.Hide();
            chapterView.Hide();
            questionView.Hide();
            pageHandler.Hide();
            questionView.HandleChapterHeader(state);
            if (state.type == GameStateType.Game)
            {
                pageHandler.Show();
                OnGameChange(state.currentGame);
            }
            else if (state.type == GameStateType.Chapter)
            {
                OnChapterChange(state.currentChapter);
            }
            else if (state.type == GameStateType.Question)
            {
                headerView.HandleGem(false);
                OnQuestionChange(state.currentQuestion);
            }
            else
            {
                OnFinish();
            }
        }


        private void OnGameChange(Model.Game game)
        {
            gameInfo.UpdateInfo(game, () =>
            {
                headerView.HandleBackButton(false);
                headerView.UpdatePage(game.Name);
                GameManager.Instance.NextState();

            });
        }

        private void OnChapterChange(Model.GameChapter chapter)
        {
            chapterView.UpdateChapter(chapter, () =>
            {
                GameManager.Instance.NextState();
            });
        }

        private void OnQuestionChange(Model.GameChapterQuestion question)
        {
            chapterView.Hide();
            pageHandler.Hide();

            questionView.UpdateInfo(question, (answerState) =>
            {
                GameManager.Instance.SubmitAnswer(answerState);
                GameManager.Instance.NextState();

            });
        }

        private void OnFinish()
        {
            gameInfo.Show();
            pageHandler.Show();
            headerView.Show();
        }

        public override void Show()
        {
            headerView.UpdatePage("Game");
            base.Show();
        }






    }
}
