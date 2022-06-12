﻿using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Web.Models.Admin
{
    public class AdminNewsModel
    {
        public int Sort { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public string Query { get; set; }
        public IList<NewsModel> News { get; set; }
    }
}
