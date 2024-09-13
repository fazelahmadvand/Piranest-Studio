using Piranest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Piranest
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private Game currentGame;
        [SerializeField] private GameChapter currentChapter;
        [SerializeField] private GameChapterQuestion currentQuestion;

        [SerializeField] private GameState currentGameState;

        [SerializeField] private GameData gameData;
        [SerializeField] private AuthData authData;

        public Game CurrentGame
        {
            get => currentGame;
            set
            {
                if (currentGame == value) return;
                currentGame = value;
            }
        }

        public GameChapter CurrentChapter
        {
            get => currentChapter;
            set
            {
                if (currentChapter == value) return;
                currentChapter = value;
                OnGameChange?.Invoke();
            }
        }


        public event Action<GameState> OnGameStateChange;

        public event Action OnGameChange;

        public void SetGame(Game game)
        {
            CurrentGame = game;
            var userInfoes = gameData.GetUserGameInfoesByGameId(game.Id);
            var allGameChapters = gameData.GetChapters(CurrentGame.Id);
            CurrentChapter = allGameChapters[0];
            var chapterQuestions = gameData.GetQuestions(CurrentChapter.Id);
            currentGameState = new()
            {
                type = GameStateType.Game,
                currentGame = CurrentGame,
                chapters = allGameChapters,
                chaptersInfo = new()
            };

            FillUserGameByLastState(game.Id, userInfoes, allGameChapters);

            for (int i = 0; i < allGameChapters.Count; i++)
            {
                var chapter = allGameChapters[i];
                var questions = gameData.GetQuestions(chapter.Id);
                var chapterInfo = new ChapterInfo()
                {
                    chapterNumber = i + 1,
                    questionStates = new()
                };
                foreach (var question in questions)
                {
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
            }

            OnGameStateChange?.Invoke(currentGameState);
        }

        public void NextState()
        {
            var type = currentGameState.type;
            switch (type)
            {
                case GameStateType.Game:
                    currentGameState.type = GameStateType.Chapter;
                    break;
                case GameStateType.Chapter:
                    currentGameState.type = GameStateType.Question;
                    break;
                case GameStateType.Question:
                    break;
                case GameStateType.QuestionResult:
                    currentGameState.NextQuestion(gameData);
                    break;
            }

            OnGameStateChange?.Invoke(currentGameState);
        }

        public async void SubmitAnswer(QuestionStateType state)
        {
            currentGameState.UpdateQuestionAnswer(state);
            currentGameState.type = GameStateType.QuestionResult;
            OnGameStateChange?.Invoke(currentGameState);

            var gameId = currentGameState.currentGame.Id;
            var chapterId = currentGameState.currentChapter.Id;
            var question = currentGameState.currentQuestion;
            bool isTrue = state == QuestionStateType.Right;
            await gameData.InsertUserGameInfo(gameId, chapterId, question.Id, isTrue);
            if (isTrue)
            {
                int prize = question.Prize;
                await authData.UpdateAccount(prize);
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
                currentGameState.SetCurrentChapterAndQuestions(gameData, allGameChapters[0], firstQuestion);
                Debug.LogError("Finished Game");
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
                        currentGameState.SetCurrentChapterAndQuestions(gameData, chapter, question);
                        return;
                    }
                }
            }
        }

    }


    [Serializable]
    public struct GameState
    {
        public GameStateType type;
        public Game currentGame;
        public List<GameChapter> chapters;
        public List<GameChapterQuestion> questions;
        public GameChapterQuestion currentQuestion;
        public GameChapter currentChapter;
        public GameChapterQuestion firstQuestionOfChapter, lastQuestionOfChapter;
        public List<ChapterInfo> chaptersInfo;
        public int questionIndex;
        public int chapterIndex;
        public bool asnwerIsTrue;

        public void UpdateQuestionAnswer(QuestionStateType questionState)
        {
            chaptersInfo[chapterIndex].questionStates[questionIndex] = questionState;
            asnwerIsTrue = questionState == QuestionStateType.Right;
        }

        public void NextQuestion(GameData gameData)
        {
            questionIndex++;
            if (questionIndex > questions.Count - 1)
            {
                type = GameStateType.Chapter;
                NextChapter(gameData);
                return;
            }
            currentQuestion = questions[questionIndex];
            type = GameStateType.Question;
        }

        public void NextChapter(GameData gameData)
        {
            chapterIndex++;
            if (chapterIndex > chapters.Count - 1)
            {
                type = GameStateType.Finished;
                return;
            }
            currentChapter = chapters[chapterIndex];
            questionIndex = 0;
            questions = gameData.GetQuestions(currentChapter.Id);
        }

        public void SetCurrentChapterAndQuestions(GameData gameData, GameChapter chapter, GameChapterQuestion question)
        {
            currentChapter = chapter;
            chapterIndex = chapters.IndexOf(chapter);
            questions = gameData.GetQuestions(currentChapter.Id);
            currentQuestion = question;
            questionIndex = questions.IndexOf(question);
            firstQuestionOfChapter = questions[0];
            lastQuestionOfChapter = questions[^1];
        }


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
