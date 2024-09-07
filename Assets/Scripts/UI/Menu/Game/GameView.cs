using Piranest.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest.UI.Menu
{
    public class GameView : View
    {
        [SerializeField] private HeaderView headerView;
        [SerializeField] private GameInfoView gameInfo;
        [SerializeField] private GameChapterView chapterView;
        [SerializeField] private QuestionInfoView questionView;
        [SerializeField] private QuestionResultView questionResultView;
        [SerializeField] private GameFinishView finishView;

        [SerializeField] private Transform gameHolder;
        [SerializeField] private GameCardView card;

        [SerializeField] private PageHandlerView pageHandler;
        [SerializeField] private GameData gameData;


        private List<GameCardView> cards = new();

        public override void InitView()
        {
            base.InitView();
            gameInfo.InitView();
            chapterView.InitView();
            questionView.InitView();
            questionResultView.InitView();
            finishView.InitView();
            var gm = GameManager.Instance;
            gm.OnGameStateChange += OnGameStateChanged;
            Manager.OnInitialized += OnInitialized;
        }

        private void OnDestroy()
        {
            Manager.OnInitialized -= OnInitialized;
            var gm = GameManager.Instance;
            if (gm == null) return;
            gm.OnGameStateChange -= OnGameStateChanged;
        }

        private void OnInitialized()
        {
            CreateGameCards();
        }

        private void OnGameStateChanged(GameState state)
        {
            gameInfo.Hide();
            chapterView.Hide();
            questionView.Hide();
            questionResultView.Hide();
            pageHandler.Hide();
            questionView.HandleChapterHeader(state);
            if (state.type == GameStateType.Game)
            {
                pageHandler.Show();
                OnGameChange(state.currentGame);
            }
            else if (state.type == GameStateType.Chapter)
            {
                OnChapterChange(state.currentChapter, state.firstQuestionOfChapter, state.lastQuestionOfChapter);
            }
            else if (state.type == GameStateType.Question)
            {
                headerView.HandleGem(false);
                OnQuestionChange(state.currentQuestion);
            }
            else if (state.type == GameStateType.QuestionResult)
            {
                questionView.Hide();
                OnQuestionResult(state);
            }
            else
            {
                OnFinish(state);
            }

        }


        private void OnGameChange(Game game)
        {
            headerView.UpdateHeader(game.Name, () =>
            {
                gameInfo.Hide();
                Show();
            });
            gameInfo.UpdateInfo(game, () =>
            {
                GameManager.Instance.NextState();
            });
        }

        private void OnChapterChange(GameChapter chapter, GameChapterQuestion first, GameChapterQuestion last)
        {
            headerView.UpdateHeader("Games", () =>
            {
                chapterView.Hide();
                Show();
            });
            chapterView.UpdateChapter(chapter, first, last, () =>
            {
                GameManager.Instance.NextState();
            });

        }

        private void OnQuestionChange(GameChapterQuestion question)
        {
            chapterView.Hide();
            pageHandler.Hide();

            questionView.UpdateInfo(question, (answerState) =>
            {
                GameManager.Instance.SubmitAnswer(answerState);
            });
        }

        private void OnQuestionResult(GameState state)
        {
            bool isTrue = state.asnwerIsTrue;
            int prize = state.currentQuestion.Prize;
            questionResultView.UpdateResult(isTrue, prize, state.currentQuestion, () =>
            {
                GameManager.Instance.NextState();
            });

        }

        private void OnFinish(GameState state)
        {
            pageHandler.Show();
            headerView.Show();
            Show();
            finishView.UpdateFinish(state.currentGame.Prize, () =>
            {
                finishView.Hide();
                pageHandler.ShowPage(FooterPageTypeEnum.Vendor);
            }, () =>
            {
                finishView.Hide();
            });
        }

        public override void Show()
        {
            headerView.UpdatePage("Game");
            headerView.HandleBackButton(false);
            pageHandler.Show();
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
            gameInfo.Hide();
            chapterView.Hide();
            questionView.Hide();
        }

        private void CreateGameCards()
        {
            gameHolder.DestroyChildren();

            foreach (var game in gameData.Games)
            {
                var newCard = Instantiate(card, gameHolder);

                newCard.UpdateCard(game, () =>
                {
                    Hide();//must be first
                    GameManager.Instance.SetGame(game);
                });
                cards.Add(newCard);
            }


        }



    }
}
