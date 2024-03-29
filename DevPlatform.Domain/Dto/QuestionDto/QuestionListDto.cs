﻿using DevPlatform.Domain.Common;
using System;
using System.Collections.Generic;

namespace DevPlatform.Domain.Dto.QuestionDto
{
    public partial class QuestionListDto
    {
        public QuestionListDto()
        {
            Tags = new List<string>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByUserPhoto { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<QuestionCommentListDto> Comments { get; set; }
    }
}
