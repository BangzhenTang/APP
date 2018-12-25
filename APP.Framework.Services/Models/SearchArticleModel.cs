﻿using APP.Framework.DynamicLinq;

namespace APP.Framework.Services.Models
{
    public class SearchArticleModel : Query
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string CreatedDate { get; set; }
        public string ChannelId { get; set; }
    }
}
