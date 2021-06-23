using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bewise.Phone.Google
{
    /// <summary>
    /// Defines a search safety level
    /// </summary>
    public enum SearchSafetyLookup
    {
        /// <summary>
        /// Enables the highest level of safe search filtering.
        /// </summary>
        active,
        /// <summary>
        /// Enables moderate safe search filtering (default).
        /// </summary>
        moderate,
        /// <summary>
        /// Disables safe search filtering.
        /// </summary>
        off
    }
    /// <summary>
    /// Filters duplicate results in the result set.
    /// </summary>
    public enum DuplicateContentFilterLookup
    {
        /// <summary>
        /// Turns duplicate filtering off.
        /// </summary>
        FilterOff = 0,
        /// <summary>
        /// Turns duplicate filtering on.
        /// </summary>
        FilterOn = 1
    }
    /// <summary>
    /// Defines the size of the result set. Use small for 4 results and large for 8 results.
    /// </summary>
    public enum ResultSetSize
    {
        /// <summary>
        /// yields 4 results per page.
        /// </summary>
        small,
        /// <summary>
        /// yields 8 results per page.
        /// </summary>
        large
    }
    /// <summary>
    /// This optional argument tells the image search system to restrict the search to images of the specified size, where size can be one of:
    /// icon, small, medium, large, xlarge, xxlarge, huge
    /// </summary>
    public enum ImageSize
    {
        icon,
        small,
        medium,
        large,
        xlarge,
        xxlarge,
        huge
    }
    /// <summary>
    /// This optional argument tells the image search system to restrict the search to images of the specified colorization, where colorization can be one of:
    /// gray - restrict to grayscale images
    /// color - restrict to color images
    /// </summary>
    public enum ImageColorization
    {
        /// <summary>
        /// restrict to grayscale images
        /// </summary>
        gray,
        /// <summary>
        /// restrict to color images
        /// </summary>
        color
    }
    /// <summary>
    /// This optional argument tells the image search system to filter the search to images of the specified color.
    /// </summary>
    public enum ImageColor
    {
        black,
        blue,
        brown,
        gray,
        green,
        orange,
        pink,
        purple,
        red,
        teal,
        white,
        yellow
    }
    /// <summary>
    /// This optional argument tells the image search system to restrict the search to images of the specified filetype.
    /// </summary>
    public enum ImageFileType
    {
        jpg,
        png,
        gif,
        bmp
    }
    /// <summary>
    /// This optional argument tells the image search system to restrict the search to images labeled with the given licenses.
    /// </summary>
    public enum ImageRights
    {
        cc_publicdomain,
        cc_attribute,
        cc_sharealike,
        cc_noncommerical,
        cc_nonderived
    }
    /// <summary>
    /// This optional argument tells the image search system to restrict the search to images of the specified filetype.
    /// </summary>
    public enum ImageType
    {
        /// <summary>
        /// restrict to images of faces
        /// </summary>
        face,
        /// <summary>
        /// restrict to photos
        /// </summary>
        photo,
        /// <summary>
        /// restrict to clipart images
        /// </summary>
        clipart,
        /// <summary>
        /// restrict to images of line drawings
        /// </summary>
        lineart
    }
    /// <summary>
    /// This optional argument specifies which type of listing the user is interested in.
    /// </summary>
    public enum LocalListingType
    {
        /// <summary>
        ///  request KML, Local Business Listings, and Geocode results
        /// </summary>
        blended,
        /// <summary>
        ///  request KML and Geocode results
        /// </summary>
        kmlonly,
        /// <summary>
        /// request Local Business Listings and Geocode results
        /// </summary>
        localonly
    }
    /// <summary>
    /// This optional argument tells the search system how to order results. Results may be ordered by relevance (which is the default) or by date. 
    /// </summary>
    public enum ResultsOrder
    {
        relevance,
        date
    }
    /// <summary>
    /// This optional argument tells the news search system to scope search results to a particular topic. 
    /// The value of the argument specifies the topic in the current or selected edition.
    /// </summary>
    public enum NewsTopic
    {
        /// <summary>
        /// specifies the top headlines topic
        /// </summary>
        h,
        /// <summary>
        /// specifies the world topic
        /// </summary>
        w,
        /// <summary>
        /// specifies the business topic
        /// </summary>
        b,
        /// <summary>
        /// specifies the nation topic
        /// </summary>
        n,
        /// <summary>
        ///  specifies the science and technology topic
        /// </summary>
        t,
        /// <summary>
        /// specifies the elections topic
        /// </summary>
        el,
        /// <summary>
        /// specifies the politics topic
        /// </summary>
        p,
        /// <summary>
        /// specifies the entertainment topic
        /// </summary>
        e,
        /// <summary>
        /// specifies the sports topic
        /// </summary>
        s,
        /// <summary>
        /// specifies the health topic
        /// </summary>
        m
    }
    public enum BookQueryType
    {
        /// <summary>
        /// The default value, searches for all books.
        /// </summary>
        all_books,
        /// <summary>
        /// yields only books that all their content is available.
        /// </summary>
        fullview
    }

    public enum TranslateResponseFormat
    {
        /// <summary>
        /// HTML format.
        /// </summary>
        html,
        /// <summary>
        /// Plain-text format.
        /// </summary>
        text
    }
}
