using Common.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.AddImage
{
    public record AddProductImageCommand : IBaseCommand
    {
        public IFormFile ImageFile { get;private set; }
        public long ProductId { get;private set; }
        public int Sequence { get;private set; }
    }

}



