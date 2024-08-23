using DynamicPixels.GameService;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Services.Table.Models;
using Piranest.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{
    [CreateAssetMenu(fileName = nameof(GameData), menuName = Utility.SCRIPTABLE_PATH + nameof(GameData))]
    public class GameData : BaseServiceData
    {
        [SerializeField] private List<Game> games = new();
        [SerializeField] private List<GameChapter> gameChapters = new();
        [SerializeField] private List<GameChapterQuestion> gameChapterQuestions = new();

        public List<Game> Games => games;
        public List<GameChapter> GameChapters => gameChapters;
        public List<GameChapterQuestion> GameChapterQuestions => gameChapterQuestions;

        private const string GAME_TABLE_ID = "6529a1ab65751b5e5cc1c8a3";
        private const string GAME_CHAPTER_TABLE_ID = "652eedcbe4c6eeb88af4c7fa";
        private const string GAME_CHAPTER_QUESTIONS_TABLE_ID = "653803246fd64f40ff22209b";

        public override async Task Init()
        {
            await GetGames();
            await GetGameChapter();
            await GetGameChampterQuestion();
        }

        private async Task GetGames()
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
            catch (DynamicPixelsException)
            {
                throw;
            }
        }

        private async Task GetGameChapter()
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
            catch (DynamicPixelsException)
            {
                throw;
            }
        }

        private async Task GetGameChampterQuestion()
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
            catch (DynamicPixelsException)
            {
                throw;
            }
        }






    }
}
