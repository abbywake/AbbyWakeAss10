using AbbyWakeAss10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbbyWakeAss10.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory urlInfo;
        public PaginationTagHelper(IUrlHelperFactory helperFun)
        {
            urlInfo = helperFun;
        }

        public PageNumberInfo PageInfo {get; set;}
        public string TeamName { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        //our own dictionary key value pairs that we are creating 
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelp = urlInfo.GetUrlHelper(ViewContext);

            
            TagBuilder finishedTag = new TagBuilder("div");
            
            for(int i =1; i <= PageInfo.NumPages; i++)
            {
                //tag builder helper and giving it the pair 
                TagBuilder individualTag = new TagBuilder("a");
                KeyValuePairs["pageNum"] = i;

                //css stuff
                if (PageClassesEnabled)
                {
                    //styling when a div is selected
                    individualTag.AddCssClass(PageClass);
                    individualTag.AddCssClass(i == PageInfo.CurrentPage ? PageClassSelected : PageClassNormal);
                }

                //adding to the tag 
                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);
                individualTag.InnerHtml.Append(i.ToString());

                finishedTag.InnerHtml.AppendHtml(individualTag);
            }
            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
        
    }
}
