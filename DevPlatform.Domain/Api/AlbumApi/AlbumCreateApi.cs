﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace DevPlatform.Domain.Api.AlbumApi
{
    public class AlbumCreateApi
    {
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public string Tag { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
