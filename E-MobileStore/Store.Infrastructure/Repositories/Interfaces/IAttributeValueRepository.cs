﻿using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
    public interface IAttributeValueRepository
    {
        void AddAttributesAsync(AttributeDTO attribute);
    }
}
