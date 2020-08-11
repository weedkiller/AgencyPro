// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Comments.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class CommentMap : EntityMap<Comment>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Story).WithMany(x => x.Comments)
                .HasForeignKey(x => x.StoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Contract).WithMany(x => x.Comments)
                .HasForeignKey(x => x.ContractId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Project).WithMany(x => x.Comments)
                .HasForeignKey(x => x.ProjectId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Lead).WithMany(x => x.Comments)
                .HasForeignKey(x => x.LeadId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Candidate).WithMany(x => x.Comments)
                .HasForeignKey(x => x.CandidateId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CustomerAccount).WithMany(x => x.Comments)
                .HasForeignKey(x => new
                {
                    x.CustomerOrganizationId,
                    x.CustomerId,
                    x.AccountManagerOrganizationId,
                    x.AccountManagerId
                }).IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(x => x.Creator).WithMany(x => x.Comments)
                .HasForeignKey(x => new
                {
                    x.OrganizationId,
                    x.CreatedById
                }).IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
