// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Middleware.Resources
{
    /// <summary>
    ///     This class is required due to the way the default implementation of localization works.
    ///     The IStringLocalizerFactory and/or IStringLocalizer class could also be implemented instead of this.
    ///     https://github.com/aspnet/Localization/issues/150
    ///     Thanks to Joe Audette for this solution.
    ///     https://github.com/joeaudette/experiments/tree/master/LocalizationWebSite/Resources
    ///     Resource files must be named with namespace...
    /// </summary>
    public class SharedResource
    {
    }
}