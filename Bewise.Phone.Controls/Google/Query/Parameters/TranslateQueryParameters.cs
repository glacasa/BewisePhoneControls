using System;

namespace Bewise.Phone.Google
{
    /// <summary>
    /// Used as the QueryLanguagePairParameter Value type. Both properties must be initialized, or else the TranslateQuery
    /// will fail. Both properties get their legal values from the TranslateLanguages Resource dictionary.
    /// </summary>
    public class LanguagePair
    {
        public LanguagePair(string srcLang, string tgtLang) { SourceLanguage = srcLang; TargetLanguage = tgtLang; }

        public string SourceLanguage { get; set; }
        public string TargetLanguage { get; set; }

        public override string ToString()
        {
            return SourceLanguage + "|" + TargetLanguage;
        }
    }
    /// <summary>
    /// This argument supplies the optional source language and required destination language.
    /// In order to translate from English to Italian, you would create a QueryLanguagePairParameter like this:
    /// QueryLanguagePairParameter qlp = new QueryLanguagePairParameter();
    /// qlp.Value = new LanguagePair(TranslateLanguages.ENGLISH, TranslateLanguages.ITALIAN);
    /// </summary>
    public class QueryLanguagePairParameter : BaseQueryParameter<LanguagePair>
    {
        public QueryLanguagePairParameter() { Key = "langpair"; }

        public override string GetValueAsString()
        {
            if (Value == null || (string.IsNullOrEmpty(Value.SourceLanguage) || string.IsNullOrEmpty(Value.TargetLanguage)))
                return null;

            return Value.ToString();
        }
    }

    /// <summary>
    /// This optional argument allows you to indicate that the text to be translated is either plain-text or HTML.
    /// A value of html indicates html and a value of text indicates plain-text. Note that text is the default behavior.
    /// </summary>
    public class QueryTranslateResponseFormatParameter : BaseQueryParameter<TranslateResponseFormat?>
    {
        public QueryTranslateResponseFormatParameter() { Key = "format"; }

        public override string GetValueAsString()
        {
            if (Value == null)
                return null;

            return Enum.GetName(Value.GetType(), Value);
        }
    }
}
