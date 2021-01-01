using System;
using System.Collections.Generic;

namespace DevPlatform.Domain.Dto.AlbumDto
{
    public class AlbumListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public string Tag { get; set; }
        public List<AlbumImagesDto> AlbumImages { get; set; }
    }
}
