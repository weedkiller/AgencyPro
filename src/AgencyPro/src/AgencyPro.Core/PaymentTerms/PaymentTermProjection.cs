// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.PaymentTerms.Models;
using AgencyPro.Core.PaymentTerms.ViewModels;

namespace AgencyPro.Core.PaymentTerms
{
    class PaymentTermProjection : Profile
    {
        public PaymentTermProjection()
        {
            CreateMap<PaymentTerm, PaymentTermOutput>()
                .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
                .ForMember(x => x.NetValue, o => o.MapFrom(x => x.NetValue))
                .ForMember(x => x.PaymentTermId, o => o.MapFrom(x => x.PaymentTermId));

            CreateMap<CategoryPaymentTerm, PaymentTermOutput>()
                .ForMember(x => x.Name, o => o.MapFrom(x => x.PaymentTerm.Name))
                .ForMember(x => x.NetValue, o => o.MapFrom(x => x.PaymentTerm.NetValue))
                .ForMember(x => x.PaymentTermId, o => o.MapFrom(x => x.PaymentTerm.PaymentTermId));

            CreateMap<OrganizationPaymentTerm, PaymentTermOutput>()
                .ForMember(x => x.Name, o => o.MapFrom(x => x.PaymentTerm.Name))
                .ForMember(x => x.NetValue, o => o.MapFrom(x => x.PaymentTerm.NetValue))
                .ForMember(x => x.PaymentTermId, o => o.MapFrom(x => x.PaymentTerm.PaymentTermId));
        }
    }
}
