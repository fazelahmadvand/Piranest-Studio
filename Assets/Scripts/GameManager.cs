using Piranest.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private Game currentGame;
        [SerializeField] private GameChapter currentChapter;
        [SerializeField] private GameChapterQuestion currentQuestion;

        [SerializeField] private GameData gameData;
        public Game CurrentGame
        {
            get => currentGame;
            set
            {
                if (currentGame == value) return;
                currentGame = value;
                OnGameChange?.Invoke(currentGame);
            }
        }

        public GameChapter CurrentChapter
        {
            get => currentChapter;
            set
            {
                if (currentChapter == value) return;
                currentChapter = value;
                OnChapterChange?.Invoke(currentChapter);
            }
        }

        public GameChapterQuestion CurrentQuestion
        {
            get => currentQuestion;
            set
            {
                if (currentQuestion == value) return;
                currentQuestion = value;
                OnQuestionChange?.Invoke(currentQuestion);
            }
        }


        public event Action<Game> OnGameChange;
        public event Action<GameChapter> OnChapterChange;
        public event Action<GameChapterQuestion> OnQuestionChange;
        public event Action OnGameFinished;

        public void Init()
        {
            //check last game
            CurrentGame = gameData.Games[0];
        }

        public void LoadChapter()
        {
            //last chapter state
            CurrentChapter = gameData.GetChapter(CurrentGame.Id)[0];
        }

        public void LoadQuestion()
        {
            CurrentQuestion = gameData.GetQuestions(currentChapter.Id)[0];
        }

    }
}
