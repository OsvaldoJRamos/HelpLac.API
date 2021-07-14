using HelpLac.Domain.Dtos;
using HelpLac.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelpLac.Domain.PaginatedEntities
{
    public class PaginatedEntity<TEntity> where TEntity : EntityBase
    {
        public IEnumerable<TEntity> Itens { get; set; }
        public PaginationResponseDto Pagination { get; set; }

        public PaginatedEntity(
            IEnumerable<TEntity> itens,
            PaginationResponseDto pagination)
        {
            Itens = itens;
            Pagination = pagination;
        }

        public PaginatedEntity(Tuple<IQueryable<TEntity>, PaginationResponseDto> paginatedEntity)
        {
            Itens = paginatedEntity.Item1;
            Pagination = paginatedEntity.Item2;
        }
    }
}
