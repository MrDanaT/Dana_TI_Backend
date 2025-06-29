﻿using System;
using System.Collections.Generic;
using System.Linq;
using TennisClub.Common;
using TennisClub.Common.GameResult;
using TennisClub.DAL.Repositories;

namespace TennisClub.BL.GameResultServiceFolder
{
    public class GameResultService : IGameResultService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<GameResultReadDTO> GetAllGameResults(int? memberId, DateTime date)
        {
            var gameResultItems = _unitOfWork.GameResults.GetAll();

            if (!memberId.IsNull())
                gameResultItems = gameResultItems.Where(x => x.GameNavigation.MemberId == memberId);

            if (!date.IsNull() && !date.Equals(new DateTime()))
                gameResultItems = gameResultItems.Where(x => x.GameNavigation.Date.Equals(date));

            return gameResultItems;
        }

        public GameResultReadDTO GetGameResultById(int id)
        {
            var gameResultItem = _unitOfWork.GameResults.GetById(id);
            return gameResultItem;
        }

        public GameResultReadDTO CreateGameResult(GameResultCreateDTO gameResult)
        {
            return _unitOfWork.GameResults.Create(gameResult);
        }

        public void UpdateGameResult(int id, GameResultUpdateDTO updateDTO)
        {
            _unitOfWork.GameResults.Update(id, updateDTO);
            _unitOfWork.Commit();
        }
    }
}