// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Levels;
using AgencyPro.Core.Levels.Services;
using AgencyPro.Core.Levels.ViewModels;

namespace AgencyPro.Services.Levels
{
    public class LevelService : Service<Level>, ILevelService
    {
        public async Task<List<LevelOutput>> GetLevels(int positionId)
        {
            throw new NotImplementedException();
        }

        public LevelService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }
}
