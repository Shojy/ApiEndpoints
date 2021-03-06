﻿using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleEndpointApp.DomainModel;
using System.Threading.Tasks;

namespace SampleEndpointApp.Authors
{
    public class Update : BaseAsyncEndpoint<UpdateAuthorCommand, UpdatedAuthorResult>
    {
        private readonly IAsyncRepository<Author> _repository;
        private readonly IMapper _mapper;

        public Update(IAsyncRepository<Author> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPut("/authors")]
        public override async Task<ActionResult<UpdatedAuthorResult>> HandleAsync([FromBody]UpdateAuthorCommand request)
        {
            var author = await _repository.GetByIdAsync(request.Id);
            _mapper.Map(request, author);
            await _repository.UpdateAsync(author);

            var result = _mapper.Map<UpdatedAuthorResult>(author);
            return Ok(result);
        }
    }
}