// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core
{
    /// <summary>
    ///     Specifies assertion type. If the assertion method argument satisfies the condition,
    ///     then the execution continues. Otherwise, execution is assumed to be halted.
    /// </summary>
    public enum AssertionConditionType
    {
        /// <summary>Marked parameter should be evaluated to true.</summary>
        IS_TRUE = 0,

        /// <summary>Marked parameter should be evaluated to false.</summary>
        IS_FALSE = 1,

        /// <summary>Marked parameter should be evaluated to null value.</summary>
        IS_NULL = 2,

        /// <summary>Marked parameter should be evaluated to not null value.</summary>
        IS_NOT_NULL = 3
    }
}