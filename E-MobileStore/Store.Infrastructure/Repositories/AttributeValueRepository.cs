using Store.Domain.Entities;
using Store.Infrastructure.Data;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories
{
    public class AttributeValueRepository : IAttributeValueRepository
    {
        private readonly ApplicationDbContext _context;

        public AttributeValueRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddAttributesAsync(AttributeDTO attribute)
        {
            var attributevalue = new AttributeValue
            {
                AttributeName = attribute.AttributeName,
            };
            _context.AttributeValues.Add(attributevalue);
            _context.SaveChanges();
        }
    }
}
