// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Comments.Models;
using AgencyPro.Core.Comments.Services;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Stories.Models;
using AgencyPro.Core.UserAccount.Services;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Comments
{
    public partial class CommentService : Service<Comment>, ICommentService
    {
        private readonly IUserInfo _userInfo;
        private readonly IRepositoryAsync<Project> _projectRepository;
        private readonly IRepositoryAsync<Contract> _contractRepository;
        private readonly IRepositoryAsync<Story> _storyRepository;
        private readonly IRepositoryAsync<Lead> _leadRepository;
        private readonly IRepositoryAsync<Candidate> _candidateRepository;
        private readonly IRepositoryAsync<CustomerAccount> _accountRepository;

        public CommentService(IServiceProvider serviceProvider, IUserInfo userInfo) : base(serviceProvider)
        {
            _userInfo = userInfo;
            _projectRepository = UnitOfWork.RepositoryAsync<Project>();
            _contractRepository = UnitOfWork.RepositoryAsync<Contract>();
            _storyRepository = UnitOfWork.RepositoryAsync<Story>();
            _leadRepository = UnitOfWork.RepositoryAsync<Lead>();
            _candidateRepository = UnitOfWork.RepositoryAsync<Candidate>();
            _accountRepository = UnitOfWork.RepositoryAsync<CustomerAccount>();
        }

        public async Task<bool> CreateComment(Comment comment, CommentInput input)
        {
            comment.InjectFrom(input);
            comment.CreatedById = _userInfo.UserId;
            comment.UpdatedById = _userInfo.UserId;
            comment.Created = DateTimeOffset.UtcNow;
            comment.Updated = DateTimeOffset.UtcNow;
            comment.ObjectState = ObjectState.Added;

            var response = await Repository.InsertAsync(comment, true);

            // todo: raise event here

            return response != 0;
        }
    }
}
