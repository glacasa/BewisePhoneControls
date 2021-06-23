using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bewise.Phone.Google
{
    public abstract class BaseQueryParameter<TValue> : IQueryParameter
    {
        public TValue Value { get; set; }

        #region IQueryParameter Members

        public string Key { get; protected set; }

        public virtual string GetValueAsString()
        {
            if (Value == null)
                return null;
            
            return Value.ToString();
        }

        string IQueryParameter.GetQueryParameter()
        {
            string value = GetValueAsString();
            if (string.IsNullOrEmpty(value))
                return null;

            return (this as IQueryParameter).Key + "=" + value + "&";
        }

        #endregion
    }
}
