﻿using DevPlatform.Domain.Dto.QuestionDto;
using System;
using System.Collections.Generic;

namespace DevPlatform.Domain.ServiceResponseModels.QuestionService
{
    /// <summary>
    /// Question service response model
    /// </summary>
    public partial class CreateResponse
    {
        public CreateResponse()
        {
            Tags = new List<string>();
            Comments = new List<QuestionCommentListDto>();
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
