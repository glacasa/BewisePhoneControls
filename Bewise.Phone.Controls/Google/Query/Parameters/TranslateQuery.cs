using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bewise.Phone.Google
{
    /// <summary>
    /// Provides a google translate query. AJAX API URL: http://ajax.googleapis.com/ajax/services/language/translate.
    /// </summary>
    public class TranslateQuery : BaseQuery
    {
        /// <summary>
        /// A translate query.
        /// </summary>
        /// <param name="queryText">The text to translate</param>
        /// <param name="languagePair">The language pair of the translation.</param>
        public TranslateQuery(string queryText, LanguagePair languagePair)
            : base(queryText)
        {
            ServiceUrl = "http://ajax.googleapis.com/ajax/services/language/";
            ServiceName = "translate";

            this.InitQueryParameter<QueryLanguagePairParameter>(ref m_LanguagePair);
            this.InitQueryParameter<QueryTranslateResponseFormatParameter>(ref m_ResponseFormat);

            m_LanguagePair.Value = languagePair;
        }


        private QueryLanguagePairParameter m_LanguagePair;
        public QueryLanguagePairParameter LanguagePair
        {
            get { return m_LanguagePair; }
            set { m_LanguagePair = value; }
        }

        private QueryTranslateResponseFormatParameter m_ResponseFormat;
        public QueryTranslateResponseFormatParameter ResponseFormat
        {
            get { return m_ResponseFormat; }
            set { m_ResponseFormat = value; }
        }
    }
}
