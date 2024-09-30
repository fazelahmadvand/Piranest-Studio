using DynamicPixels.GameService;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.inputs;
using DynamicPixels.GameService.Services.Table.Models;
using Piranest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace Piranest
{
    [CreateAssetMenu(fileName = nameof(GameData), menuName = Utility.SCRIPTABLE_PATH + nameof(GameData))]
    public class GameData : BaseServiceData
    {
        [SerializeField] private List<Game> games = new();
        [SerializeField] private List<GameChapter> gameChapters = new();
        [SerializeField] private List<GameChapterQuestion> gameChapterQuestions = new();
        [SerializeField] private List<UserGameInfo> userGameInfoes = new();

        [Space]
        [SerializeField] private AuthData authData;

        public List<Game> Games => games;
        public List<GameChapter> GameChapters => gameChapters;
        public List<GameChapterQuestion> GameChapterQuestions => gameChapterQuestions;
        public List<UserGameInfo> UserGameInfoes => userGameInfoes;

        private const string GAME_TABLE_ID = "6529a1ab65751b5e5cc1c8a3";
        private const string GAME_CHAPTER_TABLE_ID = "652eedcbe4c6eeb88af4c7fa";
        private const string GAME_CHAPTER_QUESTIONS_TABLE_ID = "653803246fd64f40ff22209b";
        private const string USER_GAME_CHAPTER_QUESTIONS_TABLE_ID = "66e174b9bb0a7482d5468558";

        public event Action<UserGameInfo> OnUserGameDataInsert;

        public override async Task Init(Action<DynamicPixelsException> OnFail)
        {
            games = new();
            gameChapters = new();
            gameChapterQuestions = new();
            userGameInfoes = new();
            await GetGames(OnFail);
            await GetGameChapter(OnFail);
            await GetGameChampterQuestion(OnFail);
            await GetUserGameInfoes(OnFail);

        }

        public Game GetGame(int Id)
        {
            return games.FirstOrDefault(g => g.Id == Id);
        }

        public List<GameChapter> GetChapters(int gameId)
        {
            return gameChapters.Where(g => g.GameId == gameId).OrderBy(g => g.SortNumber).ToList();
        }

        public List<GameChapterQuestion> GetQuestions(int gameChapterId)
        {
            return gameChapterQuestions.Where(g => g.GameChapterId == gameChapterId).OrderBy(g => g.SortNumber).ToList();
        }

        public List<UserGameInfo> GetUserGameInfoesByGameId(int gameId)
        {
            return UserGameInfoes.Where(u => u.GameId == gameId).ToList();
        }

        public int CalculateGameReward(int gameId)
        {
            if (HasUserFinishedGame(gameId))
                return 0;
            return SumGamePrize(gameId);
        }

        public int SumGamePrize(int gameId)
        {
            int sum = 0;
            var chapters = GetChapters(gameId);
            foreach (var chapter in chapters)
            {
                var questions = GetQuestions(chapter.Id);
                foreach (var question in questions)
                {
                    sum += question.Prize;
                }
            }
            return sum;
        }


        public bool HasUserFinishedGame(int gameId)
        {
            var lastChapter = GetChapters(gameId)[^1];
            var lastQuestion = GetQuestions(lastChapter.Id)[^1];
            var userInfo = UserGameInfoes.FirstOrDefault(u => u.ChapterId == lastChapter.Id && u.QuestionId == lastQuestion.Id);
            return userInfo != null;

        }

        public bool IsAlreadyAnsweredQuations(int gameId, int chapterId, int questionId)
        {
            var answer = UserGameInfoes.Where(g => g.GameId == gameId).Where(g => g.ChapterId == chapterId).FirstOrDefault(g => g.QuestionId == questionId);
            return answer != null;
        }

        private async Task GetGames(Action<DynamicPixelsException> OnFail)
        {
            var findParam = new FindParams()
            {
                options = new(),
                tableId = GAME_TABLE_ID,
            };

            try
            {
                var response = await ServiceHub.Table.Find<Game, FindParams>(findParam);
                games = response.List;
            }
            catch (DynamicPixelsException e)
            {
                OnFail?.Invoke(e);
            }
        }

        private async Task GetGameChapter(Action<DynamicPixelsException> OnFail)
        {
            var findParam = new FindParams()
            {
                options = new(),
                tableId = GAME_CHAPTER_TABLE_ID,
            };

            try
            {
                var response = await ServiceHub.Table.Find<GameChapter, FindParams>(findParam);
                gameChapters = response.List;
            }
            catch (DynamicPixelsException e)
            {
                OnFail?.Invoke(e);

            }
        }

        private async Task GetGameChampterQuestion(Action<DynamicPixelsException> OnFail)
        {
            var findParam = new FindParams()
            {
                options = new(),
                tableId = GAME_CHAPTER_QUESTIONS_TABLE_ID,
            };

            try
            {
                var response = await ServiceHub.Table.Find<GameChapterQuestion, FindParams>(findParam);
                gameChapterQuestions = response.List;
            }
            catch (DynamicPixelsException e)
            {
                OnFail?.Invoke(e);

            }
        }

        private async Task GetUserGameInfoes(Action<DynamicPixelsException> OnFail)
        {
            var findParam = new FindParams()
            {
                options = new()
                {
                    Conditions = new Eq(Account.USER_ID, authData.User.Id).ToQuery(),
                },
                tableId = USER_GAME_CHAPTER_QUESTIONS_TABLE_ID,
            };

            try
            {
                var response = await ServiceHub.Table.Find<UserGameInfo, FindParams>(findParam);
                userGameInfoes = response.List;
            }
            catch (DynamicPixelsException e)
            {
                OnFail?.Invoke(e);
            }
        }

        public async Task InsertUserGameInfo(int gameId, int chapterId, int questionId, bool isAnswerTrue)
        {
            var userGameInfo = new UserGameInfo()
            {
                GameId = gameId,
                ChapterId = chapterId,
                QuestionId = questionId,
                UserId = authData.User.Id,
                IsAnswerTrue = isAnswerTrue

            };
            var insertParam = new InsertParams()
            {
                Data = userGameInfo,
                TableId = USER_GAME_CHAPTER_QUESTIONS_TABLE_ID,
            };
            try
            {
                var response = await ServiceHub.Table.Insert<UserGameInfo, InsertParams>(insertParam);
                userGameInfoes.Add(userGameInfo);
                OnUserGameDataInsert?.Invoke(userGameInfo);
            }
            catch (DynamicPixelsException e)
            {
                Debug.Log("User Game Info: " + e.Message);
            }
        }




    }
}
