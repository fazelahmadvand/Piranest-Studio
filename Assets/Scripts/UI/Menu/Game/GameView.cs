using Piranest.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest.UI.Menu
{
    public class GameView : View
    {
        [SerializeField] private View authView;
        [SerializeField] private CategoryGameView categoryGameView;
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


        private Coroutine calculateDistance;

        public override void InitView()
        {
            CreateGameCards();
            base.InitView();
            gameInfo.InitView();
            chapterView.InitView();
            questionView.InitView();
            questionResultView.InitView();
            finishView.InitView();
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
            questionResultView.Hide();
            pageHandler.Hide();
            questionView.HandleChapterHeader(state);
            if (state.type == GameStateType.Game)
            {
                pageHandler.Show();
                OnGameChange(state);
            }
            else if (state.type == GameStateType.Chapter)
            {
                OnChapterChange(state.CurrentChapter, state.firstQuestionOfChapter, state.lastQuestionOfChapter);
            }
            else if (state.type == GameStateType.Question)
            {
                headerView.HandleGem(false);
                OnQuestionChange(state);
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


        private void OnGameChange(GameState state)
        {
            var game = state.currentGame;
            var question = state.CurrentQuestion;
            headerView.UpdateHeader(game.Name, () =>
            {
                gameInfo.Hide();
                Show();
            });

            HandleDisatance(question);
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

        private void OnQuestionChange(GameState state)
        {
            chapterView.Hide();
            pageHandler.Hide();
            int gameId = state.currentGame.Id;
            questionView.UpdateInfo(gameId, state.CurrentQuestion, (answerState) =>
            {
                GameManager.Instance.SubmitAnswer(answerState);
            });
        }

        private void OnQuestionResult(GameState state)
        {
            bool isTrue = state.asnwerIsTrue;

            int prize = gameData.IsAlreadyAnsweredQuations(state.currentGame.Id, state.CurrentChapter.Id, state.CurrentQuestion.Id) ? 0 : state.CurrentQuestion.Prize;

            questionResultView.UpdateResult(state.currentGame, isTrue, prize, state.CurrentQuestion, state.GetNextQuestsion(), () =>
            {
                GameManager.Instance.NextState();
            });
        }

        private void OnFinish(GameState state)
        {
            pageHandler.Show();
            headerView.Show();

            finishView.UpdateFinish(state.currentGame, state.timeBonusGem, state.gamePrize, () =>
            {
                finishView.Hide();
                pageHandler.ShowPage(FooterPageTypeEnum.Vendor);
            }, () =>
            {
                finishView.Hide();
                Show();
            });
        }

        public override void Show()
        {
            if (AuthData.HasUser)
            {
                headerView.UpdatePage("Game");
                headerView.HandleBackButton(true);
                headerView.UpdateButton(() =>
                {
                    Hide();
                    categoryGameView.Show();
                });
                pageHandler.Show();
                UpdateGameCards();
                base.Show();
                HandleLocationTurnOn();
            }
            else
            {
                authView.Show();
            }
        }

        public override void Hide()
        {
            base.Hide();
            gameInfo.Hide();
            chapterView.Hide();
            questionView.Hide();
            if (calculateDistance != null)
                StopCoroutine(calculateDistance);
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


        private void UpdateGameCards()
        {
            if (cards.Count == 0) return;
            var games = gameData.Games;
            for (int i = 0; i < games.Count; i++)
            {
                var game = games[i];
                var card = cards[i];
                card.UpdateCard(game, () =>
                {
                    Hide();//must call first
                    GameManager.Instance.SetGame(game);
                });
            }
        }


        private void HandleDisatance(GameChapterQuestion question)
        {
            if (calculateDistance != null)
                StopCoroutine(calculateDistance);
            calculateDistance = StartCoroutine(HandleDistanceToLocation(question));
        }

        private IEnumerator HandleDistanceToLocation(GameChapterQuestion question)
        {
            while (true)
            {
                var gps = GPSLocation.Instance;
                var tuple = gps.IsNearLocation(question.LocationLat, question.LocationLong, (int)question.LocationRadius);
                string meterValue;
                if (!Utility.HasLocationPermission())
                {
                    meterValue = "Need Location Permission";
                    gameInfo.HandleBeginButton(false, meterValue);
                }
                else
                {
                    meterValue = tuple.Item1 ? string.Empty : $"Remaining Meter: {tuple.Item2}";
                    if (tuple.Item1 == false && tuple.Item2 == 0) meterValue = $"Calculating";
                    gameInfo.HandleBeginButton(tuple.Item1, meterValue);
                }

                yield return new WaitForSeconds(1);
            }
        }

        private void HandleLocationTurnOn()
        {
            if (Utility.HasLocationPermission())
            {

            }
        }


    }
}
