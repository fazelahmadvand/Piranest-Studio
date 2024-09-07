using Piranest.Model;
using System;
using System.Collections.Generic;
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
            CurrentChapter = gameData.GetChapter(CurrentGame.Id)[0];
            var chapterQuestions = gameData.GetQuestions(currentChapter.Id);
            currentGameState = new()
            {
                type = GameStateType.Game,
                currentGame = currentGame,
                currentChapter = currentChapter,
                chapters = gameData.GetChapter(CurrentGame.Id),
                questions = chapterQuestions,
                currentQuestion = gameData.GetQuestions(currentChapter.Id)[0],
                firstQuestionOfChapter = chapterQuestions[0],
                lastQuestionOfChapter = chapterQuestions[^1],
                chaptersInfo = new()
            };

            for (int i = 0; i < currentGameState.chapters.Count; i++)
            {
                var chapter = currentGameState.chapters[i];
                var questions = gameData.GetQuestions(chapter.Id);
                var chapterInfo = new ChapterInfo()
                {
                    chapterNumber = i + 1,
                    questionStates = new()
                };
                foreach (var question in questions)
                {
                    chapterInfo.questionStates.Add(QuestionStateType.NotAnswer);
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

        public void SubmitAnswer(QuestionStateType state)
        {
            currentGameState.UpdateQuestionAnswer(state);
            currentGameState.type = GameStateType.QuestionResult;
            OnGameStateChange?.Invoke(currentGameState);

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
        private int questionIndex;
        private int chapterIndex;
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
