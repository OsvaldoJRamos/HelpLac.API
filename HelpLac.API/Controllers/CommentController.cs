﻿
using AutoMapper;
using HelpLac.API.Models;
using HelpLac.API.Models.Request;
using HelpLac.Domain.Dtos;
using HelpLac.Domain.Entities;
using HelpLac.Domain.Validation;
using HelpLac.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HelpLac.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ApiController
    {
        private readonly ICommentService _commentService;

        public CommentController(
            IMapper mapper,
            ICommentService commentService)
            : base(mapper)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Comment))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GetToken();

                var comment = await _commentService.CreateAsync(request.ProductId, _userId, request.Description, request.ReplyCommentId, cancellationToken);
                return new ObjectResult(comment);
            }
            catch (ValidationEntityException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}