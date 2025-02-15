﻿using AspNetArticle.Core.Abstractions;
using AspNetArticle.Core.DataTransferObjects;
using AspNetArticle.MvcApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AspNetArticle.MvcApp.Controllers
{
    [Authorize]
    public class CommentaryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ICommentaryService _commentaryService;
        private readonly IArticleService _articleService;

        public CommentaryController(IMapper mapper,
            IUserService userService,
            ICommentaryService commentaryService,
            IArticleService articleService)
        {
            _mapper = mapper;
            _userService = userService;
            _commentaryService = commentaryService;
            _articleService = articleService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userEmail = User.Identity?.Name;
                    var userId = (await _userService.GetUserByEmailAsync(userEmail))?.Id;

                    var dto = _mapper.Map<CommentDto>(model);

                    if (dto != null && userId != null)
                    {
                        dto.UserId = userId.Value;

                        await _commentaryService.CreateCommentAsync(dto);
                        
                        return Redirect($"~/Article/Details/{model.ArticleId}"); 
                    }
                }
                return Redirect($"~/Article/Details/{model.ArticleId}");
            }
            catch (Exception e)
            {
                Log.Error($"Error: {e.Message}. StackTrace: {e.StackTrace}, Source: {e.Source}");
                throw new Exception($"Method {nameof(Create)} is failed, stack trace {e.StackTrace}. {e.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id) 
        {
            try
            {
                var comment = _mapper.Map<CreateCommentModel>(await _commentaryService.GetCommentByIdAsync(id));

                if (comment != null)
                    return RedirectToAction("Details", "Article", comment);

                return BadRequest();
            }
            catch (Exception e)
            {
                Log.Error($"Error: {e.Message}. StackTrace: {e.StackTrace}, Source: {e.Source}");
                throw new Exception($"Method {nameof(Update)} is failed, stack trace {e.StackTrace}. {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CreateCommentModel model) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = (await _userService.GetUserByIdAsync(model.UserId))?.Id;

                    if (userId != null)
                    {
                        var dto = new CommentDto()  
                        {
                            Id = model.Id,
                            UserId = userId.Value,
                            ArticleId = model.ArticleId,
                            Description = model.Description,
                            PublicationDate = DateTime.Now,
                            IsEdited = true
                        };

                        await _commentaryService.UpdateCommentAsync(dto);
                        return Redirect($"~/Article/Details/{model.ArticleId}"); 
                        
                    }
                }
                //ModelState.AddModelError("", "Комментарий пуст");

                return Redirect($"~/Article/Details/{model.ArticleId}");
                //return RedirectToAction("Details", "Article", model);
            }
            catch (Exception e)
            {
                Log.Error($"Error: {e.Message}. StackTrace: {e.StackTrace}, Source: {e.Source}");
                throw new Exception($"Method {nameof(Update)} is failed, stack trace {e.StackTrace}. {e.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var articleId = await _articleService.GetArticleIdByCommentId(id);

                if(articleId == null)
                {
                    Log.Error( $"{nameof(Delete)} article {articleId} is not exist");
                    throw new ArgumentException($"Article {articleId} is not exist");
                }
                await _commentaryService.DeleteCommentById(id);

                return Redirect($"~/Article/Details/{articleId}"); 
            }
            catch (Exception e)
            {
                Log.Error($"Error: {e.Message}. StackTrace: {e.StackTrace}, Source: {e.Source}");
                throw new Exception($"Method {nameof(Delete)} is failed, stack trace {e.StackTrace}. {e.Message}");
            }
        }
    }
}
