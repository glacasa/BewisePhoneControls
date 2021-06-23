namespace Bewise.Phone.Google
{
    /// <summary>
    /// This optional argument tells the patent search system to restrict the search to ONLY patents that having been issued, 
    /// skipping all patents that have only been filed. When specified, that value must be 1 as in as_psrg=1
    /// </summary>
    public class QueryPatentIssuedParameter : BaseQueryParameter<bool?>
    {
        public QueryPatentIssuedParameter() { Key = "as_psrg"; }

        public override string GetValueAsString()
        {
            if (Value == null || !Value.Value)
                return string.Empty;

            return "1";
        }
    }
    /// <summary>
    /// This optional argument tells the patent search system to restrict the search to ONLY patents that only been filed,
    /// skipping over all patents that have been issued. When specified, that value must be 1 as in as_psrg=1
    /// </summary>
    public class QueryPatentFiledParameter : BaseQueryParameter<bool?>
    {
        public QueryPatentFiledParameter() { Key = "as_psra"; }

        public override string GetValueAsString()
        {
            if (Value == null || !Value.Value)
                return string.Empty;

            return "1";
        }
    }
}
