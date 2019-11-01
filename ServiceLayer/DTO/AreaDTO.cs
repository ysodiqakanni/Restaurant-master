using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class AreaCreateRequestDTO
    {
        public string Name { get; set; }
    }
    public class AreaResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class AreaUpdateRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
