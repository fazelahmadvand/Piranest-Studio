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
            }
        }


        public event Action<GameState> OnGameStateChange;


        public void Init()
        {
            CurrentGame = gameData.Games[0];
            CurrentChapter = gameData.GetChapter(CurrentGame.Id)[0];
            currentGameState = new()
            {
                type = GameStateType.Game,
                currentGame = currentGame,
                currentChapter = currentChapter,
                chapters = gameData.GetChapter(CurrentGame.Id),
                questions = gameData.GetQuestions(currentChapter.Id),
                currentQuestion = gameData.GetQuestions(currentChapter.Id)[0]
            };
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
                    currentGameState.NextQuestion(gameData);
                    break;
            }

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

        private int questionIndex;
        private int chapterIndex;
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

    public enum GameStateType
    {
        Game,
        Chapter,
        Question,
        Finished
    }
}
