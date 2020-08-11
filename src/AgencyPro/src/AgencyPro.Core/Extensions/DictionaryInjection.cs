// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Omu.ValueInjecter.Injections;

namespace AgencyPro.Core.Extensions
{
    public class DictionaryInjection : KnownSourceInjection<IDictionary<string, object>>
    {
        private readonly bool _allowNullables;
        private readonly string[] _ignoredProps;

        public DictionaryInjection(string[] ignoredProps, bool allowNullables = false)
        {
            _ignoredProps = ignoredProps;
            _allowNullables = allowNullables;
        }

        public DictionaryInjection()
        {
        }

        protected override void Inject(IDictionary<string, object> source, object target)
        {
            if (target == null) return;
            var props = target.GetType().GetProperties();

            foreach (var o in source)
            {
                var tp = props.SingleOrDefault(x => x.Name.ToLower() == o.Key.ToLower());
                if (tp == null) continue;
                if (_ignoredProps.Contains(tp.Name)) continue;
                if (o.Value == null && !_allowNullables) continue;

                object newValue;
                if (o.Value == null && _allowNullables)
                {
                    newValue = null;
                }
                else
                {
                    var t = Nullable.GetUnderlyingType(tp.PropertyType) ?? tp.PropertyType;
                    newValue = !t.GetTypeInfo().IsEnum
                        ? Convert.ChangeType(o.Value, t)
                        : Enum.Parse(t, o.Value.ToString());
                }

                tp.SetValue(target, newValue);
            }
        }
    }
}