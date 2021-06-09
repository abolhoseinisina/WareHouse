using System.Collections.Generic;

namespace Application.Model
{
    public class ProductFile
    {
        public List<product> products { get; set; }
    }

    public class product {
        public string name { get; set; }
        public List<article> articles { get; set; }
    }

    public class article
    {
        public string articleId { get; set; }
        public int amount { get; set; }
    }

}
