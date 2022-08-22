using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobCommonAction.Models
{
    public class BlobInformationDto
    {
        public BlobInformationDto(string content)
        {
            Content = content;
        }

        public string Content { get; set; }
    }
}
