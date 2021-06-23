using System;

namespace Bewise.Phone.Google
{
    /// <summary>
    /// This optional argument tells the image search system to restrict the search to images of the specified size, where size can be one of:
    /// icon - restrict to small images
    /// small|medium|large|xlarge - restrict to medium images
    /// xxlarge - restrict to large images
    /// huge - restrict to extra large images
    /// </summary>
    public class QueryImageSizeParameter : BaseQueryParameter<ImageSize[]>
    {
        public QueryImageSizeParameter() { Key = "imgsz"; }

        public override string GetValueAsString()
        {
            string result = string.Empty;

            if (Value == null)
                return result;

            foreach (var item in Value)
                result += Enum.GetName(item.GetType(), item) + "|";

            result = result.TrimEnd('|');

            return result;
        }
    }
    /// <summary>
    /// This optional argument tells the image search system to restrict the search to images of the specified colorization,
    /// where colorization can be one of:
    /// gray - restrict to grayscale images
    /// color - restrict to color images
    /// </summary>
    public class QueryImageColorizationParameter : BaseQueryParameter<ImageColorization?>
    {
        public QueryImageColorizationParameter() { Key = "imgc"; }

        public override string GetValueAsString()
        {
            if (Value == null)
                return string.Empty;

            return Enum.GetName(Value.GetType(), Value);
        }
    }
    /// <summary>
    /// This optional argument allows the caller to restrict the search to documents written in a particular language (e.g., lr=lang_ja). 
    /// Note: Use the resource Languages to get a valid language code. e.g. Languages.Hebrew
    /// </summary>
    public class QueryImageColorParameter : BaseQueryParameter<ImageColor[]>
    {
        public QueryImageColorParameter() { Key = "imgcolor"; }

        public override string GetValueAsString()
        {
            string result = string.Empty;

            if (Value == null)
                return result;

            foreach (var item in Value)
                result += Enum.GetName(item.GetType(), item) + "|";

            result = result.TrimEnd('|');

            return result;
        }
    }
    /// <summary>
    /// This optional argument tells the image search system to restrict the search to images labeled with the given licenses.
    /// TODO: Add support for negative restrictions. e.g. as_rights=(cc_publicdomain|cc_attribute|cc_sharealike).-(cc_noncommercial|cc_nonderived).
    /// </summary>
    public class QueryImageRightsParameter : BaseQueryParameter<ImageRights[]>
    {
        public QueryImageRightsParameter() { Key = "as_rights"; }
        public override string GetValueAsString()
        {
            string result = string.Empty;

            if (Value == null)
                return result;

            foreach (var item in Value)
                result += Enum.GetName(item.GetType(), item) + "|";

            result = result.TrimEnd('|');

            return result;
        }
    }
    /// <summary>
    /// This optional argument tells the image search system to restrict the search to images within the specified domain, 
    /// e.g., as_sitesearch=photobucket.com. Note: This method restricts results to images found on pages at the given URL.
    /// </summary>
    public class QuerySiteSearchParameter : BaseQueryParameter<string>
    {
        public QuerySiteSearchParameter() { Key = "as_sitesearch"; }
    }
    /// <summary>
    /// This optional argument tells the image search system to restrict the search to images of the specified filetype
    /// </summary>
    public class QueryImageFileTypeParameter : BaseQueryParameter<ImageFileType?>
    {
        public QueryImageFileTypeParameter() { Key = "as_filetype"; }

        public override string GetValueAsString()
        {
            if (Value == null)
                return string.Empty;

            return Enum.GetName(Value.GetType(), Value);
        }
    }
    /// <summary>
    /// This optional argument tells the image search system to restrict the search to images of the specified type
    /// </summary>
    public class QueryImageTypeParameter : BaseQueryParameter<ImageType?>
    {
        public QueryImageTypeParameter() { Key = "as_filetype"; }

        public override string GetValueAsString()
        {
            if (Value == null)
                return string.Empty;

            return Enum.GetName(Value.GetType(), Value);
        }
    }
}
