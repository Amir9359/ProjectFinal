﻿using Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Catalogs
{
    [Audtable]
    public class CatalogItemFavourite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public CatalogItem CatalogItem { get; set; }
        public int CatalogItemId { get; set; }

    }
}
