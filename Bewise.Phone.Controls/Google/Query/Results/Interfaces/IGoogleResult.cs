using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bewise.Phone.Google
{
    /// <summary>
    /// A generic Google result. Based on http://code.google.com/apis/ajaxsearch/documentation/reference.html
    /// </summary>
    public interface IGoogleResult
    {
        /// <summary>
        /// Supplies the title value of the result.
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// Supplies the title, but unlike Title, this property is stripped of html markup (e.g., b, i, etc.).
        /// </summary>
        string Content { get; set; }
        /// <summary>
        /// Supplies an escaped version of the above URL.
        /// </summary>
        string Url { get; set; }
    }
}
