// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Middleware.Actions
{
    public class ResultMapper<TDestination> : ObjectResult
    {
        private readonly IMapper _mapper;

        public ResultMapper(object value, IMapper mapper) : base(value)
        {
            _mapper = mapper;
            Value = FromObject(value);
            StatusCode = 200;
        }

        public TDestination FromObject(object value)
        {
            return _mapper.Map<TDestination>(value);
        }
    }
}