
using AutoMapper;
using HelpLac.API.Models;
using HelpLac.API.Models.Request;
using HelpLac.Domain.Dtos;
using HelpLac.Domain.Entities;
using HelpLac.Domain.Entities.Identity;
using HelpLac.Domain.Validation;
using HelpLac.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public CommentController(
            IMapper mapper,
            ICommentService commentService,
            UserManager<User> userManager)
            : base(mapper)
        {
            _commentService = commentService;
            _userManager = userManager;
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Comment))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                GetToken();

                var comment = await _commentService.DeleteByIdAsync(id, _userId, cancellationToken);
                return new ObjectResult(comment);
            }
            catch (ValidationEntityException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Comment>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] Guid productId, CancellationToken cancellationToken)
        {
            try
            {
                GetToken();

                var comments = await _commentService.GetAsync(productId, cancellationToken);

                foreach (var comment in comments)
                {
                    var user = await _userManager.FindByIdAsync(comment.UserId.ToString());
                    comment.UserName = user.UserName;
                }

                return new ObjectResult(comments);
            }
            catch (ValidationEntityException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
