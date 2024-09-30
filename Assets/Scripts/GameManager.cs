using Piranest.Model;
using Piranest.SaveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Piranest
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameState currentGameState;

        [SerializeField] private TimerHandler timeHandler;
        [Space]
        [SerializeField] private TimerSaveData timerSaveData;
        [SerializeField] private FinishedGameSaveData finishedGameSaveData;
        [SerializeField] private GameData gameData;
        [SerializeField] private AuthData authData;

        public event Action<GameState> OnGameStateChange;

        public void SetGame(Game game)
        {
            GPSLocation.Instance.Init();

            var userInfoes = gameData.GetUserGameInfoesByGameId(game.Id);
            var allGameChapters = gameData.GetChapters(game.Id);

            currentGameState = new()
            {
                type = GameStateType.Game,
                currentGame = game,
                chaptersInfo = new(),
                chaptersAndQuestions = new()
            };

            for (int i = 0; i < allGameChapters.Count; i++)
            {
                var chapter = allGameChapters[i];
                var questions = gameData.GetQuestions(chapter.Id);
                var chapterAndQuestions = new ChapterAndQuestions
                {
                    chapter = chapter,
                    questions = questions,
                };
                var chapterInfo = new ChapterInfo()
                {
                    chapterNumber = i + 1,
                    questionStates = new()
                };
                foreach (var question in questions)
                {
                    if (gameData.HasUserFinishedGame(game.Id))
                    {
                        chapterInfo.questionStates.Add(QuestionStateType.NotAnswer);
                        continue;
                    }
                    var userInfo = userInfoes.FirstOrDefault(u => u.ChapterId == chapter.Id && u.QuestionId == question.Id);
                    if (userInfo == null)
                    {
                        chapterInfo.questionStates.Add(QuestionStateType.NotAnswer);
                    }
                    else
                    {
                        chapterInfo.questionStates.Add(userInfo.IsAnswerTrue ? QuestionStateType.Right : QuestionStateType.Wrong);
                    }
                }
                currentGameState.chaptersInfo.Add(chapterInfo);
                currentGameState.chaptersAndQuestions.Add(chapterAndQuestions);
            }

            FillUserGameByLastState(game.Id, userInfoes, allGameChapters);
            OnGameStateChange?.Invoke(currentGameState);
        }

        public async void NextState()
        {
            var type = currentGameState.type;
            switch (type)
            {
                case GameStateType.Game:
                    currentGameState.type = GameStateType.Chapter;
                    break;
                case GameStateType.Chapter:
                    currentGameState.type = GameStateType.Question;
                    StartTime(currentGameState.currentGame);
                    break;
                case GameStateType.Question:
                    break;
                case GameStateType.QuestionResult:
                    currentGameState.NextQuestion();
                    break;
            }

            OnGameStateChange?.Invoke(currentGameState);

            if (type == GameStateType.Finished)
            {
                int gameId = currentGameState.currentGame.Id;
                int bonus = TimeBonusGem(gameId);
                if (bonus != 0)
                    await authData.UpdateAccount(bonus);
                finishedGameSaveData.GameFinished(currentGameState.currentGame.Id);
            }
        }

        public async void SubmitAnswer(QuestionStateType state)
        {
            currentGameState.UpdateQuestionAnswer(state);
            currentGameState.type = GameStateType.QuestionResult;
            OnGameStateChange?.Invoke(currentGameState);

            var gameId = currentGameState.currentGame.Id;
            var chapterId = currentGameState.CurrentChapter.Id;
            var question = currentGameState.CurrentQuestion;
            bool isTrue = state == QuestionStateType.Right;
            if (!gameData.IsAlreadyAnsweredQuations(gameId, chapterId, question.Id))
            {
                await gameData.InsertUserGameInfo(gameId, chapterId, question.Id, isTrue);
                if (isTrue)
                {
                    int prize = question.Prize;
                    await authData.UpdateAccount(prize);
                }
            }

        }

        private void FillUserGameByLastState(int gameId, List<UserGameInfo> userInfoes, List<GameChapter> allGameChapters)
        {
            var lastChapter = allGameChapters[^1];
            var firstChapter = allGameChapters[0];
            var allChapterQuestions = gameData.GetQuestions(lastChapter.Id);
            var lastQuestion = allChapterQuestions[^1];

            if (gameData.HasUserFinishedGame(gameId))//player already finished the game
            {
                var firstQuestion = gameData.GetQuestions(firstChapter.Id)[0];
                currentGameState.SetCurrentChapterAndQuestions(allGameChapters[0], firstQuestion);
                Debug.Log("Finished Game");
                return;
            }

            foreach (var chapter in allGameChapters)
            {
                var questions = gameData.GetQuestions(chapter.Id);
                foreach (var question in questions)
                {
                    var lastPlayedQuestion = userInfoes.FirstOrDefault(u => u.ChapterId == chapter.Id && u.QuestionId == question.Id);
                    if (lastPlayedQuestion == null)
                    {
                        currentGameState.SetCurrentChapterAndQuestions(chapter, question);
                        return;
                    }
                }
            }
        }

        private void StartTime(Game game)
        {
            if (!timerSaveData.HasGame(game.Id))
            {
                int time;
                time = game.TimeLimit;
                timerSaveData.AddTime(game.Id, time);
            }
            timeHandler.Init(game.Id);
        }

        public int TimeBonusGem(int gameId)
        {
            if (finishedGameSaveData.HasFinishedGame(gameId)) return 0;
            int total = gameData.SumGamePrize(gameId);
            int result = total * Utility.TIME_BONUS_PERCENT / 100;
            return result;
        }

    }


    [Serializable]
    public struct GameState
    {
        public GameStateType type;
        public Game currentGame;
        public List<ChapterAndQuestions> chaptersAndQuestions;
        public GameChapterQuestion CurrentQuestion => chaptersAndQuestions[chapterIndex].questions[questionIndex];
        public GameChapter CurrentChapter => chaptersAndQuestions[chapterIndex].chapter;
        public GameChapterQuestion firstQuestionOfChapter, lastQuestionOfChapter;
        public List<ChapterInfo> chaptersInfo;
        [SerializeField] private int questionIndex;
        [SerializeField] private int chapterIndex;
        public bool asnwerIsTrue;

        public void UpdateQuestionAnswer(QuestionStateType questionState)
        {
            chaptersInfo[chapterIndex].questionStates[questionIndex] = questionState;
            asnwerIsTrue = questionState == QuestionStateType.Right;
        }

        public void NextQuestion()
        {
            questionIndex++;
            var currentQuestions = chaptersAndQuestions[chapterIndex].questions;
            if (questionIndex > currentQuestions.Count - 1)
            {
                type = GameStateType.Chapter;
                chapterIndex++;
                if (chapterIndex > chaptersAndQuestions.Count - 1)
                {
                    type = GameStateType.Finished;
                    return;
                }
                questionIndex = 0;
                return;
            }
            type = GameStateType.Question;
        }

        public GameChapterQuestion GetNextQuestsion()
        {
            var nextQuestionIndex = questionIndex + 1;
            var currentQuestions = chaptersAndQuestions[chapterIndex].questions;
            if (nextQuestionIndex > currentQuestions.Count - 1)
            {
                nextQuestionIndex = 0;
                var nextChapterIndex = chapterIndex + 1;
                if (nextChapterIndex > chaptersAndQuestions.Count - 1)
                {
                    return null;
                }
                return chaptersAndQuestions[nextChapterIndex].questions[nextQuestionIndex];
            }

            return currentQuestions[nextQuestionIndex];


        }

        public void SetCurrentChapterAndQuestions(GameChapter chapter, GameChapterQuestion question)
        {
            chapterIndex = chaptersAndQuestions.Select(c => c.chapter).ToList().IndexOf(chapter);
            questionIndex = chaptersAndQuestions[chapterIndex].questions.IndexOf(question);
            var allQuestions = chaptersAndQuestions[chapterIndex].questions;
            firstQuestionOfChapter = allQuestions[0];
            lastQuestionOfChapter = allQuestions[^1];
        }


    }

    [Serializable]
    public struct ChapterAndQuestions
    {
        public GameChapter chapter;
        public List<GameChapterQuestion> questions;
    }

    [Serializable]
    public struct ChapterInfo
    {
        public int chapterNumber;
        public List<QuestionStateType> questionStates;
    }

    public enum QuestionStateType
    {
        Right, Wrong, NotAnswer
    }


    public enum GameStateType
    {
        Game,
        Chapter,
        Question,
        QuestionResult,
        Finished
    }
}
