using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuyenDung.Common.Extensions;
using TuyenDung.Data.EF;
using TuyenDung.Data.Entities;
using TuyenDung.Model;

namespace TuyenDung.Api.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly TuyenDungDbContext _context;

        public CommentsController(TuyenDungDbContext context)
        {
            _context = context;
        }

        #region Comments

        [HttpGet("{knowledgeBaseId}/comments/filter")]
        public async Task<IActionResult> GetCommentsPaging(int? knowledgeBaseId, string filter, int pageIndex, int pageSize)
        {
            var query = from c in _context.Comments
                        join u in _context.Users
                            on c.OwnerUserId equals u.Id
                        select new { c, u };
            if (knowledgeBaseId.HasValue)
            {
                query = query.Where(x => x.c.KnowledgeBaseId == knowledgeBaseId.Value);
            }
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.c.Content.Contains(filter));
            }
            var totalRecords = await query.CountAsync();
            var items = await query.OrderByDescending(x => x.c.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CommentModel()
                {
                    Id = c.c.Id,
                    Content = c.c.Content,
                    CreateDate = c.c.CreateDate,
                    KnowledgeBaseId = c.c.KnowledgeBaseId,
                    LastModifiedDate = c.c.LastModifiedDate,
                    OwnerUserId = c.c.OwnerUserId,
                    OwnerName = c.u.FirstName + " " + c.u.LastName
                })
                .ToListAsync();

            var pagination = new Pagination<CommentModel>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }

        [HttpGet("{knowledgeBaseId}/comments/{commentId}")]
        public async Task<IActionResult> GetCommentDetail(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found comment with id: {commentId}"));
            var user = await _context.Users.FindAsync(comment.OwnerUserId);
            var commentVm = new CommentModel()
            {
                Id = comment.Id,
                Content = comment.Content,
                CreateDate = comment.CreateDate,
                KnowledgeBaseId = comment.KnowledgeBaseId,
                LastModifiedDate = comment.LastModifiedDate,
                OwnerUserId = comment.OwnerUserId,
                OwnerName = user.FirstName + " " + user.LastName
            };

            return Ok(commentVm);
        }

        [HttpPost("{knowledgeBaseId}/comments")]
        public async Task<IActionResult> PostComment(int knowledgeBaseId, [FromBody] CommentModel request)
        {
            var claims = HttpContext.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "Id").Value;

            var comment = new Comment()
            {
                Content = request.Content,
                KnowledgeBaseId = knowledgeBaseId,
                OwnerUserId = userId,
                ReplyId = request.ReplyId
            };
            _context.Comments.Add(comment);

            var knowledgeBase = await _context.KnowledgeBases.FindAsync(knowledgeBaseId);
            if (knowledgeBase == null)
                return BadRequest(new ApiBadRequestResponse($"Cannot found knowledge base with id: {knowledgeBaseId}"));

            knowledgeBase.NumberOfComments = knowledgeBase.NumberOfComments.GetValueOrDefault(0) + 1;
            _context.KnowledgeBases.Update(knowledgeBase);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetCommentDetail), new { id = knowledgeBaseId, commentId = comment.Id }, new CommentModel()
                {
                    Id = comment.Id
                });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create comment failed"));
            }
        }

        [HttpPut("{knowledgeBaseId}/comments/{commentId}")]
        public async Task<IActionResult> PutComment(int commentId, [FromBody] CommentModel request)
        {
            var claims = HttpContext.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "Id").Value;
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
                return BadRequest(new ApiBadRequestResponse($"Cannot found comment with id: {commentId}"));
            if (comment.OwnerUserId != userId)
                return Forbid();

            comment.Content = request.Content;
            _context.Comments.Update(comment);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest(new ApiBadRequestResponse($"Update comment failed"));
        }

        [HttpDelete("{knowledgeBaseId}/comments/{commentId}")]
        public async Task<IActionResult> DeleteComment(int knowledgeBaseId, int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found the comment with id: {commentId}"));

            _context.Comments.Remove(comment);

            var knowledgeBase = await _context.KnowledgeBases.FindAsync(knowledgeBaseId);
            if (knowledgeBase == null)
                return BadRequest(new ApiBadRequestResponse($"Cannot found knowledge base with id: {knowledgeBaseId}"));

            knowledgeBase.NumberOfComments = knowledgeBase.NumberOfComments.GetValueOrDefault(0) - 1;
            _context.KnowledgeBases.Update(knowledgeBase);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                //Delete cache
                var commentVm = new CommentModel()
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreateDate = comment.CreateDate,
                    KnowledgeBaseId = comment.KnowledgeBaseId,
                    LastModifiedDate = comment.LastModifiedDate,
                    OwnerUserId = comment.OwnerUserId
                };
                return Ok(commentVm);
            }
            return BadRequest(new ApiBadRequestResponse($"Delete comment failed"));
        }

        [HttpGet("comments/recent/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRecentComments(int take)
        {
            var query = from c in _context.Comments
                        join u in _context.Users
                            on c.OwnerUserId equals u.Id
                        join k in _context.KnowledgeBases
                        on c.KnowledgeBaseId equals k.Id
                        orderby c.CreateDate descending
                        select new { c, u, k };

            var comments = await query.Take(take).Select(x => new CommentModel()
            {
                Id = x.c.Id,
                CreateDate = x.c.CreateDate,
                KnowledgeBaseId = x.c.KnowledgeBaseId,
                OwnerUserId = x.c.OwnerUserId,
                KnowledgeBaseTitle = x.k.Title,
                OwnerName = x.u.FirstName + " " + x.u.LastName,
                KnowledgeBaseSeoAlias = x.k.SeoAlias
            }).ToListAsync();

            return Ok(comments);
        }

        [HttpGet("{knowledgeBaseId}/comments/tree")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCommentTreeByKnowledgeBaseId(int knowledgeBaseId, int pageIndex, int pageSize)
        {
            var query = from c in _context.Comments
                        join u in _context.Users
                            on c.OwnerUserId equals u.Id
                        where c.KnowledgeBaseId == knowledgeBaseId
                        where c.ReplyId == null
                        select new { c, u };

            var totalRecords = await query.CountAsync();
            var rootComments = await query.OrderByDescending(x => x.c.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new CommentModel()
                {
                    Id = x.c.Id,
                    CreateDate = x.c.CreateDate,
                    KnowledgeBaseId = x.c.KnowledgeBaseId,
                    OwnerUserId = x.c.OwnerUserId,
                    OwnerName = x.u.FirstName + " " + x.u.LastName,
                })
                .ToListAsync();

            foreach (var comment in rootComments)//only loop through root categories
            {
                // you can skip the check if you want an empty list instead of null
                // when there is no children
                var repliedQuery = from c in _context.Comments
                                   join u in _context.Users
                                       on c.OwnerUserId equals u.Id
                                   where c.KnowledgeBaseId == knowledgeBaseId
                                   where c.ReplyId == comment.Id
                                   select new { c, u };

                var totalRepliedCommentsRecords = await repliedQuery.CountAsync();
                var repliedComments = await repliedQuery.OrderByDescending(x => x.c.CreateDate)
                    .Take(pageSize)
                    .Select(x => new CommentModel()
                    {
                        Id = x.c.Id,
                        CreateDate = x.c.CreateDate,
                        KnowledgeBaseId = x.c.KnowledgeBaseId,
                        OwnerUserId = x.c.OwnerUserId,
                        OwnerName = x.u.FirstName + " " + x.u.LastName,
                    })
                    .ToListAsync();

                comment.Children = new Pagination<CommentModel>()
                {
                    PageIndex = 1,
                    PageSize = 10,
                    Items = repliedComments,
                    TotalRecords = totalRepliedCommentsRecords
                };
            }

            return Ok(new Pagination<CommentModel>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = rootComments,
                TotalRecords = totalRecords
            });
        }

        [HttpGet("{knowledgeBaseId}/comments/{rootCommentId}/replied")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRepliedCommentsPaging(int knowledgeBaseId, int rootCommentId, int pageIndex, int pageSize)
        {
            var query = from c in _context.Comments
                        join u in _context.Users
                            on c.OwnerUserId equals u.Id
                        where c.KnowledgeBaseId == knowledgeBaseId
                        where c.ReplyId == rootCommentId
                        select new { c, u };

            var totalRecords = await query.CountAsync();
            var comments = await query.OrderByDescending(x => x.c.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new CommentModel()
                {
                    Id = x.c.Id,
                    CreateDate = x.c.CreateDate,
                    KnowledgeBaseId = x.c.KnowledgeBaseId,
                    OwnerUserId = x.c.OwnerUserId,
                    OwnerName = x.u.FirstName + " " + x.u.LastName,
                })
                .ToListAsync();

            return Ok(new Pagination<CommentModel>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = comments,
                TotalRecords = totalRecords
            });
        }

        #endregion Comments
    }
}