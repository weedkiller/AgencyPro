// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Levels.ViewModels;

namespace AgencyPro.Core.Levels.Services
{
    public interface ILevelService
    {
        Task<List<LevelOutput>> GetLevels(int positionId);
    }
}
