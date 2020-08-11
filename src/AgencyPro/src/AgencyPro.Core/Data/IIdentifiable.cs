// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Data
{
    public interface IIdentifiable<out TKey>
    {
        /// <summary>
        ///     Gets or sets the primary key for this entity.
        /// </summary>
        TKey Id { get; }
    }
}